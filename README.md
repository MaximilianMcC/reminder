# TODO
Advanced TODO list program for use in the terminal.

## TODO:
- [x] Make a new .td file if there isn't already one in the current directory
- [x] Manually edit the file, then print it in console.
- [ ] Check and uncheck items in the console
- [ ] Sync `.td` file to a `.md` file
- [ ] When `.td` file is edited then automatically sync with Git
- [ ] Specify where exactly the inserted `.td` file goes in the `.md` file
- [ ] Different themes (line style, color, etc)
- [ ] Handle multiple `.td` files in same directory
- [ ] Make it so you can edit the text of an item in the console
- [ ] Have a single "global" list that can be accessed anywhere
- [ ] Multiline rendering in terminal
- [ ] Check for if the terminal supports special characters. If not use a more limited ascii pallet.
- [ ] Render basic markdown in console (code blocks and stuff)

## `.td` file structure
Any file ending with `.td` is a todo list. A file could look like this:
```td
0;Unchecked item
1;Checked item
```