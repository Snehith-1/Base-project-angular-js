(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADIndividualDtlAddController', MstCADIndividualDtlAddController);

    MstCADIndividualDtlAddController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce', '$anchorScroll', 'cmnfunctionService', 'DownloaddocumentService'];

    function MstCADIndividualDtlAddController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce, $anchorScroll, cmnfunctionService, DownloaddocumentService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADIndividualDtlAddController';       
        activate();

        var application_gid = $location.search().application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
     
        function activate() {
           
            $scope.groupnameYes = true;
            $scope.companynameYes = true;

            $scope.application_gid = $location.search().application_gid;

            // Calender Popup... //

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
            var url = 'api/MstCADApplication/GetIndividualTempClear';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
            });

            var url = 'api/MstApplication360/GetInternalRatingList';
            SocketService.get(url).then(function (resp) {
                $scope.internalrating_list = resp.data.internalrating_list;
            });

            var url = 'api/MstApplication360/GetSamunnatiBranchList';
            SocketService.get(url).then(function (resp) {
                $scope.samunnatibranch_list = resp.data.samunnatibranch_list;
            });

            var url = 'api/MstApplication360/GetPhysicalStatusList';
            SocketService.get(url).then(function (resp) {
                $scope.physicalstatus_list = resp.data.physicalstatus_list;
            });

            var url = 'api/MstApplication360/GenderList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.gender_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/GetDesignationList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.designation_list = resp.data.designation_list;
            });

            var url = 'api/MstApplication360/GetUserTypeCADList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.usertype_list = resp.data.usertype_list;
            });

            var url = 'api/MstApplication360/GetMaritalStatusList';
            lockUI();

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.maritalstatus_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/EducationalQualificationList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.educationalqualification_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/IncomeTypeList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.incometype_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/IndividualProofList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.individualproof_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/OwnershipTypeList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.ownershiptype_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/GetPropertyinNameList';
            lockUI();

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.propertyin_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/ResidenceTypeList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.residencetype_list = resp.data.application_list;
            });

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/MstCADApplication/GetGroupList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.grouplist = resp.data.grouplist;
            });
            var url = 'api/MstCADApplication/GetCompanyList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.institutionlist = resp.data.institutionlist;
            });

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/MstCADApplication/GetEditProductcharges';
            lockUI();
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
                $scope.program_gid = resp.data.program_gid;
                var params = {
                    program_gid: $scope.program_gid
                }
                var url = 'api/MstApplication360/GetIndividualDocTypeList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.individualdoctype_list = resp.data.individualdoctype_list;
                });

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



            var url = 'api/MstCADApplication/PANAbsenceReasonList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.panabsencereason_list = resp.data.panabsencereason_list;
            });

            $scope.havenotpan = false;
            $scope.havepan = false;
            $scope.view_nopanreasons = false; 

            var params = {
                application_gid: $location.search().application_gid
            }
            var url = 'api/MstCADApplication/GetEditProductcharges';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lbleditoveralllimit_amount = resp.data.overalllimit_amount;
                $scope.program_gid = resp.data.program_gid;
                var params = {
                    program_gid: $scope.program_gid
                }
                var url = 'api/MstApplication360/GetIndividualDocTypeList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.individualdoctype_list = resp.data.individualdoctype_list;
                });
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
                var url = 'api/MstCustomerAdd/GetAge';
                  lockUI();
                  SocketService.getparams(url, params).then(function (resp) {
                      unlockUI();
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
                var url = 'api/MstCustomerAdd/GetAge';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtage = resp.data.age;
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
                var url = 'api/MstCustomerAdd/GetAge';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
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
                var url = 'api/MstCustomerAdd/GetAge';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
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
                var url = 'api/MstCustomerAdd/GetAge';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
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
                var url = 'api/MstApplicationAdd/FutureDateCheck';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == false) {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.PANValidation = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/CADKyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if(resp.data.result != null) {
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
                    } 
                    }
                    else {
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
                        var pan_no = ($scope.txtpan_no == "" || $scope.txtpan_no == undefined) ? 'No' : $scope.txtpan_no;
                        var params = {
                            pan_no: pan_no,
                            aadhar_no: $scope.txtaadhar_no,
                            institution_gid: 'No',
                            contact_gid: $scope.contact_gid,
                            application_gid: application_gid,
                            stakeholder_type: getStakeholderType.user_type,
                            panrenewal_flage: 'N',
                            credit_name: 'Individual'
                        }
                        var url = 'api/MstCADApplication/GetOnboardAppValidatePANAadhar';
                        SocketService.post(url, params).then(function (resp) {
                            $scope.lblcreated_by = resp.data.lscreatedby_name;
                            unlockUI();
                            if (resp.data.status == true) {
                                if (resp.data.panoraadhar == "PAN" && $scope.txtpan_no != null) {
                                    $scope.AlreadyaddedIndividualpan = true;
                                    $scope.AlreadyaddedIndividualaadhar = false;
                                }
                                else if (resp.data.panoraadhar == "Aadhar") {
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
        $scope.PANAadhaarLink = function () {
            if ($scope.txtpan_no.length == 10) {
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/CADKyc/PANAadhaarLink';
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
                var url = 'api/CADKyc/VoterID';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if(resp.data.result != null) {
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.idproofvalidation = true;
                        Notify.alert('ID Proof is verified..!', 'success');
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.idproofvalidation = false;
                        Notify.alert('ID Proof is not verified..!', 'warning');
                    } 
                    }   
                    else {
                        Notify.alert(resp.data.message, 'warning');
                    }
                });
            } else if ($scope.cboIndividualProof.individualproof_name == "PAN" && $scope.txtidproof_no.length == 10) {
                var params = {
                    pan: $scope.txtidproof_no
                }
                var url = 'api/CADKyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if(resp.data.result != null) {
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.idproofvalidation = true;
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.idproofvalidation = false;
                        Notify.alert('PAN is not verified..!', 'warning');
                    } 
                    }
                    else {
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
                var url = 'api/CADKyc/DrivingLicenseAuthentication';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.idproofvalidation = true;
                        Notify.alert('ID Proof is verified..!', 'success');
                    } else if (resp.data.statusCode == 102) {
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
                var url = 'api/CADKyc/Passport';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if(resp.data.result != null) {
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.idproofvalidation = true;
                        Notify.alert('ID Proof is verified..!', 'success');
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.idproofvalidation = false;
                        Notify.alert('ID Proof is not verified..!', 'warning');
                    } 
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning');
                    }
                });
            }
        }

        $scope.onchangeIDType = function () {
            if (($scope.cboIndividualProof.individualproof_gid == "INPF2021062300003")) {
                $scope.idprooffilenoshow = false;
                $scope.idproofdobshow = true;
            }
            else if (($scope.cboIndividualProof.individualproof_gid == "INPF2021062300004")) {
                $scope.idproofdobshow = true;
                $scope.idprooffilenoshow = true;
            } else {
                $scope.idproofdobshow = false;
                $scope.idprooffilenoshow = false;
            }
            $scope.txtidproof_no = '';
            $scope.idproofvalidation = false;
            $scope.txtidproof_dob = '';

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
                var url = 'api/MstCADApplication/MobileNumberAdd';
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
                    var url = 'api/MstCADApplication/GetMobileNoList';
                    lockUI();
                    SocketService.get(url).then(function (resp) {
                        unlockUI();
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
            var url = 'api/MstCADApplication/MobileNoDelete';
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
                var url = 'api/MstCADApplication/GetMobileNoList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    unlockUI();
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
                var url = 'api/MstCADApplication/EmailAddressAdd';
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
                    var url = 'api/MstCADApplication/GetEmailAddressList';
                    lockUI();
                    SocketService.get(url).then(function (resp) {
                        unlockUI();
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
            var url = 'api/MstCADApplication/EmailAddressDelete';
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
                var url = 'api/MstCADApplication/GetEmailAddressList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    unlockUI();
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
                        unlockUI();
                        return false;
                    }
                }

                frm.append('idproof_type', $scope.cboIndividualProof.individualproof_name);
                frm.append('idproof_no', $scope.txtidproof_no);
                frm.append('idproof_dob', $scope.txtidproof_dob);
                frm.append('file_no', $scope.txtfile_no);
                frm.append('project_flag', "Default");

                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstCADApplication/IndividualProofDocumentUpload';
                    lockUI();
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

                        var url = 'api/MstCADApplication/GetIndividualProofList';
                        lockUI();
                        SocketService.get(url).then(function (resp) {
                            unlockUI();
                            $scope.contactidproof_list = resp.data.contactidproof_list;
                        });

                       
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
            var url = 'api/MstCADApplication/IndividualProofDelete';
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
                var url = 'api/MstCADApplication/GetIndividualProofList';
                SocketService.get(url).then(function (resp) {
                    $scope.contactidproof_list = resp.data.contactidproof_list;
                });
                unlockUI();
            });
        }

        function individualaddress_list() {
            var url = 'api/MstCADApplication/GetAddressList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
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

                var url = 'api/AddressType/GetAddressTypeASC';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.addresstype_list = resp.data.addresstype_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.onchangepostal_code = function () {
                    var params = {
                        postal_code: $scope.txtpostal_code
                    }
                    var url = 'api/Mstbuyer/GetPostalCodeDetails';                   
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state_name;
                    });
                }

                $scope.getGeoCoding = function () {
                    if($scope.txtpostal_code == undefined){
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
                        var url = 'api/GoogleMapsAPI/GetGeoCoding';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            unlockUI();
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
                    var url = 'api/MstCADApplication/AddressAdd';
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
            var url = 'api/MstCADApplication/AddressDelete';
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
                var url = 'api/GoogleMapsAPI/GetStaticMapUrl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
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
                var url = 'api/GoogleMapsAPI/GetPlaceImage';
                lockUI();

                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
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
                || ($scope.cboindividualdocumenttype == null) || ($scope.cboindividualdocumenttype == '') || ($scope.cboindividualdocumenttype == undefined)) {
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
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstCADApplication/IndividualDocumentUpload';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $("#fileIndividuaDocument").val('');
                        $scope.cboIndividualDocument = '';
                        $scope.cboindividualdocumenttype = "";
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

                        var url = 'api/MstCADApplication/GetIndividualDocListAdd';
                        lockUI();
                        SocketService.get(url).then(function (resp) {
                            unlockUI();
                            $scope.uploadindividualdoc_list = resp.data.uploadindividualdoc_list;
                        });

                        
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
            var url = 'api/MstCADApplication/IndividualDocDelete';
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
                var url = 'api/MstCADApplication/GetIndividualDocList';
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
            var lsnearsamunnatiabranch_gid = '';
            var lsnearsamunnatiabranch_name = '';
            var lsphysicalstatus_gid = '';
            var lsphysicalstatus_name = '';
            var lsinternalrating_gid = '';
            var lsinternalrating_name = '';

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
            if ($scope.cbosamunnatibranchname != undefined || $scope.cbosamunnatibranchname != null) {
                lsnearsamunnatiabranch_gid = $scope.cbosamunnatibranchname.branch_gid;
                lsnearsamunnatiabranch_name = $scope.cbosamunnatibranchname.branch_name;
            }
            if ($scope.cbophysicalstatus != undefined || $scope.cbophysicalstatus != null) {
                lsphysicalstatus_gid = $scope.cbophysicalstatus.physicalstatus_gid;
                lsphysicalstatus_name = $scope.cbophysicalstatus.physicalstatus_name;
            }
            if ($scope.cbointernalrating != undefined || $scope.cbotanstatename != null) {
                lsinternalrating_gid = $scope.cbointernalrating.internalrating_gid;
                lsinternalrating_name = $scope.cbointernalrating.internalrating_name;
            }

            if ($scope.cboGroup == 'NA') {
                lsgroup_gid = 'NA';
                lsgroup_name = 'NA';
            }
            if ($scope.cboInstitution == 'NA') {
                lsinstitution_gid = 'NA';
                lsinstitution_name = 'NA';
            }

            if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                Notify.alert('Kindly Enter URN', 'warning')
            }
            else if (lsstakeholdertype_gid == '' || lsstakeholdertype_gid == undefined || lsstakeholdertype_gid == null) {
                Notify.alert('Kindly Select Stakeholder Type', 'warning')
            }
            else {
                var params = {
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
                    nearsamunnatiabranch_gid: lsnearsamunnatiabranch_gid,
                    nearsamunnatiabranch_name: lsnearsamunnatiabranch_name,
                    physicalstatus_gid: lsphysicalstatus_gid,
                    physicalstatus_name: lsphysicalstatus_name,
                    internalrating_gid: lsinternalrating_gid,
                    internalrating_name: lsinternalrating_name
                }
                var url = 'api/MstCADApplication/SaveIndividualDtlAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        if (lspage == "CADApplicationEdit") {
                            $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
                        }
                        else {

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

        $scope.individual_submit = function () {
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
            var lsnearsamunnatiabranch_gid = '';
            var lsnearsamunnatiabranch_name = '';
            var lsphysicalstatus_gid = '';
            var lsphysicalstatus_name = '';
            var lsinternalrating_gid = '';
            var lsinternalrating_name = '';
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
            if ($scope.cbosamunnatibranchname != undefined || $scope.cbosamunnatibranchname != null) {
                lsnearsamunnatiabranch_gid = $scope.cbosamunnatibranchname.branch_gid;
                lsnearsamunnatiabranch_name = $scope.cbosamunnatibranchname.branch_name;
            }
            if ($scope.cbophysicalstatus != undefined || $scope.cbophysicalstatus != null) {
                lsphysicalstatus_gid = $scope.cbophysicalstatus.physicalstatus_gid;
                lsphysicalstatus_name = $scope.cbophysicalstatus.physicalstatus_name;
            }
            if ($scope.cbointernalrating != undefined || $scope.cbotanstatename != null) {
                lsinternalrating_gid = $scope.cbointernalrating.internalrating_gid;
                lsinternalrating_name = $scope.cbointernalrating.internalrating_name;
            }

            if ($scope.cboGroup == 'NA') {
                lsgroup_gid = 'NA';
                lsgroup_name = 'NA';
            }
            if ($scope.cboInstitution == 'NA') {
                lsinstitution_gid = 'NA';
                lsinstitution_name = 'NA';
            }

            if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                Notify.alert('Kindly Enter URN', 'warning')
            }
            else if ($scope.rdbfathernominee_status == '' && $scope.rdbmothernominee_status == '' && $scope.rdbspousenominee_status == '' && $scope.rdbothernominee_status == '') {
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
            else if (($scope.AlreadyaddedIndividualpan == true) && ($scope.contactpanform60_list == '' || $scope.contactpanform60_list == undefined || $scope.contactpanform60_list == null)) {
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
                    panabsencereason_selectedlist: panabsencereason_selectedList,
                    nearsamunnatiabranch_gid: lsnearsamunnatiabranch_gid,
                    nearsamunnatiabranch_name: lsnearsamunnatiabranch_name,
                    physicalstatus_gid: lsphysicalstatus_gid,
                    physicalstatus_name: lsphysicalstatus_name,
                    internalrating_gid: lsinternalrating_gid,
                    internalrating_name: lsinternalrating_name,
                    program_gid: $scope.program_gid
                }
                var url = 'api/MstCADApplication/SubmitIndividualDtlAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
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
            else {
                Notify.alert('Kindly Select One Nominee Status as Yes', 'warning')
            }
        }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.Back = function () {
            if (lspage == "StartCreditUnderwriting") {
                $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADApplicationEdit") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "CADAcceptanceCustomers") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else if (lspage == "PendingCADReview") {
                $location.url('app/MstCADApplicationEdit?application_gid=' + application_gid + '&lspage=' + lspage);
            }
            else {

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
                var url = 'api/MstApplicationAdd/GetPANForm60List';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    unlockUI();
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
            var url = 'api/MstCADApplication/PostPANAbsenceReasons';
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
                frm.append('project_flag', "Default");
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



            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstCADApplication/PANForm60DocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
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

                    var url = 'api/MstCADApplication/GetPANForm60List';
                    lockUI();
                    SocketService.get(url).then(function (resp) {
                        unlockUI();
                        $scope.contactpanform60_list = resp.data.contactpanform60_list;
                    });

                    
                });
            }
            else {
                alert('Please select a file.')
            }

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

        $scope.IndividualPANForm60DocumentDelete = function (contact2panform60_gid) {

            var params = {
                contact2panform60_gid: contact2panform60_gid
            }
            lockUI();
            var url = 'api/MstCADApplication/PANForm60Delete';
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
                var url = 'api/MstCADApplication/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60_list;
                });
                unlockUI();
            });
        }

        $scope.addequipment_holding = function () {
            var modalInstance = $modal.open({
                templateUrl: '/Equipmentholding.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.insurancestatus_yes = function () {
                    $scope.insurancestatusyesshow = true;
                }
                $scope.insurancestatus_no = function () {
                    $scope.insurancestatusyesshow = false;
                }

                var url = 'api/MstApplication360/GetEquipmentHoldingList';
                SocketService.get(url).then(function (resp) {
                    $scope.equipment_list = resp.data.equipment_list;
                });

                $scope.equipment_submit = function () {
                    var lsequipment_gid = '';
                    var lsequipment_name = '';

                    if ($scope.cboequipmentname != undefined || $scope.cboequipmentname != null) {
                        lsequipment_gid = $scope.cboequipmentname.equipment_gid;
                        lsequipment_name = $scope.cboequipmentname.equipment_name;
                    }

                    var params = {
                        equipment_gid: lsequipment_gid,
                        equipment_name: lsequipment_name,
                        availablerenthire: $scope.rdbavailablerent,
                        quantity: $scope.txtquantity,
                        description: $scope.txtdescription,
                        insurance_status: $scope.rdbinsurancestatus,
                        insurance_details: $scope.txtinsurancedetail
                    }
                    var url = 'api/MstCADApplication/PostContactEquipmentHolding';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            equipmentholding_list();
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.deleteequipment_holding = function (contact2equipment_gid) {
            var params = {
                contact2equipment_gid: contact2equipment_gid
            }
            var url = 'api/MstCADApplication/DeleteContactEquipmentHolding';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    equipmentholding_list();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                equipmentholding_list();
            });

        }

        $scope.equipment_View = function (contact2equipment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/EquipmentholdingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    contact2equipment_gid: contact2equipment_gid
                }
                var url = 'api/MstCADApplication/GetContactEquipmentHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblquantity = resp.data.quantity;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblinsurancedetails = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        function equipmentholding_list() {
            var url = 'api/MstCADApplication/GetContactEquipmentHoldingList';
            SocketService.get(url).then(function (resp) {
                $scope.mstequipmentholding_list = resp.data.mstequipmentholding_list;

            });
        }

        $scope.addlivestock_holding = function () {
            var modalInstance = $modal.open({
                templateUrl: '/Livestockholding.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.insurancestatus_yes = function () {
                    $scope.insurancestatusyesshow = true;
                }
                $scope.insurancestatus_no = function () {
                    $scope.insurancestatusyesshow = false;
                }

                var url = 'api/MstApplication360/GetLivestockList';
                SocketService.get(url).then(function (resp) {
                    $scope.livestock_list = resp.data.livestock_list;
                });

                $scope.livestock_submit = function () {
                    var lslivestock_gid = '';
                    var lslivestock_name = '';

                    if ($scope.cbolivestockname != undefined || $scope.cbolivestockname != null) {
                        lslivestock_gid = $scope.cbolivestockname.livestock_gid;
                        lslivestock_name = $scope.cbolivestockname.livestock_name;
                    }

                    var params = {
                        livestock_gid: lslivestock_gid,
                        livestock_name: lslivestock_name,
                        count: $scope.txtcount,
                        breed: $scope.txtbreed,
                        insurance_status: $scope.rdbinsurancestatus,
                        insurance_details: $scope.txtlivestockinsurance_details
                    }
                    var url = 'api/MstCADApplication/PostContactLivestock';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            livestock_list();
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

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.deletelivestock_holding = function (contact2livestock_gid) {
            var params = {
                contact2livestock_gid: contact2livestock_gid
            }
            var url = 'api/MstCADApplication/DeleteContactLivestock';
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
                livestock_list();
            });

        }

        $scope.livestock_View = function (contact2livestock_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/LiveStockHoldingView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    contact2livestock_gid: contact2livestock_gid
                }
                var url = 'api/MstCADApplication/GetContactLivestockHoldingView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblbreed = resp.data.Breed;
                    $scope.lbllivestockinsurance_details = resp.data.insurance_details;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        function livestock_list() {
            var url = 'api/MstCADApplication/GetContactLivestockList';
            SocketService.get(url).then(function (resp) {
                $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;

            });
        }

        $scope.downloadall = function () {

            for (var i = 0; i < $scope.uploadindividualdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.uploadindividualdoc_list[i].document_path, $scope.uploadindividualdoc_list[i].document_name);
            }
        }

        $scope.downloadallform60 = function () {
            for (var i = 0; i < $scope.contactpanform60_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.contactpanform60_list[i].document_path, $scope.contactpanform60_list[i].document_name);
            }
        }

        $scope.downloadallidproof = function () {
            for (var i = 0; i < $scope.contactidproof_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.contactidproof_list[i].document_path, $scope.contactidproof_list[i].document_name);
            }
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
            var url = 'api/MstApplication360/IndividualDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.individualdocument_list = resp.data.application_list;
            });
        }

    }
})();
