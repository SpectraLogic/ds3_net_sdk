# How to generate documentation

The git repository holds the documentation in this gh-pages branch and the source code in the master branch. As a result, we recommend using two separate repository clones to generate the documentation.

1. Make sure the latest master branch is checked out in `%base_path%\ds3_net_sdk`.
2. Create another clone of the sdk in `%base_path%\ds3_net_sdk_documentation`.
3. Run `git checkout gh-pages` in the `ds3_net_sdk_documentation` repository.
4. [Download Doxygen](http://www.stack.nl/~dimitri/doxygen/download.html#srcbin) and install it.
5. Run `git rm -r api` then `git commit -m "Cleaned API documentation so we can regenerate."` to delete the previously generated documentation.
6. Run `doxygen.exe Doxyfile` to regenerate the `api` directory.
7. Run `git add api` then `git commit -m "Regenerated the API documentation"` to check it in.
8. Push the gh-pages branch back into your fork.
