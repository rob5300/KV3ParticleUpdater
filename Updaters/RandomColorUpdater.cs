﻿using System.Text.RegularExpressions;

namespace KeyValue3Updater.Updaters
{
    internal class RandomColorUpdater : ReplacementUpdater
    {
        public RandomColorUpdater()
        {
            findRegex = GetBlockRegex("C_INIT_RandomColor");
        }

        protected override string GetReplacement(ref string input)
        {
            string m_ColorMax = GetLine(ref input, "m_ColorMax");
            int[] colorMax = GetLineArrayValues(m_ColorMax);

            string m_ColorMin = GetLine(ref input, "m_ColorMin");
            int[] colorMin = GetLineArrayValues(m_ColorMin);

            string replacement = @"{{
_class = ""C_INIT_InitVec""
m_InputValue = 
{{
m_nType = ""PVEC_TYPE_FLOAT_INTERP_GRADIENT""
m_vLiteralValue = [ 0.0, 0.0, 0.0 ]
m_LiteralColor = [ 0, 0, 0, 0 ]
m_nVectorAttribute = 6
m_vVectorAttributeScale = [ 1.0, 1.0, 1.0 ]
m_nControlPoint = 0
m_vCPValueScale = [ 1.0, 1.0, 1.0 ]
m_vCPRelativePosition = [ 0.0, 0.0, 0.0 ]
m_vCPRelativeDir = [ 1.0, 0.0, 0.0 ]
m_FloatComponentX = 
{{
m_nType = ""PF_TYPE_LITERAL""
m_nMapType = ""PF_MAP_TYPE_DIRECT""
m_flLiteralValue = 0.0
m_nControlPoint = 0
m_nScalarAttribute = 3
m_nVectorAttribute = 6
m_nVectorComponent = 0
m_flRandomMin = 0.0
m_flRandomMax = 1.0
m_nRandomMode = ""PF_RANDOM_MODE_CONSTANT""
m_flLOD0 = 0.0
m_flLOD1 = 0.0
m_flLOD2 = 0.0
m_flLOD3 = 0.0
m_flNoiseOutputMin = 0.0
m_flNoiseOutputMax = 1.0
m_flNoiseScale = 0.1
m_vecNoiseOffsetRate = [ 0.0, 0.0, 0.0 ]
m_flNoiseOffset = 0.0
m_nNoiseOctaves = 1
m_nNoiseTurbulence = ""PF_NOISE_TURB_NONE""
m_nNoiseType = ""PF_NOISE_TYPE_PERLIN""
m_nNoiseModifier = ""PF_NOISE_MODIFIER_NONE""
m_flNoiseTurbulenceScale = 1.25
m_flNoiseTurbulenceMix = 0.5
m_flNoiseImgPreviewScale = 1.0
m_bNoiseImgPreviewLive = true
m_nInputMode = ""PF_INPUT_MODE_CLAMPED""
m_flMultFactor = 1.0
m_flInput0 = 0.0
m_flInput1 = 1.0
m_flOutput0 = 0.0
m_flOutput1 = 1.0
m_nBiasType = ""PF_BIAS_TYPE_STANDARD""
m_flBiasParameter = 0.0
m_Curve = 
{{
m_spline = [  ]
m_tangents = [  ]
m_vDomainMins = [ 0.0, 0.0 ]
m_vDomainMaxs = [ 0.0, 0.0 ]
}}
}}
m_FloatComponentY = 
{{
m_nType = ""PF_TYPE_LITERAL""
m_nMapType = ""PF_MAP_TYPE_DIRECT""
m_flLiteralValue = 0.0
m_nControlPoint = 0
m_nScalarAttribute = 3
m_nVectorAttribute = 6
m_nVectorComponent = 0
m_flRandomMin = 0.0
m_flRandomMax = 1.0
m_nRandomMode = ""PF_RANDOM_MODE_CONSTANT""
m_flLOD0 = 0.0
m_flLOD1 = 0.0
m_flLOD2 = 0.0
m_flLOD3 = 0.0
m_flNoiseOutputMin = 0.0
m_flNoiseOutputMax = 1.0
m_flNoiseScale = 0.1
m_vecNoiseOffsetRate = [ 0.0, 0.0, 0.0 ]
m_flNoiseOffset = 0.0
m_nNoiseOctaves = 1
m_nNoiseTurbulence = ""PF_NOISE_TURB_NONE""
m_nNoiseType = ""PF_NOISE_TYPE_PERLIN""
m_nNoiseModifier = ""PF_NOISE_MODIFIER_NONE""
m_flNoiseTurbulenceScale = 1.25
m_flNoiseTurbulenceMix = 0.5
m_flNoiseImgPreviewScale = 1.0
m_bNoiseImgPreviewLive = true
m_nInputMode = ""PF_INPUT_MODE_CLAMPED""
m_flMultFactor = 1.0
m_flInput0 = 0.0
m_flInput1 = 1.0
m_flOutput0 = 0.0
m_flOutput1 = 1.0
m_nBiasType = ""PF_BIAS_TYPE_STANDARD""
m_flBiasParameter = 0.0
m_Curve = 
{{
m_spline = [  ]
m_tangents = [  ]
m_vDomainMins = [ 0.0, 0.0 ]
m_vDomainMaxs = [ 0.0, 0.0 ]
}}
}}
m_FloatComponentZ = 
{{
m_nType = ""PF_TYPE_LITERAL""
m_nMapType = ""PF_MAP_TYPE_DIRECT""
m_flLiteralValue = 0.0
m_nControlPoint = 0
m_nScalarAttribute = 3
m_nVectorAttribute = 6
m_nVectorComponent = 0
m_flRandomMin = 0.0
m_flRandomMax = 1.0
m_nRandomMode = ""PF_RANDOM_MODE_CONSTANT""
m_flLOD0 = 0.0
m_flLOD1 = 0.0
m_flLOD2 = 0.0
m_flLOD3 = 0.0
m_flNoiseOutputMin = 0.0
m_flNoiseOutputMax = 1.0
m_flNoiseScale = 0.1
m_vecNoiseOffsetRate = [ 0.0, 0.0, 0.0 ]
m_flNoiseOffset = 0.0
m_nNoiseOctaves = 1
m_nNoiseTurbulence = ""PF_NOISE_TURB_NONE""
m_nNoiseType = ""PF_NOISE_TYPE_PERLIN""
m_nNoiseModifier = ""PF_NOISE_MODIFIER_NONE""
m_flNoiseTurbulenceScale = 1.25
m_flNoiseTurbulenceMix = 0.5
m_flNoiseImgPreviewScale = 1.0
m_bNoiseImgPreviewLive = true
m_nInputMode = ""PF_INPUT_MODE_CLAMPED""
m_flMultFactor = 1.0
m_flInput0 = 0.0
m_flInput1 = 1.0
m_flOutput0 = 0.0
m_flOutput1 = 1.0
m_nBiasType = ""PF_BIAS_TYPE_STANDARD""
m_flBiasParameter = 0.0
m_Curve = 
{{
m_spline = [  ]
m_tangents = [  ]
m_vDomainMins = [ 0.0, 0.0 ]
m_vDomainMaxs = [ 0.0, 0.0 ]
}}
}}
m_FloatInterp = 
{{
m_nType = ""PF_TYPE_RANDOM_UNIFORM""
m_nMapType = ""PF_MAP_TYPE_DIRECT""
m_flLiteralValue = 0.0
m_nControlPoint = 0
m_nScalarAttribute = 3
m_nVectorAttribute = 6
m_nVectorComponent = 0
m_flRandomMin = 0.0
m_flRandomMax = 1.0
m_nRandomMode = ""PF_RANDOM_MODE_CONSTANT""
m_flLOD0 = 0.0
m_flLOD1 = 0.0
m_flLOD2 = 0.0
m_flLOD3 = 0.0
m_flNoiseOutputMin = 0.0
m_flNoiseOutputMax = 1.0
m_flNoiseScale = 0.1
m_vecNoiseOffsetRate = [ 0.0, 0.0, 0.0 ]
m_flNoiseOffset = 0.0
m_nNoiseOctaves = 1
m_nNoiseTurbulence = ""PF_NOISE_TURB_NONE""
m_nNoiseType = ""PF_NOISE_TYPE_PERLIN""
m_nNoiseModifier = ""PF_NOISE_MODIFIER_NONE""
m_flNoiseTurbulenceScale = 1.25
m_flNoiseTurbulenceMix = 0.5
m_flNoiseImgPreviewScale = 1.0
m_bNoiseImgPreviewLive = true
m_nInputMode = ""PF_INPUT_MODE_CLAMPED""
m_flMultFactor = 1.0
m_flInput0 = 0.0
m_flInput1 = 1.0
m_flOutput0 = 0.0
m_flOutput1 = 1.0
m_nBiasType = ""PF_BIAS_TYPE_STANDARD""
m_flBiasParameter = 0.0
m_Curve = 
{{
m_spline = [  ]
m_tangents = [  ]
m_vDomainMins = [ 0.0, 0.0 ]
m_vDomainMaxs = [ 0.0, 0.0 ]
}}
}}
m_flInterpInput0 = 0.0
m_flInterpInput1 = 1.0
m_vInterpOutput0 = [ 0.0, 0.0, 0.0 ]
m_vInterpOutput1 = [ 1.0, 1.0, 1.0 ]
m_Gradient = 
{{
m_Stops = 
[
{{
m_flPosition = 0.0
m_Color = [ {3}, {4}, {5}, 255 ]
}},
{{
m_flPosition = 1.0
m_Color = [ {0}, {1}, {2}, 255 ]
}},
]
}}
}}
}},";

            return string.Format(replacement, colorMax[0], colorMax[1], colorMax[2], colorMin[0], colorMin[1], colorMin[2]);
        }
    }
}
