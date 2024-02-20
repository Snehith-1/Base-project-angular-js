
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprApplcreationBasicdtlEditController', AgrMstSuprApplcreationBasicdtlEditController);

    AgrMstSuprApplcreationBasicdtlEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstSuprApplcreationBasicdtlEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprApplcreationBasicdtlEditController';

        $scope.application_gid = $location.search().lsapplication_gid;
        $scope.product_gid = $location.search().product_gid;
        $scope.variety_gid = $location.search().variety_gid;
        var lstab = $location.search().lstab;
        $scope.lsstatus = $location.search().lsstatus;
        activate();
        function activate() {
            lockUI();
            $scope.cboPrimaryValueChain = [];
            $scope.cboSecondaryValueChain = [];
            $scope.cboVernacularLanguage = [];
            var url = 'api/AgrMstSuprApplicationAdd/GetGeneticCodeList';
            SocketService.get(url).then(function (resp) {
                $scope.geneticcode_list = resp.data.genetic_list;
            });

            var url = 'api/AgrMstSuprApplicationAdd/GetDropDown';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.vertical_list = resp.data.vertical_list;
                $scope.verticaltaggs_list = resp.data.verticaltaggs_list;
                $scope.constitution_list = resp.data.constitutionlist;
                $scope.businessunit_list = resp.data.businessunitlist;
                $scope.associatemaster_list = resp.data.associatemasterlist;
                $scope.designation_list = resp.data.designationlist;
                $scope.creditgrouplist = resp.data.creditgrouplist;
                //$scope.program_list = resp.data.program_list;
                $scope.productname_list = resp.data.productname_list;
            });

            var params = {
                product_gid: $scope.product_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetSectorcategory';
            unlockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.varietyname_list = resp.data.varietyname_list;
            });

            var param = {
                application_gid: $scope.application_gid
            };
            var url = 'api/AgrMstSuprApplicationEdit/GetApplicationBasicDetailsTempClear';
            SocketService.get(url).then(function (resp) {
            });


            var url = 'api/AgrMstSuprApplicationEdit/EditAppBasicDetail';
            unlockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtcustomer_urn = resp.data.customer_urn;
                $scope.txtcustomer_name = resp.data.customer_name;
                $scope.cboVertical = resp.data.vertical_gid;
                var params = {
                    vertical_gid: resp.data.vertical_gid,
                    lstype: '',
                    lstypegid: ''
                }
                var url = 'api/SystemMaster/GetVerticalProgramList';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.program_list = resp.data.program_list;
                    unlockUI();
                });
                /*    $scope.cboVerticaltag = resp.data.verticaltaggs_gid; */
                $scope.cboConstitution = resp.data.constitution_gid;
                $scope.cboStrategicBusinessUnit = resp.data.businessunit_gid;
               
                $scope.cboprogram = resp.data.program_gid;
                $scope.valuechain_list = resp.data.valuechainlist;
                $scope.cboproduct_name = resp.data.product_gid;
                $scope.cbovariety_name = resp.data.variety_gid;
                $scope.txtsector_name = resp.data.sector_name;
                $scope.txtcategory_name = resp.data.category_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;

                //$scope.appprimaryvaluechain_list = resp.data.primaryvaluechain_list;

                //if (resp.data.primaryvaluechain_list != null) {
                //    var count = resp.data.primaryvaluechain_list.length; 
                //    for (var i = 0; i < count; i++) {
                //        var indexs = $scope.valuechain_list.findIndex(x => x.valuechain_gid === resp.data.primaryvaluechain_list[i].valuechain_gid);
                //        $scope.cboPrimaryValueChain.push($scope.valuechain_list[indexs]);
                //        $scope.$parent.cboPrimaryValueChain = $scope.cboPrimaryValueChain;
                //    }
                //}

                //  $scope.appsecondaryvaluechain_list = resp.data.secondaryvaluechain_list;

                //if (resp.data.secondaryvaluechain_list != null) {
                //    var count = resp.data.secondaryvaluechain_list.length;
                //    for (var i = 0; i < count; i++) {
                //        var indexs = $scope.valuechain_list.findIndex(x => x.valuechain_gid === resp.data.secondaryvaluechain_list[i].valuechain_gid);
                //        $scope.cboSecondaryValueChain.push($scope.valuechain_list[indexs]);
                //        $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                //    }
                //}
                $scope.vernacularlang_list = resp.data.vernacularlang_list;

                $scope.appvernacularlanguage_list = resp.data.vernacularlanguage_list;

                if (resp.data.vernacularlanguage_list != null) {
                    var count = resp.data.vernacularlanguage_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.vernacularlang_list.map(function (x) { return x.vernacularlanguage_gid; }).indexOf(resp.data.vernacularlanguage_list[i].vernacularlanguage_gid);
                        $scope.cboVernacularLanguage.push($scope.vernacularlang_list[indexs]);
                        $scope.$parent.cboVernacularLanguage = $scope.cboVernacularLanguage;
                    }
                }

                $scope.rdbsa_status = resp.data.sa_status;
                $scope.cbosa_idname = resp.data.saname_gid;
                $scope.txtcontactpersonfirst_name = resp.data.contactpersonfirst_name;
                $scope.txtcontactpersonmiddle_name = resp.data.contactpersonmiddle_name;
                $scope.txtcontactpersonlast_name = resp.data.contactpersonlast_name;
                $scope.cboDesignation = resp.data.designation_gid;
                $scope.txtlandline_no = resp.data.landline_no;
                $scope.rdbOnboarding = resp.data.onboarding_status;
                if (resp.data.sa_status == 'Yes') {
                    $scope.SA_yes = true;
                }
                else {
                    $scope.SA_yes = false;
                }

                if (resp.data.onboarding_status == "Direct") {
                    $scope.Onboarding_Proposal = false;
                    $scope.cboCredit_Group = resp.data.creditgroup_name; 
                }
                else {
                    $scope.cbocreditgroup = '';
                    $scope.Onboarding_Proposal = true;
                } 
                $scope.cbocreditgroup = resp.data.creditgroup_gid;
                unlockUI();
            });

            var url = 'api/AgrMstSuprApplicationEdit/GetAppMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstmobileno_list = resp.data.mstmobileno_list;
            });

            var url = 'api/AgrMstSuprApplicationEdit/GetAppEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstemailaddress_list = resp.data.mstemailaddress_list;
            });

            var url = 'api/AgrMstSuprApplicationEdit/GetAppGeneticCodeList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstgeneticcode_list = resp.data.mstgeneticcode_list;

            });

            var param = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/GetAppProductList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstproduct_list = resp.data.mstproduct_list;
            });

            $scope.rdbgenetic_status = "No";
          
        }

        $scope.onselectedsa_yes = function () {
            if ($scope.rdbsa_status == 'Yes') {
                var url = 'api/AgrMstApplication360/GetAssociateMasterASC';
                SocketService.get(url).then(function (resp) {
                    $scope.associatemaster_list = resp.data.associatemaster_list;
                });
                $scope.SA_yes = true;
            }
            else {
                $scope.SA_yes = false;
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

        $scope.OnchangeVertical = function (cbovertical) {
            var params = {
                vertical_gid: cbovertical,
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

        $scope.mobileno_add = function () {
            //if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_mobileno == undefined) || ($scope.rdbwhatsapp_mobileno == undefined)) {
            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_mobileno == undefined) || ($scope.rdbwhatsapp_mobileno == undefined) || ($scope.rdbprimary_mobileno == '') || ($scope.rdbwhatsapp_mobileno == '')) {
                Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_mobileno: $scope.rdbprimary_mobileno,
                    whatsapp_mobileno: $scope.rdbwhatsapp_mobileno,
                    application_gid: $scope.application_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/PostAppMobileNo';
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
                    mobileno_templist();
                    $scope.txtmobile_no = '';
                    $('input[name=primarymobileno]').attr('checked', false);
                    $('input[name=whatsappmobileno]').attr('checked', false);
                    $scope.rdbprimary_mobileno = '';
                    $scope.rdbwhatsapp_mobileno = '';

                });
            }
        }

        function mobileno_templist() {
            var param = {
                application_gid: $scope.application_gid
            };
            var url = 'api/AgrMstSuprApplicationEdit/GetAppMobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstmobileno_list = resp.data.mstmobileno_list;
            });
        }

        $scope.mobileno_edit = function (application2contact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editgeneralmobileno.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    application2contact_gid: application2contact_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/EditAppMobileNo';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditmobile_no = resp.data.mobile_no;
                    $scope.rdbeditprimary_mobileno = resp.data.primary_mobileno;
                    $scope.rdbeditwhatsapp_mobileno = resp.data.whatsapp_mobileno;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_mobileno = function () {

                    var params = {
                        mobile_no: $scope.txteditmobile_no,
                        primary_mobileno: $scope.rdbeditprimary_mobileno,
                        whatsapp_mobileno: $scope.rdbeditwhatsapp_mobileno,
                        application2contact_gid: application2contact_gid,
                        application_gid: $scope.application_gid

                    }
                    var url = 'api/AgrMstSuprApplicationEdit/UpdateAppMobileNo';
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
                        mobileno_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.mobileno_delete = function (application2contact_gid) {
            var params =
                {
                    application2contact_gid: application2contact_gid
                }
            var url = 'api/AgrMstSuprApplicationEdit/DeleteAppMobileNo';
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

                mobileno_templist();
            });

        }

        $scope.emailaddress_add = function () {
            //if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_emailaddress == undefined)) {
            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_emailaddress == undefined) || ($scope.rdbprimary_emailaddress == '')) {
                Notify.alert('Enter Mail ID / Select Primary Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtemail_address,
                    primary_emailaddress: $scope.rdbprimary_emailaddress,
                    application_gid: $scope.application_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/PostAppEmailAddress';
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
                    emailaddress_templist();
                    $scope.txtemail_address = '';
                    $('input[name=primaryemail]').attr('checked', false);
                    $scope.rdbprimary_emailaddress = '';


                    //emailaddress_templist();
                    //$scope.txtemail_address = '';
                    //$scope.rdbprimary_emailaddress = '';
                    //$('input[name=primaryemail]').attr('checked', false);
                });
            }
        }

        function emailaddress_templist() {
            var param = {
                application_gid: $scope.application_gid
            };
            var url = 'api/AgrMstSuprApplicationEdit/GetAppEmailAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstemailaddress_list = resp.data.mstemailaddress_list;
            });
        }

        $scope.emailaddress_edit = function (application2email_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/generalemailaddressedit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    application2email_gid: application2email_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/EditAppEmailAddress';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditemail_address = resp.data.email_address;
                    $scope.rdbeditprimary_emailaddress = resp.data.primary_emailaddress;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_emailaddress = function () {

                    var params = {
                        email_address: $scope.txteditemail_address,
                        primary_emailaddress: $scope.rdbeditprimary_emailaddress,
                        application2email_gid: application2email_gid,
                        application_gid: $scope.application_gid
                    }
                    var url = 'api/AgrMstSuprApplicationEdit/UpdateAppEmailAddress';
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
                        emailaddress_templist();
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.emailaddress_delete = function (application2email_gid) {
            var params =
                {
                    application2email_gid: application2email_gid
                }
            var url = 'api/AgrMstSuprApplicationEdit/DeleteAppEmailAddress';
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

                emailaddress_templist();
            });

        }

        $scope.geneticcode_add = function () {
            if (($scope.cboGeneticCode.geneticcode_name == '') || ($scope.cboGeneticCode.geneticcode_name == undefined) || ($scope.rdbgenetic_status == undefined) || ($scope.txtgenetic_remarks == '')) {
                Notify.alert('Select Genetic Code / Select Status / Enter Genetic Code Remarks', 'warning');
            }
            else {
                var params = {
                    geneticcode_gid: $scope.cboGeneticCode.geneticcode_gid,
                    geneticcode_name: $scope.cboGeneticCode.geneticcode_name,
                    genetic_status: $scope.rdbgenetic_status,
                    genetic_remarks: $scope.txtgenetic_remarks,
                    application_gid: $scope.application_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/PostAppGeneticCode';
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
                    geneticcode_templist();
                    $scope.cboGeneticCode = '';
                    $('input[name=primaryemail]').attr('checked', false);
                    $scope.txtgenetic_remarks = '';

                });
            }
        }

        function geneticcode_templist() {
            var param = {
                application_gid: $scope.application_gid
            };
            var url = 'api/AgrMstSuprApplicationEdit/GetAppGeneticCodeTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstgeneticcode_list = resp.data.mstgeneticcode_list;
            });
        }

        $scope.geneticcode_edit = function (application2geneticcode_gid, application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editgeneticcode.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/AgrMstApplication360/GetGeneticCode';
                SocketService.get(url).then(function (resp) {
                    $scope.geneticcode_list = resp.data.application_list;
                });
                console.log(application_gid)
                var params = {
                    application2geneticcode_gid: application2geneticcode_gid
                }
                var url = 'api/AgrMstSuprApplicationEdit/EditAppGeneticCode';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblgeneticcode_name = resp.data.geneticcode_name;
                    $scope.rdbeditgenetic_status = resp.data.genetic_status;
                    $scope.txteditgenetic_remarks = resp.data.genetic_remarks;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_geneticcode = function () {
                    var geneticcode_Name = $('#GeneticCode :selected').text();

                    var params = {
                        geneticcode_gid: $scope.cboeditGeneticCode,
                        geneticcode_name: geneticcode_Name,
                        genetic_status: $scope.rdbeditgenetic_status,
                        genetic_remarks: $scope.txteditgenetic_remarks,
                        application2geneticcode_gid: application2geneticcode_gid,
                        application_gid: application_gid

                    }
                    var url = 'api/AgrMstSuprApplicationEdit/UpdateAppGeneticCode';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            geneticcode_templist();
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

                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.geneticcode_delete = function (application2geneticcode_gid) {
            var params =
               {
                   application2geneticcode_gid: application2geneticcode_gid
               }
            var url = 'api/AgrMstSuprApplicationEdit/DeleteAppGeneticCode';
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

                geneticcode_templist();
            });

        }

        $scope.basicdetail_update = function () {

            if (($scope.cboVernacularLanguage == undefined) || ($scope.cboVernacularLanguage == '' ) || ($scope.cboVernacularLanguage == null )) {

                Notify.alert('Select Vernacular Language ', 'warning');              
            }
            else if (($scope.rdbsa_status == 'Yes') && (($scope.cbosa_idname == undefined) || ($scope.cbosa_idname == '' ) || ($scope.cbosa_idname == null ))){
                Notify.alert('Kindly Add SAM Associate ID / Name  ', 'warning');  
            }
            //else if ($scope.mstproduct_list == null || $scope.mstproduct_list == undefined || $scope.mstproduct_list == "") {
            //    Notify.alert('Atleast One Record should be added in Product Details', 'warning')
            //}
            else {

            var vertical_Name = $('#Vertical :selected').text();
            var verticaltaggs_Name = $('#VerticalTag :selected').text();
            var constitution_Name = $('#Constitution :selected').text();
            //var businessunit_Name = $('#StrategicBusinessUnit :selected').text();
            var vernacular_Language = $('#VernacularLanguage :selected').text();
            var designationtype = $('#designation_type :selected').text();
            var lssa_name = $('#sa_idname :selected').text();
            var creditgroup_Name = $('#CreditGroup :selected').text();
            var program_Name = $('#program :selected').text();
            var Product_Name = $('#ProductName :selected').text();
            var Variety_Name = $('#Variety :selected').text();

            var params = {
                customer_urn: $scope.txtcustomer_urn,
                customer_name: $scope.txtcustomer_name,
                vertical_gid: $scope.cboVertical,
                vertical_name: vertical_Name,
                /*  verticaltaggs_gid: $scope.cboVerticaltag,
                 verticaltaggs_name: verticaltaggs_Name, */
                constitution_gid: $scope.cboConstitution,
                constitution_name: constitution_Name,
                //businessunit_gid: $scope.cboStrategicBusinessUnit,
                //businessunit_name: businessunit_Name,
                //primaryvaluechain_list: $scope.$parent.cboPrimaryValueChain,
                //secondaryvaluechain_list: $scope.$parent.cboSecondaryValueChain,
                sa_status: $scope.rdbsa_status,
                //sa_id: $scope.cboSAID,
                //sa_name: $scope.txtsa_name,
                saname_gid: $scope.cbosa_idname,
                sa_name: lssa_name,
                vernacularlanguage_list: $scope.cboVernacularLanguage,
                contactpersonfirst_name: $scope.txtcontactpersonfirst_name,
                contactpersonmiddle_name: $scope.txtcontactpersonmiddle_name,
                contactpersonlast_name: $scope.txtcontactpersonlast_name,
                designation_gid: $scope.cboDesignation,
                designation_type: designationtype,
                landline_no: $scope.txtlandline_no,
                application_gid: $scope.application_gid,
                creditgroup_gid: $scope.cbocreditgroup,
                creditgroup_name: creditgroup_Name,
                program_gid: $scope.cboprogram,
                program_name: program_Name,
                product_gid: $scope.cboproduct_name,
                product_name: Product_Name,
                variety_gid: $scope.cbovariety_name,
                variety_name: Variety_Name,
                sector_name: $scope.txtsector_name,
                category_name: $scope.txtcategory_name,
                botanical_name: $scope.txtbotanical_name,
                alternative_name: $scope.txtalternative_name,
                onboarding_status: $scope.rdbOnboarding
            }
            var url = 'api/AgrMstSuprApplicationEdit/UpdateAppBasicDetail';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lstab == 'add') {
                        $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                    }
                    else {
                        $state.go('app.AgrMstSuprApplicationGeneralEdit');
                    }
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
        $scope.Save_generaldetails = function () {

            var vertical_Name = $('#Vertical :selected').text();
            var verticaltaggs_Name = $('#VerticalTag :selected').text();
            var constitution_Name = $('#Constitution :selected').text();
            //var businessunit_Name = $('#StrategicBusinessUnit :selected').text();
            var vernacular_Language = $('#VernacularLanguage :selected').text();
            var designationtype = $('#designation_type :selected').text();
            var lssa_name = $('#name :selected').text();
            var creditgroup_Name = $('#CreditGroup :selected').text();
            var program_Name = $('#program :selected').text();
            var Product_Name = $('#ProductName :selected').text();
            var Variety_Name = $('#Variety :selected').text();
            var vernacularlanguage = $scope.cboVernacularLanguage;
            if (vernacular_Language == [undefined]) {
                var cboVernacularLanguage = null;
            }
            else {
                var cboVernacularLanguage = $scope.cboVernacularLanguage;
            }
            var params = {
                customer_urn: $scope.txtcustomer_urn,
                customer_name: $scope.txtcustomer_name,
                vertical_gid: $scope.cboVertical,
                vertical_name: vertical_Name,
                /* verticaltaggs_gid: $scope.cboVerticaltag,
                verticaltaggs_name: verticaltaggs_Name, */
                constitution_gid: $scope.cboConstitution,
                constitution_name: constitution_Name,
                //businessunit_gid: $scope.cboStrategicBusinessUnit,
                //businessunit_name: businessunit_Name,

                //primaryvaluechain_list: $scope.$parent.cboPrimaryValueChain,
                //secondaryvaluechain_list: $scope.$parent.cboSecondaryValueChain,

                sa_status: $scope.rdbsa_status,
                //sa_id: $scope.cboSAID,
                //sa_name: $scope.txtsa_name,

                saname_gid: $scope.cbosa_idname,
                sa_name: lssa_name,

                vernacularlanguage_list: cboVernacularLanguage,
                contactpersonfirst_name: $scope.txtcontactpersonfirst_name,
                contactpersonmiddle_name: $scope.txtcontactpersonmiddle_name,
                contactpersonlast_name: $scope.txtcontactpersonlast_name,
                designation_gid: $scope.cboDesignation,
                designation_type: designationtype,
                landline_no: $scope.txtlandline_no,
                application_gid: $scope.application_gid,
                creditgroup_gid: $scope.cbocreditgroup,
                creditgroup_name: creditgroup_Name,
                program_gid: $scope.cboprogram,
                program_name: program_Name,
                product_gid: $scope.cboproduct_name,
                product_name: Product_Name,
                variety_gid: $scope.cbovariety_name,
                variety_name: Variety_Name,
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
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lstab == 'add') {
                        $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                    }
                    else {
                        $state.go('app.AgrMstSuprApplicationGeneralEdit');
                    }
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
        $scope.Submitgeneraldetails = function () {
            var vertical_Name = $('#Vertical :selected').text();
            /*  var verticaltaggs_Name = $('#VerticalTag :selected').text(); */
            var constitution_Name = $('#Constitution :selected').text();
            //var businessunit_Name = $('#StrategicBusinessUnit :selected').text();
            var vernacular_Language = $('#VernacularLanguage :selected').text();
            var designationtype = $('#designation_type :selected').text();
            var lssa_name = $('#name :selected').text();
            var creditgroup_Name = $('#CreditGroup :selected').text();
            var program_Name = $('#program :selected').text();
            var Product_Name = $('#ProductName :selected').text();
            var Variety_Name = $('#Variety :selected').text();

            var params = {
                customer_urn: $scope.txtcustomer_urn,
                customer_name: $scope.txtcustomer_name,

                vertical_gid: $scope.cboVertical,
                vertical_name: vertical_Name,
                /*  verticaltaggs_gid: $scope.cboVerticaltag,
                 verticaltaggs_name: verticaltaggs_Name, */
                constitution_gid: $scope.cboConstitution,
                constitution_name: constitution_Name,
                //businessunit_gid: $scope.cboStrategicBusinessUnit,
                //businessunit_name: businessunit_Name,

                //primaryvaluechain_list: $scope.$parent.cboPrimaryValueChain,
                //secondaryvaluechain_list: $scope.$parent.cboSecondaryValueChain,

                sa_status: $scope.rdbsa_status,
                //sa_id: $scope.cboSAID,
                //sa_name: $scope.txtsa_name,

                saname_gid: $scope.cbosa_idname,
                sa_name: lssa_name,

                vernacularlanguage_list: $scope.cboVernacularLanguage,
                contactpersonfirst_name: $scope.txtcontactpersonfirst_name,
                contactpersonmiddle_name: $scope.txtcontactpersonmiddle_name,
                contactpersonlast_name: $scope.txtcontactpersonlast_name,
                designation_gid: $scope.cboDesignation,
                designation_type: designationtype,
                landline_no: $scope.txtlandline_no,
                application_gid: $scope.application_gid,
                creditgroup_gid: $scope.cbocreditgroup,
                creditgroup_name: creditgroup_Name,
                program_gid: $scope.cboprogram,
                program_name: program_Name,
                product_gid: $scope.cboproduct_name,
                product_name: Product_Name,
                variety_gid: $scope.cbovariety_name,
                variety_name: Variety_Name,
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
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    if (lstab == 'add') {
                        $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
                    }
                    else {
                        $state.go('app.AgrMstSuprApplicationGeneralEdit');
                    }
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
        $scope.generdtl_Back = function () {
            if (lstab == 'add') {
                $location.url('app/AgrMstSuprApplicationGeneralAdd?lstab=' + lstab);
            }
            else {
                $state.go('app.AgrMstSuprApplicationGeneralEdit');
            }
        }

        $scope.productdtl_add = function () {
            if (($scope.cboproduct_name == undefined) || ($scope.cboproduct_name == '') || ($scope.cboproduct_name == null) ||
               ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == undefined) || ($scope.cbovariety_name == '')) {
                Notify.alert('Select Product / Variety Name', 'warning');
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
                    Tempproductdetaillist();
                });
            }
        }

        function Tempproductdetaillist() {
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/GetAppProductTempList';
            SocketService.getparams(url, params).then(function (resp) {
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
                Tempproductdetaillist();
            });

        }
    }
})();

