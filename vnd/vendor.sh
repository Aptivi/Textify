#!/bin/bash

prebuild() {
    # Check for dependencies
    dotnetpath=`which dotnet`
    checkerror $? "dotnet is not found"
    sevenzpath=`which 7z`
    checkerror $? "7z is not found"

    # Turn off telemetry and logo
    export DOTNET_CLI_TELEMETRY_OPTOUT=1
    export DOTNET_NOLOGO=1

    # Download compiled Windows libicu libraries
    if [ ! -f $ROOTDIR/vnd/icu4c-77_1-Win64-MSVC2022.zip ]; then
        curl -L --output $ROOTDIR/vnd/icu4c-77_1-Win64-MSVC2022.zip https://github.com/unicode-org/icu/releases/download/release-77-1/icu4c-77_1-Win64-MSVC2022.zip
        checkvendorerror $?
    fi
    if [ ! -f $ROOTDIR/vnd/icu4c-77_1-WinARM64-MSVC2022.zip ]; then
        curl -L --output $ROOTDIR/vnd/icu4c-77_1-WinARM64-MSVC2022.zip https://github.com/unicode-org/icu/releases/download/release-77-1/icu4c-77_1-WinARM64-MSVC2022.zip
        checkvendorerror $?
    fi

    # Install the DLL for AMD64
    cd $ROOTDIR/vnd && "$sevenzpath" e $ROOTDIR/vnd/icu4c-77_1-Win64-MSVC2022.zip bin64/icuuc77.dll && cd -
    cd $ROOTDIR/vnd && "$sevenzpath" e $ROOTDIR/vnd/icu4c-77_1-Win64-MSVC2022.zip bin64/icudt77.dll && cd -
    checkvendorerror $?
    mkdir -p $ROOTDIR/public/Textify.Data/runtimes/win-x64/native/
    checkvendorerror $?
    mv $ROOTDIR/vnd/icuuc77.dll $ROOTDIR/public/Textify.Data/runtimes/win-x64/native/icuuc77.dll
    mv $ROOTDIR/vnd/icudt77.dll $ROOTDIR/public/Textify.Data/runtimes/win-x64/native/icudt77.dll
    checkvendorerror $?
    
    # Install the DLL for ARM64
    cd $ROOTDIR/vnd && "$sevenzpath" e $ROOTDIR/vnd/icu4c-77_1-WinARM64-MSVC2022.zip binARM64/icuuc77.dll && cd -
    cd $ROOTDIR/vnd && "$sevenzpath" e $ROOTDIR/vnd/icu4c-77_1-WinARM64-MSVC2022.zip binARM64/icudt77.dll && cd -
    checkvendorerror $?
    mkdir -p $ROOTDIR/public/Textify.Data/runtimes/win-arm64/native/
    checkvendorerror $?
    mv $ROOTDIR/vnd/icuuc77.dll $ROOTDIR/public/Textify.Data/runtimes/win-arm64/native/icuuc77.dll
    mv $ROOTDIR/vnd/icudt77.dll $ROOTDIR/public/Textify.Data/runtimes/win-arm64/native/icudt77.dll
    checkvendorerror $?
}

build() {
    # Check for dependencies
    dotnetpath=`which dotnet`
    checkerror $? "dotnet is not found"

    # Turn off telemetry and logo
    export DOTNET_CLI_TELEMETRY_OPTOUT=1
    export DOTNET_NOLOGO=1
    
    # Determine the release configuration
    releaseconf=$1
    if [ -z $releaseconf ]; then
	    releaseconf=Release
    fi

    # Now, build.
    echo Building with configuration $releaseconf...
    "$dotnetpath" build "$ROOTDIR/Textify.sln" -p:Configuration=$releaseconf ${@:2}
    checkvendorerror $?
}

docpack() {
    # Get the project version
    version=$(grep "<Version>" $ROOTDIR/Directory.Build.props | cut -d "<" -f 2 | cut -d ">" -f 2)
    checkerror $? "Failed to get version. Check to make sure that the version is specified correctly in D.B.props"

    # Check for dependencies
    zippath=`which zip`
    checkerror $? "zip is not found"

    # Pack documentation
    echo Packing documentation...
    cd "$ROOTDIR/docs/" && "$zippath" -r /tmp/$version-doc.zip . && cd -
    checkvendorerror $?

    # Clean things up
    rm -rf "$ROOTDIR/DocGen/api"
    checkvendorerror $?
    rm -rf "$ROOTDIR/DocGen/obj"
    checkvendorerror $?
    rm -rf "$ROOTDIR/docs"
    checkvendorerror $?
    mv /tmp/$version-doc.zip "$ROOTDIR/tools"
    checkvendorerror $?
}

docgenerate() {
    # Check for dependencies
    docfxpath=`which docfx`
    checkerror $? "docfx is not found"

    # Turn off telemetry and logo
    export DOTNET_CLI_TELEMETRY_OPTOUT=1
    export DOTNET_NOLOGO=1

    # Build docs
    echo Building documentation...
    "$docfxpath" $ROOTDIR/DocGen/docfx.json
    checkvendorerror $?
}

pushall() {
    # This script pushes.
    releaseconf=$1
    if [ -z $releaseconf ]; then
	    releaseconf=Release
    fi
    nugetsource=$2
    if [ -z $nugetsource ]; then
	    nugetsource=nuget.org
    fi
    dotnetpath=`which dotnet`
    checkerror $? "dotnet is not found"

    # Push packages
    echo Pushing packages with configuration $releaseconf to $nugetsource...
    find $ROOTDIR -type f -path "*/bin/$releaseconf/*.nupkg" -exec sh -c "echo {} ; dotnet nuget push {} --api-key $NUGET_APIKEY --source \"$nugetsource\"" \;
    checkvendorerror $?
}

clean() {
    OUTPUTS=(
        '-name "bin" -or'
        '-name "obj" -or'
        '-name "docs"'
    )
    find "$ROOTDIR" -type d \( ${OUTPUTS[@]} \) -print -exec rm -rf "{}" +
}
