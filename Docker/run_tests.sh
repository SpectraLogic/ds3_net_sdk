#!/bin/bash
echo using Git Repo: ${GIT_REPO:="https://github.com/SpectraLogic/ds3_net_sdk.git"}

# clone ds3_net_sdk from github
cd /opt
git clone $GIT_REPO
cd ds3_net_sdk
make test

