(function () {
    'use strict';

    angular
        .module('angle')
        .controller('CommonMstDashboardController', CommonMstDashboardController);

    CommonMstDashboardController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies'];

    function CommonMstDashboardController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'CommonMstDashboardController';

        activate();

        function activate() {

            $scope.lawyerempanelment = true;
            $scope.legalservices = true;
            $scope.legalcompliance = true;
            $scope.report = true;

            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilegelevel3';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var customer = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTCUS");
                var gurantor = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTGUA");
                var customer360 = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCUSRCU");
                var Stackholdertype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTSHT");
                var designation = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTDS");
                var cibildata = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTCBL");
                var customerreport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTCRT");
                var samunnatiassociationmaster = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTSAM");
                var customerreport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTCRT");
                var companydocument = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTCMD");
                var companytype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTCMT");
                var businessindustrytype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTBIT");
                var bureauname = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTBRN");
                var AMLCategory = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTAML");
                var assessmentagency = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTAMA");
                var licensetype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTLCT");
                var constructiontype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTCUT");
                var strategicbusinessunit = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTSBU");
                var geneticcode = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTGNC");
                var assessmentagencyrating = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTAAR");
                var businesscategory = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTBSC");
                var program = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTPRM");
                var gender = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMGDR");
                var maritualstatus = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMMRS");
                var education = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMEDQ");
                var incometype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMICT");
                var individualproof = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMIDP");
                var addresstype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMLAT");
                var caste = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMCST");
                var religion = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMRGN");
                var individualdocument = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMIDD");
                var countrycode = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMCCE");
                var residencetype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMRST");
                var areatype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMATP");
                var partytype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMPTP");
                var ownershiptype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMOWS");
                var designation = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMDGN");
                var relationship = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMRTP");
                var occupation = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMOCP");
                var SAType = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTSAMSAT");
                var SAEntityType = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTSAMSAE");
                var SADocumentList = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTSAMSAD");
                var RMMapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMRMM");
                var Constitution = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTCS");
                var ApplicationCreation = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTACS");
                //var RMMapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMM");
                var AddressType = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMLAT");
                var Supplier = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTMSM");
                var Buyer = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTMBS");
                var ApplicationReport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRPTAPR");
                var bankacountlevel = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTBAL");
                var encoreproduct = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTENP");
                var loantype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTLNT");
                var loanproducts = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTLPE");
                var loansubproducts = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTLSP");
                var loantermperiod = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTLTP");
                var amortizationmethod = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTAMM");
                var paymentprincipalfrequency = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTPPF");
                var interestfrequency = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTITF");
                var typeofchargecreated = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTTCC");
                var leadingarrangement = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTLDA");
                var fundedtypeindicator = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTFTI");
                var typeofdebt = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTTOD");
                var loanpurpose = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTLPE");
                var bankaccounttype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTLMTBAT");
                var securitytype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCTMCST");
                var securityclassification = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCTMSCN");
                var securitycoverage = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCTMSCE");
                var guaranteecoverage = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCTMGTC");
                var asseststype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCTMAST");
                var property = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCTMPRY");
                var bankname = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCTMBAN");
                var credittypeoffacility = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECTF");
                var creditexistingfunded = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECTE");
                var creditinstallmentfrequency = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECIF");
                var creditacctclassification = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECAC");
                var credittype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECDT");
                var colending = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECMT");
                var entity = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCPYETY");
                var vertical = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCPYVTL");
                var verticaltags = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCPYVTT");
                var samunnatibranchname = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCPYSBN");
                var samunnatibranchstate = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCPYSBS");
                var creditpolicycompliance = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECPC");
                var vernacularlanguage = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMMVL");
                var lendertype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCREMLT");
                var Creditunderwritingfacilitytype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCREMUF");
                var bsrcode = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCREBSC");
                var lineofactivity = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRELOA");
                var msme = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCREMSM");
                var natureofentity = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRENOE");
                var pslcategory = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCREPSC");
                var pslpurpose = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCREPSP");
                var turnover = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRETUO");
                var clietdetails = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECDS");
                var weakersection = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCREWSC");
                var investment = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCREINV");
                var lineofactivity = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRELOA");
                var purposecolumn = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCREPUC");
                var AssessmentCriteria = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTACT");
                var ClusterHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMCLH");
                var ClusterMapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMCLM");
                var RegionHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMRGH");
                var RegionMapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMRGM");
                var ZonalHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMZNH");
                var ZoneMapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMZNM");
                var ProductHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMPRH");
                var BusinessHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMBSH");               
                var GroupBusinessHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRMMGBH");
                var ScourceofContact = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTTLCSOC");
                var CallType = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTTLCCAT");
                var BusinessApproval = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTBUA");
                var TeleCallingFunction = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTTLCTCF");
                var CallReceivedNumber = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTTLCCRN");
                var visitormgmt = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTTLCVTM");
                var creditmapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECMM");
                var Product = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTPRO");
                var Category = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTVC");
                var CADGroup = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCADCAG");
                var CADGroupAssignment = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCADCGA");
                var CreditAllocationReport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRPTCAR");
                var DocumentType = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTDOT");
                var groupdocument = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTGRD");
                var documentseverity = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTDOS");
                var saonboarding = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTSAMSAO");
                var businessverify = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTSAMBDV");
                var businessreg = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTSAMBDR");
                var ccreport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRPTCCR");
                var creditopsmapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCRECOM");
                var csamanagement = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCAMPCM");
                var colendingverification = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCAMCLV");
                var VisitorManagemenReport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRPTVMR");
                var SanctionmisReport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRPTSMR");
                var BuyerReport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRPTBUR");
                var sanctionwaiver = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCADSAW");
                var LANWaiver = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCADLAN");
                var groupwaiver = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCADGRW");
                var digitalsignature = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCADDST");
                var PhysicalStatus = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMPHS");
                var Salutation = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTDGMSAL");
                var Busiunessrevoke = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTADMBRV");
                var Creditrevoke = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTADMCRV");
                var hierarchy = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTADMBHU");
                var maker = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCHMMAK");
                var Creditgroup = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCDMCDG");
                var BREmaster = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRLERLM");
                var BREtemplate = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRLETMM");
                var Answertype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRLEATM");
                var Grouptitle = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRLEGTM");
                var internalrating = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTIRG");
                var covenantperiod = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTCOP");
                var livestock = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTMLS");
                var equipment = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTBMTMEQ");
                var csacategory = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCSACAT");
                var csaclassification = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCSASCA");
                var guaranteeprograms = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCSAQPS");
                var colendingcategory = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCSACLC");
                var colendingprograms = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCSACLP");
                var fieldmapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTMSTFMA");
                var Couriercompany = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTCADCOC");
                var Answertype = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRLEATM");
                var GroupTitle = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("CSTRLEGTM");

                if (customer != -1) {
                    
                   
                    $scope.customer_show = 'Y';
                      }
                if (gurantor != -1) {
                    $scope.gurantor_show = 'Y';
                }
                if (customer360 != -1) {
                    $scope.Customer360_show = 'Y';
                }
                if (Stackholdertype != -1) {
                    $scope.Stackholdertype_show = 'Y';
                }
                if (designation != -1) {
                    $scope.designation_show = 'Y';
                }
                if (samunnatiassociationmaster != -1) {
                    $scope.samunnatiassociationmaster_show = 'Y';
                }
                if (cibildata != -1) {
                    $scope.cibildata_show = 'Y';
                }
                if (customerreport != -1) {
                    $scope.customerreport_show = 'Y';
                }
                if (companydocument != -1) {
                    $scope.companydocument_show = 'Y';
                }
                if (companytype != -1) {
                    $scope.companytype_show = 'Y';
                }
                //if (usertype != -1) {
                //    $scope. Stackholdertype_show = 'Y';
                //}
                if (businessindustrytype != -1) {
                    $scope. businessindustrytype_show = 'Y';
                }
                if (program != -1) {
                    $scope.program_show = 'Y';
                }
                if (bureauname != -1) {
                    $scope. bureauname_show = 'Y';
                }
                if (AMLCategory != -1) {
                    $scope. AMLcategory_show = 'Y';
                }
                if (assessmentagency != -1) {
                    $scope. assessmentagency_show = 'Y';
                }
                if (licensetype != -1) {
                    $scope. licensetype_show = 'Y';
                }
                if (constructiontype != -1) {
                    $scope. constructiontype_show = 'Y';
                }
                if (strategicbusinessunit != -1) {
                    $scope. strategicbusinessunit_show = 'Y';
                }
                if (geneticcode != -1) {
                    $scope. geneticcode_show = 'Y';
                }
                if (assessmentagencyrating != -1) {
                    $scope. assagencyrating_show = 'Y';
                }
                if (businesscategory != -1) {
                    $scope. businesscategory_show = 'Y';
                }
                if (gender != -1) {
                    $scope. gender_show = 'Y';
                }
                if (maritualstatus != -1) {
                    $scope. maritialstatus_show = 'Y';
                }
                if (education != -1) {
                    $scope. educationalqualification_show = 'Y';
                }
                if (incometype != -1) {
                    $scope. incometype_show = 'Y';
                }
                if (individualproof != -1) {
                    $scope. indivitualtype_show = 'Y';
                }
                if (addresstype != -1) {
                    $scope. addresstype_show = 'Y';
                }
                if (caste != -1) {
                    $scope. caste_show = 'Y';
                }
                if (religion != -1) {
                    $scope. religion_show = 'Y';
                }
                if (individualdocument != -1) {
                    $scope. individualdocument_show = 'Y';
                }
                if (countrycode != -1) {
                    $scope. country_show = 'Y';
                }
                if (residencetype != -1) {
                    $scope. residence_show = 'Y';
                }
                if (areatype != -1) {
                    $scope. areatype_show = 'Y';
                }
                if (partytype != -1) {
                    $scope. partytype_show = 'Y';
                }
                if (ownershiptype != -1) {
                    $scope. ownershiptype_show = 'Y';
                }
                if (designation != -1) {
                    $scope. Designation_show = 'Y';
                }
                if (relationship != -1) {
                    $scope. Relationship_show = 'Y';
                }
                if (occupation != -1) {
                    $scope.Occupation_show = 'Y';
                }
                if (SAType != -1) {
                    $scope. SAType_show = 'Y';
                }
                if (SAEntityType != -1) {
                    $scope. SAEntityType_show = 'Y';
                }
                if (SADocumentList != -1) {
                    $scope. SAdocumentlist_show = 'Y';
                }
                if (RMMapping != -1) {
                    $scope. RMMapping_show = 'Y';
                }

                if (ApplicationReport != -1) {
                    $scope.ApplicationReport_show = 'Y';
                }
                if (encoreproduct != -1) {
                    $scope.encoreproduct_show = 'Y';
                }
                if (bankacountlevel != -1) {
                    $scope.bankacctlevel_show = 'Y';
                }
                if (loantype != -1) {
                    $scope. loantype_show = 'Y';
                }
                if (loanproducts != -1) {
                    $scope. loanproducts_show = 'Y';
                }
                if (loansubproducts != -1) {
                    $scope. loansubproducts_show = 'Y';
                }
                if (loantermperiod != -1) {
                    $scope. loantermperiod_show = 'Y';
                }
                if (amortizationmethod != -1) {
                    $scope. Amortizationmethod_show = 'Y';
                }
                if (paymentprincipalfrequency != -1) {
                    $scope. principalfrequency_show = 'Y';
                }
                if (interestfrequency != -1) {
                    $scope. interestfrequency_show = 'Y';
                }
                if (typeofchargecreated != -1) {
                    $scope. chargecreated_show = 'Y';
                }
                if (leadingarrangement != -1) {
                    $scope. lendingarrangement_show = 'Y';
                }
                if (fundedtypeindicator != -1) {
                    $scope. fundedtype_show = 'Y';
                }
                if (typeofdebt != -1) {
                    $scope. typeofdebt_show = 'Y';
                }
                if (loanpurpose != -1) {
                    $scope. loanpurpose_show = 'Y';
                }
                if (bankaccounttype != -1) {
                    $scope.bankaccounttype_show = 'Y';
                }
                if (securitytype != -1) {
                    $scope. collateral_show = 'Y';
                }
                if (securityclassification != -1) {
                    $scope. securityclassification_show = 'Y';
                }
                if (securitycoverage != -1) {
                    $scope. securitycoverage_show = 'Y';
                }
                if (guaranteecoverage != -1) {
                    $scope. guarantorcoverage_show = 'Y';
                }
                if (asseststype != -1) {
                    $scope. assetstype_show = 'Y';
                }
                if (property != -1) {
                    $scope. property_show = 'Y';
                }
                if (bankname != -1) {
                    $scope. bankname_show = 'Y';
                }
                if (credittypeoffacility != -1) {
                    $scope. credittypefacility_show = 'Y';
                }
                if (creditexistingfunded != -1) {
                    $scope. existingfunded_show = 'Y';
                }
                if (creditinstallmentfrequency != -1) {
                    $scope. creditinstallment_show = 'Y';
                }
                if (creditacctclassification != -1) {
                    $scope. creditacctclassification_show = 'Y';
                }
                if (credittype != -1) {
                    $scope. Credittype_show = 'Y';
                }
                if (colending != -1) {
                    $scope. colending_show = 'Y';
                }
                if (entity != -1) {
                    $scope. entity_show = 'Y';
                }
                if (groupdocument != -1) {
                    $scope.groupdocument_show = 'Y';
                }
                if (vertical != -1) {
                    $scope. Vertical_show = 'Y';
                }
                if (verticaltags != -1) {
                    $scope. VerticalTags_show = 'Y';
                }
                if (samunnatibranchname != -1) {
                    $scope. samunnatibranch_show = 'Y';
                }
                if (samunnatibranchstate != -1) {
                    $scope. samunnatistate_show = 'Y';
                }
                if (creditpolicycompliance != -1) {
                    $scope. creditpolicycompliance_show = 'Y';
                }
                if (vernacularlanguage != -1) {
                    $scope. vernacularlanguage_show = 'Y';
                }
                if (lendertype != -1) {
                    $scope. lendertype_show = 'Y';
                }
                if (Creditunderwritingfacilitytype != -1) {
                    $scope. Creditunderwritingfacilitytype_show = 'Y';
                }
                if (bsrcode != -1) {
                    $scope.bsrcode_show = 'Y';
                }
                if (lineofactivity != -1) {
                    $scope.lineofactivity_show = 'Y';
                }
                if (msme != -1) {
                    $scope.msme_show = 'Y';
                }
                if (natureofentity != -1) {
                    $scope.natureofentity_show = 'Y';
                }
                if (pslcategory != -1) {
                    $scope.pslcategory_show = 'Y';
                }
                if (pslpurpose != -1) {
                    $scope.pslpurpose_show = 'Y';
                }
                if (turnover != -1) {
                    $scope.turnover_show = 'Y';
                }
                if (clietdetails != -1) {
                    $scope.clietdetails_show = 'Y';
                }
                if (weakersection != -1) {
                    $scope.weakersection_show = 'Y';
                }
                if (investment != -1) {
                    $scope.investment_show = 'Y';
                }
                if (purposecolumn != -1) {
                    $scope.purposecolumn_show = 'Y';
                }

                if (Constitution != -1) {
                    $scope. Constitution_show = 'Y';
                }
                if (Buyer != -1) {
                    $scope.Buyer_show = 'Y';
                }
                if (Supplier != -1) {
                    $scope.Supplier_show = 'Y';
                }
                if (ApplicationCreation != -1) {
                    $scope.ApplicationCreation_show = 'Y';
                }
                if (AddressType != -1) {
                    $scope.AddressType_show = 'Y';
                }
               
                if (AssessmentCriteria != -1) {
                    $scope.assessmentcriteria_show = 'Y';
                }
                if (ClusterHead != -1) {
                    $scope.ClusterHead_show = 'Y';
                }
                if (ClusterMapping != -1) {
                    $scope.ClusterMapping_show = 'Y';
                }
                if (ZonalHead != -1) {
                    $scope.ZonalHead_show = 'Y';
                }
                if (ZoneMapping != -1) {
                    $scope.ZoneMapping_show = 'Y';
                }
                if (RegionHead != -1) {
                    $scope.RegionHead_show = 'Y';
                }
                if (RegionMapping != -1) {
                    $scope.RegionMapping_show = 'Y';
                }
                if (BusinessHead != -1) {
                    $scope.BusinessHead_show = 'Y';
                }
                if (GroupBusinessHead != -1) {
                    $scope.GroupBusinessHead_show = 'Y';
                }
                if (ProductHead != -1) {
                    $scope.ProductHead_show = 'Y';
                }
                if (ScourceofContact != -1) {
                    $scope.ScourceofContact_show = 'Y';
                }
                if (CallType != -1) {
                    $scope.CallType_show = 'Y';
                }
                if (BusinessApproval != -1) {
                    $scope.BusinessApproval_show = 'Y';
                }
                if (TeleCallingFunction != -1) {
                    $scope.TeleCallingFunction_show = 'Y';
                }
                if (CallReceivedNumber != -1) {
                    $scope.CallReceivedNumber_show = 'Y';
                }
                if (visitormgmt != -1) {
                    $scope.visitormgmt_show = 'Y';
                }
                if (creditmapping != -1) {
                    $scope.creditmapping_show = 'Y';
                }
                if (Product != -1) {
                    $scope.Product_show = 'Y';
                }
                if (Category != -1) {
                    $scope.Category_show = 'Y';
                }
                if (CADGroup != -1) {
                    $scope.CADGroup_show = 'Y';
                }
                if (CADGroupAssignment != -1) {
                    $scope.CADGroupAssignment_show = 'Y';
                }
                if (CreditAllocationReport != -1) {
                    $scope.CreditAllocationReport_show = 'Y';
                }
                if (DocumentType != -1) {
                    $scope.documenttype_show = 'Y';
                }


                if (documentseverity != -1) {
                    $scope.documentseverity_show = 'Y';
                }
                if (saonboarding != -1) {
                    $scope.saonboarding_show = 'Y';
                }
                if (businessverify != -1) {
                    $scope.businessverify_show = 'Y';
                }
                if (businessreg != -1) {
                    $scope.businessreg_show = 'Y';
                }
                if (ccreport != -1) {
                    $scope.ccreport_show = 'Y';
                }
                if (VisitorManagemenReport != -1) {
                    $scope.VisitorManagemenReport_show = 'Y';
                }
                if (SanctionmisReport != -1) {
                    $scope.SanctionmisReport_show = 'Y';
                }
                if (BuyerReport != -1) {
                    $scope.BuyerReport_show = 'Y';
                }
                if (creditopsmapping != -1) {
                    $scope.creditopsmapping_show = 'Y';
                }
                if (csamanagement != -1) {
                    $scope.csamanagement_show = 'Y';
                }
                if (colendingverification != -1) {
                    $scope.colendingverification_show = 'Y';
                }
                if (sanctionwaiver != -1) {
                    $scope.sanctionwaiver_show = 'Y';
                }
                if (LANWaiver != -1) {
                    $scope.LANWaiver_show = 'Y';
                }
                if (groupwaiver != -1) {
                    $scope.groupwaiver_show = 'Y';
                }
                if (digitalsignature != -1) {
                    $scope.digitalsignature_show = 'Y';
                }
                if (PhysicalStatus != -1) {
                    $scope.PhysicalStatus_show = 'Y';
                }
                if (Salutation != -1) {
                    $scope.Salutation_show = 'Y';
                }
                if (Busiunessrevoke != -1) {
                    $scope.Busiunessrevoke_show = 'Y';
                }
                if (Creditrevoke != -1) {
                    $scope.Creditrevoke_show = 'Y';
                }
                if (hierarchy != -1) {
                    $scope.hierarchy_show = 'Y';
                }
                if (maker != -1) {
                    $scope.maker_show = 'Y';
                }
                if (Creditgroup != -1) {
                    $scope.Creditgroup_show = 'Y';
                }
                if (Answertype != -1) {
                    $scope.Answertype_show = 'Y';
                }
                if (Grouptitle != -1) {
                    $scope.Grouptitle_show = 'Y';
                }
                if (internalrating != -1) {
                    $scope.internalrating_show = 'Y';
                }
                if (covenantperiod != -1) {
                    $scope.covenantperiod_show = 'Y';
                }
                if (livestock != -1) {
                    $scope.livestock_show = 'Y';
                }
                if (equipment != -1) {
                    $scope.equipment_show = 'Y';
                }
                if (csacategory != -1) {
                    $scope.csacategory_show = 'Y';
                }
                if (csaclassification != -1) {
                    $scope.csaclassification_show = 'Y';
                }
                if (guaranteeprograms != -1) {
                    $scope.guaranteeprograms_show = 'Y';
                }
                if (colendingcategory != -1) {
                    $scope.colendingcategory_show = 'Y';
                }
                if (colendingprograms != -1) {
                    $scope.colendingprograms_show = 'Y';
                }
                if (fieldmapping != -1) {
                    $scope.fieldmapping_show = 'Y';
                }
                if (Couriercompany != -1) {
                    $scope.Couriercompany_show = 'Y';
                }
                if (Answertype != -1) {
                    $scope.Answertype_show = 'Y';
                }
                if (GroupTitle != -1) {
                    $scope.GroupTitle_show = 'Y';
                }
            });
            
        }
    }
})();
