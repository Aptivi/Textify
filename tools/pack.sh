#!/bin/bash

# This script builds and packs the artifacts. Use when you have MSBuild installed.
version=$(cat version)
releaseconf=$1
if [ -z $releaseconf ]; then
	releaseconf=Release
fi

# Check for dependencies
zippath=`which zip`
if [ ! $? == 0 ]; then
	echo zip is not found.
	exit 1
fi

# Pack binary
echo Packing binary...
cd "../Textify/bin/$releaseconf/netstandard2.0/" && "$zippath" -r /tmp/$version-bin.zip . && cd -
cd "../Textify.Data/bin/$releaseconf/netstandard2.0/" && "$zippath" -r /tmp/$version-data.zip . && cd -
cd "../Textify.Data.Analysis/bin/$releaseconf/netstandard2.0/" && "$zippath" -r /tmp/$version-data-analysis.zip . && cd -
cd "../Textify.Json/bin/$releaseconf/netstandard2.0/" && "$zippath" -r /tmp/$version-json.zip . && cd -
if [ ! $? == 0 ]; then
	echo Packing using zip failed.
	exit 1
fi

# Inform success
mv ~/tmp/$version-bin.zip .
mv ~/tmp/$version-data.zip .
mv ~/tmp/$version-data-analysis.zip .
mv ~/tmp/$version-json.zip .
echo Build and pack successful.
exit 0
