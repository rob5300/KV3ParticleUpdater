# KeyValue3Updater
Update old kv3 particle features to new versions.
New things being added as required, anyone is welcome to try to use it.

## Adding new Updaters
For features that become *C_INIT_InitFloat* make a new class implementing *InitFloatUpdaterBase*
```
internal class MyNewUpdater : InitFloatUpdaterBase
{
        protected override string BlockClassName => "C_INIT_ClassNameHere";
        protected override int outputField => 0;
        protected override string randomMinKey => "minKey";
        protected override string randomMaxKey => "maxKey";
}
```
For other cases, add a new class implementing Updater and use the existing classes as examples to create the find regex and replacement.

Update *updaters* in Program.cs to add a new updater to be used.

## Currently Supported:
- Random Colour
- Random Lifetime
- Random Radius
- Random Sequence
- Random Alpha
- Random Rotation
- Random Trail Length
- **Remove** Random Yaw Flip
