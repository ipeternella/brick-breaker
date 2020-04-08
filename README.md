# Brick Breaker

A simple brick breaker game created with Unity and C#!

## Technologies

For this game, the following tools were used:

- `Unity 2019.3.7f1`;
- `C#`
- `Piskel' for drawing 8-bits themed sprites (https://www.piskelapp.com/)

## Running Tests

To run the tests, Unity must be installed on the host machine. The tests of the project can be run on a pipeline 
using the `tests.sh` bash script. However, it will require the following parameters:

- `Path to Unity`;
- `Project folder`

These two must be passed as parameters to the bash script. The output of the tests will be an XML file with the name:

`test-results.xml`

Example:

```bash
# Running on a macOS machine
bash tests.sh /Applications/Unity/Hub/Editor/2019.3.7f1/Unity.app/Contents/MacOS/Unity ~/Documents/git/brick-breaker
```