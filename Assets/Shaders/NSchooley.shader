Shader "HW3/NSchooley_Simple"
{
    Properties // This section will show up in the inspector, can be used to define a color, material, etc. 
    {
       _colorStart ("Start Color", Color) = (0, 1.0, 0, 1.0)
       _colorEnd ("End Color", Color) = (0, 0, 1.0, 1.0)
        

    }
    SubShader
    {

              
        Pass
        {
            CGPROGRAM
            #pragma vertex vertexShader
            #pragma fragment fSimple
            
            #include "UnityCG.cginc"

            fixed4 _colorStart = float4(0, 1.0, 0, 1.0); // Defined colors to be switched between
            fixed4 _colorEnd = float4(0, 0, 1.0, 1.0);
            float w = float(0); // Define a variable to be used to determine when the colors will switch based on the vertical vertex

                         
                        
            struct v2f // Transfer data from vertex shader to fragment shader
            {
                //UNITY_FOG_COORDS(1)
                float4 pos : SV_POSITION;
                fixed4 color : COLOR;
             
            };

                       
            v2f vertexShader(float4 vertex : POSITION, fixed4 vertexColor : COLOR)
            {             
                v2f o;
                o.pos = UnityObjectToClipPos(vertex); // Transform vertex from object to clip space
                w = o.pos.y;
                o.color = lerp(_colorStart, _colorEnd, w); // Used to switch between two colors depending on the objects position or the position of the camera
                return o;
            }

            
            fixed4 fSimple(v2f i) : COLOR // Fragement Shader
            {
                
                return i.color;
            }
            
            ENDCG
        }
    }
}
