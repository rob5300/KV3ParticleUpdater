# KeyValue3Updater for S&Box
Update old KeyValues3 particle features to new versions for S&Box.

Made for TF:S2 so may not convert all particle features, but most should be covered.

## Adding new Updaters
Check existing updaters to see how to make a new updater. Covered use cases are:

- Move block to new block (e.g. move to operators)
- Replace an old block with new data
- Insert some data into an existing block
- Update a block to "C_INIT_InitFloat"

Any new Updater type added is automatically used.

## Currently updated particle features:
- Age Noise (Remove)
- CreateAlongPath
- CreateSequentialPath
- CreationNoise
- *Custom overbright addition for some sprites*
- PositionWarp
- RandomAlpha
- RandomColor
- RandomLifeTime
- RandomRadius
- RandomRotaionSpeed
- RandomSecondSequence
- RandomSequence
- RandomTrailLength
- RandomYawFlip
- RandomYaw
- RemapCpToVector
- RemapScalar