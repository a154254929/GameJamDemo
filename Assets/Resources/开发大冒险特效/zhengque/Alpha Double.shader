// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1761,x:34159,y:33454,varname:node_1761,prsc:2|emission-7169-OUT,alpha-8945-OUT,voffset-3179-OUT;n:type:ShaderForge.SFN_Tex2d,id:5485,x:31887,y:32470,ptovrint:False,ptlb:Textures,ptin:_Textures,varname:node_5485,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-3814-UVOUT;n:type:ShaderForge.SFN_VertexColor,id:5062,x:32460,y:32528,varname:node_5062,prsc:2;n:type:ShaderForge.SFN_Color,id:3569,x:32404,y:32180,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_3569,prsc:2,glob:False,taghide:False,taghdr:True,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:8018,x:32656,y:32289,varname:node_8018,prsc:2|A-3569-RGB,B-5485-RGB,C-5062-RGB;n:type:ShaderForge.SFN_SwitchProperty,id:4275,x:32132,y:32642,ptovrint:False,ptlb:TextureAlpha,ptin:_TextureAlpha,varname:node_4275,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5485-R,B-5485-A;n:type:ShaderForge.SFN_DepthBlend,id:5518,x:32222,y:33113,varname:node_5518,prsc:2|DIST-6694-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:8806,x:32505,y:33014,ptovrint:False,ptlb:SoftParticle,ptin:_SoftParticle,varname:node_8806,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-2176-OUT,B-5518-OUT;n:type:ShaderForge.SFN_TexCoord,id:1130,x:30093,y:31897,varname:node_1130,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:3610,x:29880,y:31954,varname:node_3610,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8092,x:29903,y:31798,ptovrint:False,ptlb:U Speed,ptin:_USpeed,varname:node_8092,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:899,x:29905,y:32139,ptovrint:False,ptlb:V Speed,ptin:_VSpeed,varname:node_899,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:6103,x:30188,y:32184,varname:node_6103,prsc:2|A-3610-T,B-899-OUT;n:type:ShaderForge.SFN_Multiply,id:2835,x:30105,y:31751,varname:node_2835,prsc:2|A-8092-OUT,B-3610-T;n:type:ShaderForge.SFN_Append,id:7461,x:30475,y:31916,varname:node_7461,prsc:2|A-5074-OUT,B-31-OUT;n:type:ShaderForge.SFN_Add,id:5074,x:30289,y:31828,varname:node_5074,prsc:2|A-2835-OUT,B-1130-U;n:type:ShaderForge.SFN_Add,id:31,x:30400,y:32050,varname:node_31,prsc:2|A-1130-V,B-6103-OUT;n:type:ShaderForge.SFN_Multiply,id:7551,x:33021,y:32897,varname:node_7551,prsc:2|A-7234-OUT,B-5062-A,C-8806-OUT,D-24-OUT,E-6339-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:7234,x:32557,y:32818,ptovrint:False,ptlb:Mask Tex,ptin:_MaskTex,varname:node_7234,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-4275-OUT,B-1096-OUT;n:type:ShaderForge.SFN_Tex2d,id:272,x:32006,y:32941,ptovrint:False,ptlb:Opacity Textures,ptin:_OpacityTextures,varname:node_272,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1969-OUT;n:type:ShaderForge.SFN_Multiply,id:1096,x:32293,y:32844,varname:node_1096,prsc:2|A-4275-OUT,B-272-R;n:type:ShaderForge.SFN_Fresnel,id:9623,x:32043,y:33336,varname:node_9623,prsc:2|EXP-8418-OUT;n:type:ShaderForge.SFN_OneMinus,id:6208,x:32193,y:33336,varname:node_6208,prsc:2|IN-9623-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8418,x:31886,y:33397,ptovrint:False,ptlb: Fresnel Op Power,ptin:_FresnelOpPower,varname:node_8418,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:24,x:32694,y:33306,ptovrint:False,ptlb:Fresnel Op,ptin:_FresnelOp,varname:node_24,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-292-OUT,B-3016-OUT;n:type:ShaderForge.SFN_Vector1,id:292,x:32381,y:33248,varname:node_292,prsc:2,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:6339,x:32738,y:33534,ptovrint:False,ptlb:Dissolve,ptin:_Dissolve,varname:node_6339,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-168-OUT,B-1855-OUT;n:type:ShaderForge.SFN_Vector1,id:168,x:32519,y:33495,varname:node_168,prsc:2,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:1785,x:31310,y:32184,ptovrint:False,ptlb:PanUV,ptin:_PanUV,varname:node_9480,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-7461-OUT,B-1362-OUT;n:type:ShaderForge.SFN_TexCoord,id:3152,x:30357,y:32286,varname:node_3152,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:358,x:30741,y:32293,varname:node_358,prsc:2|A-3152-UVOUT,B-2626-OUT;n:type:ShaderForge.SFN_Append,id:2626,x:30640,y:32436,varname:node_2626,prsc:2|A-1530-Z,B-1530-W;n:type:ShaderForge.SFN_TexCoord,id:1530,x:30384,y:32493,varname:node_1530,prsc:2,uv:1,uaff:True;n:type:ShaderForge.SFN_Lerp,id:7982,x:33078,y:32370,varname:node_7982,prsc:2|A-71-OUT,B-8018-OUT,T-8207-OUT;n:type:ShaderForge.SFN_FaceSign,id:5880,x:32746,y:32590,varname:node_5880,prsc:2,fstp:0;n:type:ShaderForge.SFN_Multiply,id:71,x:32860,y:32336,varname:node_71,prsc:2|A-8018-OUT,B-5685-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5685,x:32645,y:32465,ptovrint:False,ptlb:BackFacePower,ptin:_BackFacePower,varname:node_5685,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:6189,x:31509,y:32582,varname:node_6189,prsc:2|A-1785-OUT,B-2201-OUT;n:type:ShaderForge.SFN_Tex2d,id:5123,x:30966,y:32682,ptovrint:False,ptlb:扭曲,ptin:_,varname:node_5123,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9535-OUT;n:type:ShaderForge.SFN_Multiply,id:2201,x:31157,y:32756,varname:node_2201,prsc:2|A-5123-R,B-9247-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7867,x:30680,y:32932,ptovrint:False,ptlb:NIUQU,ptin:_NIUQU,varname:node_7867,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Rotator,id:3814,x:31693,y:32665,varname:node_3814,prsc:2|UVIN-6189-OUT,ANG-7173-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3492,x:31111,y:33026,ptovrint:False,ptlb:Rotator,ptin:_Rotator,varname:node_3492,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:813,x:31300,y:33026,varname:node_813,prsc:2|A-3492-OUT,B-6943-OUT;n:type:ShaderForge.SFN_Pi,id:6943,x:31127,y:33094,varname:node_6943,prsc:2;n:type:ShaderForge.SFN_Divide,id:7173,x:31503,y:33050,varname:node_7173,prsc:2|A-813-OUT,B-3119-OUT;n:type:ShaderForge.SFN_Vector1,id:3119,x:31255,y:33211,varname:node_3119,prsc:2,v1:180;n:type:ShaderForge.SFN_Multiply,id:7169,x:33264,y:32350,varname:node_7169,prsc:2|A-9931-RGB,B-7982-OUT;n:type:ShaderForge.SFN_Tex2d,id:9931,x:32993,y:32077,ptovrint:False,ptlb:GampColor,ptin:_GampColor,varname:node_9931,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8945,x:33372,y:32877,varname:node_8945,prsc:2|A-3569-A,B-7551-OUT,C-632-R;n:type:ShaderForge.SFN_Tex2d,id:8297,x:32047,y:33879,ptovrint:False,ptlb:Disslov,ptin:_Disslov,varname:node_8001,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:841cc2ecd68f99c47a30e493b34075d5,ntxv:0,isnm:False|UVIN-7786-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6474,x:31411,y:33698,ptovrint:False,ptlb:DissPower,ptin:_DissPower,varname:node_3928,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Smoothstep,id:1855,x:32452,y:33754,varname:node_1855,prsc:2|A-4983-OUT,B-9589-OUT,V-8297-R;n:type:ShaderForge.SFN_ValueProperty,id:6120,x:32047,y:33777,ptovrint:False,ptlb:Smoot,ptin:_Smoot,varname:node_8360,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:9589,x:32237,y:33707,varname:node_9589,prsc:2|A-4983-OUT,B-6120-OUT;n:type:ShaderForge.SFN_OneMinus,id:2205,x:31411,y:33550,varname:node_2205,prsc:2|IN-1530-U;n:type:ShaderForge.SFN_ValueProperty,id:6694,x:32033,y:33166,ptovrint:False,ptlb:SoftPower,ptin:_SoftPower,varname:node_6694,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:4983,x:31841,y:33509,ptovrint:False,ptlb:DissOrPower,ptin:_DissOrPower,varname:node_4983,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-2205-OUT,B-6474-OUT;n:type:ShaderForge.SFN_TexCoord,id:8984,x:30889,y:33931,varname:node_8984,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:6284,x:31569,y:34038,varname:node_6284,prsc:2|A-2201-OUT,B-4694-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:7786,x:31787,y:33898,ptovrint:False,ptlb:NiuQUDiss,ptin:_NiuQUDiss,varname:node_7786,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-4694-OUT,B-6284-OUT;n:type:ShaderForge.SFN_Tex2d,id:5590,x:33538,y:33839,ptovrint:False,ptlb:VertexT,ptin:_VertexT,varname:node_6095,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-6227-OUT;n:type:ShaderForge.SFN_Vector4Property,id:9339,x:33493,y:34039,ptovrint:False,ptlb:VertexPower,ptin:_VertexPower,varname:node_3318,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1,v2:1,v3:1,v4:0;n:type:ShaderForge.SFN_Append,id:6227,x:33299,y:33839,varname:node_6227,prsc:2|A-5515-OUT,B-5927-OUT;n:type:ShaderForge.SFN_Add,id:5515,x:33084,y:33822,varname:node_5515,prsc:2|A-9778-OUT,B-6300-U;n:type:ShaderForge.SFN_Add,id:5927,x:33176,y:34117,varname:node_5927,prsc:2|A-6300-V,B-880-OUT;n:type:ShaderForge.SFN_Multiply,id:880,x:32907,y:34264,varname:node_880,prsc:2|A-6902-T,B-4622-OUT;n:type:ShaderForge.SFN_Multiply,id:9778,x:32906,y:33822,varname:node_9778,prsc:2|A-6861-OUT,B-6902-T;n:type:ShaderForge.SFN_Time,id:6902,x:32768,y:33946,varname:node_6902,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:6300,x:32907,y:34069,varname:node_6300,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:6861,x:32692,y:33804,ptovrint:False,ptlb:VPan_U,ptin:_VPan_U,varname:node_626,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4622,x:32791,y:34313,ptovrint:False,ptlb:VPan_V,ptin:_VPan_V,varname:node_9913,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:3179,x:33943,y:33806,ptovrint:False,ptlb:VerTexOffest,ptin:_VerTexOffest,varname:node_5945,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6182-OUT,B-2697-OUT;n:type:ShaderForge.SFN_Multiply,id:2697,x:33754,y:34012,varname:node_2697,prsc:2|A-5590-RGB,B-9339-XYZ,C-1516-OUT,D-2611-V,E-3296-OUT;n:type:ShaderForge.SFN_NormalVector,id:1516,x:33477,y:34185,prsc:2,pt:True;n:type:ShaderForge.SFN_Vector1,id:6182,x:33678,y:33757,varname:node_6182,prsc:2,v1:0;n:type:ShaderForge.SFN_TexCoord,id:2611,x:33440,y:34333,varname:node_2611,prsc:2,uv:1,uaff:True;n:type:ShaderForge.SFN_TexCoord,id:925,x:30822,y:33416,varname:node_925,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6743,x:30846,y:33594,varname:node_6743,prsc:2|A-2775-T,B-1871-OUT;n:type:ShaderForge.SFN_Multiply,id:4948,x:30834,y:33270,varname:node_4948,prsc:2|A-773-OUT,B-2775-T;n:type:ShaderForge.SFN_Append,id:3064,x:31204,y:33435,varname:node_3064,prsc:2|A-8670-OUT,B-8504-OUT;n:type:ShaderForge.SFN_Add,id:8670,x:31018,y:33347,varname:node_8670,prsc:2|A-4948-OUT,B-925-U;n:type:ShaderForge.SFN_Add,id:8504,x:31018,y:33544,varname:node_8504,prsc:2|A-925-V,B-6743-OUT;n:type:ShaderForge.SFN_Time,id:2775,x:30609,y:33487,varname:node_2775,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:773,x:30609,y:33270,ptovrint:False,ptlb:OpacityU_Speed,ptin:_OpacityU_Speed,varname:node_773,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:1871,x:30609,y:33648,ptovrint:False,ptlb:OpacityV_Speed,ptin:_OpacityV_Speed,varname:_OpacityU_Speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:632,x:33106,y:33112,ptovrint:False,ptlb:MaskTextures,ptin:_MaskTextures,varname:node_632,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:2176,x:32313,y:32996,varname:node_2176,prsc:2,v1:1;n:type:ShaderForge.SFN_TexCoord,id:9632,x:30224,y:32786,varname:node_9632,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:8416,x:30132,y:32985,varname:node_8416,prsc:2|A-7090-T,B-4776-OUT;n:type:ShaderForge.SFN_Multiply,id:8068,x:30141,y:32618,varname:node_8068,prsc:2|A-7412-OUT,B-7090-T;n:type:ShaderForge.SFN_Append,id:5002,x:30623,y:32785,varname:node_5002,prsc:2|A-6266-OUT,B-9087-OUT;n:type:ShaderForge.SFN_Add,id:6266,x:30444,y:32722,varname:node_6266,prsc:2|A-8068-OUT,B-9632-U;n:type:ShaderForge.SFN_Add,id:9087,x:30424,y:32889,varname:node_9087,prsc:2|A-9632-V,B-8416-OUT;n:type:ShaderForge.SFN_Time,id:7090,x:29859,y:32787,varname:node_7090,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:7412,x:29874,y:32613,ptovrint:False,ptlb:N_U_Speed,ptin:_N_U_Speed,varname:node_7412,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:4776,x:29845,y:33033,ptovrint:False,ptlb:N_V_Speed,ptin:_N_V_Speed,varname:node_4776,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:3016,x:32506,y:33348,ptovrint:False,ptlb:1-Fresnel,ptin:_1Fresnel,varname:node_3016,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6208-OUT,B-1827-OUT;n:type:ShaderForge.SFN_OneMinus,id:1827,x:32337,y:33400,varname:node_1827,prsc:2|IN-6208-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:9247,x:30925,y:32932,ptovrint:False,ptlb:NIuqU_UV,ptin:_NIuqU_UV,varname:node_9247,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-7867-OUT,B-169-V;n:type:ShaderForge.SFN_TexCoord,id:169,x:30680,y:33023,varname:node_169,prsc:2,uv:1,uaff:True;n:type:ShaderForge.SFN_SwitchProperty,id:1362,x:31131,y:32381,ptovrint:False,ptlb:Tile Off,ptin:_TileOff,varname:node_1362,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-358-OUT,B-205-OUT;n:type:ShaderForge.SFN_Clamp01,id:205,x:30916,y:32438,varname:node_205,prsc:2|IN-358-OUT;n:type:ShaderForge.SFN_Multiply,id:5245,x:30920,y:34101,varname:node_5245,prsc:2|A-7646-T,B-7571-OUT;n:type:ShaderForge.SFN_Multiply,id:3525,x:30908,y:33777,varname:node_3525,prsc:2|A-1979-OUT,B-7646-T;n:type:ShaderForge.SFN_Add,id:1759,x:31092,y:33854,varname:node_1759,prsc:2|A-3525-OUT,B-8984-U;n:type:ShaderForge.SFN_Add,id:3455,x:31092,y:34051,varname:node_3455,prsc:2|A-8984-V,B-5245-OUT;n:type:ShaderForge.SFN_Time,id:7646,x:30683,y:33994,varname:node_7646,prsc:2;n:type:ShaderForge.SFN_Append,id:4694,x:31278,y:33942,varname:node_4694,prsc:2|A-1759-OUT,B-3455-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1979,x:30598,y:33802,ptovrint:False,ptlb:DissUSpeed,ptin:_DissUSpeed,varname:node_1979,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:7571,x:30360,y:33883,ptovrint:False,ptlb:DissVSpeed,ptin:_DissVSpeed,varname:node_7571,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Tex2d,id:7158,x:33356,y:34608,ptovrint:False,ptlb:VetexMax,ptin:_VetexMax,varname:node_7158,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_SwitchProperty,id:3296,x:33616,y:34572,ptovrint:False,ptlb:VEMask,ptin:_VEMask,varname:node_3296,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6778-OUT,B-7158-RGB;n:type:ShaderForge.SFN_Vector1,id:6778,x:33462,y:34540,varname:node_6778,prsc:2,v1:1;n:type:ShaderForge.SFN_OneMinus,id:1838,x:32946,y:32588,varname:node_1838,prsc:2|IN-5880-VFACE;n:type:ShaderForge.SFN_SwitchProperty,id:8207,x:33135,y:32635,ptovrint:False,ptlb:Face Filp,ptin:_FaceFilp,varname:node_8207,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5880-VFACE,B-1838-OUT;n:type:ShaderForge.SFN_RemapRange,id:9535,x:30804,y:32714,varname:node_9535,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-5002-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1969,x:31632,y:33345,ptovrint:False,ptlb:Niu-Opacity,ptin:_NiuOpacity,varname:node_1969,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-3064-OUT,B-5527-OUT;n:type:ShaderForge.SFN_Add,id:5527,x:31468,y:33253,varname:node_5527,prsc:2|A-2201-OUT,B-3064-OUT;proporder:5485-4275-3569-1785-1362-8092-899-3492-8806-6694-272-7234-773-1871-8297-6339-6120-1979-7571-4983-6474-5123-7867-9247-1969-7412-4776-7786-9931-5685-8207-24-8418-5590-3179-9339-6861-4622-632-3016-7158-3296;pass:END;sub:END;*/

Shader "cokey/Alpha Double" {
    Properties {
        _Textures ("Textures", 2D) = "white" {}
        [MaterialToggle] _TextureAlpha ("TextureAlpha", Float ) = 0
        [HDR]_Color ("Color", Color) = (0.5,0.5,0.5,1)
        [MaterialToggle] _PanUV ("PanUV", Float ) = 0
        [MaterialToggle] _TileOff ("Tile Off", Float ) = 0
        _USpeed ("U Speed", Float ) = 0
        _VSpeed ("V Speed", Float ) = 0
        _Rotator ("Rotator", Float ) = 0
        [MaterialToggle] _SoftParticle ("SoftParticle", Float ) = 1
        _SoftPower ("SoftPower", Float ) = 1
        _OpacityTextures ("Opacity Textures", 2D) = "white" {}
        [MaterialToggle] _MaskTex ("Mask Tex", Float ) = 0
        _OpacityU_Speed ("OpacityU_Speed", Float ) = 0
        _OpacityV_Speed ("OpacityV_Speed", Float ) = 0
        _Disslov ("Disslov", 2D) = "white" {}
        [MaterialToggle] _Dissolve ("Dissolve", Float ) = 1
        _Smoot ("Smoot", Float ) = 0
        _DissUSpeed ("DissUSpeed", Float ) = 0
        _DissVSpeed ("DissVSpeed", Float ) = 0
        [MaterialToggle] _DissOrPower ("DissOrPower", Float ) = 1
        _DissPower ("DissPower", Float ) = 0
        _ ("扭曲", 2D) = "white" {}
        _NIUQU ("NIUQU", Float ) = 0
        [MaterialToggle] _NIuqU_UV ("NIuqU_UV", Float ) = 0
        [MaterialToggle] _NiuOpacity ("Niu-Opacity", Float ) = 0
        _N_U_Speed ("N_U_Speed", Float ) = 0
        _N_V_Speed ("N_V_Speed", Float ) = 0
        [MaterialToggle] _NiuQUDiss ("NiuQUDiss", Float ) = 0
        _GampColor ("GampColor", 2D) = "white" {}
        _BackFacePower ("BackFacePower", Float ) = 1
        [MaterialToggle] _FaceFilp ("Face Filp", Float ) = 1
        [MaterialToggle] _FresnelOp ("Fresnel Op", Float ) = 1
        _FresnelOpPower (" Fresnel Op Power", Float ) = 0
        _VertexT ("VertexT", 2D) = "white" {}
        [MaterialToggle] _VerTexOffest ("VerTexOffest", Float ) = 0
        _VertexPower ("VertexPower", Vector) = (1,1,1,0)
        _VPan_U ("VPan_U", Float ) = 0
        _VPan_V ("VPan_V", Float ) = 0
        _MaskTextures ("MaskTextures", 2D) = "white" {}
        [MaterialToggle] _1Fresnel ("1-Fresnel", Float ) = 1
        _VetexMax ("VetexMax", 2D) = "white" {}
        [MaterialToggle] _VEMask ("VEMask", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _Textures; uniform float4 _Textures_ST;
            uniform float4 _Color;
            uniform fixed _TextureAlpha;
            uniform fixed _SoftParticle;
            uniform float _USpeed;
            uniform float _VSpeed;
            uniform fixed _MaskTex;
            uniform sampler2D _OpacityTextures; uniform float4 _OpacityTextures_ST;
            uniform float _FresnelOpPower;
            uniform fixed _FresnelOp;
            uniform fixed _Dissolve;
            uniform fixed _PanUV;
            uniform float _BackFacePower;
            uniform sampler2D _; uniform float4 __ST;
            uniform float _NIUQU;
            uniform float _Rotator;
            uniform sampler2D _GampColor; uniform float4 _GampColor_ST;
            uniform sampler2D _Disslov; uniform float4 _Disslov_ST;
            uniform float _DissPower;
            uniform float _Smoot;
            uniform float _SoftPower;
            uniform fixed _DissOrPower;
            uniform fixed _NiuQUDiss;
            uniform sampler2D _VertexT; uniform float4 _VertexT_ST;
            uniform float4 _VertexPower;
            uniform float _VPan_U;
            uniform float _VPan_V;
            uniform fixed _VerTexOffest;
            uniform float _OpacityU_Speed;
            uniform float _OpacityV_Speed;
            uniform sampler2D _MaskTextures; uniform float4 _MaskTextures_ST;
            uniform float _N_U_Speed;
            uniform float _N_V_Speed;
            uniform fixed _1Fresnel;
            uniform fixed _NIuqU_UV;
            uniform fixed _TileOff;
            uniform float _DissUSpeed;
            uniform float _DissVSpeed;
            uniform sampler2D _VetexMax; uniform float4 _VetexMax_ST;
            uniform fixed _VEMask;
            uniform fixed _FaceFilp;
            uniform fixed _NiuOpacity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_6902 = _Time;
                float2 node_6227 = float2(((_VPan_U*node_6902.g)+o.uv0.r),(o.uv0.g+(node_6902.g*_VPan_V)));
                float4 _VertexT_var = tex2Dlod(_VertexT,float4(TRANSFORM_TEX(node_6227, _VertexT),0.0,0));
                float4 _VetexMax_var = tex2Dlod(_VetexMax,float4(TRANSFORM_TEX(o.uv0, _VetexMax),0.0,0));
                v.vertex.xyz += lerp( 0.0, (_VertexT_var.rgb*_VertexPower.rgb*v.normal*o.uv1.g*lerp( 1.0, _VetexMax_var.rgb, _VEMask )), _VerTexOffest );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float4 _GampColor_var = tex2D(_GampColor,TRANSFORM_TEX(i.uv0, _GampColor));
                float node_3814_ang = ((_Rotator*3.141592654)/180.0);
                float node_3814_spd = 1.0;
                float node_3814_cos = cos(node_3814_spd*node_3814_ang);
                float node_3814_sin = sin(node_3814_spd*node_3814_ang);
                float2 node_3814_piv = float2(0.5,0.5);
                float4 node_3610 = _Time;
                float2 node_358 = (i.uv0+float2(i.uv1.b,i.uv1.a));
                float4 node_7090 = _Time;
                float2 node_9535 = (float2(((_N_U_Speed*node_7090.g)+i.uv0.r),(i.uv0.g+(node_7090.g*_N_V_Speed)))*2.0+-1.0);
                float4 __var = tex2D(_,TRANSFORM_TEX(node_9535, _));
                float node_2201 = (__var.r*lerp( _NIUQU, i.uv1.g, _NIuqU_UV ));
                float2 node_3814 = (mul((lerp( float2(((_USpeed*node_3610.g)+i.uv0.r),(i.uv0.g+(node_3610.g*_VSpeed))), lerp( node_358, saturate(node_358), _TileOff ), _PanUV )+node_2201)-node_3814_piv,float2x2( node_3814_cos, -node_3814_sin, node_3814_sin, node_3814_cos))+node_3814_piv);
                float4 _Textures_var = tex2D(_Textures,TRANSFORM_TEX(node_3814, _Textures));
                float3 node_8018 = (_Color.rgb*_Textures_var.rgb*i.vertexColor.rgb);
                float3 emissive = (_GampColor_var.rgb*lerp((node_8018*_BackFacePower),node_8018,lerp( isFrontFace, (1.0 - isFrontFace), _FaceFilp )));
                float3 finalColor = emissive;
                float _TextureAlpha_var = lerp( _Textures_var.r, _Textures_var.a, _TextureAlpha );
                float4 node_2775 = _Time;
                float2 node_3064 = float2(((_OpacityU_Speed*node_2775.g)+i.uv0.r),(i.uv0.g+(node_2775.g*_OpacityV_Speed)));
                float2 _NiuOpacity_var = lerp( node_3064, (node_2201+node_3064), _NiuOpacity );
                float4 _OpacityTextures_var = tex2D(_OpacityTextures,TRANSFORM_TEX(_NiuOpacity_var, _OpacityTextures));
                float node_6208 = (1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelOpPower));
                float _DissOrPower_var = lerp( (1.0 - i.uv1.r), _DissPower, _DissOrPower );
                float4 node_7646 = _Time;
                float2 node_4694 = float2(((_DissUSpeed*node_7646.g)+i.uv0.r),(i.uv0.g+(node_7646.g*_DissVSpeed)));
                float2 _NiuQUDiss_var = lerp( node_4694, (node_2201+node_4694), _NiuQUDiss );
                float4 _Disslov_var = tex2D(_Disslov,TRANSFORM_TEX(_NiuQUDiss_var, _Disslov));
                float4 _MaskTextures_var = tex2D(_MaskTextures,TRANSFORM_TEX(i.uv0, _MaskTextures));
                fixed4 finalRGBA = fixed4(finalColor,(_Color.a*(lerp( _TextureAlpha_var, (_TextureAlpha_var*_OpacityTextures_var.r), _MaskTex )*i.vertexColor.a*lerp( 1.0, saturate((sceneZ-partZ)/_SoftPower), _SoftParticle )*lerp( 1.0, lerp( node_6208, (1.0 - node_6208), _1Fresnel ), _FresnelOp )*lerp( 1.0, smoothstep( _DissOrPower_var, (_DissOrPower_var+_Smoot), _Disslov_var.r ), _Dissolve ))*_MaskTextures_var.r));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _VertexT; uniform float4 _VertexT_ST;
            uniform float4 _VertexPower;
            uniform float _VPan_U;
            uniform float _VPan_V;
            uniform fixed _VerTexOffest;
            uniform sampler2D _VetexMax; uniform float4 _VetexMax_ST;
            uniform fixed _VEMask;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 uv1 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_6902 = _Time;
                float2 node_6227 = float2(((_VPan_U*node_6902.g)+o.uv0.r),(o.uv0.g+(node_6902.g*_VPan_V)));
                float4 _VertexT_var = tex2Dlod(_VertexT,float4(TRANSFORM_TEX(node_6227, _VertexT),0.0,0));
                float4 _VetexMax_var = tex2Dlod(_VetexMax,float4(TRANSFORM_TEX(o.uv0, _VetexMax),0.0,0));
                v.vertex.xyz += lerp( 0.0, (_VertexT_var.rgb*_VertexPower.rgb*v.normal*o.uv1.g*lerp( 1.0, _VetexMax_var.rgb, _VEMask )), _VerTexOffest );
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
