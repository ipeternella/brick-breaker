#!/bin/sh

UNITY_EXECUTABLE="$1"
PROJECT_FOLDER="$2"
RESULTS_FILENAME="$3"
RESULTS_FILEPATH="$PROJECT_FOLDER/test-results.xml"

echo "Testing Unity project: $PROJECT_FOLDER"
echo "Running Unity.app (batch mode, run tests)..."

time \
"$UNITY_EXECUTABLE" \
-projectPath "$PROJECT_FOLDER" \
-batchmode \
-runEditorTests \
-editorTestsResultFile "$RESULTS_FILEPATH" \
-nographics

echo ""
echo "Editor log: ~/Library/Logs/Unity/Editor.log"
echo "Results xml: $RESULTS_FILEPATH"