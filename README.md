# Timer Adjuster

Simple mod that lets you move the position of the timer

For those using it to fix it being hidden at certain resolutions/scales here are a few reccomended:

- 16x10 (1920x1200)
	- x : -35

## Initial setup

After initial boot a config file is generated inside `steamapps/common/Blackwake/Blackwake_Data/Managed/Mods/Configs` called `timerFix.cfg`.

Inside this file you will find the following setup:

```text
Update_key=[
Change_at_runtime=true
x=0
y=0
```

All of the above options can be changed. It is reccomended to set `Change_at_runtime` to `false` after you're happy with the value

You can press the `Update_key` button at runtime to reload the offset value should you change them inside the **timerFix.cfg**.

Setting **x** to a positive value will move it to the right, negative will move it to the left.