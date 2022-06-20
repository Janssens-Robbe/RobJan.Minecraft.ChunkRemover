# How to use
Open your terminal and navigate to the world folder. From there you can use the command as followed:
```
chunkrm -r <range> -c <coord> [<coord> <coord> ...]
```

## Arguments
* `-w` or `--world` Defaults to the current directory. Path to the world folder.
* `-r` or `--range` Defaults to 32. Range of chunks to not remove.
* `-c` or `--coordinates` List of coordinates to not remove any chunks around within the range. Coordinates are in the format `x,z`. Optinally the reange can be overriden for each coordinate by using the format `x,z,r` where `r` is the range.

## Examples
Remove everything but regions within 32 chunks from origin.
```
chunkrm -c 0,0
```

Remove everything but regions that are within 50 chunks from x:0, z:0; x:-1000, z:500; and x:5600, z:5000.
```
chunkrm -r 50 -c 0,0 -1000,500 5600,5000
```

Remove everything but regions that are within 32 chunks from origin in the world located at `/home/user/world`.
```
chunkrm -w /home/user/world -c 0,0
```

Remove everything but regions that are within 50 chunk from x:0, z:0. Or 16 chunks from x:1234, y:-2345; x:-4321, z:567; and x:5678, z:8765.
We use the spesified 16 chunk range for each coordinate. However, for the orgin we want a larger range, so for that we add `,50` to its coodinate, making it `0,0,50`.
```
chunkrm -r 16 -c 0,0,50 1234,-2345 -4321,567 5678,8765
```