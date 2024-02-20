
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApplicationIndividualEdit', AgrMstApplicationIndividualEdit);

    AgrMstApplicationIndividualEdit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstApplicationIndividualEdit($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApplicationIndividualEdit';
        lockUI();
        activate();
        var application_gid = $location.search().lsapplication_gid;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;

        function activate() {
            $scope.groupnameYes = true;
            $scope.companynameYes = true;
            //$scope.valueshow = true;
            $scope.application_gid = $location.search().lsapplication_gid;

            // Calender Popup... //
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1

            };

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;


                var url = 'api/AgrMstApplicationEdit/GetEditProductcharges';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program_gid = resp.data.program_gid;

                    var params = {
                        program_gid: $scope.program_gid
                    }
                    var url = 'api/AgrMstApplication360/GetIndividualDocTypeList';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.individualdoctype_list = resp.data.individualdoctype_list;
                    });


                });

               
            });
            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrTrnApplicationApproval/Getproceedapprovalflag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.proceedtoapproval_flag = resp.data.proceedtoapproval_flag;

            });

            var url = 'api/AgrMstApplicationAdd/GetPANForm60List';
            SocketService.get(url).then(function (resp) {
                $scope.contactpanform60_list = resp.data.contactpanform60_list;
                $scope.contactpanform60_list = '';
            });

            var url = 'api/AgrMstApplicationAdd/PANAbsenceReasonList';
            SocketService.get(url).then(function (resp) {
                $scope.panabsencereason_list = resp.data.panabsencereason_list;
            });


            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && $scope.approveinitiated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                    $scope.resubmitshow = false;
                }
                else if ($scope.proceed_flag == 'Y' && $scope.approveinitiated_flag == 'N' && $scope.hierarchyupdated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_enable = true;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'Y' && approveinitiated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = true;
                    $scope.done_enable = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N' && approveinitiated_flag == 'N') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_disable = true;
                    $scope.resubmitshow = false;
                }
                else {

                }
            });

            $scope.amendmentshow = false;
            var url = 'api/AgrMstApplicationView/GetApplicationBasicView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblbasiccustomer_name = resp.data.customer_name;
                $scope.lblamendment_remarks = resp.data.amendment_remarks;
                unlockUI();

                if ($scope.lblamendment_remarks == null || $scope.lblamendment_remarks == '' || $scope.lblamendment_remarks == undefined) {
                    $scope.amendmentshow = false;
                }
                else {
                    $scope.amendmentshow = true;
                }
            });


            var url = 'api/AgrMstApplicationAdd/GetIndividualTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/AgrMstApplication360/GenderList';
            SocketService.get(url).then(function (resp) {
                $scope.gender_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/GetDesignationList';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });

            var url = 'api/AgrMstApplication360/GetUserTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.usertype_list = resp.data.usertype_list;
            });

            var url = 'api/AgrMstApplication360/GetMaritalStatusList';
            SocketService.get(url).then(function (resp) {
                $scope.maritalstatus_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/EducationalQualificationList';
            SocketService.get(url).then(function (resp) {
                $scope.educationalqualification_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/IncomeTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.incometype_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/IndividualProofList';
            SocketService.get(url).then(function (resp) {
                $scope.individualproof_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/OwnershipTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.ownershiptype_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/GetPropertyinNameList';
            SocketService.get(url).then(function (resp) {
                $scope.propertyin_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/ResidenceTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.residencetype_list = resp.data.application_list;
            });

            //var url = 'api/AgrMstApplication360/IndividualDocumentList';
            //SocketService.get(url).then(function (resp) {
            //    $scope.individualdocument_list = resp.data.application_list;
            //});
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetGroupList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.grouplist = resp.data.grouplist;
            });
            var url = 'api/AgrMstApplicationAdd/GetCompanyList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionlist = resp.data.institutionlist;
            });

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrMstApplicationEdit/GetEditProductcharges';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lbloveralllimit_amount = resp.data.overalllimit_amount;
                $scope.lblprocessing_fee = resp.data.processing_fee;
                $scope.lbldoc_charges = resp.data.doc_charges;
                $scope.application_gid = resp.data.application_gid;
                $scope.applicant_type = resp.data.applicant_type;
                $scope.created_date = resp.data.created_date;
                $scope.created_by = resp.data.created_by;
                $scope.productcharge_flag = resp.data.productcharge_flag;
                $scope.economical_flag = resp.data.economical_flag;
                $scope.lblproductcharges_status = resp.data.productcharges_status;
                $scope.application_status = resp.data.application_status;

                if ($scope.economical_flag == 'N') {
                    $scope.social_tradetab = false;
                    $scope.social_trade = true;
                }
                else {
                    $scope.social_tradetab = true;
                    $scope.social_trade = false;
                }

                if ($scope.productcharge_flag == 'N') {
                    $scope.product_chargetab = false;
                    $scope.product_charge = true;
                }
                else {
                    $scope.product_chargetab = true;
                    $scope.product_charge = false;
                }
            });
            $scope.havenotpan = false;
            $scope.havepan = false;
            $scope.view_nopanreasons = false;

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });
        }
        $scope.onselectedfathernominee_yes = function (rdbfathernominee_status) {
            if (rdbfathernominee_status == 'Yes') {
                if ($scope.rdbmothernominee_status == 'Yes' || $scope.rdbspousenominee_status == 'Yes' || $scope.rdbothernominee_status == 'Yes') {
                    Notify.alert('Select Only One Nominee Status', 'warning');
                }
            }
            else {
                $scope.rdbfathernominee_status = rdbfathernominee_status;
            }
        }
        $scope.onselectedmothernominee_yes = function (rdbmothernominee_status) {
            if (rdbmothernominee_status == 'Yes') {
                if ($scope.rdbfathernominee_status == 'Yes' || $scope.rdbspousenominee_status == 'Yes' || $scope.rdbothernominee_status == 'Yes') {
                    Notify.alert('Select Only One Nominee Status', 'warning');
                }
            }
            else {
                $scope.rdbmothernominee_status = rdbmothernominee_status;
            }
        }
        $scope.onselectedspousenominee_yes = function (rdbspousenominee_status) {
            if (rdbspousenominee_status == 'Yes') {
                if ($scope.rdbmothernominee_status == 'Yes' || $scope.rdbfathernominee_status == 'Yes' || $scope.rdbothernominee_status == 'Yes') {
                    Notify.alert('Select Only One Nominee Status', 'warning');
                }
            }
            else {
                $scope.rdbspousenominee_status = rdbspousenominee_status;
            }
        }

        $scope.onselectedgroupname = function () {
            if ($scope.cboGroup == null || $scope.cboGroup == '' || $scope.cboGroup == undefined || $scope.cboGroup.group_gid == 'NA') {
                $scope.profile_yes = false;
                $scope.txtprofile = '';
                $scope.companynameYes = true;
                $scope.companynameNo = false;
            }
            else {
                $scope.profile_yes = true;
                $scope.companynameYes = false;
                $scope.companynameNo = true;
            }
        }

        $scope.onselectedcomapanyname = function () {
            if ($scope.cboInstitution.institution_gid == 'NA') {
                $scope.groupnameYes = true;
                $scope.groupnameNo = false;
            }
            else {
                $scope.groupnameYes = false;
                $scope.groupnameNo = true;
            }
        }

        $scope.onselectedurn_yes = function () {
            if ($scope.rdburn_status == 'Yes') {
                $scope.URN_yes = true;
            }
            else {
                $scope.URN_yes = false;
                $scope.txt_urn = '';
            }
        }
        $scope.onselectednominee_yes = function () {
            if ($scope.rdbothernominee_status == 'Yes') {
                $scope.relationshiptype_yes = true;
            }
            else {
                $scope.relationshiptype_yes = false;
                $scope.relationshiptype_yes = false;
                $scope.txtnomineefirst_name = '';
                $scope.txtnominee_middlename = '';
                $scope.txtnominee_lastname = '';
                $scope.txtnominee_dob = '';
                $scope.txtnominee_age = '';
            }
        }
        $scope.onchangeddobnominee = function (string) {
            if (string.length >= 10) {
                var params = {
                    dob: $scope.txtnominee_dob
                }
                var url = 'api/AgrMstCustomerAdd/GetAge';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtnominee_age = resp.data.age;
                });
            }
            else if (string.length == 0) {
                $scope.txtnominee_age = ""
            }
            else {
                $scope.txtnominee_age = ""
            }
        }

        $scope.onchangeddobindividual = function (string) {
            if (string.length >= 10) {
                var params = {
                    dob: $scope.txtindividual_dob
                }
                var url = 'api/AgrMstCustomerAdd/GetAge';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtage = resp.data.age;
                    if (resp.data.status == false) {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
            else if (string.length == 0) {
                $scope.txtage = ""
            }
            else {
                $scope.txtage = ""
            }
        }

        $scope.onchangeddobfather = function (string) {
            if (string.length >= 10) {
                var params = {
                    dob: $scope.txtfather_dob
                }
                var url = 'api/AgrMstCustomerAdd/GetAge';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtfather_age = resp.data.age;
                });
            }
            else if (string.length == 0) {
                $scope.txtfather_age = ""
            }
            else {
                $scope.txtfather_age = ""
            }
        }

        $scope.onchangeddobmother = function (string) {
            if (string.length >= 10) {
                var params = {
                    dob: $scope.txtmother_dob
                }
                var url = 'api/AgrMstCustomerAdd/GetAge';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtmother_age = resp.data.age;
                });
            }
            else if (string.length == 0) {
                $scope.txtmother_age = ""
            }
            else {
                $scope.txtmother_age = ""
            }
        }

        $scope.onchangeddobspouse = function (string) {
            if (string.length >= 10) {
                var params = {
                    dob: $scope.txtspouse_dob
                }
                var url = 'api/AgrMstCustomerAdd/GetAge';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtspouse_age = resp.data.age;
                });
            }
            else if (string.length == 0) {
                $scope.txtspouse_age = ""
            }
            else {
                $scope.txtspouse_age = ""
            }
        }

        $scope.txtannualincome_change = function () {
            var input = document.getElementById('annual_income').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualincome = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtannual_income = "";
            }
            else {
                $scope.txtannual_income = output;
                document.getElementById('words_annualincome').innerHTML = lswords_annualincome;
            }
        }

        
        $scope.txtmonthlyincome_change = function () {
            var input = document.getElementById('monthly_income').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_monthlyincome = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtmonthly_income = "";
            }
            else {
                $scope.txtmonthly_income = output;
                document.getElementById('words_monthlyincome').innerHTML = lswords_monthlyincome;
            }
        }

        $scope.futuredatecheck = function (val) {
            if (val.length >= 10) {
                var parts = val.split("-");
                var finalval = new Date(parts[2], parts[1] - 1, parts[0]);
                var params = {
                    date: finalval.toDateString()
                }
                var url = 'api/AgrMstApplicationAdd/FutureDateCheck';
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == false) {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.PANAadhaarLink = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/AgrKyc/PANAadhaarLink';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.isAadhaarLinked == true) {
                        $scope.panaadhaarlinkstatus = "Aadhaar is linked";
                    } else if (resp.data.result.isAadhaarLinked == false) {
                        $scope.panaadhaarlinkcheck = true;
                        $scope.panaadhaarlinkstatus = "Aadhaar is not linked";
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.IDProofValidation = function () {

            if ($scope.cboIndividualProof.individualproof_gid == "INPF2021062300002" && $scope.txtidproof_no.length == 10) {
                var params = {
                    epic_no: $scope.txtidproof_no
                }
                var url = 'api/AgrKyc/VoterID';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.idproofvalidation = true;
                        Notify.alert('ID Proof is verified..!', 'success');
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.idproofvalidation = false;
                        Notify.alert('ID Proof is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning');
                    }
                });
            } else if ($scope.cboIndividualProof.individualproof_name == "PAN" && $scope.txtidproof_no.length == 10) {
                var params = {
                    pan: $scope.txtidproof_no
                }
                var url = 'api/AgrKyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.idproofvalidation = true;
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.idproofvalidation = false;
                        Notify.alert('PAN is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.PassportAndDLValidation = function () {

            if ($scope.cboIndividualProof.individualproof_gid == "INPF2021062300003" && $scope.txtidproof_no.length == 16 && $scope.txtidproof_dob.length == 10) {
                
                    var params = {
                        dlno: $scope.txtidproof_no,
                    dob: $scope.txtidproof_dob
                }
                var url = 'api/AgrKyc/DrivingLicenseAuthentication';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.idproofvalidation = true;
                        Notify.alert('ID Proof is verified..!', 'success');
                    } else if (resp.data.statusCode == 102 || resp.data.statusCode == 103) {
                        $scope.idproofvalidation = false;
                        Notify.alert('ID Proof is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning');
                    }
                });
            }
            else if ($scope.cboIndividualProof.individualproof_gid == "INPF2021062300004" && $scope.txtfile_no.length == 15 && $scope.txtidproof_dob.length == 10) {
                var params = {
                    fileNo: $scope.txtfile_no,
                    dob: $scope.txtidproof_dob.replace(/-/g, "/")
                }
                var url = 'api/AgrKyc/Passport';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.idproofvalidation = true;
                        Notify.alert('ID Proof is verified..!', 'success');
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.idproofvalidation = false;
                        Notify.alert('ID Proof is not verified..!', 'warning');
                    } else {
                        Notify.alert(resp.data.message, 'warning');
                    }
                });
            }
        }

        $scope.onchangeIDType = function () {
                $scope.drivingPassportid_dob = false;
            if (($scope.cboIndividualProof.individualproof_gid == "INPF2021062300003" )) {
                $scope.idprooffilenoshow = false;
                $scope.idproofdobshow = true;
                // $scope.drivingPassportid_dob = false;
                if(($scope.txtidproof_dob == null || $scope.txtidproof_dob == '' || $scope.txtidproof_dob == undefined)){
                    // Notify.alert('Kindly Enter Date of Birth', 'warning');
                    $scope.drivingPassportid_dob=true;
                }
            }
            else if (($scope.cboIndividualProof.individualproof_gid == "INPF2021062300004")) {
                $scope.idproofdobshow = true;
                $scope.idprooffilenoshow = true;
                if(($scope.txtidproof_dob == null || $scope.txtidproof_dob == '' || $scope.txtidproof_dob == undefined) && ($scope.txtfile_no == null || $scope.txtfile_no == '' || $scope.txtfile_no == undefined)){
                    // Notify.alert('Kindly Enter Date of Birth', 'warning');
                    $scope.drivingPassportid_dob=true;
                }
            } else {
                $scope.idproofdobshow = false;
                $scope.idprooffilenoshow = false;
            }
            $scope.txtidproof_no = '';
            $scope.idproofvalidation = false;
            $scope.txtidproof_dob = '';

        }

        $scope.Drivingid_dob =  function (){

            if (($scope.cboIndividualProof.individualproof_gid == "INPF2021062300003" ) && ($scope.txtidproof_dob != null || $scope.txtidproof_dob != '' || $scope.txtidproof_dob != undefined)){
                // Notify.alert('Kindly Enter Date of Birth', 'warning');
                
                $scope.drivingPassportid_dob=false;
            }
            else {
                
            }
        }

        $scope.Passportid_dob =  function (){

            if (($scope.cboIndividualProof.individualproof_gid == "INPF2021062300004" ) && ($scope.txtidproof_dob != null || $scope.txtidproof_dob != '' || $scope.txtidproof_dob != undefined) && ($scope.txtfile_no != null || $scope.txtfile_no != '' || $scope.txtfile_no != undefined)){
                // Notify.alert('Kindly Enter Date of Birth', 'warning');
                
                $scope.drivingPassportid_dob=false;
            }
            else {
                
            }
        }


        $scope.mobileno_add = function () {

            if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_no == undefined) || ($scope.rdbwhatsapp_no == undefined) || ($scope.rdbwhatsapp_no == '') || ($scope.rdbprimary_no == '')) {
                Notify.alert('Enter Mobile No/Select Primary Status and Whatsapp Number', 'warning');
            }
            else if ($scope.txtmobile_no.length < 10) {
                Notify.alert('Enter 10 Digit Mobile Number', 'warning');
            }
            else {
                var params = {
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimary_no,
                    whatsapp_no: $scope.rdbwhatsapp_no
                }
                var url = 'api/AgrMstApplicationAdd/MobileNumberAdd';
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
                    var url = 'api/AgrMstApplicationAdd/GetMobileNoList';
                    SocketService.get(url).then(function (resp) {
                        $scope.contactmobileno_list = resp.data.contactmobileno_list;

                    });
                    $scope.txtmobile_no = '';
                    $scope.rdbprimary_no = '';
                    $scope.rdbwhatsapp_no = '';
                });

            }

        }

        $scope.mobileno_delete = function (contact2mobileno_gid) {
            var params =
                {
                    contact2mobileno_gid: contact2mobileno_gid
                }
            var url = 'api/AgrMstApplicationAdd/MobileNoDelete';
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
                var url = 'api/AgrMstApplicationAdd/GetMobileNoList';
                SocketService.get(url).then(function (resp) {
                    $scope.contactmobileno_list = resp.data.contactmobileno_list;

                });

            });

        }


        $scope.email_add = function () {

            if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_email == undefined) || ($scope.rdbprimary_email == '')) {
                Notify.alert('Enter Email Address/Select Primary Status', 'warning');
            }
            else {


                var params = {
                    email_address: $scope.txtemail_address,
                    primary_status: $scope.rdbprimary_email,
                }
                var url = 'api/AgrMstApplicationAdd/EmailAddressAdd';
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
                    var url = 'api/AgrMstApplicationAdd/GetEmailAddressList';
                    SocketService.get(url).then(function (resp) {
                        $scope.contactemail_list = resp.data.contactemail_list;

                    });
                    $scope.txtemail_address = '';
                    $scope.rdbprimary_email = '';
                });

            }

        }

        $scope.emailaddress_delete = function (contact2email_gid) {
            var params =
                {
                    contact2email_gid: contact2email_gid
                }
            var url = 'api/AgrMstApplicationAdd/EmailAddressDelete';
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
                var url = 'api/AgrMstApplicationAdd/GetEmailAddressList';
                SocketService.get(url).then(function (resp) {
                    $scope.contactemail_list = resp.data.contactemail_list;

                });

            });

        }


        $scope.idproofdocument_upload = function (val, val1, name) {
            lockUI();
            if (($scope.cboIndividualProof == null) || ($scope.cboIndividualProof == '') || ($scope.cboIndividualProof == undefined) || ($scope.txtidproof_no == null) || ($scope.txtidproof_no == '') || ($scope.txtidproof_no == undefined)) {
                $("#fileIndividuaDocument").val('');
                Notify.alert('Kindly Enter the ID Value/ID Type', 'warning');
                unlockUI();
            }
            else {
                var frm = new FormData();
                
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
    
                            if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                return false;
                            }
                }

                frm.append('idproof_type', $scope.cboIndividualProof.individualproof_name);
                frm.append('idproof_no', $scope.txtidproof_no);
                frm.append('idproof_dob', $scope.txtidproof_dob);
                frm.append('file_no', $scope.txtfile_no);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/AgrMstApplicationAdd/IndividualProofDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $("#file").val('');
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $("#file").val('');
                            $scope.cboIndividualProof = '';
                            $scope.txtidproof_no = '';
                            $scope.txtidproof_dob = '';
                            $scope.txtfile_no = '';
                            $scope.txtindividualproof_document = '';
                            $scope.idproofvalidation = false;
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                        var url = 'api/AgrMstApplicationAdd/GetIndividualProofList';
                        SocketService.get(url).then(function (resp) {
                            $scope.contactidproof_list = resp.data.contactidproof_list;
                        });

                        unlockUI();
                    });
                }
                else {
                    alert('Please select a file.')
                }
            }
        }

        $scope.idproof_delete = function (contact2idproof_gid) {

            var params = {
                contact2idproof_gid: contact2idproof_gid
            }
            lockUI();
            var url = 'api/AgrMstApplicationAdd/IndividualProofDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
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
                var url = 'api/AgrMstApplicationAdd/GetIndividualProofList';
                SocketService.get(url).then(function (resp) {
                    $scope.contactidproof_list = resp.data.contactidproof_list;
                });
                unlockUI();
            });
        }

        function individualaddress_list() {
            var url = 'api/AgrMstApplicationAdd/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.contactindividualaddress_list = resp.data.contactaddress_list;
            });
        }

        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addresstype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.geocodingFailed = false;

                var url = 'api/AgrMstAddressType/GetAddressTypeASC';
                SocketService.get(url).then(function (resp) {
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/AgrMstbuyer/GetPostalCodeDetails';

                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined || $scope.txtpostal_code.length < 6){
                        $scope.txtlatitude = '';
                        $scope.txtlongitude = '';
                    }
                    else if ($scope.txtpostal_code.length == 6) {
                        if ($scope.txtaddressline2 == undefined) {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtpostal_code.toString());
                        } else {
                            var addressString = ''.concat($scope.txtaddressline1.toString(), ",", $scope.txtaddressline2.toString(), ",", $scope.txtpostal_code.toString());
                        }
                        var params = {
                            address: addressString
                        }
                        var url = 'api/AgrGoogleMapsAPI/GetGeoCoding';
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == "OK") {
                                $scope.txtlatitude = resp.data.results[0].geometry.location.lat.toString();
                                $scope.txtlongitude = resp.data.results[0].geometry.location.lng.toString();
                                $scope.geocodingFailed = false;
                            }
                            else if (resp.data.status == "ZERO_RESULTS") {
                                $scope.geocodingFailed = true;
                            }
                        });



                    }
                }

                $scope.txtcountry = "India";
                $scope.addressSubmit = function () {

                    var params = {
                        addresstype_gid: $scope.cboaddresstype.address_gid,
                        addresstype_name: $scope.cboaddresstype.address_type,
                        primary_status: $scope.rdbprimaryaddress,
                        addressline1: $scope.txtaddressline1,
                        addressline2: $scope.txtaddressline2,
                        postal_code: $scope.txtpostal_code,
                        landmark: $scope.txtLand_Mark,
                        taluka: $scope.txttaluka,
                        city: $scope.txtcity,
                        state: $scope.txtstate,
                        district: $scope.txtdistrict,
                        country: $scope.txtcountry,
                        latitude: $scope.txtlatitude,
                        longitude: $scope.txtlongitude
                    }
                    var url = 'api/AgrMstApplicationAdd/AddressAdd';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            individualaddress_list();
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

        $scope.address_delete = function (contact2address_gid) {
            var params =
                {
                    contact2address_gid: contact2address_gid
                }
            var url = 'api/AgrMstApplicationAdd/AddressDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    individualaddress_list();
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

        $scope.StaticMapAndPhotos_View = function (latitude, longitude, addressline1, addressline2, postal_code) {
            var modalInstance = $modal.open({
                templateUrl: '/StaticMapAndPhotosView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    latitude: latitude,
                    longitude: longitude
                }
                var url = 'api/AgrGoogleMapsAPI/GetStaticMapUrl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.staticmapImgUrl = resp.data;
                });
                if (addressline2 == '') {
                    var addressString = ''.concat(addressline1.toString(), ",", postal_code.toString());
                } else {
                    var addressString = ''.concat(addressline1.toString(), ",", addressline2.toString(), ",", postal_code.toString());
                }
                var params = {
                    address: addressString
                }
                var url = 'api/AgrGoogleMapsAPI/GetPlaceImage';
                SocketService.getparams(url, params).then(function (resp) {
                    var photoUrlArray = [];
                    for (var i = 0; i < resp.data.length; i++) {
                        if (resp.data[i] != null) {
                            photoUrlArray[i] = resp.data[i];
                        }
                    }
                    if (photoUrlArray.length == 0) {
                        $scope.photoNotFound = true;
                    } else {
                        $scope.photoUrlList = photoUrlArray;
                        $scope.photoFound = true;
                    }
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.individualdocument_upload = function (val, val1, name) {
            lockUI();
            if (($scope.cboIndividualDocument == null) || ($scope.cboIndividualDocument == '') || ($scope.cboIndividualDocument == undefined)
                || ($scope.cboindividualdocumenttype == null) || ($scope.cboindividualdocumenttype == '') || ($scope.cboindividualdocumenttype == undefined))
            {
                $("#fileIndividuaDocument").val('');
                Notify.alert('Kindly Enter the Document Title', 'warning');
                unlockUI();
            }
            else {
                var frm = new FormData();
                
                for (var i = 0; i < val.length; i++) {
                    var item = {
                        name: val[i].name,
                        file: val[i]
                    };
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
    
                            if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                return false;
                            }
                }

                frm.append('document_title', $scope.cboIndividualDocument.individualdocument_name);
                frm.append('individualdocument_gid', $scope.cboIndividualDocument.individualdocument_gid);
                frm.append('documenttype_gid', $scope.cboindividualdocumenttype.documenttypes_gid);
                frm.append('documenttype_name', $scope.cboindividualdocumenttype.documenttype_name);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/AgrMstApplicationAdd/IndividualDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $("#fileIndividuaDocument").val('');
                        $scope.cboIndividualDocument = '';
                        $scope.cboindividualdocumenttype = '';
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

                        var url = 'api/AgrMstApplicationAdd/GetIndividualDocList';
                        SocketService.get(url).then(function (resp) {
                            $scope.uploadindividualdoc_list = resp.data.uploadindividualdoc_list;
                        });

                        unlockUI();
                    });
                }
                else {
                    alert('Please select a file.')
                }
            }
        }

        $scope.individualdocument_delete = function (contact2document_gid) {

            var params = {
                contact2document_gid: contact2document_gid
            }
            lockUI();
            var url = 'api/AgrMstApplicationAdd/IndividualDocDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
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
                var url = 'api/AgrMstApplicationAdd/GetIndividualDocList';
                SocketService.get(url).then(function (resp) {
                    $scope.uploadindividualdoc_list = resp.data.uploadindividualdoc_list;
                });
                unlockUI();
            });
        }

        $scope.individual_save = function () {

            var lsgender_gid = '';
            var lsgender_name = '';
            var lsdesignation_gid = '';
            var lsdesignation_name = '';
            var lsstakeholdertype_gid = '';
            var lsstakeholdertype_name = '';
            var lsmaritalstatus_gid = '';
            var lsmaritalstatus_name = '';
            var lseducationalqualification_gid = '';
            var lseducationalqualification_name = '';
            var lsownershiptype_gid = '';
            var lsownershiptype_name = '';
            var lsincometype_gid = '';
            var lsincometype_name = '';
            var lsresidencetype_gid = '';
            var lsresidencetype_name = '';
            var lspropertyin_gid = '';
            var lspropertyin_name = '';
            var lsgroup_gid = '';
            var lsgroup_name = '';
            var lsinstitution_gid = '';
            var lsinstitution_name = '';
            var panabsencereasons_checked = false;

            for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                if ($scope.panabsencereason_list[i].checked == true) {
                    panabsencereasons_checked = true;
                    break;
                }
            }

            if ($scope.cboGender != undefined || $scope.cboGender != null) {
                lsgender_gid = $scope.cboGender.gender_gid;
                lsgender_name = $scope.cboGender.gender_name;
            }
            if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                lsdesignation_gid = $scope.cboDesignation.designation_gid;
                lsdesignation_name = $scope.cboDesignation.designation_type;
            }
            if ($scope.cboStakeholderType != undefined || $scope.cboStakeholderType != null) {
                lsstakeholdertype_gid = $scope.cboStakeholderType.usertype_gid;
                lsstakeholdertype_name = $scope.cboStakeholderType.user_type;
            }
            if ($scope.cboMaritalStatus != undefined || $scope.cboMaritalStatus != null) {
                lsmaritalstatus_gid = $scope.cboMaritalStatus.maritalstatus_gid;
                lsmaritalstatus_name = $scope.cboMaritalStatus.maritalstatus_name;
            }
            if ($scope.cboEducationalQualification != undefined || $scope.cboEducationalQualification != null) {
                lseducationalqualification_gid = $scope.cboEducationalQualification.educationalqualification_gid;
                lseducationalqualification_name = $scope.cboEducationalQualification.educationalqualification_name;
            }
            if ($scope.cboOwnershipType != undefined || $scope.cboOwnershipType != null) {
                lsownershiptype_gid = $scope.cboOwnershipType.ownershiptype_gid;
                lsownershiptype_name = $scope.cboOwnershipType.ownershiptype_name;
            }
            if ($scope.cboIncomeType != undefined || $scope.cboIncomeType != null) {
                lsincometype_gid = $scope.cboIncomeType.incometype_gid;
                lsincometype_name = $scope.cboIncomeType.incometype_name;
            }
            if ($scope.cboResidenceType != undefined || $scope.cboResidenceType != null) {
                lsresidencetype_gid = $scope.cboResidenceType.residencetype_gid;
                lsresidencetype_name = $scope.cboResidenceType.residencetype_name;
            }
            if ($scope.cboPropertyin != undefined || $scope.cboPropertyin != null) {
                lspropertyin_gid = $scope.cboPropertyin.propertyin_gid;
                lspropertyin_name = $scope.cboPropertyin.propertyin_name;
            }
            if ($scope.cboGroup != undefined || $scope.cboGroup != null) {
                lsgroup_gid = $scope.cboGroup.group_gid;
                lsgroup_name = $scope.cboGroup.group_name;
            }
            if ($scope.cboInstitution != undefined || $scope.cboInstitution != null) {
                lsinstitution_gid = $scope.cboInstitution.institution_gid;
                lsinstitution_name = $scope.cboInstitution.institution_name;
            }
            if ($scope.cboGroup == 'NA') {
                lsgroup_gid = 'NA';
                lsgroup_name = 'NA';
            }
            if ($scope.cboInstitution == 'NA') {
                lsinstitution_gid = 'NA';
                lsinstitution_name = 'NA';
            }

            //if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
            //    Notify.alert('Kindly Enter URN', 'warning')
            //}
            //else

                if (lsstakeholdertype_gid == '' || lsstakeholdertype_gid == undefined || lsstakeholdertype_gid == null) {
                Notify.alert('Kindly Select Stakeholder Type', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting PAN') && ($scope.txtpan_no == '' || $scope.txtpan_no == undefined || $scope.txtpan_no == null)) {
                Notify.alert('Kindly Enter PAN Value', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && ($scope.contactpanform60_list == '' || $scope.contactpanform60_list == undefined || $scope.contactpanform60_list == null)) {
                Notify.alert('Kindly Upload Form 60 Document', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && (panabsencereasons_checked == false)) {
                Notify.alert('Kindly Select Reasons for Uploading Form 60 Document', 'warning')
            }
            else if ($scope.AlreadyaddedIndividualaadhar == true || $scope.AlreadyaddedIndividualpan == true) {
                if ($scope.AlreadyaddedIndividualaadhar == true && $scope.AlreadyaddedIndividualpan == true) {
                    Notify.alert('PAN & Aadhar number is already approved, you cannot add', 'warning')
                }
                else if ($scope.AlreadyaddedIndividualaadhar == true && $scope.AlreadyaddedIndividualpan == false)
                    Notify.alert('Aadhar number is already approved, you cannot add', 'warning')
                else
                    Notify.alert('PAN number is already approved, you cannot add', 'warning')
            }
            else {
                var panabsencereason_selectedList = [];
                angular.forEach($scope.panabsencereason_list, function (val) {

                    if (val.checked == true) {
                        var panabsencereason = val.panabsencereason;
                        panabsencereason_selectedList.push(panabsencereason);
                    }

                });
                var params = {
                    pan_status: $scope.cbopanstatus,
                    pan_no: $scope.txtpan_no,
                    aadhar_no: $scope.txtaadhar_no,
                    first_name: $scope.txtfirst_name,
                    middle_name: $scope.txtmiddle_name,
                    last_name: $scope.txtlast_name,
                    individual_dob: $scope.txtindividual_dob,
                    age: $scope.txtage,
                    gender_gid: lsgender_gid,
                    gender_name: lsgender_name,
                    designation_gid: lsdesignation_gid,
                    designation_name: lsdesignation_name,
                    pep_status: $scope.rdbpep_status,
                    pepverified_date: $scope.txtpepverified_date,
                    stakeholdertype_gid: lsstakeholdertype_gid,
                    stakeholdertype_name: lsstakeholdertype_name,
                    maritalstatus_gid: lsmaritalstatus_gid,
                    maritalstatus_name: lsmaritalstatus_name,
                    father_firstname: $scope.txtfather_firstname,
                    father_middlename: $scope.txtfather_middlename,
                    father_lastname: $scope.txtfather_lastname,
                    father_dob: $scope.txtfather_dob,
                    father_age: $scope.txtfather_age,
                    mother_firstname: $scope.txtmother_firstname,
                    mother_middlename: $scope.txtmother_middlename,
                    mother_lastname: $scope.txtmother_lastname,
                    mother_dob: $scope.txtmother_dob,
                    mother_age: $scope.txtmother_age,
                    spouse_firstname: $scope.txtspouse_firstname,
                    spouse_middlename: $scope.txtspouse_middlename,
                    spouse_lastname: $scope.txtspouse_lastname,
                    spouse_dob: $scope.txtspouse_dob,
                    spouse_age: $scope.txtspouse_age,
                    educationalqualification_gid: lseducationalqualification_gid,
                    educationalqualification_name: lseducationalqualification_name,
                    main_occupation: $scope.txtmain_occupation,
                    annual_income: $scope.txtannual_income,
                    monthly_income: $scope.txtmonthly_income,
                    incometype_gid: lsincometype_gid,
                    incometype_name: lsincometype_name,
                    ownershiptype_gid: lsownershiptype_gid,
                    ownershiptype_name: lsownershiptype_name,
                    propertyholder_gid: lspropertyin_gid,
                    propertyholder_name: lspropertyin_name,
                    residencetype_gid: lsresidencetype_gid,
                    residencetype_name: lsresidencetype_name,
                    currentresidence_years: $scope.txtcurrentresidence_years,
                    branch_distance: $scope.txtbranch_distance,
                    application_gid: $scope.application_gid,

                    group_gid: lsgroup_gid,
                    group_name: lsgroup_name,
                    profile: $scope.txtprofile,
                    urn_status: $scope.rdburn_status,
                    urn: $scope.txt_urn,
                    fathernominee_status: $scope.rdbfathernominee_status,
                    mothernominee_status: $scope.rdbmothernominee_status,
                    spousenominee_status: $scope.rdbspousenominee_status,
                    othernominee_status: $scope.rdbothernominee_status,
                    relationshiptype: $scope.txtrelationshiptype,
                    nomineefirst_name: $scope.txtnomineefirst_name,
                    nominee_middlename: $scope.txtnominee_middlename,
                    nominee_lastname: $scope.txtnominee_lastname,
                    nominee_dob: $scope.txtnominee_dob,
                    nominee_age: $scope.txtnominee_age,
                    totallandinacres: $scope.txttotallandinacres,
                    cultivatedland: $scope.txtcultivatedland,
                    previouscrop: $scope.txtpreviouscrop,
                    prposedcrop: $scope.txtprposedcrop,
                    institution_gid: lsinstitution_gid,
                    institution_name: lsinstitution_name,
                    program_gid: $scope.program_gid,
                    panabsencereason_selectedlist: panabsencereason_selectedList
                }
                var url = 'api/AgrMstApplicationEdit/SaveIndividualDtlAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url('app/AgrMstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
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

        $scope.individual_submit = function () {

            //if ($scope.uploadindividualdoc_list == null) {
            //    Notify.alert("Kindly upload the document", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            //else {

            var lsgender_gid = '';
            var lsgender_name = '';
            var lsdesignation_gid = '';
            var lsdesignation_name = '';
            var lsstakeholdertype_gid = '';
            var lsstakeholdertype_name = '';
            var lsmaritalstatus_gid = '';
            var lsmaritalstatus_name = '';
            var lseducationalqualification_gid = '';
            var lseducationalqualification_name = '';
            var lsownershiptype_gid = '';
            var lsownershiptype_name = '';
            var lsincometype_gid = '';
            var lsincometype_name = '';
            var lsresidencetype_gid = '';
            var lsresidencetype_name = '';
            var lspropertyin_gid = '';
            var lspropertyin_name = '';
            var lsgroup_gid = '';
            var lsgroup_name = '';
            var lsinstitution_gid = '';
            var lsinstitution_name = '';
            var panabsencereasons_checked = false;

            for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                if ($scope.panabsencereason_list[i].checked == true) {
                    panabsencereasons_checked = true;
                    break;
                }
            }

            if ($scope.cboGender != undefined || $scope.cboGender != null) {
                lsgender_gid = $scope.cboGender.gender_gid;
                lsgender_name = $scope.cboGender.gender_name;
            }
            if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                lsdesignation_gid = $scope.cboDesignation.designation_gid;
                lsdesignation_name = $scope.cboDesignation.designation_name;
            }
            if ($scope.cboStakeholderType != undefined || $scope.cboStakeholderType != null) {
                lsstakeholdertype_gid = $scope.cboStakeholderType.usertype_gid;
                lsstakeholdertype_name = $scope.cboStakeholderType.user_type;
            }
            if ($scope.cboMaritalStatus != undefined || $scope.cboMaritalStatus != null) {
                lsmaritalstatus_gid = $scope.cboMaritalStatus.maritalstatus_gid;
                lsmaritalstatus_name = $scope.cboMaritalStatus.maritalstatus_name;
            }
            if ($scope.cboEducationalQualification != undefined || $scope.cboEducationalQualification != null) {
                lseducationalqualification_gid = $scope.cboEducationalQualification.educationalqualification_gid;
                lseducationalqualification_name = $scope.cboEducationalQualification.educationalqualification_name;
            }
            if ($scope.cboOwnershipType != undefined || $scope.cboOwnershipType != null) {
                lsownershiptype_gid = $scope.cboOwnershipType.ownershiptype_gid;
                lsownershiptype_name = $scope.cboOwnershipType.ownershiptype_name;
            }
            if ($scope.cboIncomeType != undefined || $scope.cboIncomeType != null) {
                lsincometype_gid = $scope.cboIncomeType.incometype_gid;
                lsincometype_name = $scope.cboIncomeType.incometype_name;
            }
            if ($scope.cboResidenceType != undefined || $scope.cboResidenceType != null) {
                lsresidencetype_gid = $scope.cboResidenceType.residencetype_gid;
                lsresidencetype_name = $scope.cboResidenceType.residencetype_name;
            }
            if ($scope.cboPropertyin != undefined || $scope.cboPropertyin != null) {
                lspropertyin_gid = $scope.cboPropertyin.propertyin_gid;
                lspropertyin_name = $scope.cboPropertyin.propertyin_name;
            }
            if ($scope.cboGroup != undefined || $scope.cboGroup != null) {
                lsgroup_gid = $scope.cboGroup.group_gid;
                lsgroup_name = $scope.cboGroup.group_name;
            }
            if ($scope.cboInstitution != undefined || $scope.cboInstitution != null) {
                lsinstitution_gid = $scope.cboInstitution.institution_gid;
                lsinstitution_name = $scope.cboInstitution.institution_name;
            }

            if ($scope.cboGroup == 'NA') {
                lsgroup_gid = 'NA';
                lsgroup_name = 'NA';
            }
            if ($scope.cboInstitution == 'NA') {
                lsinstitution_gid = 'NA';
                lsinstitution_name = 'NA';
            }

            //if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
            //    Notify.alert('Kindly Enter URN', 'warning')
            //}
            //else
                if ($scope.rdbfathernominee_status == '' && $scope.rdbmothernominee_status == '' && $scope.rdbspousenominee_status == '' && $scope.rdbothernominee_status == '') {
                Notify.alert('Kindly Select One Nominee Status', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting PAN') && ($scope.txtpan_no == '' || $scope.txtpan_no == undefined || $scope.txtpan_no == null)) {
                Notify.alert('Kindly Enter PAN Value', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && ($scope.contactpanform60_list == '' || $scope.contactpanform60_list == undefined || $scope.contactpanform60_list == null)) {
                Notify.alert('Kindly Upload Form 60 Document', 'warning')
            }
            else if (($scope.cbopanstatus == 'Customer Submitting Form 60') && (panabsencereasons_checked == false)) {
                Notify.alert('Kindly Select Reasons for Uploading Form 60 Document', 'warning')
            }
            else if ($scope.AlreadyaddedIndividualaadhar == true || $scope.AlreadyaddedIndividualpan == true) {
                if ($scope.AlreadyaddedIndividualaadhar == true && $scope.AlreadyaddedIndividualpan == true) {
                    Notify.alert('PAN & Aadhar number is already approved, you cannot add', 'warning')
                }
                else if ($scope.AlreadyaddedIndividualaadhar == true && $scope.AlreadyaddedIndividualpan == false)
                    Notify.alert('Aadhar number is already approved, you cannot add', 'warning')
                else
                    Notify.alert('PAN number is already approved, you cannot add', 'warning')
            }
            else if ($scope.rdbfathernominee_status == 'Yes' || $scope.rdbmothernominee_status == 'Yes' || $scope.rdbspousenominee_status == 'Yes' || $scope.rdbothernominee_status == 'Yes') {
                var panabsencereason_selectedList = [];
                angular.forEach($scope.panabsencereason_list, function (val) {

                    if (val.checked == true) {
                        var panabsencereason = val.panabsencereason;
                        panabsencereason_selectedList.push(panabsencereason);
                    }

                });
                var params = {
                    pan_status: $scope.cbopanstatus,
                    pan_no: $scope.txtpan_no,
                    aadhar_no: $scope.txtaadhar_no,
                    first_name: $scope.txtfirst_name,
                    middle_name: $scope.txtmiddle_name,
                    last_name: $scope.txtlast_name,
                    individual_dob: $scope.txtindividual_dob,
                    age: $scope.txtage,
                    gender_gid: lsgender_gid,
                    gender_name: lsgender_name,
                    designation_gid: lsdesignation_gid,
                    designation_name: lsdesignation_name,
                    pep_status: $scope.rdbpep_status,
                    pepverified_date: $scope.txtpepverified_date,
                    stakeholdertype_gid: lsstakeholdertype_gid,
                    stakeholdertype_name: lsstakeholdertype_name,
                    maritalstatus_gid: lsmaritalstatus_gid,
                    maritalstatus_name: lsmaritalstatus_name,
                    father_firstname: $scope.txtfather_firstname,
                    father_middlename: $scope.txtfather_middlename,
                    father_lastname: $scope.txtfather_lastname,
                    father_dob: $scope.txtfather_dob,
                    father_age: $scope.txtfather_age,
                    mother_firstname: $scope.txtmother_firstname,
                    mother_middlename: $scope.txtmother_middlename,
                    mother_lastname: $scope.txtmother_lastname,
                    mother_dob: $scope.txtmother_dob,
                    mother_age: $scope.txtmother_age,
                    spouse_firstname: $scope.txtspouse_firstname,
                    spouse_middlename: $scope.txtspouse_middlename,
                    spouse_lastname: $scope.txtspouse_lastname,
                    spouse_dob: $scope.txtspouse_dob,
                    spouse_age: $scope.txtspouse_age,
                    educationalqualification_gid: lseducationalqualification_gid,
                    educationalqualification_name: lseducationalqualification_name,
                    main_occupation: $scope.txtmain_occupation,
                    annual_income: $scope.txtannual_income,
                    monthly_income: $scope.txtmonthly_income,
                    incometype_gid: lsincometype_gid,
                    incometype_name: lsincometype_name,
                    ownershiptype_gid: lsownershiptype_gid,
                    ownershiptype_name: lsownershiptype_name,
                    propertyholder_gid: lspropertyin_gid,
                    propertyholder_name: lspropertyin_name,
                    residencetype_gid: lsresidencetype_gid,
                    residencetype_name: lsresidencetype_name,
                    currentresidence_years: $scope.txtcurrentresidence_years,
                    branch_distance: $scope.txtbranch_distance,
                    application_gid: $scope.application_gid,

                    group_gid: lsgroup_gid,
                    group_name: lsgroup_name,
                    profile: $scope.txtprofile,
                    urn_status: $scope.rdburn_status,
                    urn: $scope.txt_urn,
                    fathernominee_status: $scope.rdbfathernominee_status,
                    mothernominee_status: $scope.rdbmothernominee_status,
                    spousenominee_status: $scope.rdbspousenominee_status,
                    othernominee_status: $scope.rdbothernominee_status,
                    relationshiptype: $scope.txtrelationshiptype,
                    nomineefirst_name: $scope.txtnomineefirst_name,
                    nominee_middlename: $scope.txtnominee_middlename,
                    nominee_lastname: $scope.txtnominee_lastname,
                    nominee_dob: $scope.txtnominee_dob,
                    nominee_age: $scope.txtnominee_age,
                    totallandinacres: $scope.txttotallandinacres,
                    cultivatedland: $scope.txtcultivatedland,
                    previouscrop: $scope.txtpreviouscrop,
                    prposedcrop: $scope.txtprposedcrop,
                    institution_gid: lsinstitution_gid,
                    institution_name: lsinstitution_name,
                    program_gid: $scope.program_gid,
                    panabsencereason_selectedlist: panabsencereason_selectedList
                }
                var url = 'api/AgrMstApplicationEdit/SubmitIndividualDtlAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url('app/AgrMstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        //$location.url('app/AgrMstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
                    }

                });
            }
            else {
                Notify.alert('Kindly Select One Nominee Status as Yes', 'warning')
            }
            //}

        }

        $scope.downloads = function (val1, val2) {
            //var phyPath = val1;
            //var relPath = phyPath.split("StoryboardAPI");
            //var relpath1 = relPath[1].replace("\\", "/");
            //var hosts = window.location.host;
            //var prefix = location.protocol + "//";
            //var str = prefix.concat(hosts, relpath1);
            //var link = document.createElement("a");
            //link.download = val2;
            //var uri = str;
            //link.href = uri;
            //link.click();
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.Back = function () {
            $state.go('app.AgrMstApplicationCreationSummary');
        }

        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls = true;
            }
        }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationIndividualEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Individual_dtls = true;
            }
        }

        $scope.Group_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationGroupEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Group_dtls = true;
            }
        }
        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationInstitutionEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Institution_dtls = true;
            }
        }
        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationSocialTradeCapitalEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.EconomicCapital_dtls = true;
            }
        }

        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ProductCharges_dtls = true;
            }
            else {
                $location.url('app/AgrMstApplicationProductChargesEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditProductAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditChargeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.Tradeclick = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Trade_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditTradeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }


        $scope.doneclick = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                unlockUI();
            });
            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && $scope.approveinitiated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                    $scope.resubmitshow = false;
                }
                else if ($scope.proceed_flag == 'Y' && $scope.approveinitiated_flag == 'N' && $scope.hierarchyupdated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_enable = true;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'Y' && approveinitiated_flag == 'Y') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = true;
                    $scope.done_enable = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N' && approveinitiated_flag == 'N') {
                    $scope.hierarchyshow = false;
                    $scope.resubmitshow = false;
                    $scope.done_disable = true;
                    $scope.resubmitshow = false;
                }
                else {

                }
            });
        }

        $scope.overallsubmit_application = function () {
            var url = 'api/AgrMstApplicationEdit/EditAppProceed';
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
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.AgrMstApplicationCreationSummary');
            });

        }

        $scope.submit = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstApplicationEdit/EditAppReProceed';
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
                $state.go('app.AgrMstApplicationCreationSummary');
            });

        }

        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstAppEditHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstApplicationEditCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.BureauUpdates_dtls = true;
            }
        }

        $scope.Back = function () {
            $state.go('app.AgrMstApplicationCreationSummary');
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

                var url = 'api/AgrMstApplicationAdd/FnKycDcoumentValidation';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == false) {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $modalInstance.close('closed');
                    }
                    else {

                    }

                });


                var url = 'api/AgrMstApplicationAdd/GetApprovalHierarchyChangeList';
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
                    var url = 'api/AgrMstApplicationAdd/UpdateApprovalHierarchyChange';
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

        // PAN Change

        $scope.change_pan = function (cbopanstatus) {
            if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                $scope.havepan = true;
                $scope.havenotpan = false;
                angular.forEach($scope.panabsencereason_list, function (val) {
                    val.checked = false;
                });
                var url = 'api/AgrMstApplicationAdd/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60_list;
                    $scope.contactpanform60_list = '';
                });
            }
            else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                $scope.havenotpan = true;
                $scope.havepan = false;
                $scope.view_nopanreasons = true;
                $scope.txtpan_no = '';
                $scope.panvalidation = false;
                $scope.txtfirst_name = '';
                $scope.txtmiddle_name = '';
                $scope.txtlast_name = '';
            }
            else {
                $scope.havepan = false;
                $scope.havenotpan = false;
            }
        }

        $scope.pandtl_submit = function () {

            var panabsencereason_selectedList = [];
            angular.forEach($scope.panabsencereason_list, function (val) {

                if (val.checked == true) {
                    var panabsencereason = val.panabsencereason;
                    panabsencereason_selectedList.push(panabsencereason);
                }

            });

            var params = {
                panabsencereason_selectedlist: panabsencereason_selectedList,
            }
            var url = 'api/AgrMstApplicationAdd/PostPANAbsenceReasons';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.view_nopanreasons = false;
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

        }
        $scope.pandtl_close = function () {
            $scope.view_nopanreasons = false;
        }

        $scope.IndividualPANForm60DocumentUpload = function (val, val1, name) {
            lockUI();
            var frm = new FormData();
            
            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
    
                            if (IsValidExtension == false) {
                                Notify.alert("File format is not supported..!", {
                                    status: 'danger',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                unlockUI();
                                return false;
                            }
            }
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/AgrMstApplicationAdd/PANForm60DocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#panform60file").val('');
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#panform60file").val('');
                        $scope.txtindividualpanform60_document = ''
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                    var url = 'api/AgrMstApplicationAdd/GetPANForm60List';
                    SocketService.get(url).then(function (resp) {
                        $scope.contactpanform60_list = resp.data.contactpanform60_list;
                    });

                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }

        }

        $scope.IndividualPANForm60DocumentDelete = function (contact2panform60_gid) {

            var params = {
                contact2panform60_gid: contact2panform60_gid
            }
            lockUI();
            var url = 'api/AgrMstApplicationAdd/PANForm60Delete';
            SocketService.getparams(url, params).then(function (resp) {

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
                var url = 'api/AgrMstApplicationAdd/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60_list;
                });
                unlockUI();
            });
        }

        $scope.PANIndividualValidation = function () {
            if ($scope.txtpan_no != undefined && $scope.txtpan_no.length == 10) {
                $scope.AlreadyaddedIndividualpanaadhar = false;
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/AgrKyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.panvalidation = true;
                        var parts = resp.data.result.name.split(" ");
                        if (parts.length == 3) {
                            $scope.txtfirst_name = parts[0];
                            $scope.txtmiddle_name = parts[1];
                            $scope.txtlast_name = parts[2];
                        } else {
                            $scope.txtfirst_name = parts[0];
                            $scope.txtlast_name = parts[1];
                        }
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.panvalidation = false;
                        Notify.alert('PAN is not verified..!', 'warning');
                        $scope.txtfirst_name = '';
                        $scope.txtmiddle_name = '';
                        $scope.txtlast_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                }); 
               
            }

            if (($scope.cboStakeholderType != undefined && $scope.cboStakeholderType != null) || ($scope.cboStakeholderType != undefined && $scope.cboStakeholderType != null)) {
                var getStakeholderType = "";
                if ($scope.cboStakeholderType != undefined && $scope.cboStakeholderType != "") {
                    getStakeholderType = $scope.cboStakeholderType;
                }
                else {
                    var getStakeholderType = $scope.usertype_list.find(function (v) {
                        return v.usertype_gid === $scope.cboStakeholderType
                    });
                }
               
                if (getStakeholderType != null && getStakeholderType != "") {
                    if (getStakeholderType.user_type != "") { 
                        lockUI();
 						var pan_no = ($scope.txtpan_no =="" || $scope.txtpan_no ==undefined) ? 'No': $scope.txtpan_no;
                        var params = {
                            pan_no: pan_no,
                            aadhar_no: $scope.txtaadhar_no,
                            institution_gid: 'No',
                            contact_gid: $scope.contact_gid,
                            application_gid: $scope.application_gid,
							stakeholder_type: getStakeholderType.user_type
                        }
                        var url = 'api/AgrMstApplicationAdd/GetOnboardAppValidatePANAadhar';
                        SocketService.post(url, params).then(function (resp) {
                            $scope.lblcreated_by = resp.data.lscreatedby_name;
                            unlockUI();
                            if (resp.data.status == true) {
                                if (resp.data.panoraadhar == "PAN" && $scope.txtpan_no != null){
                                    $scope.AlreadyaddedIndividualpan = true;
                                    $scope.AlreadyaddedIndividualaadhar = false;
                                }  
                                else if (resp.data.panoraadhar == "Aadhar"){
                                    $scope.AlreadyaddedIndividualaadhar = true;
                                    $scope.AlreadyaddedIndividualpan = false;
                                } 
                                else if (resp.data.panoraadhar == "Both") {
                                    $scope.AlreadyaddedIndividualpan = true;
                                    $scope.AlreadyaddedIndividualaadhar = true;
                                }
                                else {
                                    $scope.AlreadyaddedIndividualaadhar = false;
                                    $scope.AlreadyaddedIndividualpan = false;
                                }
                            }
                            else {
                                $scope.AlreadyaddedIndividualaadhar = false;
                                $scope.AlreadyaddedIndividualpan = false;
                            }
                        });
                    }
                    else {
                        $scope.AlreadyaddedIndividualaadhar = false;
                        $scope.AlreadyaddedIndividualpan = false;
                    }
                }
            }
        }

        $scope.AadharIndividualValidation = function () {
            var getStakeholderType = $scope.usertype_list.find(function (v) {
                return v.usertype_gid === $scope.cboStakeholderType
            });
            if ((getStakeholderType != undefined && getStakeholderType.user_type != "") || ($scope.cboStakeholderType.user_type != "")) { 
                $scope.AlreadyaddedIndividualpanaadhar = false; 
                lockUI();
                var params = {
                    pan_no: 'No',
                    aadhar_no: $scope.txtaadhar_no,
                    institution_gid: 'No',
                    contact_gid: $scope.contact_gid,
                    application_gid: $scope.application_gid,
					stakeholder_type: $scope.cboStakeholderType.user_type
                }
                var url = 'api/AgrMstApplicationAdd/GetOnboardAppValidatePANAadhar';
                SocketService.post(url, params).then(function (resp) {
                    $scope.lblcreated_by = resp.data.lscreatedby_name;
                    unlockUI();
                    if (resp.data.status == true) {
                        if (resp.data.panoraadhar == "Aadhar")
                            $scope.AlreadyaddedIndividualaadhar = true
                        else
                            $scope.AlreadyaddedIndividualaadhar = false;
                    }
                    else {
                        $scope.AlreadyaddedIndividualaadhar = false;
                    }
                });
            }
            else {
                $scope.AlreadyaddedIndividualaadhar = false;
            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.contactpanform60_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.contactpanform60_list[i].document_path, $scope.contactpanform60_list[i].document_name);
            }
        }
        $scope.downloadall_2 = function () {
            for (var i = 0; i < $scope.contactidproof_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.contactidproof_list[i].document_path, $scope.contactidproof_list[i].document_name);
            }
        }
        $scope.downloadall_3 = function () {
            for (var i = 0; i < $scope.uploadindividualdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.uploadindividualdoc_list[i].document_path, $scope.uploadindividualdoc_list[i].document_name);
            }
        }

        $scope.IFSCValidation = function () {

            if ($scope.txtIFSC_Code.length == 11) {
                var params = {
                    ifsc: $scope.txtIFSC_Code
                }
                lockUI();
                var url = 'api/AgrKyc/IfscVerification';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                        $scope.ifscvalidation = true;
                        $scope.txtBank_Name = resp.data.result.bank;
                        $scope.txtBranch_Name = resp.data.result.branch;
                        $scope.txtBank_Address = resp.data.result.address;
                        $scope.txtMICR_Code = resp.data.result.micr;

                        if (resp.data.result.micr != "" && resp.data.result.micr != null) {
                            $scope.micrempty = true;
                        }

                    } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                        $scope.ifscvalidation = false;
                        Notify.alert('IFSC is not verified..!', 'warning');
                        $scope.txtBank_Name = '';
                        $scope.txtBranch_Name = '';
                        $scope.txtBank_Address = '';
                        $scope.txtMICR_Code = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.BankAccValidation = function () {
            if ($scope.txtbankacct_no == $scope.txtconfirmbankacct_no) {
                var params = {
                    ifsc: $scope.txtIFSC_Code,
                    accountNumber: $scope.txtconfirmbankacct_no
                }
                var url = 'api/AgrKyc/BankAccVerification';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                        $scope.bankaccvalidation = true;
                        $scope.txtacctholder_name = resp.data.result.accountName;

                    } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                        $scope.bankaccvalidation = false;
                        Notify.alert('Bank Account is not verified..!', 'warning');
                        $scope.txtacctholder_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.add_indbankacctdtl = function () {

            if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
                ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
            ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.rdbprimarystatus == undefined) || ($scope.rdbprimarystatus == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '')
                || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                if ($scope.rdbJoint_Account == 'Yes') {

                    if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                        Notify.alert('Enter Joint Account Holder Name', 'warning');
                    }
                    else {
                        var params = {
                            application_gid: $scope.application_gid,
                            //institution_gid: $scope.institution_gid,
                            applicant_type: 'Institution',
                            ifsc_code: $scope.txtIFSC_Code,
                            bank_name: $scope.txtBank_Name,
                            branch_name: $scope.txtBranch_Name,
                            Bank_Address: $scope.txtBank_Address,
                            micr_code: $scope.txtMICR_Code,
                            bankaccount_number: $scope.txtbankacct_no,
                            confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                            bankaccount_name: $scope.txtacctholder_name,
                            bankaccounttype_gid: $scope.cboAccountType.bankaccounttype_gid,
                            bankaccounttype_name: $scope.cboAccountType.bankaccounttype_name,
                            joint_account: $scope.rdbJoint_Account,
                            jointaccountholder_name: $scope.txtjointacctholder_name,
                            chequebook_status: $scope.rdbCheque_Book,
                            accountopen_date: $scope.txtAccountOpen_Date,
                            primary_status: $scope.rdbprimarystatus
                        }
                        var url = 'api/AgrMstApplicationAdd/PostIndividualBank';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                //$scope.creditbankacc_list = resp.data.BuyerOnboardIndividual2bankacc_list;
                                $scope.credituploaddocument_list = null;
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                indbankacc_list();
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $scope.txtIFSC_Code = '';
                            $scope.txtBank_Name = '';
                            $scope.txtBranch_Name = '';
                            $scope.txtBank_Address = '';
                            $scope.txtMICR_Code = '';
                            $scope.txtbankacct_no = '';
                            $scope.txtconfirmbankacct_no = '';
                            $scope.txtacctholder_name = '';
                            $scope.cboAccountType = '';
                            $scope.rdbJoint_Account = '';
                            $scope.txtjointacctholder_name = '';
                            $scope.rdbCheque_Book = '';
                            $scope.txtAccountOpen_Date = '';
                            $scope.txtverify = '';
                            $scope.bankaccvalidation = null;
                            $scope.ifscvalidation = '';
                            $scope.rdbprimarystatus = '';
                            indbankacc_list();
                        });
                    }
                }
                else {
                    var params = {
                        application_gid: $scope.application_gid,
                        //credit_gid: institution_gid,
                        applicant_type: 'Institution',
                        ifsc_code: $scope.txtIFSC_Code,
                        bank_name: $scope.txtBank_Name,
                        branch_name: $scope.txtBranch_Name,
                        Bank_Address: $scope.txtBank_Address,
                        micr_code: $scope.txtMICR_Code,
                        bankaccount_number: $scope.txtbankacct_no,
                        confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                        bankaccount_name: $scope.txtacctholder_name,
                        bankaccounttype_gid: $scope.cboAccountType.bankaccounttype_gid,
                        bankaccounttype_name: $scope.cboAccountType.bankaccounttype_name,
                        joint_account: $scope.rdbJoint_Account,
                        jointaccountholder_name: $scope.txtjointacctholder_name,
                        chequebook_status: $scope.rdbCheque_Book,
                        accountopen_date: $scope.txtAccountOpen_Date,
                        primary_status: $scope.rdbprimarystatus
                    }
                    var url = 'api/AgrMstApplicationAdd/PostIndividualBank';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            //$scope.creditbankacc_list = resp.data.BuyerOnboardIndividual2bankacc_list;

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
                        $scope.txtIFSC_Code = '';
                        $scope.txtBank_Name = '';
                        $scope.txtBranch_Name = '';
                        $scope.txtBank_Address = '';
                        $scope.txtMICR_Code = '';
                        $scope.txtbankacct_no = '';
                        $scope.txtconfirmbankacct_no = '';
                        $scope.txtacctholder_name = '';
                        $scope.cboAccountType = '';
                        $scope.rdbJoint_Account = '';
                        $scope.txtjointacctholder_name = '';
                        $scope.rdbCheque_Book = '';
                        $scope.txtAccountOpen_Date = '';
                        $scope.txtverify = '';
                        $scope.bankaccvalidation = null;
                        $scope.ifscvalidation = '';
                        $scope.rdbprimarystatus = '';
                        indbankacc_list();

                    });
                }

            }
        }

        function indbankacc_list() {
            var url = 'api/AgrMstApplicationAdd/GetIndividualBankAccDtl';
            //var url = 'api/AgrMstApplicationAdd/Individual2bankTmpList';
            SocketService.get(url).then(function (resp) {
                $scope.creditbankacc_list = resp.data.individual2bankacc_list;

            });
        }

        $scope.indbankacctdtl_add = function (contact2bankdtl_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/companybankdtl.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.calender01 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open01 = true;
                };
                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditAccountType';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.accounttype_list = resp.data.creditbankacc_list;
                });

                var param = {
                    contact2bankdtl_gid: contact2bankdtl_gid
                }

                var url = 'api/AgrMstApplicationAdd/EditGetIndividualBankAccDtl';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.txtIFSC_Code = resp.data.ifsc_code;
                    $scope.txtBank_Name = resp.data.bank_name;
                    $scope.txtBranch_Name = resp.data.branch_name;
                    $scope.txtBank_Address = resp.data.bank_address;
                    $scope.txtMICR_Code = resp.data.micr_code;
                    $scope.txtbankacct_no = resp.data.bankaccount_number;
                    $scope.txtconfirmbankacct_no = resp.data.confirmbankaccountnumber;
                    $scope.txtacctholder_name = resp.data.bankaccount_name;
                    $scope.cboAccountType = resp.data.bankaccounttype_gid;
                    $scope.rdbJoint_Account = resp.data.joint_account;
                    $scope.txtjointacctholder_name = resp.data.jointaccountholder_name;
                    $scope.rdbCheque_Book = resp.data.chequebook_status;
                    $scope.txtAccountOpen_Date = resp.data.accountopen_date;
                    $scope.rdbprimarystatus = resp.data.primary_status;
                    //$scope.txtAccountOpen_Date = Date.parse($scope.txtAccountOpen_Date);
                    //$scope.credituploaddocument_list = resp.data.credituploaddocument_list;
                    unlockUI();
                });

                vm.submitted = false;
                vm.validateInput = function (name, type) {
                    var input = vm.formValidate[name];
                    return (input.$dirty || vm.submitted) && input.$error[type];
                };

                // Submit form
                vm.submitForm = function () {
                    vm.submitted = true;
                    if (vm.formValidate.$valid) {
                    } else {
                        return false;
                    }
                };

                // Calender Popup... //

                vm.calender1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    vm.open1 = true;
                };


                vm.formats = ['dd-MM-yyyy'];
                vm.format = vm.formats[0];
                vm.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };

                $scope.change = function () {
                    $scope.txtjointacctholder_name = '';
                }

                $scope.IFSCValidation = function () {

                    if ($scope.txtIFSC_Code.length == 11) {
                        var params = {
                            ifsc: $scope.txtIFSC_Code
                        }
                        lockUI();
                        var url = 'api/AgrKyc/IfscVerification';
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.bank != "" && resp.data.result.bank != null) {
                                $scope.ifscvalidation = true;
                                $scope.txtBank_Name = resp.data.result.bank;
                                $scope.txtBranch_Name = resp.data.result.branch;
                                $scope.txtBank_Address = resp.data.result.address;
                                $scope.txtMICR_Code = resp.data.result.micr;

                                if (resp.data.result.micr != "" && resp.data.result.micr != null) {
                                    $scope.micrempty = true;
                                }

                            } else if (resp.data.result.bank == "" || resp.data.result.bank == null) {
                                $scope.ifscvalidation = false;
                                Notify.alert('IFSC is not verified..!', 'warning');
                                $scope.txtBank_Name = '';
                                $scope.txtBranch_Name = '';
                                $scope.txtBank_Address = '';
                                $scope.txtMICR_Code = '';
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_Bankacctdtl = function () {
                    if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
                       ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
                   ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                        ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.rdbprimarystatus == undefined) || ($scope.rdbprimarystatus == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '')
                        || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
                        Notify.alert('Enter All Mandatory Fields', 'warning');
                    }

                    else if ($scope.txtbankacct_no > $scope.txtconfirmbankacct_no || $scope.txtbankacct_no < $scope.txtconfirmbankacct_no) {
                        Notify.alert('Account Number does not match', 'warning');
                    }

                    else {
                        if ($scope.rdbJoint_Account == 'Yes') {

                            if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                                Notify.alert('Enter Joint Account Holder Name', 'warning');
                            }
                            else {
                                var bankaccounttype_name = $('#AccountType :selected').text();
                                var params = {
                                    contact2bankdtl_gid: contact2bankdtl_gid,
                                    ifsc_code: $scope.txtIFSC_Code,
                                    bank_name: $scope.txtBank_Name,
                                    branch_name: $scope.txtBranch_Name,
                                    Bank_Address: $scope.txtBank_Address,
                                    micr_code: $scope.txtMICR_Code,
                                    bankaccount_number: $scope.txtbankacct_no,
                                    confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                                    bankaccount_name: $scope.txtacctholder_name,
                                    bankaccounttype_gid: $scope.cboAccountType,
                                    bankaccounttype_name: bankaccounttype_name,
                                    joint_account: $scope.rdbJoint_Account,
                                    jointaccountholder_name: $scope.txtjointacctholder_name,
                                    chequebook_status: $scope.rdbCheque_Book,
                                    accountopen_date: $scope.txtAccountOpen_Date,
                                    primary_status: $scope.rdbprimarystatus

                                }
                                var url = 'api/AgrMstApplicationAdd/UpdateIndividualBankAccDtl';
                                lockUI();
                                SocketService.post(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {

                                        Notify.alert(resp.data.message, {
                                            status: 'success',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                        indbankacc_list();
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
                        else {
                            var bankaccounttype_name = $('#AccountType :selected').text();
                            var params = {
                                contact2bankdtl_gid: contact2bankdtl_gid,
                                ifsc_code: $scope.txtIFSC_Code,
                                bank_name: $scope.txtBank_Name,
                                branch_name: $scope.txtBranch_Name,
                                Bank_Address: $scope.txtBank_Address,
                                micr_code: $scope.txtMICR_Code,
                                bankaccount_number: $scope.txtbankacct_no,
                                confirmbankaccountnumber: $scope.txtconfirmbankacct_no,
                                bankaccount_name: $scope.txtacctholder_name,
                                bankaccounttype_gid: $scope.cboAccountType,
                                bankaccounttype_name: bankaccounttype_name,
                                joint_account: $scope.rdbJoint_Account,
                                jointaccountholder_name: $scope.txtjointacctholder_name,
                                chequebook_status: $scope.rdbCheque_Book,
                                accountopen_date: $scope.txtAccountOpen_Date,
                                primary_status: $scope.rdbprimarystatus

                            }
                            var url = 'api/AgrMstApplicationAdd/UpdateIndividualBankAccDtl';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    indbankacc_list();
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

            }
        }
        $scope.indbankacctdtl_delete = function (contact2bankdtl_gid, contact_gid) {
            var params = {
                contact2bankdtl_gid: contact2bankdtl_gid,
                contact_gid: contact_gid
            }
            var url = 'api/AgrMstApplicationAdd/DeleteIndividualBankAcc';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    indbankacc_list();
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

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.documenttype_change = function () {
             var params = {
                 documenttypes_gid: $scope.cboindividualdocumenttype.documenttypes_gid,
            program_gid: $scope.program_gid
        }
            var url = 'api/AgrMstApplication360/IndividualDocumentList';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.individualdocument_list = resp.data.application_list;
        });
    }     
    }
})();
