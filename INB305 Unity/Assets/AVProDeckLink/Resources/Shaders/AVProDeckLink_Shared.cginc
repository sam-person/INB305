//-----------------------------------------------------------------------------
// Copyright 2014-2016 RenderHeads Ltd.  All rights reserved.
//-----------------------------------------------------------------------------

// BT470
float4
convertYUV(float y, float u, float v)
{
    float rr = saturate(y + 1.402 * (u - 0.5));
    float gg = saturate(y - 0.344 * (v - 0.5) - 0.714 * (u - 0.5));
    float bb = saturate(y + 1.772 * (v - 0.5));
	return float4(rr, gg, bb, 1);
}

// BT709
float4
convertYUV_HD(float y, float u, float v)
{
	float rr = saturate( 1.164 * (y - (16.0 / 255.0)) + 1.793 * (u - 0.5) );
	float gg = saturate( 1.164 * (y - (16.0 / 255.0)) - 0.534 * (u - 0.5) - 0.213 * (v - 0.5) );
	float bb = saturate( 1.164 * (y - (16.0 / 255.0)) + 2.115 * (v - 0.5) );
	return float4(rr, gg, bb, 1);
}

float4
yuvToRGB(float y, float u, float v)
{
    float b = saturate(1.164 * (y - 16.0 / 255) + 1.596 * (v - 128.0 / 255));
    float g = saturate(1.164 * (y - 16.0 / 255) - 0.813 * (v - 128.0 / 255) - 0.391 * (u - 128.0 / 255));
    float r = saturate(1.164 * (y - 16.0/255) + 2.018 * (u - 128.0/255));
    return float4(r, g, b, 1);
}