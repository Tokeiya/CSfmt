﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSfmtTest.FloatData
{
	public static class CloseOpen
	{
		public static IList<double> Expected = new double[]
		{
			0.42683407684592445,
			0.66957357522698357,
			0.16134894337663219,
			0.21879031352245071,
			0.40985981974183661,
			0.50758506892872468,
			0.15766516087023885,
			0.76332016643296763,
			0.24411603772732016,
			0.47163144337320917,
			0.54895078501209538,
			0.63171210586731341,
			0.6468544152485165,
			0.44150507042799081,
			0.779880230398589,
			0.26525025994811569,
			0.24183472250646165,
			0.29143672845665836,
			0.30129353933992853,
			0.33080059999664679,
			0.57026106892847173,
			0.96319479960712684,
			0.53728341557726966,
			0.98108916221952258,
			0.92141822237926774,
			0.96946043609020682,
			0.72465299474211209,
			0.24471945839512843,
			0.75972841749829412,
			0.77561093067776921,
			0.022380872515205708,
			0.78276629454935809,
			0.83139548303510935,
			0.64270571398928023,
			0.010552292294011334,
			0.35540982983213065,
			0.90734437654705835,
			0.65810905057555003,
			0.37347994858454037,
			0.27918876964136286,
			0.74458656009508406,
			0.017850971812924499,
			0.040170071312173983,
			0.21589382183322514,
			0.92340073848335313,
			0.059892715077562331,
			0.55006402641752161,
			0.76999632045694621,
			0.39643488418655348,
			0.23456861948424756,
			0.60768179757710317,
			0.95818919634929589,
			0.26992172408671666,
			0.17422919080014565,
			0.23721436822029052,
			0.094049878485849447,
			0.13789716717771805,
			0.38011294948483654,
			0.49659627083379965,
			0.73574563594601861,
			0.22450333110933651,
			0.14130279734340223,
			0.91826606840998726,
			0.13032292769390574,
			0.73328910807604508,
			0.25564997402864864,
			0.55989227738298264,
			0.63316163234601053,
			0.96266443159132908,
			0.39222626047822318,
			0.89217128784843536,
			0.27902740057202746,
			0.43712673553577486,
			0.38672693520186541,
			0.36106258045008266,
			0.32203081409633327,
			0.76260177571489596,
			0.50078784723742076,
			0.16784071120953192,
			0.83161700843404973,
			0.042123180114229664,
			0.22927272774367413,
			0.04594057122759887,
			0.53532589437468947,
			0.58328053797370805,
			0.1151746744595552,
			0.94619124055446813,
			0.23566853405614352,
			0.81392991799401515,
			0.34140825434376021,
			0.10752617303722367,
			0.053206871863764382,
			0.88987512921548695,
			0.7660058759698003,
			0.9461859238891055,
			0.40994134733205434,
			0.53098262510415606,
			0.75619201960766946,
			0.26009845755914762,
			0.41628611642316371,
			0.25962215364830343,
			0.57484693274786958,
			0.12055510530066105,
			0.73870709604139595,
			0.21151398675154032,
			0.6661169223485417,
			0.79613643783226618,
			0.49017148936846766,
			0.30783878426287759,
			0.35612280306552013,
			0.052320157786442278,
			0.61716465727545478,
			0.31608003167864762,
			0.7304048345112335,
			0.69586248434024944,
			0.8567863182802018,
			0.48339304624485546,
			0.8622055292773827,
			0.59488049873993631,
			0.98574259221040395,
			0.16683948708795082,
			0.13371685397945865,
			0.36507400923361599,
			0.60518537921489601,
			0.83183498315026694,
			0.66072948111624918,
			0.6203110511091916,
			0.52842167200310386,
			0.082897677308351847,
			0.84384999679179029,
			0.59903245449249232,
			0.58801600493453554,
			0.18023615067997434,
			0.43627322315793604,
			0.65951134040648873,
			0.59047806129662517,
			0.09362893930508176,
			0.40731530759838352,
			0.50137513463146588,
			0.30998804241742661,
			0.30540985335627502,
			0.48763836594409238,
			0.76829942506506099,
			0.010844059113899629,
			0.63512626818314266,
			0.011204656788836864,
			0.33964546450635691,
			0.095284674847235351,
			0.41455189192843966,
			0.20974810484933148,
			0.3836813077925334,
			0.61829032313993526,
			0.1290841706104453,
			0.31192481751717915,
			0.73853131178441678,
			0.022939676896749805,
			0.44265102737483164,
			0.67332307519681822,
			0.4871896003032139,
			0.53203436378410829,
			0.40034888492563114,
			0.36315631914067814,
			0.18057247738313853,
			0.37259720701860766,
			0.60405643230899586,
			0.377853904814897,
			0.66363272622869163,
			0.57337547481779527,
			0.35550881662163825,
			0.51451111289580864,
			0.10677749757163868,
			0.55099472165407137,
			0.68364384411815915,
			0.64692769589159504,
			0.57772683198607933,
			0.57126607493776138,
			0.77865191135758716,
			0.52003168134760824,
			0.12471163502606464,
			0.35224626824480287,
			0.48755989362544661,
			0.012764455867504854,
			0.8167195639109619,
			0.20825783245066209,
			0.041803856577993681,
			0.34191115040403464,
			0.49537028016800422,
			0.92017027249365269,
			0.9658749113247509,
			0.38795151847384135,
			0.30827214739757269,
			0.95107169866206087,
			0.66321763389754951,
			0.94595326149616166,
			0.68678357293117642,
			0.32452072494271267,
			0.12739216849270707,
			0.39484294242398721,
			0.012506832463119233,
			0.82224989560296491,
			0.43604406375427129,
			0.97120634269986383,
			0.56000282866574591,
			0.34860457857035088,
			0.9858693626451327,
			0.68400405804220932,
			0.68341118335819173,
			0.50282089815641529,
			0.96086341747185355,
			0.49371806990342582,
			0.19721301949459491,
			0.21425971730131144,
			0.1260819424979418,
			0.36741481546862031,
			0.38062670695551959,
			0.16657828690432241,
			0.54630097450045256,
			0.23371598932842552,
			0.71278972742579261,
			0.38746877484036446,
			0.96067312786569747,
			0.39351137967253891,
			0.19079888564178882,
			0.80250608388461919,
			0.62172320326715491,
			0.7332198914194854,
			0.89661415447378623,
			0.22925885753117514,
			0.096987145590862633,
			0.0072154465934390899,
			0.47984801324702975,
			0.0064825336923945986,
			0.4756954067160295,
			0.3090829819693941,
			0.67804791864908709,
			0.089843444536436667,
			0.69485911297873937,
			0.11401722892525612,
			0.99144586673181689,
			0.22163015389620044,
			0.91793109887625568,
			0.68939570738268463,
			0.73762118538205756,
			0.61290192053166281,
			0.33028268887321599,
			0.033762799477598859,
			0.011530866922912386,
			0.92694965258223383,
			0.62091930885902613,
			0.8109442657923307,
			0.55038443042728757,
			0.22891288854125014,
			0.91151626588738477,
			0.43881606236257409,
			0.94579707968860594,
			0.91691393871749671,
			0.15265048285474281,
			0.843852063923894,
			0.068355630824362024,
			0.27396978339739331,
			0.37524596029823809,
			0.50128318288607532,
			0.098455261332467625,
			0.099965212689163696,
			0.17065864438442824,
			0.49684978834946403,
			0.42446565633888644,
			0.90473536898342344,
			0.39842639870659857,
			0.86006278053388519,
			0.43225225285440749,
			0.67119849715729507,
			0.20871271877342235,
			0.71025794113238239,
			0.57711164607125398,
			0.18530671485166894,
			0.31296661379400548,
			0.35898123566518803,
			0.95308047724773681,
			0.15491369000274235,
			0.65466395057218452,
			0.04601057009737719,
			0.32597920902051158,
			0.9556931966544493,
			0.16413462935623335,
			0.68242458354562907,
			0.11534683993393857,
			0.59905412216115472,
			0.33580925238372217,
			0.060673863590638977,
			0.3133829904818064,
			0.65619406372129663,
			0.73932726293023387,
			0.27666313493173322,
			0.39567165136948668,
			0.32925324652256727,
			0.77874065664526237,
			0.22657888787972058,
			0.43256696674790929,
			0.98394902840730625,
			0.91379281823681957,
			0.21964375233259092,
			0.35930747251750828,
			0.80851639540609321,
			0.12190667485983431,
			0.27048371609855382,
			0.51777900045583691,
			0.71177667483290508,
			0.39058348775013974,
			0.99460551465614566,
			0.90687953241040353,
			0.33294196619818406,
			0.68795854114022625,
			0.48726921176855886,
			0.39428682199875831,
			0.42160797865953414,
			0.15059854248926752,
			0.073661645834245837,
			0.70882984355227041,
			0.94475565621899871,
			0.10756460047284166,
			0.54931143476816113,
			0.084858213461930676,
			0.533238420069023,
			0.49563740065500483,
			0.86496593129846588,
			0.8933503637931679,
			0.90426029921768913,
			0.28547211268993644,
			0.6695753087086016,
			0.2531805206226978,
			0.48866212541473453,
			0.63461728942184026,
			0.88356650543818338,
			0.61778555339994723,
			0.6175256332589163,
			0.9229291713897565,
			0.83806047957827756,
			0.41107038090403769,
			0.4202466373024718,
			0.62747527230853239,
			0.11969063246653344,
			0.73021093247770952,
			0.93797697415187731,
			0.45625798176453958,
			0.20889612858093276,
			0.46727655708239313,
			0.2093130144431341,
			0.88235232055506274,
			0.94571756284181774,
			0.22294890856326188,
			0.27907222315404123,
			0.86708495199665458,
			0.62534711034459112,
			0.24583908177984704,
			0.30012190197639255,
			0.58760646299287056,
			0.81882127918478331,
			0.81455199303978998,
			0.13699221779448179,
			0.45266511008995969,
			0.001840654985076462,
			0.80965373469593849,
			0.85881737417770543,
			0.46983439618865086,
			0.17613833860717443,
			0.92997474425845827,
			0.56416336793388289,
			0.30416919095576622,
			0.51967508922807437,
			0.11578066849598478,
			0.68214077435481402,
			0.19806943725294968,
			0.0060466177188138737,
			0.65544607616821171,
			0.22526041303877364,
			0.56402914607669641,
			0.40659462835760363,
			0.13528668549121248,
			0.99948980666656606,
			0.099847864711104517,
			0.98547248316486535,
			0.21299025837626462,
			0.27229376650407477,
			0.27906257685088409,
			0.94501088169796077,
			0.10245224212577142,
			0.67134580434659874,
			0.77258175172094412,
			0.54618656746054994,
			0.42397726712158934,
			0.72552263292129404,
			0.65162905750773303,
			0.63092644302953893,
			0.70558007831725256,
			0.29688731182603512,
			0.37142729168211819,
			0.062966634032281643,
			0.88359507604505882,
			0.014029636998457162,
			0.41895837626152321,
			0.19790950251336525,
			0.048295132060077739,
			0.53193541722896387,
			0.015332714518062618,
			0.20766275258084055,
			0.68590054565727598,
			0.063825393685158938,
			0.39203212756724715,
			0.30035553667294557,
			0.85462272569632813,
			0.60646379440696419,
			0.59581720518275261,
			0.96572889316684818,
			0.95477217286389826,
			0.97379031394234983,
			0.32746421766359068,
			0.89677473086393777,
			0.0035476288478697438,
			0.5950620273633036,
			0.20629145935630122,
			0.72158840271318536,
			0.89920495568905578,
			0.8150352515989272,
			0.099356487704735486,
			0.78354943584640768,
			0.37589656812124295,
			0.45305002752514634,
			0.94367742491958895,
			0.54653816170614822,
			0.90090910051153528,
			0.34994371816182035,
			0.53334583025345061,
			0.093044716493812185,
			0.21915863537236935,
			0.074416006345266128,
			0.67860449790782695,
			0.97458909476075539,
			0.73967319834874434,
			0.24124762194675031,
			0.21681225851096464,
			0.92790292229125426,
			0.94842776188519751,
			0.13767401089637743,
			0.47004045166327879,
			0.16775291851172214,
			0.70887631594117306,
			0.38517108442497627,
			0.1075427019647186,
			0.96198356996090628,
			0.89461670513167046,
			0.36136973282047835,
			0.3043920508033704,
			0.46448858020155015,
			0.48254298358484582,
			0.75752339461497398,
			0.059289470756480167,
			0.44998436411052767,
			0.26600459779944008,
			0.80844608279077601,
			0.32979844142499148,
			0.53045801304048079,
			0.4579723763984016,
			0.38492259312093058,
			0.5035936224189228,
			0.20517722637021052,
			0.95913005365424731,
			0.89949317237774662,
			0.54579649776292682,
			0.47615067447630244,
			0.23202095914495868,
			0.79659144870772503,
			0.70199015675869414,
			0.12430654938882002,
			0.10742908343980595,
			0.42277548503874396,
			0.045325648385642348,
			0.32379727238409917,
			0.76469497816785115,
			0.35085521636453953,
			0.47275450147915277,
			0.1056161796969568,
			0.70928837879515005,
			0.90816244719114181,
			0.081639218134926805,
			0.97909873111419321,
			0.42978678316279639,
			0.94690660576644659,
			0.7893022035104964,
			0.71582628618242716,
			0.013140333158484951,
			0.94512218651467195,
			0.73282053834686467,
			0.60843662380737529,
			0.26589214379962312,
			0.14173649977708891,
			0.83737744423386973,
			0.4353270306320598,
			0.70200559636672111,
			0.37027307001882548,
			0.83018571524518148,
			0.94173245002523753,
			0.56342232405023873,
			0.87763864890368892,
			0.22434512930456996,
			0.45354098297890855,
			0.55984048201934944,
			0.53376600938470165,
			0.8301396671681287,
			0.29905411099370305,
			0.10900545104167092,
			0.093376809887977696,
			0.5451202784662974,
			0.77894275826209158,
			0.24410050757363444,
			0.9086952122806804,
			0.19570432485901712,
			0.48313034071200267,
			0.40647517876619355,
			0.79404652536862597,
			0.91130095299547942,
			0.96855127279856812,
			0.71575484787186117,
			0.28563164375363992,
			0.34790942721245655,
			0.76756456733344813,
			0.76192265582683261,
			0.19717501331810094,
			0.50580985306152204,
			0.013768334933746518,
			0.70188599598925783,
			0.32145217482469168,
			0.52973040455224774,
			0.37009811846464524,
			0.20833823709765165,
			0.46627725232664474,
			0.83300091507543006,
			0.25810642480409696,
			0.32487391563928303,
			0.83847401274436018,
			0.89180218597666583,
			0.84040269486132679,
			0.27128380882148706,
			0.81685446402889306,
			0.47974954541073145,
			0.57431396241618327,
			0.43888607475186769,
			0.38140670536880505,
			0.0017867635419812622,
			0.079701210963645241,
			0.10087033215532082,
			0.62508241286452004,
			0.85406051963339635,
			0.50263608097997614,
			0.56925002964849192,
			0.55065855934556907,
			0.59996238994583684,
			0.014333184419400791,
			0.8544127780057178,
			0.17200773820330961,
			0.58771478902951935,
			0.090658637254134256,
			0.2139499825899247,
			0.7189367826394979,
			0.06387039361634228,
			0.56251014256426646,
			0.09052063754023898,
			0.09964186685214016,
			0.94750472888836601,
			0.62476271508752501,
			0.92314861252264935,
			0.67940532455779046,
			0.55187017728734333,
			0.78628272411988576,
			0.18393825097015726,
			0.10279419287879277,
			0.69544057013637106,
			0.88588426741462167,
			0.24399489423195164,
			0.76783677276869677,
			0.75871475143933442,
			0.41707515375004278,
			0.58748963218567551,
			0.35872858011498532,
			0.29871351001140978,
			0.73227095742954451,
			0.99213185658362346,
			0.65841622368700015,
			0.43668219646124706,
			0.06542460847826792,
			0.66615969802265718,
			0.56496547296604471,
			0.49785376360544187,
			0.61799622495622453,
			0.95183186683221366,
			0.85378966241086784,
			0.10056210283690503,
			0.74604761186587965,
			0.89271853316738459,
			0.44804669667016017,
			0.88159219720560733,
			0.70482014461240206,
			0.028457929428327811,
			0.81621359159238893,
			0.68807047786705366,
			0.088679935463696014,
			0.32593920150465516,
			0.79157879997997482,
			0.94606383689067308,
			0.68910808135255119,
			0.6950575991763035,
			0.56176009651757597,
			0.84696183510249079,
			0.3343619208733839,
			0.80131539966961696,
			0.19843448465250635,
			0.70658836758821009,
			0.090504480829449285,
			0.91359960790328665,
			0.61694522913056904,
			0.49308745987057634,
			0.27432709182002335,
			0.43057634332871242,
			0.31401503510526596,
			0.44392537891331152,
			0.24110514052639775,
			0.17880606556796397,
			0.077427059046699664,
			0.30732663281464645,
			0.18019322810702665,
			0.73048547487740056,
			0.97402139937553889,
			0.93049006410003043,
			0.2232575223199107,
			0.59181309052352904,
			0.069220381984795187,
			0.057934876986561523,
			0.12650129877343752,
			0.35435263670435369,
			0.53839573401968499,
			0.4343801587966345,
			0.57504515024363245,
			0.22715849690286594,
			0.51182422327436239,
			0.84223915273742178,
			0.33683265322672562,
			0.81912219339045556,
			0.026339652430970695,
			0.34417766505408753,
			0.3825526856651591,
			0.15415686604694145,
			0.64909947040863614,
			0.87777481651988287,
			0.21217494184228358,
			0.016663692544345965,
			0.7859480443190594,
			0.5174672449472244,
			0.079171656646388033,
			0.86075817547676192,
			0.77327973947879314,
			0.15139475484592846,
			0.98205895328456783,
			0.32943952948576971,
			0.063742478855740137,
			0.62580662406683052,
			0.15808366823744846,
			0.31978280770257128,
			0.067387868979390086,
			0.090509256668987126,
			0.75679162575725845,
			0.50293702874611679,
			0.49671464957789491,
			0.20058029810684519,
			0.78785854267882738,
			0.78016917149033804,
			0.63015793516831686,
			0.49321320607614338,
			0.48551522642095413,
			0.86559876538218172,
			0.35906757542841006,
			0.56029937480793102,
			0.3697213246299611,
			0.82940936237414409,
			0.74692487312224176,
			0.56030479755171547,
			0.49550192830610706,
			0.98760306670599896,
			0.51426283674435824,
			0.35799300237749709,
			0.984587685243,
			0.51907099605548046,
			0.031641667706883814,
			0.2681868876909399,
			0.48683550344042392,
			0.37403774889453123,
			0.87017045063247345,
			0.34474139586483243,
			0.49054659432109715,
			0.26033413393122129,
			0.37896707667488361,
			0.64930794245216394,
			0.23414294917952927,
			0.056548341868476015,
			0.71039475239900773,
			0.82718787212873468,
			0.74957042877246804,
			0.11585626071380983,
			0.83863799083490953,
			0.75485723414108663,
			0.070798286819274914,
			0.33594279266498339,
			0.25305573177166929,
			0.81200787316440759,
			0.81263070048194597,
			0.82689661829124828,
			0.83867039361668372,
			0.47713586048780865,
			0.26167163292350937,
			0.58783101301490315,
			0.29869634146737267,
			0.62885645548380253,
			0.80798354215870183,
			0.0020099024844038027,
			0.30398519255851575,
			0.56936520606849994,
			0.86643869589770151,
			0.35540679432667366,
			0.43713805626269608,
			0.50150982951542478,
			0.71981880609173454,
			0.062094290639519212,
			0.47948251263850272,
			0.55262255943725891,
			0.53674686645576841,
			0.61031435602614081,
			0.89833559650217243,
			0.24467855034137442,
			0.50981619651604904,
			0.73879991255089195,
			0.46579555572957165,
			0.45267299355055446,
			0.63712892333830595,
			0.33034386731406395,
			0.23388402526614405,
			0.8240206982004834,
			0.49943971380394547,
			0.69225350221322923,
			0.72519680316702839,
			0.65596706739052069,
			0.62042856507483735,
			0.044509752979817963,
			0.52905893017586525,
			0.55192039273319748,
			0.073781522588193926,
			0.71288475354588887,
			0.38401795329469368,
			0.29801931652335467,
			0.16743773830423558,
			0.61395278828570277,
			0.095421380231373698,
			0.45275761948973225,
			0.39050411508622607,
			0.35784795563214833,
			0.79267836685451609,
			0.79793538255601648,
			0.055404279924490263,
			0.81192355277942929,
			0.03525870538403808,
			0.35321654349533871,
			0.65119583280599036,
			0.4700753567619802,
			0.99904345848867226,
			0.98381872480911903,
			0.48187182823360142,
			0.0036725198516565083,
			0.81983334506608774,
			0.29098761812671903,
			0.62388246244695234,
			0.23520326751307241,
			0.058003931779888607,
			0.19176950540329818,
			0.12811329480274725,
			0.45070308812474025,
			0.30015088260895384,
			0.16526490317497378,
			0.018297020805552178,
			0.37052034582853444,
			0.56407636848443876,
			0.50338504179256516,
			0.79473220759266749,
			0.73011028313109705,
			0.42513360708494452,
			0.20517355462393994,
			0.5234887265621091,
			0.11545779377792886,
			0.090052980555380469,
			0.62895760790643762,
			0.43144035910152123,
			0.96453409249636568,
			0.61224972365014096,
			0.68087327133035003,
			0.02309162408265597,
			0.6600267921553209,
			0.76604612347943779,
			0.4834842326632065,
			0.19044041263511491,
			0.4752687403663145,
			0.58841339267158466,
			0.89264957504750764,
			0.011700702342692049,
			0.95851829120105236,
			0.4166248413193141,
			0.73670579000939118,
			0.61430902545963395,
			0.86458968303207295,
			0.41895755378793464,
			0.33524391136467857,
			0.4427876653649141,
			0.50973692110620727,
			0.66202876185289128,
			0.77486210329583316,
			0.78382287934085038,
			0.43865324801446293,
			0.85357644562618473,
			0.55105055821716231,
			0.029888826980436045,
			0.85805257250557654,
			0.96804934153663069,
			0.25622408212180159,
			0.27028737147851012,
			0.40304092470239228,
			0.92422833410410887,
			0.048825171074923457,
			0.85601236610379106,
			0.40987687385798188,
			0.45561423395067102,
			0.87222626059934849,
			0.37455931853974311,
			0.16077705099014072,
			0.18229947416509673,
			0.66689132092681858,
			0.45517643258943519,
			0.51893587850390999,
			0.2271068078678895,
			0.26057652591118163,
			0.9778900873556331,
			0.9841761331126242,
			0.13494200019261493,
			0.82698199813345297,
			0.26094337744385299,
			0.8423764542984653,
			0.28056834668202746,
			0.66663219194306111,
			0.88737593101164292,
			0.69555505461546385,
			0.16245265332210312,
			0.83546618916560922,
			0.0056688735707814963,
			0.63775944047624922,
			0.23437194011305662,
			0.41483552331009399,
			0.50105160616235556,
			0.67502165750200738,
			0.30317289947730086,
			0.90959295488184,
			0.95351964140599055,
			0.32325297026240407,
			0.83359092474031637,
			0.12403982407761305,
			0.60574327282279072,
			0.46631353492215299,
			0.48270530659998623,
			0.080545711817593846,
			0.26403163547248809,
			0.85630400906850146,
			0.084919116430415054,
			0.57487660119367789,
			0.32056864685183517,
			0.25127015662395746,
			0.57454515736657652,
			0.069342773038135075,
			0.47665173835383978,
			0.50643186595171374,
			0.79910003689807718,
			0.03992821969383642,
			0.11499832215502392,
			0.21259130742907395,
			0.3022727303935091,
			0.52759461533203544,
			0.90801486603921799,
			0.20404477566728985,
			0.183417296474645,
			0.4068446825595875,
			0.0121249117561959,
			0.49715017911688775,
			0.78276199593736551,
			0.64206509688529234,
			0.0023724269378977425,
			0.92634870714808293,
			0.5835137603237861,
			0.90650000407454878,
			0.27185145239263253,
			0.322812584825108,
			0.62959837609527947,
			0.18291297257482841,
			0.56072101959273812,
			0.079454073146467463,
			0.56653911791446521,
			0.95763424370009398,
			0.2786680023631094,
			0.060051048261784423,
			0.26595263742558406,
			0.26966003157497753,
			0.47308586923085527,
			0.87770733720818783,
			0.85751959684534795,
			0.16412025203523073,
			0.167333872031177,
			0.12888719581595187,
			0.37152107288688563,
			0.022814861420373367,
			0.20140016487886303,
			0.54138460343708794,
			0.93799580057733523,
			0.51619693368600084,
			0.082664013195098196,
			0.74594570795877857,
			0.38588782952963063,
			0.96767236418675684,
			0.82606046790178156,
			0.44340738111790379,
			0.88096236607169942,
			0.98849095110919705,
			0.88441877791756607,
			0.96043842009602076,
			0.68937886736763931,
			0.10447330847836223,
			0.1723724836899152,
			0.65914565412280535,
			0.60968753747763627,
			0.48282160658391371,
			0.38189962018615353,
			0.81837834706118162,
			0.35944010175245134,
			0.61296574624250666,
			0.13450813353959101,
			0.70458991402600657,
			0.97022870548161433,
			0.15191863719151355,
			0.98756517781891717,
			0.67973255338305139,
			0.74434220668054629,
			0.35421442739128972,
			0.9545143148776547,
			0.88802515671338189,
			0.089873408339493199,
			0.91041051374328719,
			0.75689899045741504,
			0.78319250158978004,
			0.74427197475963092,
			0.64163482983459819,
			0.460781477020485,
			0.085146660894919401,
			0.12671833523590803,
			0.49132474803888759,
			0.42930562789809112,
			0.15043330066056959,
			0.022335509137851872,
			0.16000269014282886,
			0.4317362053248599,
			0.45277306029622566,
			0.795516161299469,
			0.097854723917131459,
			0.58984195465580291,
			0.74931916059558779,
			0.3665589514329175,
			0.96249465544292279,
			0.63296567880366994,
			0.85487361531305361,
			0.22966332727794025,
			0.15751714473728162,
			0.32536965104482718,
			0.76955758472758773,
			0.24490362532717236,
			0.41572841913825775,
			0.5924673441815882,
			0.22627087113866873,
			0.55848182839269644,
			0.71325935615316105,
			0.7986932895378982,
			0.23468787627589327,
			0.32883170033474807,
			0.0093238775619910452,
			0.74446907414348584,
			0.31771544426868181,
			0.041985542271969045,
			0.21872089802934958,
			0.81502845328523321,
			0.19065767055307004,
			0.19999837058128422,
			0.65680665192394816,
			0.016180539265479021,
			0.67033762385858586,
			0.44448283974156411,
			0.55942012719112921,
			0.9763330302774782,
			0.12972058549578769,
			0.40908531440782081,
			0.14886327974059066,
			0.28312557240292646,
			0.99320994644953453,
			0.58615829479302772,
			0.67206898677182991,
			0.084707665965251477,
			0.78821873540819221,
			0.40294775050770992,
			0.035334677975066064,
			0.2474892794713377,
			0.8922062453125017,
			0.23477753703708104,
			0.70977663793111589,
			0.37724435864436301,
			0.17968657036974411,
			0.88805169158973141,

		};

		public static IList<uint> ExpectedAfterStatus = new uint[]
		{
			4071836957,
			1072931440,
			2562996556,
			1073229934,
			3271995399,
			1073576399,
			2732414673,
			1073046442,
			3749749503,
			1073552159,
			547124964,
			1072720867,
			1886829620,
			1073054144,
			2426330233,
			1073094383,
			815923689,
			1072854893,
			542382240,
			1073373878,
			2602780902,
			1073613661,
			2370036889,
			1072915729,
			635970325,
			1072710721,
			1101744590,
			1073517374,
			3151809269,
			1073235851,
			2143358946,
			1072776265,
			1565982533,
			1073595818,
			2474346615,
			1073504090,
			3893204563,
			1072851996,
			1928437391,
			1073723011,
			1649568392,
			1073038690,
			3579892335,
			1072760086,
			3464481686,
			1073349453,
			3180447982,
			1072859010,
			2479783255,
			1073028564,
			1297522217,
			1072763909,
			3583381151,
			1072788153,
			2301114813,
			1073486801,
			2997262204,
			1073220615,
			259397946,
			1073214091,
			2949215243,
			1072903571,
			2401951845,
			1073519377,
			2874040299,
			1073511314,
			2091765265,
			1073354016,
			2279659111,
			1073210419,
			2637365807,
			1073202347,
			390882339,
			1073600894,
			2757250551,
			1073069757,
			2049724184,
			1073280764,
			3898521139,
			1073080928,
			3227929188,
			1073562946,
			2129268811,
			1073476455,
			701787295,
			1073280770,
			1846730730,
			1073212819,
			3750684600,
			1073728824,
			2870335458,
			1073232491,
			3738601469,
			1073068630,
			71474114,
			1073725663,
			1669710934,
			1073237533,
			2977947415,
			1072726426,
			1434293226,
			1072974462,
			106753757,
			1073203732,
			28281638,
			1073085455,
			3652622280,
			1073605687,
			2379026892,
			1073054735,
			1656512359,
			1073207623,
			536102389,
			1072966228,
			3356050692,
			1073090623,
			3114129261,
			1073374096,
			2908031621,
			1072938764,
			1005550968,
			1072752543,
			3813465135,
			1073438150,
			1504120139,
			1073560617,
			2413430254,
			1073479229,
			405591989,
			1072814732,
			2877102840,
			1073572623,
			769429321,
			1073484773,
			1650984604,
			1072767485,
			2361207567,
			1073045509,
			717251850,
			1072958596,
			4155858639,
			1073544699,
			212122237,
			1073545353,
			4073438741,
			1073560311,
			2777370700,
			1073572657,
			910734348,
			1073193561,
			2551916674,
			1072967630,
			2114425779,
			1073309633,
			3500185710,
			1073006453,
			3378702096,
			1073352651,
			647262751,
			1073540480,
			2299987140,
			1072695355,
			2479365384,
			1073011999,
			2964895310,
			1073290270,
			3530418571,
			1073601774,
			149326934,
			1073065919,
			2037892029,
			1073151620,
			734413492,
			1073219119,
			3111344947,
			1073448032,
			2503543414,
			1072758358,
			3672937622,
			1073196021,
			3233614233,
			1073272714,
			3789195056,
			1073256067,
			4235630032,
			1073333208,
			628945828,
			1073635221,
			238812023,
			1072949812,
			120594146,
			1073227829,
			3681229200,
			1073467935,
			174567620,
			1073181670,
			158421012,
			1073167910,
			420556272,
			1073361326,
			2796078307,
			1073039638,
			754528998,
			1072938493,
			1407187065,
			1073557296,
			2136066276,
			1073216948,
			1753792940,
			1073419128,
			4136387096,
			1073453671,
			1390092290,
			1073381079,
			2160591513,
			1073343814,
			3688262645,
			1072739919,
			2133602692,
			1073248006,
			2051837326,
			1073271978,
			2292779978,
			1072770613,
			3622092525,
			1073440761,
			40346651,
			1073095920,
			3877680270,
			1073005743,
			832708698,
			1072868819,
			682596872,
			1073337024,
			2444684617,
			1072793304,
			3322647111,
			1073167998,
			1043593965,
			1073102721,
			3341162157,
			1073068478,
			2195599484,
			1073524431,
			3829818290,
			1073529943,
			2569361545,
			1072751343,
			2367726319,
			1073544611,
			1856528704,
			1072730219,
			1676377998,
			1073063622,
			1381177190,
			1073376076,
			3166645243,
			1073186157,
			4267171204,
			1073740820,
			3020158497,
			1073724856,
			1005717603,
			1073198527,
			3934945831,
			1072697098,
			2446568068,
			1073552905,
			2717274808,
			1072998370,
			759963434,
			1073347436,
			2153650308,
			1072939876,
			2279639914,
			1072754069,
			3869326454,
			1072894332,
			2260059403,
			1072827584,
			1895512896,
			1073165844,
			51035234,
			1073007979,
			3483697801,
			1072866540,
			3508508115,
			1072712433,
			3187499247,
			1073081766,
			4046546125,
			1073284724,
			2034022126,
			1073221085,
			512426917,
			1073526585,
			516446080,
			1073458824,
			3853370367,
			1073139032,
			280089216,
			1072908388,
			3070659338,
			1073242165,
			1166377764,
			1072814314,
			1692813422,
			1072787675,
			3662181607,
			1073352757,
			25706396,
			1073145646,
			1290953220,
			1073704635,
			1572929424,
			1073335238,
			1594972583,
			1073407195,
			1386475981,
			1072717461,
			1089251199,
			1073385336,
			2490967537,
			1073496505,
			4134975703,
			1073200217,
			1057074251,
			1072892939,
			1695216499,
			1073191603,
			694212742,
			1073310244,
			3954827427,
			1073629258,
			324955897,
			1072705517,
			1184184480,
			1073698327,
			3477253803,
			1073130110,
			44946349,
			1073465740,
			3009399236,
			1073337397,
			4258352867,
			1073599835,
			3590252229,
			1073132556,
			3090671906,
			1073044776,
			2229078063,
			1073157544,
			3873140787,
			1073227745,
			2022880880,
			1073387435,
			3456699337,
			1073505749,
			3691643411,
			1073515145,
			1151867337,
			1073153209,
			3328809774,
			1073588287,
			1675608988,
			1073271066,
			3034994993,
			1072724588,
			1435668442,
			1073592981,
			1315768016,
			1073708321,
			1817350773,
			1072961918,
			3654310433,
			1072976664,
			174636530,
			1073115867,
			2790275111,
			1073662371,
			3876573310,
			1072744444,
			97923746,
			1073590842,
			227128639,
			1073123035,
			648450120,
			1073170994,
			2248132867,
			1073607843,
			3916997846,
			1073086001,
			4110365469,
			1072861834,
			3665420183,
			1072884402,
			1003871982,
			1073392534,
			356391660,
			1073170535,
			3039712062,
			1073237391,
			3213352282,
			1072931386,
			1250839832,
			1072966482,
			327444099,
			1073718640,
			2031324982,
			1073725231,
			4049269274,
			1072834744,
			2043007639,
			1073560401,
			4135824007,
			1072966866,
			3147963928,
			1073576543,
			1007987823,
			1072987445,
			2221781803,
			1073392262,
			447692206,
			1073623729,
			1447182636,
			1073422590,
			4094864244,
			1072863591,
			3413513382,
			1073569297,
			1051293558,
			1072699192,
			1043921108,
			1073361987,
			3399363486,
			1072939004,
			2463981528,
			1073128234,
			2959160733,
			1073218638,
			2188443999,
			1073401059,
			3548683701,
			1073011147,
			1469987691,
			1073647025,
			3485395716,
			1073693085,
			1316604489,
			1073032203,
			1879050348,
			1073567331,
			784140800,
			1072823313,
			3685268473,
			1073328415,
			778224587,
			1073182213,
			857161171,
			1073199401,
			1289842439,
			1072777706,
			1014459248,
			1072970105,
			3576045661,
			1073591147,
			633207625,
			1072782292,
			3465923769,
			1073296049,
			2531631149,
			1073029388,
			3675427381,
			1072956723,
			1129279253,
			1073295702,
			719755926,
			1072765959,
			2461858572,
			1073193053,
			1289659211,
			1073224280,
			516643498,
			1073531165,
			3319553097,
			1072735115,
			2064384734,
			1072813832,
			613230056,
			1072916166,
			3996660790,
			1073010203,
			220617027,
			1073246471,
			2560538981,
			1073645370,
			1952879127,
			1072907204,
			4187886019,
			1072885574,
			2447528354,
			1073119855,
			3828833056,
			1072705961,
			3204980487,
			1073214547,
			1901175945,
			1073514033,
			219179020,
			1073366502,
			2877408328,
			1072695735,
			94259535,
			1073664595,
			2253763792,
			1073305106,
			636825427,
			1073643782,
			3902167012,
			1072978304,
			2271803935,
			1073031741,
			3207535194,
			1073353429,
			657691019,
			1072885046,
			2593475092,
			1073281206,
			2723883851,
			1072776561,
			1383435931,
			1073287307,
			1222871978,
			1073697400,
			2487842196,
			1072985452,
			377880452,
			1072756216,
			2374005245,
			1072972119,
			160068121,
			1072976007,
			2097724791,
			1073189314,
			3645655913,
			1073613590,
			2013402103,
			1073592422,
			2394006585,
			1072865340,
			1212035319,
			1072868710,
			86929741,
			1072828396,
			345845537,
			1073082816,
			498769095,
			1072717171,
			1629029636,
			1072904431,
			3873775521,
			1073260930,
			3799268713,
			1073676807,
			3074924850,
			1073234519,
			2148772410,
			1072779927,
			3292816496,
			1073475428,
			3078361382,
			1073097880,
			72797517,
			1073707926,
			778207609,
			1073559435,
			1451969903,
			1073158194,
			4269051456,
			1073617003,
			3811999415,
			1073729755,
			1307704540,
			1073620628,
			2906793515,
			1073700340,
			580803659,
			1073416114,
			875791107,
			1072802796,
			2789399309,
			1072873993,
			1346117804,
			1073384412,
			3089362030,
			1073332551,
			639715338,
			1073199523,
			3333480095,
			1073093698,
			3838189044,
			1073551379,
			1134451953,
			1073070148,
			731570295,
			1073335989,
			2724771,
			1072834290,
			316494997,
			1073432064,
			2298147189,
			1073710606,
			1017528126,
			1072852546,
			618031401,
			1073728785,
			1038936210,
			1073405999,
			1605081937,
			1073473747,
			3210128346,
			1073064668,
			3445582330,
			1073694128,
			3722559357,
			1073624409,
			425300514,
			1072787487,
			2640798786,
			1073647882,
			497433983,
			1073486914,
			3696021244,
			1073514484,
			3136208977,
			1073473673,
			3793867636,
			1073366050,
			1709604139,
			1073176412,
			3200156729,
			1072782530,
			3457828037,
			1072826121,
			1455992301,
			1073208439,
			2482829836,
			1073143407,
			3215528015,
			1072850988,
			2056558040,
			1072716668,
			4212586417,
			1072861022,
			958785823,
			1073145956,
			4142380745,
			1073168014,
			662931432,
			1073527407,
			493861668,
			1072795856,
			504423135,
			1073311742,
			378561334,
			1073478966,
			3947322863,
			1073077612,
			3418045433,
			1073702496,
			2661235769,
			1073356960,
			4106205633,
			1073589647,
			1945894176,
			1072934067,
			2995997558,
			1072858416,
			3466957648,
			1073034422,
			2636952359,
			1073500187,
			274152337,
			1072950048,
			3619911471,
			1073129170,
			162747276,
			1073314495,
			880361354,
			1072930510,
			2756031981,
			1073278858,
			2760107720,
			1073441154,
			2640419154,
			1073530738,
			320206438,
			1072939336,
			124597898,
			1073038053,
			3411228135,
			1072703024,
			1734690389,
			1073473880,
			3391690479,
			1073026396,
			137324588,
			1072737273,
			2080361999,
			1072922593,
			1182971504,
			1073547867,
			247209109,
			1072893167,
			2110678530,
			1072902961,
			2971463700,
			1073381959,
			2255462729,
			1072710214,
			4055630847,
			1073396147,
			163916784,
			1073159322,
			2230331701,
			1073279842,
			1647361494,
			1073717007,
			3833931896,
			1072829269,
			183139532,
			1073122205,
			1986067050,
			1072849342,
			2921470990,
			1072990126,
			484507818,
			1073734704,
			2233902229,
			1073307879,
			1760392240,
			1073397963,
			1827711219,
			1072782070,
			1068155706,
			1073519755,
			1462163086,
			1073115769,
			409277668,
			1072730299,
			1368853053,
			1072952759,
			240023396,
			1073628794,
			389451289,
			1072939430,
			3212185673,
			1073437502,
			1634706951,
			1073088817,
			108284822,
			1072881663,
			2965934309,
			1073624437,

		};
	}
}