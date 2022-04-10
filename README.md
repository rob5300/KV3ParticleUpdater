# KeyValue3Updater
Update old KeyValues3 particle features to new versions for S&Box.

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

*InsertUpdater* can be used to move a block from an old location to a new one, and update some things too.

Update *updaters* in Program.cs to add a new updater to be used.

## Currently Updated:
- Random Colour
- Random Lifetime
- Random Radius
- Random Sequence
- Random Alpha
- Random Rotation
- Random Trail Length
- **Remove** Random Yaw Flip
- Random Rotation Speed
- Move RemapCPToVector to operators
- Random Yaw
