# How to use
Open your terminal and navigate to the world folder. From there you can use the command as followed:
```
chunkrm -r <range> -c <coord> [<coord> <coord> ...]
```

## Arguments
* `-w` or `--world` Defaults to the current directory. Path to the world folder.
* `-r` or `--range` Defaults to 32. Range of chunks to not remove.
* `-c` or `--coordinates` List of coordinates to not remove any chunks around within the range. Coordinates are in the format `x,y`.

## Examples
Remove everything but regions within 32 chunks from origin:
```
chunkrm -c 0,0
```

Remove everything but regions that are within 50 chunks from x:0, y:0; x:-1000, y:500; and x:5600, y:5000:
```
chunkrm -r 50 -c 0,0 -1000,500 5600,5000
```

Remove everything but regions that are within 32 chunks from origin in the world located at `/home/user/world`:
```
chunkrm -w /home/user/world -c 0,0
```