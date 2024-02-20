(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprGeneralTabController', AgrMstSuprGeneralTabController);

    AgrMstSuprGeneralTabController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AgrMstSuprGeneralTabController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprGeneralTabController';
        lockUI();
        activate();
        function activate() {
            var lstab = $location.search().lstab;
            $scope.application_gid = $location.search().lsapplication_gid;
            var application_gid = $scope.application_gid;
            var lsstatus = $location.search().lsstatus;

            $scope.SA_yes = true;
            $scope.Onboarding_Proposal = true;
            $scope.application_status=false;
        if (lstab == 'add') {
            $scope.hide_generalsummary = false;
            $scope.show_generalform = false;
        }
        else {
            $scope.hide_generalsummary = true;
            $scope.show_generalform = true;
        }
            var url = 'api/AgrMstSuprApplicationAdd/GetDropDown';
               SocketService.get(url).then(function (resp) {
                   $scope.vertical_list = resp.data.vertical_list;
                   $scope.verticaltaggs_list = resp.data.verticaltaggs_list;
                   $scope.vernacularlang_list = resp.data.vernacularlang_list;
                   $scope.constitutionlist = resp.data.constitutionlist;
                   $scope.businessunitlist = resp.data.businessunitlist;
                   $scope.valuechainlist = resp.data.valuechainlist;
                   $scope.designationlist = resp.data.designationlist;
                   $scope.creditgrouplist = resp.data.creditgrouplist;
                   $scope.program_list = resp.data.program_list;
                   $scope.productname_list = resp.data.productname_list;
                   }); 
                var url = 'api/AgrMstSuprApplicationAdd/GetGeneticCodeList';
              SocketService.get(url).then(function (resp) {
                $scope.genetic_list = resp.data.genetic_list;
              });

              var url = 'api/AgrMstSuprApplicationAdd/GetGeneralInfo';
              SocketService.get(url).then(function (resp) {
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.lblvertical_name = resp.data.vertical_name;
                $scope.lblcustomer_urn = resp.data.customer_urn;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblcreated_date = resp.data.created_date;
                $scope.application_gid = resp.data.application_gid;
                $scope.application_status = resp.data.application_status;
                $scope.applicant_type = resp.data.applicant_type;
                $scope.product_gid = resp.data.product_gid;
                $scope.variety_gid = resp.data.variety_gid;
                });

                var url = 'api/AgrMstSuprApplicationAdd/GetTempApp';
                SocketService.get(url).then(function (resp) {
                   
                });

                var url = 'api/AgrMstSuprApplicationAdd/GetIndividualSummary';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.individual_list = resp.data.cicindividual_list;
                });

                var url = 'api/AgrMstSuprApplicationAdd/GetInstitutionSummary';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.institution_list = resp.data.cicinstitution_list;
                });

                
                var url = 'api/AgrMstSuprApplicationAdd/GetGroupSummary';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.group_list = resp.data.group_list;
                });

                var url = 'api/AgrMstSuprApplicationAdd/GetAppSocialTradeSummary';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.application_no = resp.data.application_no;
                    $scope.social_capital = resp.data.social_capital;
                    $scope.trade_capital = resp.data.trade_capital;
                    $scope.application_gid = resp.data.application_gid;
                    $scope.created_date = resp.data.created_date;
                    $scope.created_by = resp.data.created_by;
                    $scope.lblcreated_date = resp.data.created_date;
                    $scope.lblcreated_by = resp.data.created_by;
                    $scope.updated_date = resp.data.updated_date;
                    $scope.updated_by = resp.data.updated_by;
                });
                var url = 'api/AgrMstSuprApplicationAdd/GetAppProductcharges';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                    $scope.lblprocessing_fee = resp.data.processing_fee;
                    $scope.lbldoc_charges = resp.data.doc_charges;
                    $scope.application_gid = resp.data.application_gid;
                    $scope.applicant_type = resp.data.applicant_type;

                    $scope.productcharge_flag = resp.data.productcharge_flag;
                    $scope.lblproductcharges_status = resp.data.productcharges_status;
                    $scope.economical_flag = resp.data.economical_flag;

                    if ($scope.economical_flag == 'Y') {
                        $scope.social_tradetab = false;
                        $scope.social_trade = true;

                    }
                    else {
                        $scope.social_tradetab = true;
                        $scope.social_trade = false;
                    }
                });

                var url = 'api/AgrMstSuprApplicationAdd/GetAddHypothecation';
                
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.security_type = resp.data.security_type;
                    $scope.security_value = resp.data.security_value;
                    $scope.securityassessed_date = resp.data.securityassessed_date;
                    $scope.asset_id = resp.data.asset_id;
                    $scope.created_by = resp.data.created_by;
                    $scope.created_date = resp.data.created_date;
                });
            
                $scope.rdbStatus = "No";

                var url = 'api/AgrMstSuprApplicationAdd/GetProceed';
                SocketService.get(url).then(function (resp) {
                    $scope.proceed_flag = resp.data.proceed_flag;
                    $scope.level_zero = resp.data.level_zero;
                    $scope.level_one = resp.data.level_one;
                    $scope.clusterhead = resp.data.cluster_head;
                    $scope.zonalhead = resp.data.zonal_head;
                    $scope.regionhead = resp.data.regional_head;
                    $scope.businesshead = resp.data.business_head;
                    $scope.onboarding_status = resp.data.onboarding_status;
                    unlockUI();                   
                });
                var proceed_flag = $scope.proceed_flag;
                var application_gid = $scope.application_gid;
                var params = {
                    application_gid: application_gid
                }

                var url = 'api/AgrMstSuprApplicationAdd/GetApprovalHierarchyFlag';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                    $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                    if ($scope.hierarchyupdated_flag == 'N' && proceed_flag == 'Y') {
                        $scope.hierarchyshow = true;
                        $scope.done_disable = true;
                        $scope.done_enable = false;
                    }
                    else if ($scope.hierarchyupdated_flag == 'Y' && proceed_flag == 'Y') {
                        $scope.done_enable = true;
                        $scope.hierarchyshow = false;
                        $scope.done_disable = false;
                    }
                    else if (proceed_flag == 'N') {
                        $scope.done_disable = true;
                        $scope.hierarchyshow = false;
                        $scope.done_enable = false;
                    }
                    else {

                    }
                });
        }

        $scope.productname_change = function (cboproduct_name) {
            var params = {
                product_gid: $scope.cboproduct_name.product_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetSectorcategory';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.businessunit_gid = resp.data.businessunit_gid;
                $scope.txtsector_name = resp.data.businessunit_name;
                $scope.valuechain_gid = resp.data.valuechain_gid;
                $scope.txtcategory_name = resp.data.valuechain_name;
                $scope.varietyname_list = resp.data.varietyname_list;
            });
            $scope.txtbotanical_name = '';
            $scope.txtalternative_name = '';
        }

        $scope.Variety_change = function (cbovariety_name) {
            var params = {
                variety_gid: $scope.cbovariety_name.variety_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetVarietyDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.product_gid = resp.data.product_gid;
                $scope.variety_gid = resp.data.variety_gid;
                $scope.variety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txthsn_code = resp.data.hsn_code;
            });            
        }

        $scope.OnchangeVertical = function (cbovertical) {
            var params = {
                vertical_gid: cbovertical.vertical_gid,
                lstype: '',
                lstypegid: ''
            }
            var url = 'api/SystemMaster/GetVerticalProgramList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.program_list = resp.data.program_list;
                unlockUI();
            });
        }

        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
            {
                $location.url('app/AgrMstSuprApplicationGeneralAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls=true;
                }
            }

        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
            {
                $location.url('app/AgrMstSuprApplicationInstitutionAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls=true;
                }
            }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/AgrMstSuprApplicationIndividualAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
                else {
                    $scope.Individual_dtls=true;
                    }
                }

        $scope.Group_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
                if ($scope.application_status=='Completed')
                    {
                        $location.url('app/AgrMstSuprApplicationGroupAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                    }
                    else {
                        $scope.Group_dtls=true;
                    }
                }

        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/AgrMstSuprApplicationSocialTradeCapitalAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
                else {
                    $scope.EconomicCapital_dtls=true;
                }
            }

        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.OverallLimit_dtls=true;
                }
                else {                    
                    $location.url('app/AgrMstSuprApplicationOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.ProductCharges_dtls=true;
                }
                else {                    
                    $location.url('app/AgrMstSuprApplicationProductChargesAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.ServiceCharges_dtls=true;
                }
                else {                    
                    $location.url('app/AgrMstSuprApplicationServiceChargeAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.Hypothecation_dtls=true;
                }
                else {                    
                    $location.url('app/AgrMstSuprApplicationHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/AgrMstSuprApplicationCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
            else {
                 $scope.BureauUpdates_dtls=true;
                }
            }

        $scope.doneclick = function () {
            lockUI();
            var url = 'api/AgrMstSuprApplicationAdd/GetGeneralInfo';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.application_status = resp.data.application_status;
            });
            lockUI();
            var url = 'api/AgrMstSuprApplicationAdd/GetProceed';
            SocketService.get(url).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.onboarding_status = resp.data.onboarding_status;
                unlockUI();               
            });
            var proceed_flag = $scope.proceed_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstSuprApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                }
                else if ($scope.hierarchyupdated_flag == 'Y' && proceed_flag == 'Y') {
                    $scope.done_enable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N') {
                    $scope.done_disable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_enable = false;
                }
                else {
                   
                }
            });
            
        }

        $scope.Back = function () {
            $state.go('app.AgrMstSuprApplicationCreationSummary');
        }
        $scope.overallsubmit_application = function () {

            var params = {

            }
            var url = 'api/AgrMstSuprApplicationAdd/PostAppProceed';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.AgrMstSuprApplicationCreationSummary');
            });

        }

        $scope.onselectedsa_yes = function () {
            if ($scope.rdbassociate == 'Yes') {
                var url = 'api/AgrMstApplication360/GetAssociateMasterASC';
                SocketService.get(url).then(function (resp) {
                    $scope.associatemaster_list = resp.data.associatemaster_list;
                });
                $scope.SA_yes = true;
            }
            else {
                $scope.SA_yes = false;
                $scope.cbosa_idname = '';
                $scope.txtsa_name = '';
            }
        }
    
        $scope.onselected_Onboarding = function () {
            if ($scope.rdbOnboarding == 'Proposal') { 
                $scope.Onboarding_Proposal = true;
                $scope.cboCredit_Group = ''; 
            }
            else {
                $scope.Onboarding_Proposal = false; 
                $scope.cboCredit_Group = 'IT Internal Use - Do not select this';
                $scope.cbocreditgroup = "";
            }
        }

        $scope.generalmobileno_add = function () {
            if (($scope.txtgeneralmobileno == undefined) || ($scope.txtgeneralmobileno == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbprimarywhatsapp_no == undefined) || ($scope.rdbprimarymobile_no == '') || ($scope.rdbprimarywhatsapp_no == '')) {
                Notify.alert('Enter Mobile Number / Select Primary Status','warning');
            }
            else if ($scope.txtgeneralmobileno.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtgeneralmobileno,
                    primary_mobileno: $scope.rdbprimarymobile_no,
                    whatsapp_mobileno: $scope.rdbprimarywhatsapp_no
                }
                 var url = 'api/AgrMstSuprApplicationAdd/PostMobileNo';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtgeneralmobileno = '';
                    $scope.rdbprimarymobile_no =''
                    $scope.rdbprimarymobile_no ==false;
                    $scope.rdbprimarywhatsapp_no='';
                    generalmobilenolist();
                });
            }
        }
        
        function generalmobilenolist() {

            var url = 'api/AgrMstSuprApplicationAdd/GetAppMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.generalmobileno_list = resp.data.mstmobileno_list;
            });
        }

        $scope.generalmobileno_delete = function (application2contact_gid) {
            var params =
                {
                    application2contact_gid: application2contact_gid
                }
            var url = 'api/AgrMstSuprApplicationAdd/DeleteMobileNo';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                generalmobilenolist();
            });

        }
       
        $scope.add_generalmaildetails = function () {
            if (($scope.txtgeneralmail_id == undefined) || ($scope.txtgeneralmail_id == '') || ($scope.rdbgeneralmaildetails == undefined) || ($scope.rdbgeneralmaildetails == '')) {
                Notify.alert('Enter Mail ID / Select Primary Status','warning');
            }
            else {
               var params = {
                   email_address: $scope.txtgeneralmail_id,
                   primary_emailaddress: $scope.rdbgeneralmaildetails
                }
                var url = 'api/AgrMstSuprApplicationAdd/PostEmailAddress';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.txtgeneralmail_id = '';
                    $scope.rdbgeneralmaildetails ='';
                    generalmail_list();
                }); 
            }
        }
     
        function generalmail_list() {
            var url = 'api/AgrMstSuprApplicationAdd/GetAppEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.generalmaildetails_list = resp.data.mstemailaddress_list;
            });
        }  
        
        $scope.generalmail_delete = function (application2email_gid) {
            var params =
                {
                    application2email_gid: application2email_gid
                }
            var url = 'api/AgrMstSuprApplicationAdd/DeleteEmailAddress';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                generalmail_list();
            });

        }

        $scope.addgeneticcode = function () {
            if (($scope.cboGeneticCode == undefined) || ($scope.rdbStatus == undefined) || ($scope.txtgenetic_remarks == undefined) || ($scope.cboGeneticCode == null) || ($scope.rdbStatus == null) || ($scope.txtgenetic_remarks == null) || ($scope.cboGeneticCode == '') || ($scope.rdbStatus == '') || ($scope.txtgenetic_remarks == '')) {
                Notify.alert('Select Genetic Code / Select Status / Enter Genetic Code Remarks','warning');
            }
            else {
               var params = {
                   geneticcode_gid: $scope.cboGeneticCode.geneticcode_gid,
                   geneticcode_name: $scope.cboGeneticCode.geneticcode_name,
                    genetic_status: $scope.rdbStatus,
                    genetic_remarks: $scope.txtgenetic_remarks
               }
                  var url = 'api/AgrMstSuprApplicationAdd/PostGeneticCode';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.mstgeneticcode_list = resp.data.mstgeneticcode_list;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    $scope.cboGeneticCode = '';
                    $scope.txtgenetic_remarks = '';
                   
                }); 
            }
        }
        $scope.geneticcode_delete = function (application2geneticcode_gid) {
            var params = {
                application2geneticcode_gid: application2geneticcode_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/DeleteGenetic';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.mstgeneticcode_list = resp.data.mstgeneticcode_list;
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }


            });
        }
          

        $scope.Save_generaldetails = function () {
            var lsvertical_gid = '';
            var lsvertical_name = '';
            var lsverticaltaggs_gid = '';
            var lsverticaltaggs_name = '';
            var lsconstitution_gid = '';
            var lsconstitution_name = '';
            var lsbusinessunit_gid = '';
            var lsbusinessunit_name = '';
            var lsname = '';
            var lsassociatemaster_gid = '';
            var lsdesignation_gid = '';
            var lsdesignation_type = '';
            var lscreditgroup_name = '';
            var lscreditgroup_gid = '';
            var lsprogram_name = '';
            var lsprogram_gid = '';
            var lsproduct_name = '';
            var lsproduct_gid = '';
            var lsvariety_name = '';
            var lsvariety_gid = '';

            if ($scope.cbovertical!=undefined|| $scope.cbovertical!=null)
            {
                 lsvertical_gid= $scope.cbovertical.vertical_gid;
                 lsvertical_name = $scope.cbovertical.vertical_name;
            }
            /* if ($scope.cbovertical_tag != undefined || $scope.cbovertical_tag != null) {
                 lsverticaltaggs_gid= $scope.cbovertical_tag.verticaltaggs_gid;
                 lsverticaltaggs_name = $scope.cbovertical_tag.verticaltaggs_name;
            } */
            if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                 lsconstitution_gid = $scope.cboConstitution.constitution_gid;
                 lsconstitution_name = $scope.cboConstitution.constitution_name;
            }
            //if ($scope.cboStrategicBusinessUnitSector != undefined || $scope.cboStrategicBusinessUnitSector != null) {
            //     lsbusinessunit_gid = $scope.cboStrategicBusinessUnitSector.businessunit_gid;
            //     lsbusinessunit_name = $scope.cboStrategicBusinessUnitSector.businessunit_name;
            //}
        /*    if ($scope.cboVernacularLanguage != undefined || $scope.cboVernacularLanguage != null) {
                 lsvernacularlanguage_gid = $scope.cboVernacularLanguage.vernacularlanguage_gid;
                 lsvernacular_language = $scope.cboVernacularLanguage.vernacular_language;
            } */
            if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                 lsdesignation_gid = $scope.cboDesignation.designation_gid;
                 lsdesignation_type = $scope.cboDesignation.designation_type;
            }
            if ($scope.cbocreditgroup != undefined || $scope.cbocreditgroup != null) {
                lscreditgroup_name = $scope.cbocreditgroup.creditgroup_name;
                lscreditgroup_gid = $scope.cbocreditgroup.creditmapping_gid;
            }
            if ($scope.cboprogram != undefined || $scope.cboprogram != null) {
                lsprogram_name = $scope.cboprogram.program;
                lsprogram_gid = $scope.cboprogram.program_gid;
            }
            if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                lsproduct_name = $scope.cboproduct_name.product_name;
                lsproduct_gid = $scope.cboproduct_name.product_gid;
            }
            if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                lsvariety_name = $scope.cbovariety_name.variety_name;
                lsvariety_gid = $scope.cbovariety_name.variety_gid;
            }
             var params = {
                customer_urn: $scope.txtcustomer_URN,
                customer_name: $scope.txtcustomer_name,
                vertical_gid: lsvertical_gid,
                vertical_name: lsvertical_name,
               /*  verticaltaggs_gid: lsverticaltaggs_gid,
                verticaltaggs_name: lsverticaltaggs_name, */
                constitution_gid: lsconstitution_gid,
                constitution_name: lsconstitution_name,
                //businessunit_gid: lsbusinessunit_gid,
                //businessunit_name: lsbusinessunit_name,
                //primaryvaluechain_list: $scope.$parent.cboprimaryvalue_chain,
                //secondaryvaluechain_list: $scope.$parent.cbosecondaryvalue_chain,
                sa_status: $scope.rdbassociate,
                saname_gid: lsassociatemaster_gid,
                sa_name: lsname,
               /* vernacular_language: lsvernacularlanguage_gid,
                vernacularlanguage_gid: lsvernacular_language, */
                vernacularlanguage_list: $scope.cboVernacularLanguage,
                contactpersonfirst_name: $scope.txtfirst_name,
                contactpersonmiddle_name: $scope.txtmiddle_name,
                contactpersonlast_name: $scope.txtlast_name,
                designation_gid: lsdesignation_gid,
                designation_type: lsdesignation_type,
                landline_no: $scope.txtlandline_no,
                creditgroup_gid: lscreditgroup_gid,
                creditgroup_name: lscreditgroup_name,
                program_gid: lsprogram_gid,
                program_name: lsprogram_name,
                product_gid: lsproduct_gid,
                product_name: lsproduct_name,
                variety_gid: lsvariety_gid,
                variety_name: lsvariety_name,
                sector_name: $scope.txtsector_name,
                category_name: $scope.txtcategory_name,
                botanical_name: $scope.txtbotanical_name,
                alternative_name: $scope.txtalternative_name,
                onboarding_status: $scope.rdbOnboarding
             }
             var url = 'api/AgrMstSuprApplicationAdd/SaveGeneralDtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                     Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                     });
                     $scope.hide_generalsummary = false;
                     $scope.show_generalform = false;
                     
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=add');
            });
           
        }
        function overallinfo()
        {
            var url = 'api/AgrMstSuprApplicationAdd/GetGeneralInfo';
            SocketService.get(url).then(function (resp) {
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.lblvertical_name = resp.data.vertical_name;
                $scope.lblcustomer_urn = resp.data.customer_urn;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblcreated_date = resp.data.created_date;
                $scope.application_gid = resp.data.application_gid;
                $scope.application_status = resp.data.application_status
                  });
        }
        $scope.Submitgeneraldetails=function()
        {

            if (($scope.cboVernacularLanguage == undefined) || ($scope.cboVernacularLanguage == '' ) || ($scope.cboVernacularLanguage == null )) {

                Notify.alert('Select Vernacular Language ', 'warning');              
            }
            else if (($scope.rdbassociate == 'Yes') && (($scope.cbosa_idname == undefined) || ($scope.cbosa_idname == '' ) || ($scope.cbosa_idname == null ))){
                Notify.alert('Kindly Add SAM Associate ID / Name  ', 'warning');  
            }
            //else if ($scope.mstproduct_list == null || $scope.mstproduct_list == undefined || $scope.mstproduct_list == "") {
            //    Notify.alert('Atleast One Record should be added in Product Details', 'warning')
            //}
            else {
            var lsvertical_gid = '';
            var lsvertical_name = '';
            var lsverticaltaggs_gid = '';
            var lsverticaltaggs_name = '';
            var lsconstitution_gid = '';
            var lsconstitution_name = '';
            var lsbusinessunit_gid = '';
            var lsbusinessunit_name = '';
            var lsname = '';
            var lsassociatemaster_gid = '';
            var lsdesignation_gid = '';
            var lsdesignation_type = '';
            var lscreditgroup_name = '';
            var lscreditgroup_gid = '';
            var lsprogram_name = '';
            var lsprogram_gid = '';
            var lsproduct_name = '';
            var lsproduct_gid = '';
            var lsvariety_name = '';
            var lsvariety_gid = '';

            if ($scope.cbovertical != undefined || $scope.cbovertical != null) {
                lsvertical_gid = $scope.cbovertical.vertical_gid;
                lsvertical_name = $scope.cbovertical.vertical_name;
            }
            /* if ($scope.cbovertical_tag != undefined || $scope.cbovertical_tag != null) {
                lsverticaltaggs_gid = $scope.cbovertical_tag.verticaltaggs_gid;
                lsverticaltaggs_name = $scope.cbovertical_tag.verticaltaggs_name;
            } */
            if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                lsconstitution_gid = $scope.cboConstitution.constitution_gid;
                lsconstitution_name = $scope.cboConstitution.constitution_name;
            }
            //if ($scope.cboStrategicBusinessUnitSector != undefined || $scope.cboStrategicBusinessUnitSector != null) {
            //    lsbusinessunit_gid = $scope.cboStrategicBusinessUnitSector.businessunit_gid;
            //    lsbusinessunit_name = $scope.cboStrategicBusinessUnitSector.businessunit_name;
            //}
            //if ($scope.cboVernacularLanguage != undefined || $scope.cboVernacularLanguage != null) {
            //    lsvernacularlanguage_gid = $scope.cboVernacularLanguage.vernacularlanguage_gid;
            //    lsvernacular_language = $scope.cboVernacularLanguage.vernacular_language;
            //}
            if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                lsdesignation_gid = $scope.cboDesignation.designation_gid;
                lsdesignation_type = $scope.cboDesignation.designation_type;
            }
            if ($scope.cbosa_idname != undefined || $scope.cbosa_idname != null) {
                lsname = $scope.cbosa_idname.name;
                lsassociatemaster_gid = $scope.cbosa_idname.associatemaster_gid;
            }
            if ($scope.cbocreditgroup != undefined || $scope.cbocreditgroup != null) {
                lscreditgroup_name = $scope.cbocreditgroup.creditgroup_name;
                lscreditgroup_gid = $scope.cbocreditgroup.creditmapping_gid;
            }
            if ($scope.cboprogram != undefined || $scope.cboprogram != null) {
                lsprogram_name = $scope.cboprogram.program;
                lsprogram_gid = $scope.cboprogram.program_gid;
            }
            if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                lsproduct_name = $scope.cboproduct_name.product_name;
                lsproduct_gid = $scope.cboproduct_name.product_gid;
            }
            if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                lsvariety_name = $scope.cbovariety_name.variety_name;
                lsvariety_gid = $scope.cbovariety_name.variety_gid;
            }
            var params = {
                customer_urn: $scope.txtcustomer_URN,
                customer_name: $scope.txtcustomer_name,
                vertical_gid: lsvertical_gid,
                vertical_name: lsvertical_name,
               /*  verticaltaggs_gid: lsverticaltaggs_gid,
                verticaltaggs_name: lsverticaltaggs_name, */
                constitution_gid: lsconstitution_gid,
                constitution_name: lsconstitution_name,
                //businessunit_gid: lsbusinessunit_gid,
                //businessunit_name: lsbusinessunit_name,
                primaryvaluechain_list: $scope.$parent.cboprimaryvalue_chain,
                secondaryvaluechain_list: $scope.$parent.cbosecondaryvalue_chain,
                sa_status: $scope.rdbassociate,
                saname_gid: lsassociatemaster_gid,
                sa_name: lsname,
                //vernacular_language: lsvernacularlanguage_gid,
                //vernacularlanguage_gid: lsvernacular_language,
                vernacularlanguage_list: $scope.cboVernacularLanguage,
                contactpersonfirst_name: $scope.txtfirst_name,
                contactpersonmiddle_name: $scope.txtmiddle_name,
                contactpersonlast_name: $scope.txtlast_name,
                designation_gid: lsdesignation_gid,
                designation_type: lsdesignation_type,
                landline_no: $scope.txtlandline_no,
                creditgroup_gid: lscreditgroup_gid,
                creditgroup_name: lscreditgroup_name,
                program_gid: lsprogram_gid,
                program_name: lsprogram_name,
                product_gid: lsproduct_gid,
                product_name: lsproduct_name,
                variety_gid: lsvariety_gid,
                variety_name: lsvariety_name,
                sector_name: $scope.txtsector_name,
                category_name: $scope.txtcategory_name,
                botanical_name: $scope.txtbotanical_name,
                alternative_name: $scope.txtalternative_name,
                onboarding_status: $scope.rdbOnboarding
            }
            var url = 'api/AgrMstSuprApplicationAdd/SubmitGeneralDtl';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    Notify.alert('General Information Submitted Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    window.scrollTo(0, 0);
                    $scope.hide_generalsummary = false;
                    $scope.show_generalform = false;
                    $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=add');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                
            });

        }
        }
        $scope.DeleteGeneral=function(application_gid)
        {
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/DeleteGeneral';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                  
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.txtcustomer_URN = '';
                    $scope.txtcustomer_name = '';
                    $scope.cbovertical = '';
                    $scope.cbovertical_tag = '';
                    $scope.cboConstitution = '';
                    $scope.cboStrategicBusinessUnitSector = '';
                    $scope.cboprimaryvalue_chain = '';
                    $scope.cbosecondaryvalue_chain = '';
                    $scope.rdbassociate = '';
                    $scope.cbosa_idname = '';
                    $scope.cbosa_idname = '';
                    $scope.cboVernacularLanguage = '';
                    $scope.txtfirst_name = '';
                    $scope.txtmiddle_name = '';
                    $scope.txtlast_name = '';
                    $scope.cboDesignation = '';
                    $scope.txtlandline_no = '';
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                $scope.hide_generalsummary = true;
                $scope.show_generalform = true;
            });
        }
        $scope.Editgeneral = function (application_gid, application_status, product_gid, variety_gid)
        {
            $location.url('app/AgrMstSuprApplcreationBasicdtlEdit?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }
        $scope.Editindividual = function (contact_gid) {
            $location.url('app/AgrMstSuprApplcreationIndividualdtlEdit?lscontact_gid=' + contact_gid + '&lstab=add');
             }
        $scope.Editinstitution = function (institution_gid) {
            $location.url('app/AgrMstSuprApplcreationInstitutiondtlEdit?lsinstitution_gid=' + institution_gid + '&lstab=add');
        }
        $scope.EditHypothecation = function (application_gid) {
            $location.url('app/AgrMstSuprApplicationHypothecationEdit?lsapplication_gid=' + application_gid + '&lstab=add');
        }
        $scope.Deleteindividual = function (contact_gid) {
            var params = {
                contact_gid: contact_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/Deleteindividual';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/AgrMstSuprApplicationAdd/GetIndividualSummary';
                    SocketService.get(url).then(function (resp) {
                        $scope.individual_list = resp.data.cicindividual_list;
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }


            });
        }
        $scope.DeleteInstitution = function (institution_gid) {
            var params = {
                institution_gid: institution_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/Deleteinstitution';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/AgrMstSuprApplicationAdd/GetCICInstitutionSummary';
                    SocketService.get(url).then(function (resp) {
                        $scope.institution_list = resp.data.cicinstitution_list;
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }


            });
        }
        $scope.Delete_Group = function (group_gid) {
            var params = {
                group_gid: group_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/DeleteGroup';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = 'api/AgrMstSuprApplicationAdd/GetGroupSummary';
                    SocketService.get(url).then(function (resp) {
                        unlockUI();
                        $scope.group_list = resp.data.group_list;
                    });

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }


            });
        }

        $scope.Editsocialtrade = function (application_gid) {
            $location.url('app/AgrMstSuprApplcreationSocialTradeEdit?lsapplication_gid=' + application_gid + '&lstab=add');
              }

        $scope.Editproductcharges = function (application_gid) {
            $location.url('app/AgrMstSuprApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lstab=add');
        }

        $scope.Editgroup = function (group_gid) {
            $location.url('app/AgrMstSuprApplcreationGroupdtlEdit?lsgroup_gid=' + group_gid + '&lstab=add');
        }

        $scope.edit_cicupload = function () {
            $location.url('app/AgrMstSuprApplcreationInstitutiondtlEdit?lsinstitution_gid=' + institution_gid + '&lstab=add');
            $state.go('app.AgrMstSuprApplcreationCICUploadEdit');
        }
        $scope.SubmitOverallLimit=function()
        {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_month,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_day,
                calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
               }
            
            var url = 'api/AgrMstSuprApplicationAdd/SubmitOverallLimit';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    overallinfo();
                    Notify.alert('General Information Submitted Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    window.scrollTo(0, 0);
                    $scope.hide_generalsummary = false;
                    $scope.show_generalform = false;
                    $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=add');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }

        $scope.addLoan = function () {
            if ($scope.txtfacilityreqon_date == null || $scope.txtfacilityreqon_date == '' || $scope.cboProductTypelist == null || $scope.cboProductTypelist == '' || $scope.cboProductSubTypelist == null || $scope.cboProductSubTypelist == '' || $scope.cboLoanTypelist == null || $scope.cboLoanTypelist == '' || $scope.txtloanfaility_amount == null || $scope.txtloanfaility_amount == '' || $scope.txtrate_interest == null || $scope.txtrate_interest == '' || $scope.txtpanel_interest == null || $scope.txtpanel_interest == '' || $scope.txttenure_year == null || $scope.txttenure_year == '' || $scope.txttenure_month == null || $scope.txttenure_month == '' || $scope.cboFacilityTypelist == null || $scope.cboFacilityTypelist == '' || $scope.txttenure_days == null || $scope.txttenure_days == '' || $scope.cboFacilitymodelist == null || $scope.cboFacilitymodelist == '') {
                Notify.alert('Kindly fill all mandatory values', 'warning');
            }
            else {
                if ($scope.rdbmoratorium_status == 'Yes') {
                    if ($scope.cbomoratorium_type == null || $scope.cbomoratorium_type == '' || $scope.txtmoratorium_startdate == null || $scope.txtmoratorium_startdate == '' || $scope.txtmoratorium_enddate == null || $scope.txtmoratorium_enddate == '') {
                        Notify.alert('Kindly fill Moratorium Details', 'warning');
                    }
                    else {
                        var lsloanproduct_name = '';
                        var lsloanproduct_gid = '';
                        var lsloansubproduct_name = '';
                        var lsloansubproduct_gid = '';
                        var lsloantype_gid = '';
                        var lsloan_type = '';
                        var lsprincipalfrequency_gid = '';
                        var lsprincipalfrequency_name = '';
                        var lsinterestfrequency_name = '';
                        var lsinterestfrequency_gid = '';

                        if ($scope.lsloanproduct_gid != undefined || $scope.cboProductTypelist != null) {
                            lsloanproduct_name = $scope.cboProductTypelist.loanproduct_name;
                            lsloanproduct_gid = $scope.cboProductTypelist.loanproduct_gid;
                        }
                        if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
                            lsloansubproduct_name = $scope.cboProductSubTypelist.loansubproduct_name;
                            lsloansubproduct_gid = $scope.cboProductSubTypelist.loansubproduct_gid;
                        }
                        if ($scope.cboLoanTypelist != undefined || $scope.cboLoanTypelist != null) {
                            lsloantype_gid = $scope.cboLoanTypelist.loantype_gid;
                            lsloan_type = $scope.cboLoanTypelist.loan_type;
                        }
                        if ($scope.cboprincipalfrequency != undefined || $scope.cboprincipalfrequency != null) {
                            lsprincipalfrequency_gid = $scope.cboprincipalfrequency.principalfrequency_gid;
                            lsprincipalfrequency_name = $scope.cboprincipalfrequency.principalfrequency_name;
                        }
                        if ($scope.cboInterestFrequency != undefined || $scope.cboInterestFrequency != null) {
                            lsinterestfrequency_name = $scope.cboInterestFrequency.interestfrequency_name;
                            lsinterestfrequency_gid = $scope.cboInterestFrequency.interestfrequency_gid;
                        }
                        var params = {
                            product_type: lsloanproduct_name,
                            producttype_gid: lsloanproduct_gid,
                            facilityrequested_date: $scope.txtfacilityreqon_date,
                            productsub_type: lsloansubproduct_name,
                            productsubtype_gid: lsloansubproduct_gid,
                            loantype_gid: lsloantype_gid,
                            loan_type: lsloan_type,
                            facilityloan_amount: $scope.txtloanfaility_amount,
                            rate_interest: $scope.txtrate_interest,
                            penal_interest: $scope.txtpanel_interest,
                            facilityvalidity_year: $scope.txtvalidity_year,
                            facilityvalidity_month: $scope.txtvalidity_month,
                            facilityvalidity_days: $scope.txtvalidity_days,
                            facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                            tenureproduct_year: $scope.txttenure_year,
                            tenureproduct_month: $scope.txttenure_month,
                            tenureproduct_days: $scope.txttenure_days,
                            tenureoverall_limit: $scope.txtoveralltenurevalidity_limit,
                            facility_type: $scope.cboFacilityTypelist,
                            facility_mode: $scope.cboFacilitymodelist,
                            principalfrequency_name: lsprincipalfrequency_name,
                            principalfrequency_gid: lsprincipalfrequency_gid,
                            interestfrequency_name: lsinterestfrequency_name,
                            interestfrequency_gid: lsinterestfrequency_gid,
                            interest_status: $scope.rdbinterest_status,
                            moratorium_status: $scope.rdbmoratorium_status,
                            moratorium_type: $scope.cbomoratorium_type,
                            moratorium_startdate: $scope.txtmoratorium_startdate,
                            moratorium_enddate: $scope.txtmoratorium_enddate,
                        }
                        var url = 'api/AgrMstSuprApplicationAdd/PostLoanDtl';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();

                            if (resp.data.status == true) {
                                $scope.mstloan_list = resp.data.mstloan_list;
                                $scope.collateral_status = resp.data.collateral_status;
                                $scope.buyer_status = resp.data.buyer_status;
                                Notify.alert(resp.data.message, {

                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $scope.cboProductTypelist = '';
                                $scope.txtfacilityreqon_date = '';
                                $scope.cboProductSubTypelist = '';
                                $scope.cboLoanTypelist = '';
                                $scope.txtloanfaility_amount = '';
                                $scope.txtrate_interest = '';
                                $scope.txtpanel_interest = '';
                                $scope.txtvalidity_year = '';
                                $scope.txtvalidity_month = '';
                                $scope.txtvalidity_days = '';
                                $scope.txtoverallfacilityvalidity_limit = '';
                                $scope.txttenure_year = '';
                                $scope.txttenure_month = '';
                                $scope.txttenure_days = '';
                                $scope.txtoveralltenurevalidity_limit = '';
                                $scope.cboFacilitymodelist = '';
                                document.getElementById('words_totalamount1').innerHTML = '';
                                $scope.cboprincipalfrequency = '';
                                $scope.cboInterestFrequency = '';
                                $scope.rdbinterest_status = '';
                                $scope.rdbmoratorium_status = '';
                                $scope.cbomoratorium_type = '';
                                $scope.txtmoratorium_startdate = '';
                                $scope.txtmoratorium_enddate = '';
                                $scope.cboFacilityTypelist = '';
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'info',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                        });
                    }
                }
                else {


                    var lsloanproduct_name = '';
                    var lsloanproduct_gid = '';
                    var lsloansubproduct_name = '';
                    var lsloansubproduct_gid = '';
                    var lsloantype_gid = '';
                    var lsloan_type = '';
                    var lsprincipalfrequency_gid = '';
                    var lsprincipalfrequency_name = '';
                    var lsinterestfrequency_name = '';
                    var lsinterestfrequency_gid = '';

                    if ($scope.lsloanproduct_gid != undefined || $scope.cboProductTypelist != null) {
                        lsloanproduct_name = $scope.cboProductTypelist.loanproduct_name;
                        lsloanproduct_gid = $scope.cboProductTypelist.loanproduct_gid;
                    }
                    if ($scope.cboProductSubTypelist != undefined || $scope.cboProductSubTypelist != null) {
                        lsloansubproduct_name = $scope.cboProductSubTypelist.loansubproduct_name;
                        lsloansubproduct_gid = $scope.cboProductSubTypelist.loansubproduct_gid;
                    }
                    if ($scope.cboLoanTypelist != undefined || $scope.cboLoanTypelist != null) {
                        lsloantype_gid = $scope.cboLoanTypelist.loantype_gid;
                        lsloan_type = $scope.cboLoanTypelist.loan_type;
                    }
                    if ($scope.cboprincipalfrequency != undefined || $scope.cboprincipalfrequency != null) {
                        lsprincipalfrequency_gid = $scope.cboprincipalfrequency.principalfrequency_gid;
                        lsprincipalfrequency_name = $scope.cboprincipalfrequency.principalfrequency_name;
                    }
                    if ($scope.cboInterestFrequency != undefined || $scope.cboInterestFrequency != null) {
                        lsinterestfrequency_name = $scope.cboInterestFrequency.interestfrequency_name;
                        lsinterestfrequency_gid = $scope.cboInterestFrequency.interestfrequency_gid;
                    }
                    var params = {
                        product_type: lsloanproduct_name,
                        producttype_gid: lsloanproduct_gid,
                        facilityrequested_date: $scope.txtfacilityreqon_date,
                        productsub_type: lsloansubproduct_name,
                        productsubtype_gid: lsloansubproduct_gid,
                        loantype_gid: lsloantype_gid,
                        loan_type: lsloan_type,
                        facilityloan_amount: $scope.txtloanfaility_amount,
                        rate_interest: $scope.txtrate_interest,
                        penal_interest: $scope.txtpanel_interest,
                        facilityvalidity_year: $scope.txtvalidity_year,
                        facilityvalidity_month: $scope.txtvalidity_month,
                        facilityvalidity_days: $scope.txtvalidity_days,
                        facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                        tenureproduct_year: $scope.txttenure_year,
                        tenureproduct_month: $scope.txttenure_month,
                        tenureproduct_days: $scope.txttenure_days,
                        tenureoverall_limit: $scope.txtoveralltenurevalidity_limit,
                        facility_type: $scope.cboFacilityTypelist,
                        facility_mode: $scope.cboFacilitymodelist,
                        principalfrequency_name: lsprincipalfrequency_name,
                        principalfrequency_gid: lsprincipalfrequency_gid,
                        interestfrequency_name: lsinterestfrequency_name,
                        interestfrequency_gid: lsinterestfrequency_gid,
                        interest_status: $scope.rdbinterest_status,
                        moratorium_status: $scope.rdbmoratorium_status,
                        moratorium_type: $scope.cbomoratorium_type,
                        moratorium_startdate: $scope.txtmoratorium_startdate,
                        moratorium_enddate: $scope.txtmoratorium_enddate,
                    }
                    var url = 'api/AgrMstSuprApplicationAdd/PostLoanDtl';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.mstloan_list = resp.data.mstloan_list;
                            $scope.collateral_status = resp.data.collateral_status;
                            $scope.buyer_status = resp.data.buyer_status;
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.cboProductTypelist = '';
                            $scope.txtfacilityreqon_date = '';
                            $scope.cboProductSubTypelist = '';
                            $scope.cboLoanTypelist = '';
                            $scope.txtloanfaility_amount = '';
                            $scope.txtrate_interest = '';
                            $scope.txtpanel_interest = '';
                            $scope.txtvalidity_year = '';
                            $scope.txtvalidity_month = '';
                            $scope.txtvalidity_days = '';
                            $scope.txtoverallfacilityvalidity_limit = '';
                            $scope.txttenure_year = '';
                            $scope.txttenure_month = '';
                            $scope.txttenure_days = '';
                            $scope.txtoveralltenurevalidity_limit = '';
                            $scope.cboFacilitymodelist = '';
                            document.getElementById('words_totalamount1').innerHTML = '';
                            $scope.cboprincipalfrequency = '';
                            $scope.cboInterestFrequency = '';
                            $scope.rdbinterest_status = '';
                            $scope.rdbmoratorium_status = '';
                            $scope.cbomoratorium_type = '';
                            $scope.txtmoratorium_startdate = '';
                            $scope.txtmoratorium_enddate = '';
                            $scope.cboFacilityTypelist = '';
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }
            }
        }
        $scope.uploadhypothecationdoc = function (val, val1, name) {
            if (($scope.cbohypodoc_title == null) || ($scope.cbohypodoc_title == '') || ($scope.cbohypodoc_title == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbohypodoc_title);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                var url = 'api/AgrMstSuprApplicationAdd/PostHypoDoc';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $("#file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.hypo_documentList = resp.data.DocumentList;
                        $scope.cbohypodoc_title = '';

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert('File Format Not Supported!', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                });
            }
        }
        $scope.hypodoccancel = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/AgrMstSuprApplicationAdd/deleteHypoDoc';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.hypo_documentList = resp.data.DocumentList;
                    angular.forEach($scope.hypo_documentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.hypo_documentList.splice(key, 1);
                        }
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'Warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }
        $scope.add_Hypothecation = function () {
            if ($scope.cboSecurityType == null || $scope.cboSecurityType == '') {
                Notify.alert('Kindly select security type', 'warning')
            }
            else {

                var params = {
                    securitytype_gid: $scope.cboSecurityType.securitytype_gid,
                    security_type: $scope.cboSecurityType.security_type,
                    security_description: $scope.txtsecurity_desc,
                    security_value: $scope.txtSecurity_Value,
                    securityassessed_date: $scope.txtSecurityAssessed_date,
                    asset_id: $scope.txtasset_id,
                    roc_fillingid: $scope.txtroc_fillingid,
                    CERSAI_fillingid: $scope.txtCERSAI_fillingid,
                    hypoobservation_summary: $scope.txthypoobservation_summary,
                    primary_security: $scope.txtprimary_security
                }
                var url = 'api/AgrMstSuprApplicationAdd/PostHypothecation';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.hypothecation_list = resp.data.hypothecation_list;
                        $scope.hypo_documentList = resp.data.DocumentList;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.cboSecurityType = '';
                        $scope.txtsecurity_desc = '';
                        $scope.txtSecurity_Value = '';
                        $scope.txtSecurityAssessed_date = '';
                        $scope.txtasset_id = '';
                        $scope.txtroc_fillingid = '';
                        $scope.txtCERSAI_fillingid = '';
                        $scope.txthypoobservation_summary = '';
                        $scope.txtprimary_security = '';
                        document.getElementById('words_totalamount6').innerHTML = '';

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });
            }
        }
        $scope.SubmitServiceCharges=function()
        {
            var params = {
                processing_fee: $scope.txtprocessing_fee,
                processing_collectiontype: $scope.rdbprocessing_collectiontype,
                doc_charges: $scope.txtdoc_charges,
                doccharge_collectiontype: $scope.rdbdoccharge_collectiontype,
                fieldvisit_charge: $scope.txtfieldvisit_charges,
                fieldvisit_collectiontype: $scope.rdbfieldvisit_collectiontype,
                adhoc_fee: $scope.txtadhoc_fee,
                adhoc_collectiontype: $scope.rdbadhoc_collectiontype,
                life_insurance: $scope.txtlife_insurance,
                lifeinsurance_collectiontype: $scope.rdblifeinsurance_collectiontype,
                acct_insurance: $scope.txtacct_insurance,
                total_collect: $scope.txttotal_collect,
                total_deduct: $scope.txttotal_deduct,
            }
            
            var url = 'api/AgrMstSuprApplicationAdd/PostServiceCharges';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.hypothecation_list = resp.data.hypothecation_list;
                    $scope.hypo_documentList = resp.data.DocumentList;
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.txtprocessing_fee = '';
                    $scope.rdbprocessing_collectiontype = '';
                    $scope.txtdoc_charges = '';
                    $scope.rdbdoccharge_collectiontype = '';
                    $scope.txtfieldvisit_charges = '';
                    $scope.rdbfieldvisit_collectiontype = '';
                    $scope.txtadhoc_fee = '';
                    $scope.rdbadhoc_collectiontype = '';
                    $scope.txtlife_insurance = '';
                    $scope.rdblifeinsurance_collectiontype = '';
                    $scope.txtacct_insurance = '';
                    $scope.txttotal_collect = '';
                    $scope.txttotal_deduct = '';
                    document.getElementById('words_totalamount51').innerHTML = '';
                    document.getElementById('words_totalamount52').innerHTML = '';
                    document.getElementById('words_totalamount53').innerHTML = '';
                    document.getElementById('words_totalamount54').innerHTML = '';
                    document.getElementById('words_totalamount55').innerHTML = '';
                    document.getElementById('words_totalamount56').innerHTML = '';
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.importIndividual = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importIndividual.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {                             

                $scope.application_gid = application_gid;

                    var params = {
                        application_gid: application_gid
                    }

                    var url = 'api/AgrMstSuprApplicationView/GetIndividualImportLog';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.individualimport_List = resp.data.individualimport_List;
                    });
                
               

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_individual = function () {
                    //var filename = "ImportExcelIndividual.xlsx";
                    ////var phyPath = resp.data.file_path;
                    //var phyPath = "E:\\Web\\EMS\\templates\\ImportExcelIndividual.xlsx";
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = "http://"
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = filename.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelIndividual.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();
                }

                $scope.uploadIndividual = function (val, val1, name) {
                    var application_gid = $scope.application_gid;

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelIndividual") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('application_gid', application_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                    }
                }


                $scope.uploadExcelIndividual = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/AgrMstSuprApplicationAdd/ImportExcelIndividual';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            if (resp.data.status == true) {
                                activate();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadExcelCancel = function () {
                    $("#fileimport").val('');
                };



            }
        }

        $scope.importInstitution = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importInstitution.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.application_gid = application_gid;

                var params = {
                    application_gid: application_gid
                }

                var url = 'api/AgrMstSuprApplicationView/GetInstitutionImportLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.institutionimport_List = resp.data.institutionimport_List;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_institution = function () {
                //    var filename = "ImportExcelInstitution.xlsx";
                //    //var phyPath = resp.data.file_path;
                //    var phyPath = "E:\\Web\\EMS\\templates\\ImportExcelInstitution.xlsx";
                //    var relPath = phyPath.split("EMS");
                //    var relpath1 = relPath[1].replace("\\", "/");
                //    var hosts = window.location.host;
                //    var prefix = "http://"
                //    var str = prefix.concat(hosts, relpath1);
                //    var link = document.createElement("a");
                //    var name = filename.split('.');
                //    link.download = name[0];
                //    var uri = str;
                //    link.href = uri;
                //    link.click();
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelInstitution.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();
                }

                $scope.uploadInstitution = function (val, val1, name) {
                    var application_gid = $scope.application_gid;

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelInstitution") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('application_gid', application_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                    }
                }


                $scope.uploadExcelInstitution = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')

                    }
                    else {
                        var url = 'api/AgrMstSuprApplicationAdd/ImportExcelInstitution';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            if (resp.data.status == true) {
                                activate();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadExcelCancel = function () {
                    $("#fileimport").val('');
                };



            }
        }

        $scope.importGroup = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/importGroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.application_gid = application_gid;

                var params = {
                    application_gid: application_gid
                }

                var url = 'api/AgrMstSuprApplicationView/GetGroupImportLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.groupimport_List = resp.data.groupimport_List;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.downloadtemplate_group = function () {
                    //var filename = "ImportExcelGroup.xlsx";
                    ////var phyPath = resp.data.file_path;
                    //var phyPath = "E:\\Web\\EMS\\templates\\ImportExcelGroup.xlsx";
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = "http://"
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = filename.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                    var Templateurl = apiManage.GetCommonData['TemplatePath'].Path;
                    var filename = "\ImportExcelGroup.xlsx";
                    //var phyPath = resp.data.file_path;
                    var phyPath = Templateurl + filename;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var prefix = window.location.protocol + "//";
                    var hosts = window.location.host;
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = filename.split('.');
                    link.download = name[0];
                    link.href = str;
                    link.click();
                }

                $scope.uploadGroup = function (val, val1, name) {
                    var application_gid = $scope.application_gid;

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    else if (filePath.includes("ImportExcelGroup") == false) {
                        Notify.alert('File Name / Template Not Supported!', 'warning')
                        $modalInstance.close('closed');
                    }
                    else {
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        frm.append('application_gid', application_gid);
                        frm.append('project_flag', "documentformatonly");
                        $scope.uploadfrm = frm;
                    }
                }


                $scope.uploadExcelGroup = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')

                    }
                    else {
                        var url = 'api/AgrMstSuprApplicationAdd/ImportExcelGroup';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            if (resp.data.status == true) {
                                activate();
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadExcelCancel = function () {
                    $("#fileimport").val('');
                };



            }
        }

         $scope.hierarchy_change = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/HierarchyChange.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            var application_gid = $scope.application_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    application_gid: application_gid
                }

                var url = 'api/AgrMstSuprApplicationAdd/GetApprovalHierarchyChangeList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rm_name = resp.data.rm_name;
                    $scope.directreportingto_name = resp.data.directreportingto_name;
                    $scope.clustermanager_gid = resp.data.clustermanager_gid;
                    $scope.clustermanager_name = resp.data.clustermanager_name;
                    $scope.regionalhead_gid = resp.data.regionalhead_gid;
                    $scope.regionhead_name = resp.data.regionhead_name;
                    $scope.zonalhead_gid = resp.data.zonalhead_gid;
                    $scope.zonalhead_name = resp.data.zonalhead_name;
                    $scope.businesshead_gid = resp.data.businesshead_gid;
                    $scope.businesshead_name = resp.data.businesshead_name;
                });

                $scope.Update_hierarchy = function () {
                    var params = {
                        application_gid: application_gid,
                        clustermanager_gid: $scope.clustermanager_gid,
                        clustermanager_name: $scope.clustermanager_name,
                        regionalhead_gid: $scope.regionalhead_gid,
                        regionalhead_name: $scope.regionhead_name,
                        zonalhead_gid: $scope.zonalhead_gid,
                        zonalhead_name: $scope.zonalhead_name,
                        businesshead_gid: $scope.businesshead_gid,
                        businesshead_name: $scope.businesshead_name
                    }
                    var url = 'api/AgrMstSuprApplicationAdd/UpdateApprovalHierarchyChange';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                    });
                    $modalInstance.close('closed');
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }
        }


         $scope.productdtl_add = function () {
             if (($scope.cboproduct_name == undefined) || ($scope.cboproduct_name == '') || ($scope.cboproduct_name == null) ||
                ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == '')) {
                 Notify.alert('Select Product / Commodity Name', 'warning');
             }
             else {
                 var lsproduct_gid = '';
                 var lsproduct_name = '';
                 if ($scope.cboproduct_name != undefined || $scope.cboproduct_name != null) {
                     lsproduct_gid = $scope.cboproduct_name.product_gid;
                     lsproduct_name = $scope.cboproduct_name.product_name;
                 }

                 var lsvariety_gid = '';
                 var lsvariety_name = '';
                 if ($scope.cbovariety_name != undefined || $scope.cbovariety_name != null) {
                     lsvariety_gid = $scope.cbovariety_name.variety_gid;
                     lsvariety_name = $scope.cbovariety_name.variety_name;
                 }

                 var params = {
                     product_gid: lsproduct_gid,
                     product_name: lsproduct_name,
                     variety_gid: lsvariety_gid,
                     variety_name: lsvariety_name,
                     sector_name: $scope.txtsector_name,
                     category_name: $scope.txtcategory_name,
                     botanical_name: $scope.txtbotanical_name,
                     alternative_name: $scope.txtalternative_name,
                     hsn_code: $scope.txthsn_code,
                 }
                 var url = 'api/AgrMstSuprApplicationAdd/PostProductDetailAdd';
                 lockUI();
                 SocketService.post(url, params).then(function (resp) {
                     unlockUI();
                     if (resp.data.status == true) {

                         Notify.alert(resp.data.message, {
                             status: 'success',
                             pos: 'top-center',
                             timeout: 3000
                         });
                     }
                     else {
                         Notify.alert(resp.data.message, {
                             status: 'warning',
                             pos: 'top-center',
                             timeout: 3000
                         });
                     }
                     $scope.cboproduct_name = '';
                     $scope.cbovariety_name = '';
                     $scope.txtsector_name = '';
                     $scope.txtcategory_name = '';
                     $scope.txtbotanical_name = '';
                     $scope.txtalternative_name = '';
                     $scope.txthsn_code = '';
                     $scope.varietyname_list = '';
                     productdetaillist();
                 });
             }
         }

         function productdetaillist() {

             var url = 'api/AgrMstSuprApplicationAdd/GetProductDetailList';
             SocketService.get(url).then(function (resp) {
                 $scope.mstproduct_list = resp.data.mstproduct_list;
             });
         }

         $scope.product_delete = function (application2product_gid) {
             var params =
                 {
                     application2product_gid: application2product_gid
                 }
             var url = 'api/AgrMstSuprApplicationAdd/DeleteProductDetail';
             lockUI();
             SocketService.getparams(url, params).then(function (resp) {
                 unlockUI();
                 if (resp.data.status == true) {

                     Notify.alert(resp.data.message, {
                         status: 'success',
                         pos: 'top-center',
                         timeout: 3000
                     });
                 }
                 else {
                     Notify.alert(resp.data.message, {
                         status: 'warning',
                         pos: 'top-center',
                         timeout: 3000
                     });
                 }
                 productdetaillist();
             });

         }


         $scope.Tradeclick = function () {
             var application_gid = $scope.application_gid;
             var applicant_type = $scope.applicant_type;

             if ($scope.applicant_type == null || $scope.applicant_type == '') {
                 $scope.Trade_dtls = true;
             }
             else {
                 $location.url('app/AgrMstSuprAppEditTradeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
             }
         }
    }
})();

