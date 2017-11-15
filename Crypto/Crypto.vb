Imports System.Security.Cryptography
'Imports System.Threading
'Imports System.IO
Imports System.Text


Public Class Crypto
    Private Const GENSALT_DEFAULT_LOG2_ROUNDS As Integer = 10
    Private Const BCRYPT_SALT_LEN As Integer = 16

    Private Const BLOWFISH_NUM_ROUNDS As Integer = 16

    Private ReadOnly pbox As UInteger() = {
        &H243F6A88UI, &H85A308D3UI, &H13198A2EUI, &H3707344UI,
        &HA4093822UI, &H299F31D0UI, &H82EFA98UI, &HEC4E6C89UI,
        &H452821E6UI, &H38D01377UI, &HBE5466CFUI, &H34E90C6CUI,
        &HC0AC29B7UI, &HC97C50DDUI, &H3F84D5B5UI, &HB5470917UI,
        &H9216D5D9UI, &H8979FB1BUI
    }

    Private ReadOnly sbox As UInteger() = {
        &HD1310BA6UI, &H98DFB5ACUI, &H2FFD72DBUI, &HD01ADFB7UI,
        &HB8E1AFEDUI, &H6A267E96UI, &HBA7C9045UI, &HF12C7F99UI,
        &H24A19947UI, &HB3916CF7UI, &H801F2E2UI, &H858EFC16UI,
        &H636920D8UI, &H71574E69UI, &HA458FEA3UI, &HF4933D7EUI,
        &HD95748FUI, &H728EB658UI, &H718BCD58UI, &H82154AEEUI,
        &H7B54A41DUI, &HC25A59B5UI, &H9C30D539UI, &H2AF26013UI,
        &HC5D1B023UI, &H286085F0UI, &HCA417918UI, &HB8DB38EFUI,
        &H8E79DCB0UI, &H603A180EUI, &H6C9E0E8BUI, &HB01E8A3EUI,
        &HD71577C1UI, &HBD314B27UI, &H78AF2FDAUI, &H55605C60UI,
        &HE65525F3UI, &HAA55AB94UI, &H57489862UI, &H63E81440UI,
        &H55CA396AUI, &H2AAB10B6UI, &HB4CC5C34UI, &H1141E8CEUI,
        &HA15486AFUI, &H7C72E993UI, &HB3EE1411UI, &H636FBC2AUI,
        &H2BA9C55DUI, &H741831F6UI, &HCE5C3E16UI, &H9B87931EUI,
        &HAFD6BA33UI, &H6C24CF5CUI, &H7A325381UI, &H28958677UI,
        &H3B8F4898UI, &H6B4BB9AFUI, &HC4BFE81BUI, &H66282193UI,
        &H61D809CCUI, &HFB21A991UI, &H487CAC60UI, &H5DEC8032UI,
        &HEF845D5DUI, &HE98575B1UI, &HDC262302UI, &HEB651B88UI,
        &H23893E81UI, &HD396ACC5UI, &HF6D6FF3UI, &H83F44239UI,
        &H2E0B4482UI, &HA4842004UI, &H69C8F04AUI, &H9E1F9B5EUI,
        &H21C66842UI, &HF6E96C9AUI, &H670C9C61UI, &HABD388F0UI,
        &H6A51A0D2UI, &HD8542F68UI, &H960FA728UI, &HAB5133A3UI,
        &H6EEF0B6CUI, &H137A3BE4UI, &HBA3BF050UI, &H7EFB2A98UI,
        &HA1F1651DUI, &H39AF0176UI, &H66CA593EUI, &H82430E88UI,
        &H8CEE8619UI, &H456F9FB4UI, &H7D84A5C3UI, &H3B8B5EBEUI,
        &HE06F75D8UI, &H85C12073UI, &H401A449FUI, &H56C16AA6UI,
        &H4ED3AA62UI, &H363F7706UI, &H1BFEDF72UI, &H429B023DUI,
        &H37D0D724UI, &HD00A1248UI, &HDB0FEAD3UI, &H49F1C09BUI,
        &H75372C9UI, &H80991B7BUI, &H25D479D8UI, &HF6E8DEF7UI,
        &HE3FE501AUI, &HB6794C3BUI, &H976CE0BDUI, &H4C006BAUI,
        &HC1A94FB6UI, &H409F60C4UI, &H5E5C9EC2UI, &H196A2463UI,
        &H68FB6FAFUI, &H3E6C53B5UI, &H1339B2EBUI, &H3B52EC6FUI,
        &H6DFC511FUI, &H9B30952CUI, &HCC814544UI, &HAF5EBD09UI,
        &HBEE3D004UI, &HDE334AFDUI, &H660F2807UI, &H192E4BB3UI,
        &HC0CBA857UI, &H45C8740FUI, &HD20B5F39UI, &HB9D3FBDBUI,
        &H5579C0BDUI, &H1A60320AUI, &HD6A100C6UI, &H402C7279UI,
        &H679F25FEUI, &HFB1FA3CCUI, &H8EA5E9F8UI, &HDB3222F8UI,
        &H3C7516DFUI, &HFD616B15UI, &H2F501EC8UI, &HAD0552ABUI,
        &H323DB5FAUI, &HFD238760UI, &H53317B48UI, &H3E00DF82UI,
        &H9E5C57BBUI, &HCA6F8CA0UI, &H1A87562EUI, &HDF1769DBUI,
        &HD542A8F6UI, &H287EFFC3UI, &HAC6732C6UI, &H8C4F5573UI,
        &H695B27B0UI, &HBBCA58C8UI, &HE1FFA35DUI, &HB8F011A0UI,
        &H10FA3D98UI, &HFD2183B8UI, &H4AFCB56CUI, &H2DD1D35BUI,
        &H9A53E479UI, &HB6F84565UI, &HD28E49BCUI, &H4BFB9790UI,
        &HE1DDF2DAUI, &HA4CB7E33UI, &H62FB1341UI, &HCEE4C6E8UI,
        &HEF20CADAUI, &H36774C01UI, &HD07E9EFEUI, &H2BF11FB4UI,
        &H95DBDA4DUI, &HAE909198UI, &HEAAD8E71UI, &H6B93D5A0UI,
        &HD08ED1D0UI, &HAFC725E0UI, &H8E3C5B2FUI, &H8E7594B7UI,
        &H8FF6E2FBUI, &HF2122B64UI, &H8888B812UI, &H900DF01CUI,
        &H4FAD5EA0UI, &H688FC31CUI, &HD1CFF191UI, &HB3A8C1ADUI,
        &H2F2F2218UI, &HBE0E1777UI, &HEA752DFEUI, &H8B021FA1UI,
        &HE5A0CC0FUI, &HB56F74E8UI, &H18ACF3D6UI, &HCE89E299UI,
        &HB4A84FE0UI, &HFD13E0B7UI, &H7CC43B81UI, &HD2ADA8D9UI,
        &H165FA266UI, &H80957705UI, &H93CC7314UI, &H211A1477UI,
        &HE6AD2065UI, &H77B5FA86UI, &HC75442F5UI, &HFB9D35CFUI,
        &HEBCDAF0CUI, &H7B3E89A0UI, &HD6411BD3UI, &HAE1E7E49UI,
        &H250E2DUI, &H2071B35EUI, &H226800BBUI, &H57B8E0AFUI,
        &H2464369BUI, &HF009B91EUI, &H5563911DUI, &H59DFA6AAUI,
        &H78C14389UI, &HD95A537FUI, &H207D5BA2UI, &H2E5B9C5UI,
        &H83260376UI, &H6295CFA9UI, &H11C81968UI, &H4E734A41UI,
        &HB3472DCAUI, &H7B14A94AUI, &H1B510052UI, &H9A532915UI,
        &HD60F573FUI, &HBC9BC6E4UI, &H2B60A476UI, &H81E67400UI,
        &H8BA6FB5UI, &H571BE91FUI, &HF296EC6BUI, &H2A0DD915UI,
        &HB6636521UI, &HE7B9F9B6UI, &HFF34052EUI, &HC5855664UI,
        &H53B02D5DUI, &HA99F8FA1UI, &H8BA4799UI, &H6E85076AUI,
        &H4B7A70E9UI, &HB5B32944UI, &HDB75092EUI, &HC4192623UI,
        &HAD6EA6B0UI, &H49A7DF7DUI, &H9CEE60B8UI, &H8FEDB266UI,
        &HECAA8C71UI, &H699A17FFUI, &H5664526CUI, &HC2B19EE1UI,
        &H193602A5UI, &H75094C29UI, &HA0591340UI, &HE4183A3EUI,
        &H3F54989AUI, &H5B429D65UI, &H6B8FE4D6UI, &H99F73FD6UI,
        &HA1D29C07UI, &HEFE830F5UI, &H4D2D38E6UI, &HF0255DC1UI,
        &H4CDD2086UI, &H8470EB26UI, &H6382E9C6UI, &H21ECC5EUI,
        &H9686B3FUI, &H3EBAEFC9UI, &H3C971814UI, &H6B6A70A1UI,
        &H687F3584UI, &H52A0E286UI, &HB79C5305UI, &HAA500737UI,
        &H3E07841CUI, &H7FDEAE5CUI, &H8E7D44ECUI, &H5716F2B8UI,
        &HB03ADA37UI, &HF0500C0DUI, &HF01C1F04UI, &H200B3FFUI,
        &HAE0CF51AUI, &H3CB574B2UI, &H25837A58UI, &HDC0921BDUI,
        &HD19113F9UI, &H7CA92FF6UI, &H94324773UI, &H22F54701UI,
        &H3AE5E581UI, &H37C2DADCUI, &HC8B57634UI, &H9AF3DDA7UI,
        &HA9446146UI, &HFD0030EUI, &HECC8C73EUI, &HA4751E41UI,
        &HE238CD99UI, &H3BEA0E2FUI, &H3280BBA1UI, &H183EB331UI,
        &H4E548B38UI, &H4F6DB908UI, &H6F420D03UI, &HF60A04BFUI,
        &H2CB81290UI, &H24977C79UI, &H5679B072UI, &HBCAF89AFUI,
        &HDE9A771FUI, &HD9930810UI, &HB38BAE12UI, &HDCCF3F2EUI,
        &H5512721FUI, &H2E6B7124UI, &H501ADDE6UI, &H9F84CD87UI,
        &H7A584718UI, &H7408DA17UI, &HBC9F9ABCUI, &HE94B7D8CUI,
        &HEC7AEC3AUI, &HDB851DFAUI, &H63094366UI, &HC464C3D2UI,
        &HEF1C1847UI, &H3215D908UI, &HDD433B37UI, &H24C2BA16UI,
        &H12A14D43UI, &H2A65C451UI, &H50940002UI, &H133AE4DDUI,
        &H71DFF89EUI, &H10314E55UI, &H81AC77D6UI, &H5F11199BUI,
        &H43556F1UI, &HD7A3C76BUI, &H3C11183BUI, &H5924A509UI,
        &HF28FE6EDUI, &H97F1FBFAUI, &H9EBABF2CUI, &H1E153C6EUI,
        &H86E34570UI, &HEAE96FB1UI, &H860E5E0AUI, &H5A3E2AB3UI,
        &H771FE71CUI, &H4E3D06FAUI, &H2965DCB9UI, &H99E71D0FUI,
        &H803E89D6UI, &H5266C825UI, &H2E4CC978UI, &H9C10B36AUI,
        &HC6150EBAUI, &H94E2EA78UI, &HA5FC3C53UI, &H1E0A2DF4UI,
        &HF2F74EA7UI, &H361D2B3DUI, &H1939260FUI, &H19C27960UI,
        &H5223A708UI, &HF71312B6UI, &HEBADFE6EUI, &HEAC31F66UI,
        &HE3BC4595UI, &HA67BC883UI, &HB17F37D1UI, &H18CFF28UI,
        &HC332DDEFUI, &HBE6C5AA5UI, &H65582185UI, &H68AB9802UI,
        &HEECEA50FUI, &HDB2F953BUI, &H2AEF7DADUI, &H5B6E2F84UI,
        &H1521B628UI, &H29076170UI, &HECDD4775UI, &H619F1510UI,
        &H13CCA830UI, &HEB61BD96UI, &H334FE1EUI, &HAA0363CFUI,
        &HB5735C90UI, &H4C70A239UI, &HD59E9E0BUI, &HCBAADE14UI,
        &HEECC86BCUI, &H60622CA7UI, &H9CAB5CABUI, &HB2F3846EUI,
        &H648B1EAFUI, &H19BDF0CAUI, &HA02369B9UI, &H655ABB50UI,
        &H40685A32UI, &H3C2AB4B3UI, &H319EE9D5UI, &HC021B8F7UI,
        &H9B540B19UI, &H875FA099UI, &H95F7997EUI, &H623D7DA8UI,
        &HF837889AUI, &H97E32D77UI, &H11ED935FUI, &H16681281UI,
        &HE358829UI, &HC7E61FD6UI, &H96DEDFA1UI, &H7858BA99UI,
        &H57F584A5UI, &H1B227263UI, &H9B83C3FFUI, &H1AC24696UI,
        &HCDB30AEBUI, &H532E3054UI, &H8FD948E4UI, &H6DBC3128UI,
        &H58EBF2EFUI, &H34C6FFEAUI, &HFE28ED61UI, &HEE7C3C73UI,
        &H5D4A14D9UI, &HE864B7E3UI, &H42105D14UI, &H203E13E0UI,
        &H45EEE2B6UI, &HA3AAABEAUI, &HDB6C4F15UI, &HFACB4FD0UI,
        &HC742F442UI, &HEF6ABBB5UI, &H654F3B1DUI, &H41CD2105UI,
        &HD81E799EUI, &H86854DC7UI, &HE44B476AUI, &H3D816250UI,
        &HCF62A1F2UI, &H5B8D2646UI, &HFC8883A0UI, &HC1C7B6A3UI,
        &H7F1524C3UI, &H69CB7492UI, &H47848A0BUI, &H5692B285UI,
        &H95BBF00UI, &HAD19489DUI, &H1462B174UI, &H23820E00UI,
        &H58428D2AUI, &HC55F5EAUI, &H1DADF43EUI, &H233F7061UI,
        &H3372F092UI, &H8D937E41UI, &HD65FECF1UI, &H6C223BDBUI,
        &H7CDE3759UI, &HCBEE7460UI, &H4085F2A7UI, &HCE77326EUI,
        &HA6078084UI, &H19F8509EUI, &HE8EFD855UI, &H61D99735UI,
        &HA969A7AAUI, &HC50C06C2UI, &H5A04ABFCUI, &H800BCADCUI,
        &H9E447A2EUI, &HC3453484UI, &HFDD56705UI, &HE1E9EC9UI,
        &HDB73DBD3UI, &H105588CDUI, &H675FDA79UI, &HE3674340UI,
        &HC5C43465UI, &H713E38D8UI, &H3D28F89EUI, &HF16DFF20UI,
        &H153E21E7UI, &H8FB03D4AUI, &HE6E39F2BUI, &HDB83ADF7UI,
        &HE93D5A68UI, &H948140F7UI, &HF64C261CUI, &H94692934UI,
        &H411520F7UI, &H7602D4F7UI, &HBCF46B2EUI, &HD4A20068UI,
        &HD4082471UI, &H3320F46AUI, &H43B7D4B7UI, &H500061AFUI,
        &H1E39F62EUI, &H97244546UI, &H14214F74UI, &HBF8B8840UI,
        &H4D95FC1DUI, &H96B591AFUI, &H70F4DDD3UI, &H66A02F45UI,
        &HBFBC09ECUI, &H3BD9785UI, &H7FAC6DD0UI, &H31CB8504UI,
        &H96EB27B3UI, &H55FD3941UI, &HDA2547E6UI, &HABCA0A9AUI,
        &H28507825UI, &H530429F4UI, &HA2C86DAUI, &HE9B66DFBUI,
        &H68DC1462UI, &HD7486900UI, &H680EC0A4UI, &H27A18DEEUI,
        &H4F3FFEA2UI, &HE887AD8CUI, &HB58CE006UI, &H7AF4D6B6UI,
        &HAACE1E7CUI, &HD3375FECUI, &HCE78A399UI, &H406B2A42UI,
        &H20FE9E35UI, &HD9F385B9UI, &HEE39D7ABUI, &H3B124E8BUI,
        &H1DC9FAF7UI, &H4B6D1856UI, &H26A36631UI, &HEAE397B2UI,
        &H3A6EFA74UI, &HDD5B4332UI, &H6841E7F7UI, &HCA7820FBUI,
        &HFB0AF54EUI, &HD8FEB397UI, &H454056ACUI, &HBA489527UI,
        &H55533A3AUI, &H20838D87UI, &HFE6BA9B7UI, &HD096954BUI,
        &H55A867BCUI, &HA1159A58UI, &HCCA92963UI, &H99E1DB33UI,
        &HA62A4A56UI, &H3F3125F9UI, &H5EF47E1CUI, &H9029317CUI,
        &HFDF8E802UI, &H4272F70UI, &H80BB155CUI, &H5282CE3UI,
        &H95C11548UI, &HE4C66D22UI, &H48C1133FUI, &HC70F86DCUI,
        &H7F9C9EEUI, &H41041F0FUI, &H404779A4UI, &H5D886E17UI,
        &H325F51EBUI, &HD59BC0D1UI, &HF2BCC18FUI, &H41113564UI,
        &H257B7834UI, &H602A9C60UI, &HDFF8E8A3UI, &H1F636C1BUI,
        &HE12B4C2UI, &H2E1329EUI, &HAF664FD1UI, &HCAD18115UI,
        &H6B2395E0UI, &H333E92E1UI, &H3B240B62UI, &HEEBEB922UI,
        &H85B2A20EUI, &HE6BA0D99UI, &HDE720C8CUI, &H2DA2F728UI,
        &HD0127845UI, &H95B794FDUI, &H647D0862UI, &HE7CCF5F0UI,
        &H5449A36FUI, &H877D48FAUI, &HC39DFD27UI, &HF33E8D1EUI,
        &HA476341UI, &H992EFF74UI, &H3A6F6EABUI, &HF4F8FD37UI,
        &HA812DC60UI, &HA1EBDDF8UI, &H991BE14CUI, &HDB6E6B0DUI,
        &HC67B5510UI, &H6D672C37UI, &H2765D43BUI, &HDCD0E804UI,
        &HF1290DC7UI, &HCC00FFA3UI, &HB5390F92UI, &H690FED0BUI,
        &H667B9FFBUI, &HCEDB7D9CUI, &HA091CF0BUI, &HD9155EA3UI,
        &HBB132F88UI, &H515BAD24UI, &H7B9479BFUI, &H763BD6EBUI,
        &H37392EB3UI, &HCC115979UI, &H8026E297UI, &HF42E312DUI,
        &H6842ADA7UI, &HC66A2B3BUI, &H12754CCCUI, &H782EF11CUI,
        &H6A124237UI, &HB79251E7UI, &H6A1BBE6UI, &H4BFB6350UI,
        &H1A6B1018UI, &H11CAEDFAUI, &H3D25BDD8UI, &HE2E1C3C9UI,
        &H44421659UI, &HA121386UI, &HD90CEC6EUI, &HD5ABEA2AUI,
        &H64AF674EUI, &HDA86A85FUI, &HBEBFE988UI, &H64E4C3FEUI,
        &H9DBC8057UI, &HF0F7C086UI, &H60787BF8UI, &H6003604DUI,
        &HD1FD8346UI, &HF6381FB0UI, &H7745AE04UI, &HD736FCCCUI,
        &H83426B33UI, &HF01EAB71UI, &HB0804187UI, &H3C005E5FUI,
        &H77A057BEUI, &HBDE8AE24UI, &H55464299UI, &HBF582E61UI,
        &H4E58F48FUI, &HF2DDFDA2UI, &HF474EF38UI, &H8789BDC2UI,
        &H5366F9C3UI, &HC8B38E74UI, &HB475F255UI, &H46FCD9B9UI,
        &H7AEB2661UI, &H8B1DDF84UI, &H846A0E79UI, &H915F95E2UI,
        &H466E598EUI, &H20B45770UI, &H8CD55591UI, &HC902DE4CUI,
        &HB90BACE1UI, &HBB8205D0UI, &H11A86248UI, &H7574A99EUI,
        &HB77F19B6UI, &HE0A9DC09UI, &H662D09A1UI, &HC4324633UI,
        &HE85A1F02UI, &H9F0BE8CUI, &H4A99A025UI, &H1D6EFE10UI,
        &H1AB93D1DUI, &HBA5A4DFUI, &HA186F20FUI, &H2868F169UI,
        &HDCB7DA83UI, &H573906FEUI, &HA1E2CE9BUI, &H4FCD7F52UI,
        &H50115E01UI, &HA70683FAUI, &HA002B5C4UI, &HDE6D027UI,
        &H9AF88C27UI, &H773F8641UI, &HC3604C06UI, &H61A806B5UI,
        &HF0177A28UI, &HC0F586E0UI, &H6058AAUI, &H30DC7D62UI,
        &H11E69ED7UI, &H2338EA63UI, &H53C2DD94UI, &HC2C21634UI,
        &HBBCBEE56UI, &H90BCB6DEUI, &HEBFC7DA1UI, &HCE591D76UI,
        &H6F05E409UI, &H4B7C0188UI, &H39720A3DUI, &H7C927C24UI,
        &H86E3725FUI, &H724D9DB9UI, &H1AC15BB4UI, &HD39EB8FCUI,
        &HED545578UI, &H8FCA5B5UI, &HD83D7CD3UI, &H4DAD0FC4UI,
        &H1E50EF5EUI, &HB161E6F8UI, &HA28514D9UI, &H6C51133CUI,
        &H6FD5C7E7UI, &H56E14EC4UI, &H362ABFCEUI, &HDDC6C837UI,
        &HD79A3234UI, &H92638212UI, &H670EFA8EUI, &H406000E0UI,
        &H3A39CE37UI, &HD3FAF5CFUI, &HABC27737UI, &H5AC52D1BUI,
        &H5CB0679EUI, &H4FA33742UI, &HD3822740UI, &H99BC9BBEUI,
        &HD5118E9DUI, &HBF0F7315UI, &HD62D1C7EUI, &HC700C47BUI,
        &HB78C1B6BUI, &H21A19045UI, &HB26EB1BEUI, &H6A366EB4UI,
        &H5748AB2FUI, &HBC946E79UI, &HC6A376D2UI, &H6549C2C8UI,
        &H530FF8EEUI, &H468DDE7DUI, &HD5730A1DUI, &H4CD04DC6UI,
        &H2939BBDBUI, &HA9BA4650UI, &HAC9526E8UI, &HBE5EE304UI,
        &HA1FAD5F0UI, &H6A2D519AUI, &H63EF8CE2UI, &H9A86EE22UI,
        &HC089C2B8UI, &H43242EF6UI, &HA51E03AAUI, &H9CF2D0A4UI,
        &H83C061BAUI, &H9BE96A4DUI, &H8FE51550UI, &HBA645BD6UI,
        &H2826A2F9UI, &HA73A3AE1UI, &H4BA99586UI, &HEF5562E9UI,
        &HC72FEFD3UI, &HF752F7DAUI, &H3F046F69UI, &H77FA0A59UI,
        &H80E4A915UI, &H87B08601UI, &H9B09E6ADUI, &H3B3EE593UI,
        &HE990FD5AUI, &H9E34D797UI, &H2CF0B7D9UI, &H22B8B51UI,
        &H96D5AC3AUI, &H17DA67DUI, &HD1CF3ED6UI, &H7C7D2D28UI,
        &H1F9F25CFUI, &HADF2B89BUI, &H5AD6B472UI, &H5A88F54CUI,
        &HE029AC71UI, &HE019A5E6UI, &H47B0ACFDUI, &HED93FA9BUI,
        &HE8D3C48DUI, &H283B57CCUI, &HF8D56629UI, &H79132E28UI,
        &H785F0191UI, &HED756055UI, &HF7960E44UI, &HE3D35E8CUI,
        &H15056DD4UI, &H88F46DBAUI, &H3A16125UI, &H564F0BDUI,
        &HC3EB9E15UI, &H3C9057A2UI, &H97271AECUI, &HA93A072AUI,
        &H1B3F6D9BUI, &H1E6321F5UI, &HF59C66FBUI, &H26DCF319UI,
        &H7533D928UI, &HB155FDF5UI, &H3563482UI, &H8ABA3CBBUI,
        &H28517711UI, &HC20AD9F8UI, &HABCC5167UI, &HCCAD925FUI,
        &H4DE81751UI, &H3830DC8EUI, &H379D5862UI, &H9320F991UI,
        &HEA7A90C2UI, &HFB3E7BCEUI, &H5121CE64UI, &H774FBE32UI,
        &HA8B6E37EUI, &HC3293D46UI, &H48DE5369UI, &H6413E680UI,
        &HA2AE0810UI, &HDD6DB224UI, &H69852DFDUI, &H9072166UI,
        &HB39A460AUI, &H6445C0DDUI, &H586CDECFUI, &H1C20C8AEUI,
        &H5BBEF7DDUI, &H1B588D40UI, &HCCD2017FUI, &H6BB4E3BBUI,
        &HDDA26A7EUI, &H3A59FF45UI, &H3E350A44UI, &HBCB4CDD5UI,
        &H72EACEA8UI, &HFA6484BBUI, &H8D6612AEUI, &HBF3C6F47UI,
        &HD29BE463UI, &H542F5D9EUI, &HAEC2771BUI, &HF64E6370UI,
        &H740E0D8DUI, &HE75B1357UI, &HF8721671UI, &HAF537D5DUI,
        &H4040CB08UI, &H4EB4E2CCUI, &H34D2466AUI, &H115AF84UI,
        &HE1B00428UI, &H95983A1DUI, &H6B89FB4UI, &HCE6EA048UI,
        &H6F3F3B82UI, &H3520AB82UI, &H11A1D4BUI, &H277227F8UI,
        &H611560B1UI, &HE7933FDCUI, &HBB3A792BUI, &H344525BDUI,
        &HA08839E1UI, &H51CE794BUI, &H2F32C9B7UI, &HA01FBAC9UI,
        &HE01CC87EUI, &HBCC7D1F6UI, &HCF0111C3UI, &HA1E8AAC7UI,
        &H1A908749UI, &HD44FBD9AUI, &HD0DADECBUI, &HD50ADA38UI,
        &H339C32AUI, &HC6913667UI, &H8DF9317CUI, &HE0B12B4FUI,
        &HF79E59B7UI, &H43F5BB3AUI, &HF2D519FFUI, &H27D9459CUI,
        &HBF97222CUI, &H15E6FC2AUI, &HF91FC71UI, &H9B941525UI,
        &HFAE59361UI, &HCEB69CEBUI, &HC2A86459UI, &H12BAA8D1UI,
        &HB6C1075EUI, &HE3056A0CUI, &H10D25065UI, &HCB03A442UI,
        &HE0EC6E0EUI, &H1698DB3BUI, &H4C98A0BEUI, &H3278E964UI,
        &H9F1F9532UI, &HE0D392DFUI, &HD3A0342BUI, &H8971F21EUI,
        &H1B0A7441UI, &H4BA3348CUI, &HC5BE7120UI, &HC37632D8UI,
        &HDF359F8DUI, &H9B992F2EUI, &HE60B6F47UI, &HFE3F11DUI,
        &HE54CDA54UI, &H1EDAD891UI, &HCE6279CFUI, &HCD3E7E6FUI,
        &H1618B166UI, &HFD2C1D05UI, &H848FD2C5UI, &HF6FB2299UI,
        &HF523F357UI, &HA6327623UI, &H93A83531UI, &H56CCCD02UI,
        &HACF08162UI, &H5A75EBB5UI, &H6E163697UI, &H88D273CCUI,
        &HDE966292UI, &H81B949D0UI, &H4C50901BUI, &H71C65614UI,
        &HE6C6C7BDUI, &H327A140AUI, &H45E1D006UI, &HC3F27B9AUI,
        &HC9AA53FDUI, &H62A80F00UI, &HBB25BFE2UI, &H35BDD2F6UI,
        &H71126905UI, &HB2040222UI, &HB6CBCF7CUI, &HCD769C2BUI,
        &H53113EC0UI, &H1640E3D3UI, &H38ABBD60UI, &H2547ADF0UI,
        &HBA38209CUI, &HF746CE76UI, &H77AFA1C5UI, &H20756060UI,
        &H85CBFE4EUI, &H8AE88DD8UI, &H7AAAF9B0UI, &H4CF9AA7EUI,
        &H1948C25CUI, &H2FB8A8CUI, &H1C36AE4UI, &HD6EBE1F9UI,
        &H90D4F869UI, &HA65CDEA0UI, &H3F09252DUI, &HC208E69FUI,
        &HB74E6132UI, &HCE77E25BUI, &H578FDFE3UI, &H3AC372E6UI
    }

    Private ReadOnly bf_crypt_ciphertext As UInteger() = {
        &H4F727068UI, &H65616E42UI, &H65686F6CUI,
        &H64657253UI, &H63727944UI, &H6F756274UI
    }

    Private ReadOnly base64_code As Char() = {
        ".", "/", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
        "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h",
        "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5",
        "6", "7", "8", "9"
    }

    Private ReadOnly index64() As Integer = {
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
        -1, -1, -1, -1, -1, -1, 0, 1, 54, 55,
        56, 57, 58, 59, 60, 61, 62, 63, -1, -1,
        -1, -1, -1, -1, -1, 2, 3, 4, 5, 6,
        7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
        17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27,
        -1, -1, -1, -1, -1, -1, 28, 29, 30,
        31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
        41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
        51, 52, 53, -1, -1, -1, -1, -1
    }

    Private p As UInteger()
    Private s As UInteger()

    Public Function GenerateSalt(workFactor As Integer) As String
        If workFactor < 4 Or workFactor > 31 Then
            Throw New ArgumentOutOfRangeException("workFactor", workFactor, "The work factor must be between 4 and 31 (inclusive)")
        End If

        Dim rnd(BCRYPT_SALT_LEN - 1) As Byte
        Dim rng As New RNGCryptoServiceProvider
        rng.GetBytes(rnd)

        Dim rs As New StringBuilder()
        rs.AppendFormat("$2a${0:00}$", workFactor)
        rs.Append(EncodeBase64(rnd, rnd.Length))
        Return rs.ToString()
    End Function

    Public Function GenerateSalt()
        Return GenerateSalt(GENSALT_DEFAULT_LOG2_ROUNDS)
    End Function

    Public Function Verify(txt As String, hash As String) As Boolean
        Dim h As String = HashPassword(txt, hash)
        'Dim msg = txt + vbCrLf + hash + vbCrLf + h
        'Throw New CryptographicException(msg)
        Return (hash = h)
    End Function

    Public Function HashPassword(str As String, salt As String) As String
        If IsNothing(str) Then
            Throw New ArgumentNullException("str")
        End If

        If IsNothing(salt) Then
            Throw New ArgumentException("Invalid salt", "salt")
        End If

        Dim startOffset As Integer
        Dim minor As Char = Chr(0)
        If salt(0) <> "$" Or salt(1) <> "2" Then
            Throw New Exception("invalid salt version")
        End If
        If salt(2) = "$" Then
            startOffset = 3
        Else
            minor = salt(2)
            If minor <> "a" Or salt(3) <> "$" Then
                Throw New Exception("Invalid salt revision")
            End If
            startOffset = 4
        End If
        If salt(startOffset + 2) > "$" Then
            Throw New Exception("Missing salt rounds")
        End If

        Dim logRounds As Integer = Convert.ToInt32(salt.Substring(startOffset, 2))
        Dim extractedSalt = salt.Substring(startOffset + 3, 22)

        Dim inputBytes As Byte() = Encoding.UTF8.GetBytes((str & If(minor >= "a", Chr(0), "")))
        Dim saltBytes = DecodeBase64(extractedSalt, extractedSalt.Length)

        'saltBytes = {229, 242, 102, 221, 137, 174, 25, 174, 58, 53, 24, 109, 92, 100, 34, 251}

        Dim hashed As Byte() = CryptRaw(inputBytes, saltBytes, logRounds)
        Dim result As New StringBuilder()
        result.Append("$2")
        If (minor >= "a") Then
            result.Append(minor)
        End If
        result.AppendFormat("${0:00}$", logRounds)
        result.Append(EncodeBase64(saltBytes, saltBytes.Length))
        result.Append(EncodeBase64(hashed, (bf_crypt_ciphertext.Length * 4) - 1))

        Return result.ToString()
    End Function

    Public Function HashPassword(str As String, workFactor As Integer) As String
        Return HashPassword(str, GenerateSalt(workFactor))
    End Function

    Public Function HashPassword(str As String) As String
        Return HashPassword(str, GenerateSalt())
    End Function

    Public Function HashString(src As String, workFactor As Integer) As String
        Return HashPassword(src, GenerateSalt(workFactor))
    End Function

    Public Function HashString(src As String) As String
        Return HashPassword(src)
    End Function

    Private Function EncodeBase64(d As Byte(), length As Integer) As String
        If length <= 0 Or length > d.Length Then
            Throw New ArgumentException("Invalid length", "length")
        End If

        'Dim tmp As String

        'tmp = Convert.ToBase64String(d)
        'Return Left(tmp, length)

        Dim off As Integer = 0
        Dim c1 As Integer, c2 As Integer
        Dim rs As New StringBuilder()

        While off < length
            c1 = d(off) And &HFF
            off += 1
            rs.Append(base64_code((c1 >> 2) And &H3F))
            c1 = (c1 And &H3) << 4
            If off >= length Then
                rs.Append(base64_code(c1 And &H3F))
                Exit While
            End If
            c2 = d(off) And &HFF
            off += 1
            c1 = c1 Or ((c2 >> 4) And &HF)
            rs.Append(base64_code(c1 And &H3F))
            c1 = (c2 And &HF) << 2
            If off >= length Then
                rs.Append(base64_code(c1 And &H3F))
                Exit While
            End If
            c2 = d(off) And &HFF
            off += 1
            c1 = c1 Or ((c2 >> 6) And &H3)
            'Debug.WriteLine(Encoding.UTF8.GetString(d) + " off " + off.ToString() + ": c1=" + c1.ToString + " c2=" + c2.ToString)
            rs.Append(base64_code(c1 And &H3F))
            rs.Append(base64_code(c2 And &H3F))
        End While

        Return rs.ToString()
    End Function

    Private Function DecodeBase64(s As String, length As Integer) As Byte()

        Dim position As Integer = 0
        Dim srcLen As Integer = s.Length
        Dim outLen As Integer = 0
        Dim c1 As Integer, c2 As Integer, c3 As Integer, c4 As Integer

        If length <= 0 Then
            Throw New ArgumentException("Invalid length value", "length")
        End If

        'Return Convert.FromBase64String(s)

        Dim rs As New StringBuilder()
        While position < srcLen - 1 And outLen < length
            c1 = Char64(Asc(s(position)))
            position += 1
            c2 = Char64(Asc(s(position)))
            position += 1
            If c1 = -1 Or c2 = -1 Then
                Exit While
            End If

            'Debug.WriteLine("out " + outLen.ToString() + ": " + ((c1 << 2) Or ((c2 And &H30) >> 4)).ToString())
            rs.Append(Chr((c1 << 2) Or ((c2 And &H30) >> 4)))
            outLen += 1
            If outLen >= length Or position >= srcLen Then
                Exit While
            End If

            c3 = Char64(Asc(s(position)))
            position += 1
            If c3 = -1 Then
                Exit While
            End If

            'Debug.WriteLine("out " + outLen.ToString() + ": " + (((c2 And &HF) << 4) Or ((c3 And &H3C) >> 2)).ToString())
            rs.Append(Chr(((c2 And &HF) << 4) Or ((c3 And &H3C) >> 2)))
            outLen += 1
            If outLen >= length Or position >= srcLen Then
                Exit While
            End If

            c4 = Char64(Asc(s(position)))
            position += 1
            'Debug.WriteLine("out " + outLen.ToString() + ": " + (((c3 And &H3) << 6) Or c4).ToString())
            rs.Append(Chr(((c3 And &H3) << 6) Or c4))

            outLen += 1
        End While

        Dim ret(outLen - 1) As Byte
        For position = 0 To outLen - 1
            ret(position) = Asc(rs(position))
        Next

        Return ret
    End Function

    Private Function Char64(c As Byte) As Integer

        If (c < 0 Or c > index64.Length) Then
            Return -1
        End If
        Return index64(c)
    End Function

    Private Sub Enchiper(blockArray As UInteger(), offset As Integer)
        Dim round As UInteger, n As UInteger
        Dim block As UInteger = blockArray(offset), r As UInteger = blockArray(offset + 1)

        block = block Xor p(0)
        round = 0
        While round <= BLOWFISH_NUM_ROUNDS - 2
            'For round = 0 To BLOWFISH_NUM_ROUNDS - 2
            n = s((block >> 24) And &HFF)
            n += s(&H100 Or ((block >> 16) And &HFF))
            n = n Xor s(&H200 Or ((block >> 8) And &HFF))
            n += s(&H300 Or ((block And &HFF)))
            round += 1
            r = r Xor (n Xor p(round))
            'Debug.Write("round l " + round.ToString() + ": " + n.ToString() + vbCrLf)

            n = s((r >> 24) And &HFF)
            n += s(&H100 Or ((r >> 16) And &HFF))
            n = n Xor s(&H200 Or ((r >> 8) And &HFF))
            n += s(&H300 Or (r And &HFF))
            round += 1
            block = block Xor (n Xor p(round))
            'Debug.Write("round r " + round.ToString() + ": " + n.ToString() + vbCrLf)
            'Next
        End While
        blockArray(offset) = r Xor p(BLOWFISH_NUM_ROUNDS + 1)
        blockArray(offset + 1) = block
    End Sub

    Private Function StreamToWord(data As Byte(), ByRef offset As Integer) As UInteger
        Dim i As Integer, word As UInteger = 0

        For i = 0 To 3
            word = (word << 8) Or Convert.ToUInt32(data(offset) And &HFF)
            offset = (offset + 1) Mod data.Length
        Next

        Return word
    End Function

    Private Sub InitializeKey()
        ReDim p(pbox.Length - 1)
        ReDim s(sbox.Length - 1)
        Array.Copy(pbox, p, pbox.Length)
        Array.Copy(sbox, s, sbox.Length)
    End Sub

    Private Sub Key(keyBytes As Byte())
        Dim i As Integer, koffp As Integer = 0
        Dim lr() As UInteger = {0, 0}
        Dim plen As Integer = p.Length, slen As Integer = s.Length

        For i = 0 To plen - 1
            p(i) = p(i) Xor StreamToWord(keyBytes, koffp)
        Next

        For i = 0 To plen - 1 Step 2
            Enchiper(lr, 0)
            p(i) = lr(0)
            p(i + 1) = lr(1)
        Next

        For i = 0 To slen - 1 Step 2
            Enchiper(lr, 0)
            s(i) = lr(0)
            s(i + 1) = lr(1)
        Next
    End Sub

    Private Sub EKSKey(saltBytes As Byte(), inputBytes As Byte())
        Dim i As Integer
        Dim passwordOffset As Integer = 0, saltOffset As Integer = 0
        Dim lr() As UInteger = {0, 0}
        Dim plen As Integer = p.Length, slen As Integer = s.Length

        For i = 0 To plen - 1
            p(i) = p(i) Xor StreamToWord(inputBytes, passwordOffset)
        Next

        For i = 0 To plen - 1 Step 2
            lr(0) = lr(0) Xor StreamToWord(saltBytes, saltOffset)
            lr(1) = lr(1) Xor StreamToWord(saltBytes, saltOffset)
            Enchiper(lr, 0)
            p(i) = lr(0)
            p(i + 1) = lr(1)
        Next

        For i = 0 To slen - 1 Step 2
            lr(0) = lr(0) Xor StreamToWord(saltBytes, saltOffset)
            lr(1) = lr(1) Xor StreamToWord(saltBytes, saltOffset)
            Enchiper(lr, 0)
            s(i) = lr(0)
            s(i + 1) = lr(1)
        Next
    End Sub

    Private Function CryptRaw(inputBytes As Byte(), saltBytes As Byte(), logRounds As Integer) As Byte()
        Dim i As Integer, j As Integer
        Dim cdata(bf_crypt_ciphertext.Length - 1) As UInteger
        Array.Copy(bf_crypt_ciphertext, cdata, bf_crypt_ciphertext.Length)
        Dim clen As Integer = cdata.Length

        If logRounds < 4 Or logRounds > 31 Then
            Throw New ArgumentException("Bad number of rounds", "logRounds")
        End If

        If saltBytes.Length <> BCRYPT_SALT_LEN Then
            Throw New ArgumentException("Bad salt length", "saltBytes")
        End If

        Dim rounds As UInteger = 1& << logRounds
        'Debug.Assert(rounds > 0, "Rounds must be > 0")

        InitializeKey()
        EKSKey(saltBytes, inputBytes)

        For i = 0 To rounds - 1
            Key(inputBytes)
            Key(saltBytes)
        Next

        For i = 0 To 63
            For j = 0 To (clen >> 1) - 1
                Enchiper(cdata, j << 1)
            Next
        Next
        Dim ret(clen * 4 - 1) As Byte
        j = 0
        For i = 0 To clen - 1
            ret(j) = (cdata(i) >> 24) And &HFF
            j += 1
            ret(j) = (cdata(i) >> 16) And &HFF
            j += 1
            ret(j) = (cdata(i) >> 8) And &HFF
            j += 1
            ret(j) = cdata(i) And &HFF
            j += 1
        Next

        Return ret
    End Function

    Public Function encodeBlowfish(text As String, key As String) As String
        Dim algo = New Blowfish()
        Dim bSize = algo.BlockSize / 8
        Dim iv(bSize - 1) As Byte
        Dim bKey() As Byte
        Dim plain() As Byte
        Dim cipher() As Byte
        Dim pkcs() As Byte
        Dim pkcsStart As Integer
        Dim rng As New RNGCryptoServiceProvider

        Try
            bKey = algo.GetBytes(key)
        Catch ex As Exception
            key = BitConverter.ToString(Encoding.ASCII.GetBytes(key)).Replace("-", "")
            bKey = algo.GetBytes(key)
        End Try

        algo.Mode = CipherMode.CBC
        algo.Padding = PaddingMode.PKCS7

        rng.GetBytes(iv)

        plain = Encoding.BigEndianUnicode.GetBytes(text)
        pkcsStart = plain.Length
        ReDim pkcs(bSize - (plain.Length Mod bSize) - 1)
        ReDim Preserve plain(plain.Length + pkcs.Length - 1)
        ReDim cipher(plain.Length - 1)

        For i = pkcsStart To plain.Length - 1
            plain(i) = pkcs.Length
        Next

        Dim bFish = algo.CreateEncryptor(bKey, iv)

        bFish.TransformBlock(plain, 0, plain.Length, cipher, 0)

        Return algo.GetString(iv) + algo.GetString(cipher)
    End Function

    Public Function decodeBlowfish(ciphertext As String, key As String)
        Dim algo = New Blowfish()
        Dim bSize = algo.BlockSize / 8
        Dim iv() As Byte = algo.GetBytes(Left(ciphertext, 16))
        Dim bKey() As Byte
        Dim cipher() As Byte = algo.GetBytes(Mid(ciphertext, 17))

        Try
            bKey = algo.GetBytes(key)
        Catch ex As Exception
            key = BitConverter.ToString(Encoding.ASCII.GetBytes(key)).Replace("-", "")
            bKey = algo.GetBytes(key)
        End Try

        If cipher.Length Mod bSize > 0 Then
            Dim msg As String = "Wrong cipher size (" & cipher.Length & ")" &
                vbCrLf & algo.GetString(iv) & vbCrLf & algo.GetString(cipher) & vbCrLf &
                ciphertext
            Throw New CryptographicException(msg)
        End If
        Dim decipher(cipher.Length - 1) As Byte

        algo.Mode = CipherMode.CBC
        algo.Padding = PaddingMode.PKCS7

        Dim bFish = algo.CreateDecryptor(bKey, iv)
        bFish.TransformBlock(cipher, 0, cipher.Length, decipher, 0)

        Return Encoding.BigEndianUnicode.GetString(decipher)
    End Function

    Public Class Blowfish : Inherits SymmetricAlgorithm

        Public Function GetBytes(s As String) As Byte()
            Dim result((s.Length >> 1) - 1) As Byte

            For i = 0 To result.Length - 1
                result(i) = Convert.ToByte(s.Substring(2 * i, 2), 16)
            Next

            Return result
        End Function

        Public Function GetString(b() As Byte) As String
            Dim sb As New StringBuilder

            For i = 0 To b.Length - 1
                sb.Append(String.Format("{0:X2}", b(i)))
            Next
            Return sb.ToString()
            'Return Replace(BitConverter.ToString(b), "-", "")
        End Function

        'Public Sub test()
        '    Dim stream As New MemoryStream()
        '    Dim algo As New Blowfish()
        '    Dim bFish As BlowfishTransform
        '    Dim key() As Byte

        '    algo.Mode = CipherMode.CBC
        '    algo.Key = {&HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF, &HFF}
        '    algo.Padding = PaddingMode.PKCS7

        '    Dim plain() As Byte
        '    'Dim correctResult() As Byte
        '    Dim cipher((algo.BlockSize >> 3) - 1) As Byte
        '    Dim decipher((algo.BlockSize >> 3) - 1) As Byte

        '    'key = GetBytes("0000000000000000")
        '    'plain = GetBytes("0000000000000000")
        '    'correctResult = GetBytes("4EF997456198DD78")

        '    'bFish = algo.CreateEncryptor()
        '    'bFish = New BlowfishTransform(algo, True, key, Nothing)
        '    'bFish.TransformBlock(plain, 0, plain.Length, cipher, 0)

        '    'bFish = New BlowfishTransform(algo, False, key, Nothing)
        '    'bFish.TransformBlock(cipher, 0, cipher.Length, decipher, 0)

        '    'MsgBox(GetString(cipher) + ", " + GetString(correctResult) + ", " + GetString(plain) + ", " + GetString(decipher))
        '    'If GetString(cipher) <> GetString(correctResult) Then MsgBox("Error in encryption")
        '    'If GetString(plain) <> GetString(decipher) Then MsgBox("Error in decryption")

        '    'key = GetBytes("FFFFFFFFFFFFFFFF")
        '    'plain = GetBytes("FFFFFFFFFFFFFFFF")
        '    'correctResult = GetBytes("51866FD5B85ECB8A")

        '    'bFish = New BlowfishTransform(algo, True, key, Nothing)
        '    'bFish.TransformBlock(plain, 0, plain.Length, cipher, 0)

        '    'bFish = New BlowfishTransform(algo, False, key, Nothing)
        '    'bFish.TransformBlock(cipher, 0, cipher.Length, decipher, 0)

        '    'MsgBox(GetString(cipher) + ", " + GetString(correctResult) + ", " + GetString(plain) + ", " + GetString(decipher))
        '    'If GetString(cipher) <> GetString(correctResult) Then MsgBox("Error in encryption")
        '    'If GetString(plain) <> GetString(decipher) Then MsgBox("Error in decryption")

        '    'key = GetBytes("3000000000000000")
        '    'plain = GetBytes("1000000000000001")
        '    'correctResult = GetBytes("7D856F9A613063F2")

        '    'bFish = New BlowfishTransform(algo, True, key, Nothing)
        '    'bFish.TransformBlock(plain, 0, plain.Length, cipher, 0)

        '    'bFish = New BlowfishTransform(algo, False, key, Nothing)
        '    'bFish.TransformBlock(cipher, 0, cipher.Length, decipher, 0)

        '    'MsgBox(GetString(cipher) + ", " + GetString(correctResult) + ", " + GetString(plain) + ", " + GetString(decipher))
        '    'If GetString(cipher) <> GetString(correctResult) Then MsgBox("Error in encryption")
        '    'If GetString(plain) <> GetString(decipher) Then MsgBox("Error in decryption")

        '    'key = GetBytes("1111111111111111")
        '    'plain = GetBytes("1111111111111111")
        '    'correctResult = GetBytes("2466DD878B963C9D")

        '    'bFish = New BlowfishTransform(algo, True, key, Nothing)
        '    'bFish.TransformBlock(plain, 0, plain.Length, cipher, 0)

        '    'bFish = New BlowfishTransform(algo, False, key, Nothing)
        '    'bFish.TransformBlock(cipher, 0, cipher.Length, decipher, 0)

        '    'MsgBox(GetString(cipher) + ", " + GetString(correctResult) + ", " + GetString(plain) + ", " + GetString(decipher))
        '    'If GetString(cipher) <> GetString(correctResult) Then MsgBox("Error in encryption")
        '    'If GetString(plain) <> GetString(decipher) Then MsgBox("Error in decryption")

        '    'key = GetBytes("0113B970FD34F2CE")
        '    'plain = GetBytes("059B5E0851CF143A")
        '    'correctResult = GetBytes("48F4D0884C379918")

        '    'bFish = New BlowfishTransform(algo, True, key, Nothing)
        '    'bFish.TransformBlock(plain, 0, plain.Length, cipher, 0)

        '    'bFish = New BlowfishTransform(algo, False, key, Nothing)
        '    'bFish.TransformBlock(cipher, 0, cipher.Length, decipher, 0)

        '    'MsgBox(GetString(cipher) + ", " + GetString(correctResult) + ", " + GetString(plain) + ", " + GetString(decipher))
        '    'If GetString(cipher) <> GetString(correctResult) Then MsgBox("Error in encryption")
        '    'If GetString(plain) <> GetString(decipher) Then MsgBox("Error in decryption")

        '    'key = GetBytes("1C587F1C13924FEF")
        '    'plain = GetBytes("305532286D6F295A")
        '    'correctResult = GetBytes("55CB3774D13EF201")

        '    'bFish = New BlowfishTransform(algo, True, key, Nothing)
        '    'bFish.TransformBlock(plain, 0, plain.Length, cipher, 0)

        '    'bFish = New BlowfishTransform(algo, False, key, Nothing)
        '    'bFish.TransformBlock(cipher, 0, cipher.Length, decipher, 0)

        '    'MsgBox(GetString(cipher) + ", " + GetString(correctResult) + ", " + GetString(plain) + ", " + GetString(decipher))
        '    'If GetString(cipher) <> GetString(correctResult) Then MsgBox("Error in encryption")
        '    'If GetString(plain) <> GetString(decipher) Then MsgBox("Error in decryption")

        '    key = GetBytes("b0817650c0241afb14a17f4ad739eb9ef9e166de")
        '    plain = Encoding.BigEndianUnicode.GetBytes("rasengan")
        '    'cipher = GetBytes("c534ed99f3088eabe2ea5b8ac3f09690")
        '    Dim cIV() As Byte = GetBytes("557f6b7ea6230d08")

        '    Dim bSize As Integer = algo.BlockSize / 8

        '    bFish = algo.CreateEncryptor(key, cIV)
        '    Dim pkcs(bSize - (plain.Length Mod bSize) - 1)
        '    Dim startPkcs As Integer = plain.Length
        '    ReDim Preserve plain(plain.Length + pkcs.Length - 1)

        '    For i = startPkcs To plain.Length - 1
        '        plain(i) = pkcs.Length
        '    Next

        '    ReDim cipher(plain.Length - 1)

        '    bFish.TransformBlock(plain, 0, plain.Length, cipher, 0)

        '    ReDim decipher(cipher.Length - 1)

        '    bFish = algo.CreateDecryptor(key, cIV)
        '    bFish.TransformBlock(cipher, 0, cipher.Length, decipher, 0)

        '    MsgBox(GetString(cipher))
        '    MsgBox(GetString(decipher))
        '    MsgBox(Encoding.BigEndianUnicode.GetString(decipher))
        'End Sub

        Dim rng As New RNGCryptoServiceProvider

        Public Overrides Sub GenerateIV()
            Dim iv((BlockSizeValue >> 3) - 1) As Byte
            rng.GetBytes(iv)

            IVValue = iv
        End Sub

        Public Overrides Sub GenerateKey()
            Dim key((KeySize >> 3) - 1) As Byte
            rng.GetBytes(key)

            KeyValue = key
        End Sub

        Public Overrides Function CreateDecryptor(rgbKey() As Byte, rgbIV() As Byte) As ICryptoTransform
            Return New BlowfishTransform(Me, False, rgbKey, rgbIV)
        End Function

        Public Overrides Function CreateEncryptor(rgbKey() As Byte, rgbIV() As Byte) As ICryptoTransform
            Return New BlowfishTransform(Me, True, rgbKey, rgbIV)
        End Function

        Public Sub New()
            BlockSizeValue = 64
            KeySizeValue = 128
            ModeValue = CipherMode.CBC

            ReDim Preserve LegalBlockSizesValue(0)
            LegalBlockSizesValue(0) = New KeySizes(64, 64, 64)

            ReDim Preserve LegalKeySizesValue(0)
            LegalKeySizesValue(0) = New KeySizes(32, 448, 8)
        End Sub

        Public MustInherit Class SymmetricTransform : Implements ICryptoTransform

            Protected algo As SymmetricAlgorithm
            Protected encrypt As Boolean
            Private blockSizeByte As Integer
            Private temp() As Byte
            Private temp2() As Byte
            Private workBuff() As Byte
            Private workout() As Byte
            Private feedBackByte As Integer
            Private feedBackIter As Integer
            Private m_disposed As Boolean = False
            Private lastBlock As Boolean

            Public Sub New(symmAlgo As SymmetricAlgorithm, encryption As Boolean, rgbIV() As Byte)

                algo = symmAlgo
                encrypt = encryption
                blockSizeByte = (algo.BlockSize >> 3)

                If IsNothing(rgbIV) Then
                    ReDim rgbIV(blockSizeByte - 1)
                    Me.Random(rgbIV, 0, rgbIV.Length)
                Else
                    rgbIV = rgbIV.Clone()
                End If

                If (rgbIV.Length < blockSizeByte) Then
                    Dim msg As String = "IV is too small (" + rgbIV.Length.ToString() + " bytes), it should be " + blockSizeByte.ToString() + " bytes long."

                    Throw New CryptographicException(msg)
                End If

                ReDim temp(blockSizeByte - 1)
                Buffer.BlockCopy(rgbIV, 0, temp, 0, Math.Min(blockSizeByte, rgbIV.Length))
                ReDim temp2(blockSizeByte - 1)
                feedBackByte = (algo.FeedbackSize >> 3)
                If feedBackByte = 0 Then
                    feedBackIter = blockSizeByte / feedBackByte
                End If

                ReDim workBuff(blockSizeByte - 1)
                ReDim workout(blockSizeByte - 1)
            End Sub

            Public Sub Dispose() Implements IDisposable.Dispose
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub

            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not m_disposed Then
                    If disposing Then
                        Array.Clear(temp, 0, blockSizeByte)
                        temp = Nothing
                        Array.Clear(temp2, 0, blockSizeByte)
                        temp2 = Nothing
                    End If
                    m_disposed = True
                End If
            End Sub

            Public ReadOnly Property CanTransformMultipleBlocks As Boolean Implements ICryptoTransform.CanTransformMultipleBlocks
                Get
                    Return True
                End Get
            End Property

            Public ReadOnly Property CanReuseTransform As Boolean Implements ICryptoTransform.CanReuseTransform
                Get
                    Return True
                End Get
            End Property

            Public ReadOnly Property InputBlockSize As Integer Implements ICryptoTransform.InputBlockSize
                Get
                    Return blockSizeByte
                End Get
            End Property

            Public ReadOnly Property OutputBlockSize As Integer Implements ICryptoTransform.OutputBlockSize
                Get
                    Return blockSizeByte
                End Get
            End Property

            Protected Overridable Sub Transform(input() As Byte, output() As Byte)
                Select Case algo.Mode
                    Case CipherMode.ECB
                        ECB(input, output)
                    Case CipherMode.CBC
                        CBC(input, output)
                    Case CipherMode.CFB
                        CFB(input, output)
                    Case CipherMode.OFB
                        OFB(input, output)
                    Case CipherMode.CTS
                        CTS(input, output)
                    Case Else
                        Throw New NotImplementedException("Unkown CipherMode" + algo.Mode.ToString())
                End Select
            End Sub

            Protected MustOverride Sub ECB(input() As Byte, output() As Byte)

            Protected Overridable Sub CBC(input() As Byte, output() As Byte)
                If encrypt Then
                    For i = 0 To blockSizeByte - 1
                        temp(i) = temp(i) Xor input(i)
                    Next
                    ECB(temp, output)
                    Buffer.BlockCopy(output, 0, temp, 0, blockSizeByte)
                Else
                    Buffer.BlockCopy(input, 0, temp2, 0, blockSizeByte)
                    ECB(input, output)
                    For i = 0 To blockSizeByte - 1
                        output(i) = output(i) Xor temp(i)
                    Next
                    Buffer.BlockCopy(temp2, 0, temp, 0, blockSizeByte)
                End If
            End Sub

            Protected Overridable Sub CFB(input() As Byte, output() As Byte)

                If encrypt Then
                    For x = 0 To feedBackIter - 1
                        ECB(temp, temp2)

                        For i = 0 To feedBackByte - 1
                            output(i + x) = temp2(i) Xor input(i + x)
                        Next
                        Buffer.BlockCopy(temp, feedBackByte, temp, 0, blockSizeByte - feedBackByte)
                        Buffer.BlockCopy(output, x, temp, blockSizeByte - feedBackByte, feedBackByte)
                    Next
                Else
                    For x = 0 To feedBackIter - 1
                        encrypt = True
                        ECB(temp, temp2)
                        encrypt = False

                        Buffer.BlockCopy(temp, feedBackByte, temp, 0, blockSizeByte - feedBackByte)
                        Buffer.BlockCopy(input, x, temp, blockSizeByte - feedBackByte, feedBackByte)
                        For i = 0 To feedBackByte - 1
                            output(i + x) = temp2(i) Xor input(i + x)
                        Next
                    Next
                End If
            End Sub

            Protected Overridable Sub OFB(input() As Byte, output() As Byte)
                Throw New CryptographicException("OFB isn't supported by the framework")
            End Sub

            Protected Overridable Sub CTS(input() As Byte, output() As Byte)
                Throw New CryptographicException("CTS isn't supported by the framework")
            End Sub

            Private Sub CheckInput(inputBuffer() As Byte, inputOffset As Integer, inputCount As Integer)
                If IsNothing(inputBuffer) Then Throw New ArgumentNullException("inputBuffer")
                If inputOffset < 0 Then Throw New ArgumentOutOfRangeException("inputOffset", "< 0")
                If inputCount < 0 Then Throw New ArgumentOutOfRangeException("inputCount", "< 0")
                If inputOffset > inputBuffer.Length - inputCount Then Throw New ArgumentException("inputBuffer Overflow")
            End Sub

            Public Overridable Function TransformBlock(inputBuffer() As Byte, inputOffset As Integer, inputCount As Integer, outputBuffer() As Byte, outputOffset As Integer) As Integer Implements ICryptoTransform.TransformBlock

                If m_disposed Then Throw New ObjectDisposedException("Object is disposed")
                CheckInput(inputBuffer, inputOffset, inputCount)

                If IsNothing(outputBuffer) Then Throw New ArgumentNullException("outputBuffer")
                If outputOffset < 0 Then Throw New ArgumentOutOfRangeException("outputOffset", "< 0")

                Dim len As Integer = outputBuffer.Length - inputCount - outputOffset
                If Not encrypt And (0 > len) And ((algo.Padding = PaddingMode.None) Or (algo.Padding = PaddingMode.Zeros)) Then
                    Throw New CryptographicException("outputBuffer Overflow")
                ElseIf (KeepLastBlock) Then
                    If (0 > len + blockSizeByte) Then Throw New CryptographicException("outputBuffer Overflow")
                Else
                    If (0 > len) Then
                        If (inputBuffer.Length - inputOffset - outputBuffer.Length) = blockSizeByte Then
                            inputCount = outputBuffer.Length - outputOffset
                        Else
                            Throw New CryptographicException("outputBuffer Overflow")
                        End If
                    End If
                End If
                Return InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset)
            End Function

            Private ReadOnly Property KeepLastBlock As Boolean
                Get
                    Return ((Not encrypt) And (algo.Padding <> PaddingMode.None) And (algo.Padding <> PaddingMode.Zeros))
                End Get
            End Property

            Private Function InternalTransformBlock(inputBuffer() As Byte, inputOffset As Integer, inputCount As Integer, outputBuffer() As Byte, outputOffset As Integer) As Integer

                Dim offs As Integer = inputOffset
                Dim full As Integer

                If inputCount <> blockSizeByte Then
                    If (inputCount Mod blockSizeByte) <> 0 Then Throw New CryptographicException("Invalid input block size.")
                    full = inputCount / blockSizeByte
                Else
                    full = 1
                End If

                If KeepLastBlock Then full -= 1

                Dim total As Integer = 0

                If lastBlock Then
                    Transform(workBuff, workout)
                    Buffer.BlockCopy(workout, 0, outputBuffer, outputOffset, blockSizeByte)
                    outputOffset += blockSizeByte
                    total += blockSizeByte
                    lastBlock = False
                End If

                For i = 0 To full - 1
                    Buffer.BlockCopy(inputBuffer, offs, workBuff, 0, blockSizeByte)
                    Transform(workBuff, workout)
                    Buffer.BlockCopy(workout, 0, outputBuffer, outputOffset, blockSizeByte)
                    offs += blockSizeByte
                    outputOffset += blockSizeByte
                    total += blockSizeByte
                Next

                If KeepLastBlock Then
                    Buffer.BlockCopy(inputBuffer, offs, workBuff, 0, blockSizeByte)
                    lastBlock = True
                End If

                Return total
            End Function

            Private _rng As New RNGCryptoServiceProvider

            Private Sub Random(buffer() As Byte, start As Integer, length As Integer)
                Dim random(length - 1) As Byte
                _rng.GetBytes(random)
                System.Buffer.BlockCopy(random, 0, buffer, start, length)
            End Sub

            Private Sub ThrowBadPaddingException(padding As PaddingMode, length As Integer, position As Integer)
                Dim msg As String = String.Format(("Bad {0} padding."), padding)
                If length >= 0 Then msg += String.Format((" Invalid length {0}."), length)
                If position >= 0 Then msg += String.Format((" Error found at position {0}."), position)
                Throw New CryptographicException(msg)
            End Sub

            Private Function FinalEncrypt(inputBuffer() As Byte, inputOffset As Integer, inputCount As Integer) As Byte()

                Dim full As Integer = (inputCount / blockSizeByte) * blockSizeByte
                Dim _rem As Integer = inputCount - full
                Dim total As Integer = full

                Select Case algo.Padding
                    Case PaddingMode.ANSIX923, PaddingMode.ISO10126, PaddingMode.PKCS7
                        total += blockSizeByte
                    Case Else
                        If inputCount = 0 Then Return {}
                        If _rem <> 0 Then
                            If algo.Padding = PaddingMode.None Then Throw New CryptographicException("invalid block length")

                            Dim paddedInput(full + blockSizeByte - 1) As Byte
                            Buffer.BlockCopy(inputBuffer, inputOffset, paddedInput, 0, inputCount)
                            inputBuffer = paddedInput
                            inputOffset = 0
                            inputCount = paddedInput.Length
                            total = inputCount
                        End If
                End Select

                Dim res(total - 1) As Byte
                Dim outputOffset As Integer = 0

                While total > blockSizeByte
                    InternalTransformBlock(inputBuffer, inputOffset, blockSizeByte, res, outputOffset)
                    inputOffset += blockSizeByte
                    outputOffset += blockSizeByte
                    total -= blockSizeByte
                End While

                Dim padding As Byte = (blockSizeByte - _rem)
                Select Case algo.Padding
                    Case PaddingMode.ANSIX923
                        res(res.Length - 1) = padding
                        Buffer.BlockCopy(inputBuffer, inputOffset, res, full, _rem)
                        InternalTransformBlock(res, full, blockSizeByte, res, full)
                    Case PaddingMode.ISO10126
                        Random(res, res.Length - padding, padding - 1)
                        res(res.Length - 1) = padding
                        Buffer.BlockCopy(inputBuffer, inputOffset, res, full, _rem)
                        InternalTransformBlock(res, full, blockSizeByte, res, full)
                    Case PaddingMode.PKCS7
                        For i = res.Length To res.Length - padding Step -1
                            res(i) = padding
                        Next
                        Buffer.BlockCopy(inputBuffer, inputOffset, res, full, _rem)
                        InternalTransformBlock(res, full, blockSizeByte, res, full)
                    Case Else
                        InternalTransformBlock(inputBuffer, inputOffset, blockSizeByte, res, outputOffset)
                End Select
                Return res
            End Function

            Private Function FinalDecrypt(inputBuffer() As Byte, inputOffset As Integer, inputCount As Integer) As Byte()

                If (inputCount Mod blockSizeByte) > 0 Then Throw New CryptographicException("Invalid input block size.")

                Dim total As Integer = inputCount
                If lastBlock Then total += blockSizeByte

                Dim res(total - 1) As Byte
                Dim outputOffset As Integer = 0

                While inputCount > 0
                    Dim len As Integer = InternalTransformBlock(inputBuffer, inputOffset, blockSizeByte, res, outputOffset)
                    inputOffset += blockSizeByte
                    outputOffset += len
                    inputCount -= blockSizeByte
                End While

                If lastBlock Then
                    Transform(workBuff, workout)
                    Buffer.BlockCopy(workout, 0, res, outputOffset, blockSizeByte)
                    outputOffset += blockSizeByte
                    lastBlock = False
                End If

                Dim Padding As Byte = If(total > 0, res(total - 1), 0)
                Select Case algo.Padding
                    Case PaddingMode.ANSIX923
                        If (Padding = 0) Or (Padding > blockSizeByte) Then ThrowBadPaddingException(algo.Padding, Padding, -1)
                        For i = Padding To 1 Step -1
                            If res(total - 1 - i) <> &H0 Then ThrowBadPaddingException(algo.Padding, -1, i)
                        Next
                        total -= Padding
                    Case PaddingMode.ISO10126
                        If (Padding = 0) Or (Padding > blockSizeByte) Then ThrowBadPaddingException(algo.Padding, Padding, -1)
                        total -= Padding
                    Case PaddingMode.PKCS7
                        If (Padding = 0) Or (Padding > blockSizeByte) Then ThrowBadPaddingException(algo.Padding, Padding, -1)
                        For i = Padding - 1 To 1 Step -1
                            If res(total - 1 - i) <> Padding Then ThrowBadPaddingException(algo.Padding, -1, i)
                        Next
                        total -= Padding
                    Case PaddingMode.None, PaddingMode.Zeros
                        'nothing to do
                End Select

                If total > 0 Then
                    Dim data(total - 1) As Byte
                    Buffer.BlockCopy(res, 0, data, 0, total)
                    Array.Clear(res, 0, res.Length)
                    Return data
                Else
                    Return {}
                End If
            End Function

            Public Overridable Function TransformFinalBlock(inputBuffer() As Byte, inputOffset As Integer, inputCount As Integer) As Byte() Implements ICryptoTransform.TransformFinalBlock

                If (m_disposed) Then Throw New ObjectDisposedException("Object is disposed")
                CheckInput(inputBuffer, inputOffset, inputCount)

                If (encrypt) Then
                    Return FinalEncrypt(inputBuffer, inputOffset, inputCount)
                Else
                    Return FinalDecrypt(inputBuffer, inputOffset, inputCount)
                End If
            End Function
        End Class


        Public Class BlowfishTransform : Inherits SymmetricTransform

            Protected pbox As UInteger() =
            {
                &H243F6A88UI, &H85A308D3UI, &H13198A2EUI, &H3707344UI, &HA4093822UI, &H299F31D0UI,
                &H82EFA98UI, &HEC4E6C89UI, &H452821E6UI, &H38D01377UI, &HBE5466CFUI, &H34E90C6CUI,
                &HC0AC29B7UI, &HC97C50DDUI, &H3F84D5B5UI, &HB5470917UI, &H9216D5D9UI, &H8979FB1BUI
            }

            Protected sbox1 As UInteger() =
            {
                &HD1310BA6UI, &H98DFB5ACUI, &H2FFD72DBUI, &HD01ADFB7UI, &HB8E1AFEDUI, &H6A267E96UI,
                &HBA7C9045UI, &HF12C7F99UI, &H24A19947UI, &HB3916CF7UI, &H801F2E2UI, &H858EFC16UI,
                &H636920D8UI, &H71574E69UI, &HA458FEA3UI, &HF4933D7EUI, &HD95748FUI, &H728EB658UI,
                &H718BCD58UI, &H82154AEEUI, &H7B54A41DUI, &HC25A59B5UI, &H9C30D539UI, &H2AF26013UI,
                &HC5D1B023UI, &H286085F0UI, &HCA417918UI, &HB8DB38EFUI, &H8E79DCB0UI, &H603A180EUI,
                &H6C9E0E8BUI, &HB01E8A3EUI, &HD71577C1UI, &HBD314B27UI, &H78AF2FDAUI, &H55605C60UI,
                &HE65525F3UI, &HAA55AB94UI, &H57489862UI, &H63E81440UI, &H55CA396AUI, &H2AAB10B6UI,
                &HB4CC5C34UI, &H1141E8CEUI, &HA15486AFUI, &H7C72E993UI, &HB3EE1411UI, &H636FBC2AUI,
                &H2BA9C55DUI, &H741831F6UI, &HCE5C3E16UI, &H9B87931EUI, &HAFD6BA33UI, &H6C24CF5CUI,
                &H7A325381UI, &H28958677UI, &H3B8F4898UI, &H6B4BB9AFUI, &HC4BFE81BUI, &H66282193UI,
                &H61D809CCUI, &HFB21A991UI, &H487CAC60UI, &H5DEC8032UI, &HEF845D5DUI, &HE98575B1UI,
                &HDC262302UI, &HEB651B88UI, &H23893E81UI, &HD396ACC5UI, &HF6D6FF3UI, &H83F44239UI,
                &H2E0B4482UI, &HA4842004UI, &H69C8F04AUI, &H9E1F9B5EUI, &H21C66842UI, &HF6E96C9AUI,
                &H670C9C61UI, &HABD388F0UI, &H6A51A0D2UI, &HD8542F68UI, &H960FA728UI, &HAB5133A3UI,
                &H6EEF0B6CUI, &H137A3BE4UI, &HBA3BF050UI, &H7EFB2A98UI, &HA1F1651DUI, &H39AF0176UI,
                &H66CA593EUI, &H82430E88UI, &H8CEE8619UI, &H456F9FB4UI, &H7D84A5C3UI, &H3B8B5EBEUI,
                &HE06F75D8UI, &H85C12073UI, &H401A449FUI, &H56C16AA6UI, &H4ED3AA62UI, &H363F7706UI,
                &H1BFEDF72UI, &H429B023DUI, &H37D0D724UI, &HD00A1248UI, &HDB0FEAD3UI, &H49F1C09BUI,
                &H75372C9UI, &H80991B7BUI, &H25D479D8UI, &HF6E8DEF7UI, &HE3FE501AUI, &HB6794C3BUI,
                &H976CE0BDUI, &H4C006BAUI, &HC1A94FB6UI, &H409F60C4UI, &H5E5C9EC2UI, &H196A2463UI,
                &H68FB6FAFUI, &H3E6C53B5UI, &H1339B2EBUI, &H3B52EC6FUI, &H6DFC511FUI, &H9B30952CUI,
                &HCC814544UI, &HAF5EBD09UI, &HBEE3D004UI, &HDE334AFDUI, &H660F2807UI, &H192E4BB3UI,
                &HC0CBA857UI, &H45C8740FUI, &HD20B5F39UI, &HB9D3FBDBUI, &H5579C0BDUI, &H1A60320AUI,
                &HD6A100C6UI, &H402C7279UI, &H679F25FEUI, &HFB1FA3CCUI, &H8EA5E9F8UI, &HDB3222F8UI,
                &H3C7516DFUI, &HFD616B15UI, &H2F501EC8UI, &HAD0552ABUI, &H323DB5FAUI, &HFD238760UI,
                &H53317B48UI, &H3E00DF82UI, &H9E5C57BBUI, &HCA6F8CA0UI, &H1A87562EUI, &HDF1769DBUI,
                &HD542A8F6UI, &H287EFFC3UI, &HAC6732C6UI, &H8C4F5573UI, &H695B27B0UI, &HBBCA58C8UI,
                &HE1FFA35DUI, &HB8F011A0UI, &H10FA3D98UI, &HFD2183B8UI, &H4AFCB56CUI, &H2DD1D35BUI,
                &H9A53E479UI, &HB6F84565UI, &HD28E49BCUI, &H4BFB9790UI, &HE1DDF2DAUI, &HA4CB7E33UI,
                &H62FB1341UI, &HCEE4C6E8UI, &HEF20CADAUI, &H36774C01UI, &HD07E9EFEUI, &H2BF11FB4UI,
                &H95DBDA4DUI, &HAE909198UI, &HEAAD8E71UI, &H6B93D5A0UI, &HD08ED1D0UI, &HAFC725E0UI,
                &H8E3C5B2FUI, &H8E7594B7UI, &H8FF6E2FBUI, &HF2122B64UI, &H8888B812UI, &H900DF01CUI,
                &H4FAD5EA0UI, &H688FC31CUI, &HD1CFF191UI, &HB3A8C1ADUI, &H2F2F2218UI, &HBE0E1777UI,
                &HEA752DFEUI, &H8B021FA1UI, &HE5A0CC0FUI, &HB56F74E8UI, &H18ACF3D6UI, &HCE89E299UI,
                &HB4A84FE0UI, &HFD13E0B7UI, &H7CC43B81UI, &HD2ADA8D9UI, &H165FA266UI, &H80957705UI,
                &H93CC7314UI, &H211A1477UI, &HE6AD2065UI, &H77B5FA86UI, &HC75442F5UI, &HFB9D35CFUI,
                &HEBCDAF0CUI, &H7B3E89A0UI, &HD6411BD3UI, &HAE1E7E49UI, &H250E2DUI, &H2071B35EUI,
                &H226800BBUI, &H57B8E0AFUI, &H2464369BUI, &HF009B91EUI, &H5563911DUI, &H59DFA6AAUI,
                &H78C14389UI, &HD95A537FUI, &H207D5BA2UI, &H2E5B9C5UI, &H83260376UI, &H6295CFA9UI,
                &H11C81968UI, &H4E734A41UI, &HB3472DCAUI, &H7B14A94AUI, &H1B510052UI, &H9A532915UI,
                &HD60F573FUI, &HBC9BC6E4UI, &H2B60A476UI, &H81E67400UI, &H8BA6FB5UI, &H571BE91FUI,
                &HF296EC6BUI, &H2A0DD915UI, &HB6636521UI, &HE7B9F9B6UI, &HFF34052EUI, &HC5855664UI,
                &H53B02D5DUI, &HA99F8FA1UI, &H8BA4799UI, &H6E85076AUI
            }

            Protected sbox2 As UInteger() =
            {
                &H4B7A70E9UI, &HB5B32944UI,
                &HDB75092EUI, &HC4192623UI, &HAD6EA6B0UI, &H49A7DF7DUI, &H9CEE60B8UI, &H8FEDB266UI,
                &HECAA8C71UI, &H699A17FFUI, &H5664526CUI, &HC2B19EE1UI, &H193602A5UI, &H75094C29UI,
                &HA0591340UI, &HE4183A3EUI, &H3F54989AUI, &H5B429D65UI, &H6B8FE4D6UI, &H99F73FD6UI,
                &HA1D29C07UI, &HEFE830F5UI, &H4D2D38E6UI, &HF0255DC1UI, &H4CDD2086UI, &H8470EB26UI,
                &H6382E9C6UI, &H21ECC5EUI, &H9686B3FUI, &H3EBAEFC9UI, &H3C971814UI, &H6B6A70A1UI,
                &H687F3584UI, &H52A0E286UI, &HB79C5305UI, &HAA500737UI, &H3E07841CUI, &H7FDEAE5CUI,
                &H8E7D44ECUI, &H5716F2B8UI, &HB03ADA37UI, &HF0500C0DUI, &HF01C1F04UI, &H200B3FFUI,
                &HAE0CF51AUI, &H3CB574B2UI, &H25837A58UI, &HDC0921BDUI, &HD19113F9UI, &H7CA92FF6UI,
                &H94324773UI, &H22F54701UI, &H3AE5E581UI, &H37C2DADCUI, &HC8B57634UI, &H9AF3DDA7UI,
                &HA9446146UI, &HFD0030EUI, &HECC8C73EUI, &HA4751E41UI, &HE238CD99UI, &H3BEA0E2FUI,
                &H3280BBA1UI, &H183EB331UI, &H4E548B38UI, &H4F6DB908UI, &H6F420D03UI, &HF60A04BFUI,
                &H2CB81290UI, &H24977C79UI, &H5679B072UI, &HBCAF89AFUI, &HDE9A771FUI, &HD9930810UI,
                &HB38BAE12UI, &HDCCF3F2EUI, &H5512721FUI, &H2E6B7124UI, &H501ADDE6UI, &H9F84CD87UI,
                &H7A584718UI, &H7408DA17UI, &HBC9F9ABCUI, &HE94B7D8CUI, &HEC7AEC3AUI, &HDB851DFAUI,
                &H63094366UI, &HC464C3D2UI, &HEF1C1847UI, &H3215D908UI, &HDD433B37UI, &H24C2BA16UI,
                &H12A14D43UI, &H2A65C451UI, &H50940002UI, &H133AE4DDUI, &H71DFF89EUI, &H10314E55UI,
                &H81AC77D6UI, &H5F11199BUI, &H43556F1UI, &HD7A3C76BUI, &H3C11183BUI, &H5924A509UI,
                &HF28FE6EDUI, &H97F1FBFAUI, &H9EBABF2CUI, &H1E153C6EUI, &H86E34570UI, &HEAE96FB1UI,
                &H860E5E0AUI, &H5A3E2AB3UI, &H771FE71CUI, &H4E3D06FAUI, &H2965DCB9UI, &H99E71D0FUI,
                &H803E89D6UI, &H5266C825UI, &H2E4CC978UI, &H9C10B36AUI, &HC6150EBAUI, &H94E2EA78UI,
                &HA5FC3C53UI, &H1E0A2DF4UI, &HF2F74EA7UI, &H361D2B3DUI, &H1939260FUI, &H19C27960UI,
                &H5223A708UI, &HF71312B6UI, &HEBADFE6EUI, &HEAC31F66UI, &HE3BC4595UI, &HA67BC883UI,
                &HB17F37D1UI, &H18CFF28UI, &HC332DDEFUI, &HBE6C5AA5UI, &H65582185UI, &H68AB9802UI,
                &HEECEA50FUI, &HDB2F953BUI, &H2AEF7DADUI, &H5B6E2F84UI, &H1521B628UI, &H29076170UI,
                &HECDD4775UI, &H619F1510UI, &H13CCA830UI, &HEB61BD96UI, &H334FE1EUI, &HAA0363CFUI,
                &HB5735C90UI, &H4C70A239UI, &HD59E9E0BUI, &HCBAADE14UI, &HEECC86BCUI, &H60622CA7UI,
                &H9CAB5CABUI, &HB2F3846EUI, &H648B1EAFUI, &H19BDF0CAUI, &HA02369B9UI, &H655ABB50UI,
                &H40685A32UI, &H3C2AB4B3UI, &H319EE9D5UI, &HC021B8F7UI, &H9B540B19UI, &H875FA099UI,
                &H95F7997EUI, &H623D7DA8UI, &HF837889AUI, &H97E32D77UI, &H11ED935FUI, &H16681281UI,
                &HE358829UI, &HC7E61FD6UI, &H96DEDFA1UI, &H7858BA99UI, &H57F584A5UI, &H1B227263UI,
                &H9B83C3FFUI, &H1AC24696UI, &HCDB30AEBUI, &H532E3054UI, &H8FD948E4UI, &H6DBC3128UI,
                &H58EBF2EFUI, &H34C6FFEAUI, &HFE28ED61UI, &HEE7C3C73UI, &H5D4A14D9UI, &HE864B7E3UI,
                &H42105D14UI, &H203E13E0UI, &H45EEE2B6UI, &HA3AAABEAUI, &HDB6C4F15UI, &HFACB4FD0UI,
                &HC742F442UI, &HEF6ABBB5UI, &H654F3B1DUI, &H41CD2105UI, &HD81E799EUI, &H86854DC7UI,
                &HE44B476AUI, &H3D816250UI, &HCF62A1F2UI, &H5B8D2646UI, &HFC8883A0UI, &HC1C7B6A3UI,
                &H7F1524C3UI, &H69CB7492UI, &H47848A0BUI, &H5692B285UI, &H95BBF00UI, &HAD19489DUI,
                &H1462B174UI, &H23820E00UI, &H58428D2AUI, &HC55F5EAUI, &H1DADF43EUI, &H233F7061UI,
                &H3372F092UI, &H8D937E41UI, &HD65FECF1UI, &H6C223BDBUI, &H7CDE3759UI, &HCBEE7460UI,
                &H4085F2A7UI, &HCE77326EUI, &HA6078084UI, &H19F8509EUI, &HE8EFD855UI, &H61D99735UI,
                &HA969A7AAUI, &HC50C06C2UI, &H5A04ABFCUI, &H800BCADCUI, &H9E447A2EUI, &HC3453484UI,
                &HFDD56705UI, &HE1E9EC9UI, &HDB73DBD3UI, &H105588CDUI, &H675FDA79UI, &HE3674340UI,
                &HC5C43465UI, &H713E38D8UI, &H3D28F89EUI, &HF16DFF20UI, &H153E21E7UI, &H8FB03D4AUI,
                &HE6E39F2BUI, &HDB83ADF7UI
            }

            Protected sbox3 As UInteger() =
            {
                &HE93D5A68UI, &H948140F7UI, &HF64C261CUI, &H94692934UI,
                &H411520F7UI, &H7602D4F7UI, &HBCF46B2EUI, &HD4A20068UI, &HD4082471UI, &H3320F46AUI,
                &H43B7D4B7UI, &H500061AFUI, &H1E39F62EUI, &H97244546UI, &H14214F74UI, &HBF8B8840UI,
                &H4D95FC1DUI, &H96B591AFUI, &H70F4DDD3UI, &H66A02F45UI, &HBFBC09ECUI, &H3BD9785UI,
                &H7FAC6DD0UI, &H31CB8504UI, &H96EB27B3UI, &H55FD3941UI, &HDA2547E6UI, &HABCA0A9AUI,
                &H28507825UI, &H530429F4UI, &HA2C86DAUI, &HE9B66DFBUI, &H68DC1462UI, &HD7486900UI,
                &H680EC0A4UI, &H27A18DEEUI, &H4F3FFEA2UI, &HE887AD8CUI, &HB58CE006UI, &H7AF4D6B6UI,
                &HAACE1E7CUI, &HD3375FECUI, &HCE78A399UI, &H406B2A42UI, &H20FE9E35UI, &HD9F385B9UI,
                &HEE39D7ABUI, &H3B124E8BUI, &H1DC9FAF7UI, &H4B6D1856UI, &H26A36631UI, &HEAE397B2UI,
                &H3A6EFA74UI, &HDD5B4332UI, &H6841E7F7UI, &HCA7820FBUI, &HFB0AF54EUI, &HD8FEB397UI,
                &H454056ACUI, &HBA489527UI, &H55533A3AUI, &H20838D87UI, &HFE6BA9B7UI, &HD096954BUI,
                &H55A867BCUI, &HA1159A58UI, &HCCA92963UI, &H99E1DB33UI, &HA62A4A56UI, &H3F3125F9UI,
                &H5EF47E1CUI, &H9029317CUI, &HFDF8E802UI, &H4272F70UI, &H80BB155CUI, &H5282CE3UI,
                &H95C11548UI, &HE4C66D22UI, &H48C1133FUI, &HC70F86DCUI, &H7F9C9EEUI, &H41041F0FUI,
                &H404779A4UI, &H5D886E17UI, &H325F51EBUI, &HD59BC0D1UI, &HF2BCC18FUI, &H41113564UI,
                &H257B7834UI, &H602A9C60UI, &HDFF8E8A3UI, &H1F636C1BUI, &HE12B4C2UI, &H2E1329EUI,
                &HAF664FD1UI, &HCAD18115UI, &H6B2395E0UI, &H333E92E1UI, &H3B240B62UI, &HEEBEB922UI,
                &H85B2A20EUI, &HE6BA0D99UI, &HDE720C8CUI, &H2DA2F728UI, &HD0127845UI, &H95B794FDUI,
                &H647D0862UI, &HE7CCF5F0UI, &H5449A36FUI, &H877D48FAUI, &HC39DFD27UI, &HF33E8D1EUI,
                &HA476341UI, &H992EFF74UI, &H3A6F6EABUI, &HF4F8FD37UI, &HA812DC60UI, &HA1EBDDF8UI,
                &H991BE14CUI, &HDB6E6B0DUI, &HC67B5510UI, &H6D672C37UI, &H2765D43BUI, &HDCD0E804UI,
                &HF1290DC7UI, &HCC00FFA3UI, &HB5390F92UI, &H690FED0BUI, &H667B9FFBUI, &HCEDB7D9CUI,
                &HA091CF0BUI, &HD9155EA3UI, &HBB132F88UI, &H515BAD24UI, &H7B9479BFUI, &H763BD6EBUI,
                &H37392EB3UI, &HCC115979UI, &H8026E297UI, &HF42E312DUI, &H6842ADA7UI, &HC66A2B3BUI,
                &H12754CCCUI, &H782EF11CUI, &H6A124237UI, &HB79251E7UI, &H6A1BBE6UI, &H4BFB6350UI,
                &H1A6B1018UI, &H11CAEDFAUI, &H3D25BDD8UI, &HE2E1C3C9UI, &H44421659UI, &HA121386UI,
                &HD90CEC6EUI, &HD5ABEA2AUI, &H64AF674EUI, &HDA86A85FUI, &HBEBFE988UI, &H64E4C3FEUI,
                &H9DBC8057UI, &HF0F7C086UI, &H60787BF8UI, &H6003604DUI, &HD1FD8346UI, &HF6381FB0UI,
                &H7745AE04UI, &HD736FCCCUI, &H83426B33UI, &HF01EAB71UI, &HB0804187UI, &H3C005E5FUI,
                &H77A057BEUI, &HBDE8AE24UI, &H55464299UI, &HBF582E61UI, &H4E58F48FUI, &HF2DDFDA2UI,
                &HF474EF38UI, &H8789BDC2UI, &H5366F9C3UI, &HC8B38E74UI, &HB475F255UI, &H46FCD9B9UI,
                &H7AEB2661UI, &H8B1DDF84UI, &H846A0E79UI, &H915F95E2UI, &H466E598EUI, &H20B45770UI,
                &H8CD55591UI, &HC902DE4CUI, &HB90BACE1UI, &HBB8205D0UI, &H11A86248UI, &H7574A99EUI,
                &HB77F19B6UI, &HE0A9DC09UI, &H662D09A1UI, &HC4324633UI, &HE85A1F02UI, &H9F0BE8CUI,
                &H4A99A025UI, &H1D6EFE10UI, &H1AB93D1DUI, &HBA5A4DFUI, &HA186F20FUI, &H2868F169UI,
                &HDCB7DA83UI, &H573906FEUI, &HA1E2CE9BUI, &H4FCD7F52UI, &H50115E01UI, &HA70683FAUI,
                &HA002B5C4UI, &HDE6D027UI, &H9AF88C27UI, &H773F8641UI, &HC3604C06UI, &H61A806B5UI,
                &HF0177A28UI, &HC0F586E0UI, &H6058AAUI, &H30DC7D62UI, &H11E69ED7UI, &H2338EA63UI,
                &H53C2DD94UI, &HC2C21634UI, &HBBCBEE56UI, &H90BCB6DEUI, &HEBFC7DA1UI, &HCE591D76UI,
                &H6F05E409UI, &H4B7C0188UI, &H39720A3DUI, &H7C927C24UI, &H86E3725FUI, &H724D9DB9UI,
                &H1AC15BB4UI, &HD39EB8FCUI, &HED545578UI, &H8FCA5B5UI, &HD83D7CD3UI, &H4DAD0FC4UI,
                &H1E50EF5EUI, &HB161E6F8UI, &HA28514D9UI, &H6C51133CUI, &H6FD5C7E7UI, &H56E14EC4UI,
                &H362ABFCEUI, &HDDC6C837UI, &HD79A3234UI, &H92638212UI, &H670EFA8EUI, &H406000E0UI
            }

            Protected sbox4 As UInteger() =
            {
                &H3A39CE37UI, &HD3FAF5CFUI, &HABC27737UI, &H5AC52D1BUI, &H5CB0679EUI, &H4FA33742UI,
                &HD3822740UI, &H99BC9BBEUI, &HD5118E9DUI, &HBF0F7315UI, &HD62D1C7EUI, &HC700C47BUI,
                &HB78C1B6BUI, &H21A19045UI, &HB26EB1BEUI, &H6A366EB4UI, &H5748AB2FUI, &HBC946E79UI,
                &HC6A376D2UI, &H6549C2C8UI, &H530FF8EEUI, &H468DDE7DUI, &HD5730A1DUI, &H4CD04DC6UI,
                &H2939BBDBUI, &HA9BA4650UI, &HAC9526E8UI, &HBE5EE304UI, &HA1FAD5F0UI, &H6A2D519AUI,
                &H63EF8CE2UI, &H9A86EE22UI, &HC089C2B8UI, &H43242EF6UI, &HA51E03AAUI, &H9CF2D0A4UI,
                &H83C061BAUI, &H9BE96A4DUI, &H8FE51550UI, &HBA645BD6UI, &H2826A2F9UI, &HA73A3AE1UI,
                &H4BA99586UI, &HEF5562E9UI, &HC72FEFD3UI, &HF752F7DAUI, &H3F046F69UI, &H77FA0A59UI,
                &H80E4A915UI, &H87B08601UI, &H9B09E6ADUI, &H3B3EE593UI, &HE990FD5AUI, &H9E34D797UI,
                &H2CF0B7D9UI, &H22B8B51UI, &H96D5AC3AUI, &H17DA67DUI, &HD1CF3ED6UI, &H7C7D2D28UI,
                &H1F9F25CFUI, &HADF2B89BUI, &H5AD6B472UI, &H5A88F54CUI, &HE029AC71UI, &HE019A5E6UI,
                &H47B0ACFDUI, &HED93FA9BUI, &HE8D3C48DUI, &H283B57CCUI, &HF8D56629UI, &H79132E28UI,
                &H785F0191UI, &HED756055UI, &HF7960E44UI, &HE3D35E8CUI, &H15056DD4UI, &H88F46DBAUI,
                &H3A16125UI, &H564F0BDUI, &HC3EB9E15UI, &H3C9057A2UI, &H97271AECUI, &HA93A072AUI,
                &H1B3F6D9BUI, &H1E6321F5UI, &HF59C66FBUI, &H26DCF319UI, &H7533D928UI, &HB155FDF5UI,
                &H3563482UI, &H8ABA3CBBUI, &H28517711UI, &HC20AD9F8UI, &HABCC5167UI, &HCCAD925FUI,
                &H4DE81751UI, &H3830DC8EUI, &H379D5862UI, &H9320F991UI, &HEA7A90C2UI, &HFB3E7BCEUI,
                &H5121CE64UI, &H774FBE32UI, &HA8B6E37EUI, &HC3293D46UI, &H48DE5369UI, &H6413E680UI,
                &HA2AE0810UI, &HDD6DB224UI, &H69852DFDUI, &H9072166UI, &HB39A460AUI, &H6445C0DDUI,
                &H586CDECFUI, &H1C20C8AEUI, &H5BBEF7DDUI, &H1B588D40UI, &HCCD2017FUI, &H6BB4E3BBUI,
                &HDDA26A7EUI, &H3A59FF45UI, &H3E350A44UI, &HBCB4CDD5UI, &H72EACEA8UI, &HFA6484BBUI,
                &H8D6612AEUI, &HBF3C6F47UI, &HD29BE463UI, &H542F5D9EUI, &HAEC2771BUI, &HF64E6370UI,
                &H740E0D8DUI, &HE75B1357UI, &HF8721671UI, &HAF537D5DUI, &H4040CB08UI, &H4EB4E2CCUI,
                &H34D2466AUI, &H115AF84UI, &HE1B00428UI, &H95983A1DUI, &H6B89FB4UI, &HCE6EA048UI,
                &H6F3F3B82UI, &H3520AB82UI, &H11A1D4BUI, &H277227F8UI, &H611560B1UI, &HE7933FDCUI,
                &HBB3A792BUI, &H344525BDUI, &HA08839E1UI, &H51CE794BUI, &H2F32C9B7UI, &HA01FBAC9UI,
                &HE01CC87EUI, &HBCC7D1F6UI, &HCF0111C3UI, &HA1E8AAC7UI, &H1A908749UI, &HD44FBD9AUI,
                &HD0DADECBUI, &HD50ADA38UI, &H339C32AUI, &HC6913667UI, &H8DF9317CUI, &HE0B12B4FUI,
                &HF79E59B7UI, &H43F5BB3AUI, &HF2D519FFUI, &H27D9459CUI, &HBF97222CUI, &H15E6FC2AUI,
                &HF91FC71UI, &H9B941525UI, &HFAE59361UI, &HCEB69CEBUI, &HC2A86459UI, &H12BAA8D1UI,
                &HB6C1075EUI, &HE3056A0CUI, &H10D25065UI, &HCB03A442UI, &HE0EC6E0EUI, &H1698DB3BUI,
                &H4C98A0BEUI, &H3278E964UI, &H9F1F9532UI, &HE0D392DFUI, &HD3A0342BUI, &H8971F21EUI,
                &H1B0A7441UI, &H4BA3348CUI, &HC5BE7120UI, &HC37632D8UI, &HDF359F8DUI, &H9B992F2EUI,
                &HE60B6F47UI, &HFE3F11DUI, &HE54CDA54UI, &H1EDAD891UI, &HCE6279CFUI, &HCD3E7E6FUI,
                &H1618B166UI, &HFD2C1D05UI, &H848FD2C5UI, &HF6FB2299UI, &HF523F357UI, &HA6327623UI,
                &H93A83531UI, &H56CCCD02UI, &HACF08162UI, &H5A75EBB5UI, &H6E163697UI, &H88D273CCUI,
                &HDE966292UI, &H81B949D0UI, &H4C50901BUI, &H71C65614UI, &HE6C6C7BDUI, &H327A140AUI,
                &H45E1D006UI, &HC3F27B9AUI, &HC9AA53FDUI, &H62A80F00UI, &HBB25BFE2UI, &H35BDD2F6UI,
                &H71126905UI, &HB2040222UI, &HB6CBCF7CUI, &HCD769C2BUI, &H53113EC0UI, &H1640E3D3UI,
                &H38ABBD60UI, &H2547ADF0UI, &HBA38209CUI, &HF746CE76UI, &H77AFA1C5UI, &H20756060UI,
                &H85CBFE4EUI, &H8AE88DD8UI, &H7AAAF9B0UI, &H4CF9AA7EUI, &H1948C25CUI, &H2FB8A8CUI,
                &H1C36AE4UI, &HD6EBE1F9UI, &H90D4F869UI, &HA65CDEA0UI, &H3F09252DUI, &HC208E69FUI,
                &HB74E6132UI, &HCE77E25BUI, &H578FDFE3UI, &H3AC372E6UI
            }

            Private encryption As Boolean

            Public Sub New(algo As Blowfish, encryption As Boolean, key() As Byte, iv() As Byte)
                MyBase.New(algo, encryption, iv)
                Me.encryption = encryption
                KeySetup(key)
            End Sub

            Protected Overrides Sub ECB(input() As Byte, output() As Byte)
                Dim a As UInteger, b As UInteger, c As UInteger, d As UInteger
                Dim l As UInteger, r As UInteger

                a = input(0)
                b = input(1)
                c = input(2)
                d = input(3)
                l = (a << 24) Or (b << 16) Or (c << 8) Or d

                a = input(4)
                b = input(5)
                c = input(6)
                d = input(7)
                r = (a << 24) Or (b << 16) Or (c << 8) Or d

                If encryption Then
                    EncryptBlock(l, r)
                Else
                    DecryptBlock(l, r)
                End If

                output(0) = (l >> 24) And &HFF
                output(1) = (l >> 16) And &HFF
                output(2) = (l >> 8) And &HFF
                output(3) = l And &HFF

                output(4) = (r >> 24) And &HFF
                output(5) = (r >> 16) And &HFF
                output(6) = (r >> 8) And &HFF
                output(7) = r And &HFF
            End Sub

            Protected Overrides Sub Dispose(disposing As Boolean)
                pbox = Nothing
                sbox1 = Nothing
                sbox2 = Nothing
                sbox3 = Nothing
                sbox4 = Nothing
                MyBase.Dispose(disposing)
            End Sub

            Protected Overridable Sub KeySetup(key() As Byte)
                Dim dword As UInteger = 0
                Dim keyPos As UInteger = 0
                Dim r As UInteger = 0, l As UInteger = 0

                For i = 0 To pbox.Length - 1
                    dword = 0

                    For j = 0 To 3
                        dword = (dword << 8) Or key(keyPos)
                        keyPos += 1
                        If keyPos >= key.Length Then keyPos = 0
                    Next j
                    pbox(i) = pbox(i) Xor dword
                Next i

                For i = 0 To pbox.Length - 1 Step 2
                    EncryptBlock(l, r)
                    pbox(i) = l
                    pbox(i + 1) = r
                Next

                For i = 0 To sbox1.Length - 1 Step 2
                    EncryptBlock(l, r)
                    sbox1(i) = l
                    sbox1(i + 1) = r
                Next

                For i = 0 To sbox2.Length - 1 Step 2
                    EncryptBlock(l, r)
                    sbox2(i) = l
                    sbox2(i + 1) = r
                Next

                For i = 0 To sbox3.Length - 1 Step 2
                    EncryptBlock(l, r)
                    sbox3(i) = l
                    sbox3(i + 1) = r
                Next

                For i = 0 To sbox4.Length - 1 Step 2
                    EncryptBlock(l, r)
                    sbox4(i) = l
                    sbox4(i + 1) = r
                Next
            End Sub

            Protected Overridable Sub EncryptBlock(ByRef l As UInteger, ByRef r As UInteger)
                Dim tmp As UInteger

                'R=1
                l = l Xor pbox(0)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=2
                r = r Xor pbox(1)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=3
                l = l Xor pbox(2)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=4
                r = r Xor pbox(3)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=5
                l = l Xor pbox(4)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=6
                r = r Xor pbox(5)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=7
                l = l Xor pbox(6)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=8
                r = r Xor pbox(7)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=9
                l = l Xor pbox(8)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=10
                r = r Xor pbox(9)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=11
                l = l Xor pbox(10)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=12
                r = r Xor pbox(11)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=13
                l = l Xor pbox(12)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=14
                r = r Xor pbox(13)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=15
                l = l Xor pbox(14)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=16
                r = r Xor pbox(15)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                tmp = r

                r = l Xor pbox(16)
                l = tmp Xor pbox(17)
            End Sub

            Protected Overridable Sub DecryptBlock(ByRef l As UInteger, ByRef r As UInteger)
                Dim tmp As UInteger

                'R=1
                l = l Xor pbox(17)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=2
                r = r Xor pbox(16)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=3
                l = l Xor pbox(15)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=4
                r = r Xor pbox(14)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=5
                l = l Xor pbox(13)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=6
                r = r Xor pbox(12)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=7
                l = l Xor pbox(11)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=8
                r = r Xor pbox(10)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=9
                l = l Xor pbox(9)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=10
                r = r Xor pbox(8)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=11
                l = l Xor pbox(7)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=12
                r = r Xor pbox(6)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=13
                l = l Xor pbox(5)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=14
                r = r Xor pbox(4)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                'R=15
                l = l Xor pbox(3)
                r = r Xor (((sbox1((l >> 24) And &HFF) + sbox2((l >> 16) And &HFF)) Xor sbox3((l >> 8) And &HFF)) + sbox4(l And &HFF))

                'R=16
                r = r Xor pbox(2)
                l = l Xor (((sbox1((r >> 24) And &HFF) + sbox2((r >> 16) And &HFF)) Xor sbox3((r >> 8) And &HFF)) + sbox4(r And &HFF))

                tmp = r

                r = l Xor pbox(1)
                l = tmp Xor pbox(0)
            End Sub

        End Class

    End Class

End Class
