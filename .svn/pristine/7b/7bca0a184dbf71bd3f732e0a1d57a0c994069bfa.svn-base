(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplcreationIndividualdtlEditController', MstApplcreationIndividualdtlEditController);

    MstApplcreationIndividualdtlEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstApplcreationIndividualdtlEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplcreationIndividualdtlEditController';
        $scope.contact_gid = $location.search().lscontact_gid;
        $scope.application_gid = $location.search().application_gid;

        var lstab = $location.search().lstab;

        activate();
        function activate() {
            $scope.input = false
            $scope.span = true
            $scope.inputType = 'password';
            $scope.eye = true
            $scope.eyeslash = false
            $scope.dateyes = true
            $scope.groupnameYes = true;
            $scope.companynameYes = true;

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
            // Calender Popup... //

            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };
            // Calender Popup... //

            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };
            // Calender Popup... //

            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open5 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/MstApplicationAdd/GetIndividualTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var param = {
                contact_gid: $scope.contact_gid,
                Type: 'Individual',
            };

            var url = 'api/MstApplicationAdd/GetRenewalAppValidatePANAadhar';
            /* SocketService.getparams(url, param).then(function (resp) {*/
            SocketService.post(url, param).then(function (resp) {
                $scope.panrenewal_flag = resp.data.panrenewal_flag;
            });

            var url = 'api/MstApplication360/GenderList';
            SocketService.get(url).then(function (resp) {
                $scope.gender_list = resp.data.application_list;
            });

            //var url = 'api/MstApplicationAdd/GetRenewalAppValidatePANAadhar';
            ///* SocketService.getparams(url, param).then(function (resp) {*/
            //SocketService.post(url, param).then(function (resp) {
            //    $scope.panrenewal_flag = resp.data.panrenewal_flag;
            //});

            var url = 'api/MstApplication360/GetDesignationList';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });

            var url = 'api/MstApplication360/GetUserTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.usertype_list = resp.data.usertype_list;
            });

            var url = 'api/MstApplication360/GetMaritalStatusList';
            SocketService.get(url).then(function (resp) {
                $scope.maritalstatus_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/EducationalQualificationList';
            SocketService.get(url).then(function (resp) {
                $scope.educationalqualification_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/IncomeTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.incometype_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/IndividualProofList';
            SocketService.get(url).then(function (resp) {
                $scope.individualproof_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/OwnershipTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.ownershiptype_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/GetPropertyinNameList';
            SocketService.get(url).then(function (resp) {
                $scope.propertyin_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/ResidenceTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.residencetype_list = resp.data.application_list;
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

            var url = 'api/MstApplicationEdit/GetIndividualMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactmobileno_list = resp.data.contactmobileno_list;
            });

            var url = 'api/MstApplicationEdit/GetIndividualEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactemail_list = resp.data.contactemail_list;
            });

            var url = 'api/MstApplicationEdit/GetIndividualAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactaddress_list = resp.data.contactaddress_list;
            });

            var url = 'api/MstApplicationEdit/GetIndividualProofList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactidproof_list = resp.data.contactidproof_list;
            });

            var url = 'api/MstApplicationEdit/GetIndividualDocList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.uploadindividualdoc_list = resp.data.uploadindividualdoc_list;
            });
            var param = {
                contact_gid: $scope.contact_gid
            };
            var url = 'api/MstApplicationAdd/GetContactGroupList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.grouplist = resp.data.grouplist;
            });

            var url = 'api/MstApplicationAdd/GetContactCompanyList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionlist = resp.data.institutionlist;
            });

            var url = 'api/MstApplicationEdit/GetContactEquipmentHoldingList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstequipmentholding_list = resp.data.mstequipmentholding_list;
            });

            var url = 'api/MstApplicationEdit/GetContactLivestockList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;
            });

            var url = 'api/MstApplicationEdit/EditIndividual';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.cbopanstatus = resp.data.pan_status;
                if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                    $scope.havepan = true;
                    $scope.havenotpan = false;
                }
                else if ($scope.cbopanstatus == 'Customer Submitting Form 60') {
                    $scope.havenotpan = true;
                    $scope.havepan = false;
                }
                $scope.show = function () {

                    if ($scope.inputType == 'password')
                        $scope.inputType = 'text';
                    $scope.eye = false
                    $scope.eyeslash = true
                }

                $scope.noshow = function () {

                    if ($scope.inputType == 'text')
                        $scope.inputType = 'password';
                    $scope.eye = true
                    $scope.eyeslash = false

                }
                $scope.txtpan_no = resp.data.pan_no;
                $scope.txtaadhar_no = resp.data.aadhar_no;
                $scope.txtfirst_name = resp.data.first_name;
                $scope.txtmiddle_name = resp.data.middle_name;
                $scope.txtlast_name = resp.data.last_name;
                $scope.txtindividual_dob = resp.data.individual_dob;
                $scope.txtage = resp.data.age;
                $scope.cboGender = resp.data.gender_gid;
                $scope.cboDesignation = resp.data.designation_gid;
                $scope.cboEducationalQualification = resp.data.educationalqualification_gid;

                $scope.txtmain_occupation = resp.data.main_occupation;
                $scope.txtsampleannual_income = resp.data.annual_income;
                $scope.txtannual_income = (parseInt($scope.txtsampleannual_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtannual_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_annualincome').innerHTML = $scope.lblamountwords;
                $scope.txtsamplemonthly_income = resp.data.monthly_income;
                $scope.txtmonthly_income = (parseInt($scope.txtsamplemonthly_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtmonthly_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_monthlyincome').innerHTML = $scope.lblamountwords;
                $scope.cboIncomeType = resp.data.incometype_gid;
                $scope.rdbpep_status = resp.data.pep_status;
                $scope.txtpepverified_date = resp.data.pepverified_date;
                $scope.cboMaritalStatus = resp.data.maritalstatus_gid;

                $scope.cboStakeHolder = resp.data.stakeholdertype_gid;


                $scope.txtfather_firstname = resp.data.father_firstname;
                $scope.txtfather_middlename = resp.data.father_middlename;
                $scope.txtfather_lastname = resp.data.father_lastname;
                $scope.txtfather_dob = resp.data.father_dob;
                $scope.txtfather_age = resp.data.father_age;

                $scope.txtmother_firstname = resp.data.mother_firstname;
                $scope.txtmother_middlename = resp.data.mother_middlename;
                $scope.txtmother_lastname = resp.data.mother_lastname;
                $scope.txtmother_dob = resp.data.mother_dob;
                $scope.txtmother_age = resp.data.mother_age;

                $scope.txtspouse_firstname = resp.data.spouse_firstname;
                $scope.txtspouse_middlename = resp.data.spouse_middlename;
                $scope.txtspouse_lastname = resp.data.spouse_lastname;
                $scope.txtspouse_dob = resp.data.spouse_dob;
                $scope.txtspouse_age = resp.data.spouse_age;

                $scope.cboOwnershipType = resp.data.ownershiptype_gid;
                $scope.cboPropertyin = resp.data.propertyholder_gid;
                $scope.cboResidenceType = resp.data.residencetype_gid;
                $scope.txtcurrentresidence_years = resp.data.currentresidence_years;
                $scope.txtbranch_distance = resp.data.branch_distance;
                $scope.contact_status = resp.data.contact_status;

                $scope.cboGroup = resp.data.group_gid;
                $scope.txtprofile = resp.data.profile;
                $scope.rdburn_status = resp.data.urn_status;
                $scope.txt_urn = resp.data.urn;
                $scope.rdbfathernominee_status = resp.data.fathernominee_status;
                $scope.rdbmothernominee_status = resp.data.mothernominee_status;
                $scope.rdbspousenominee_status = resp.data.spousenominee_status;
                $scope.rdbothernominee_status = resp.data.othernominee_status;
                $scope.txtrelationshiptype = resp.data.relationshiptype;
                $scope.txtnomineefirst_name = resp.data.nomineefirst_name;
                $scope.txtnominee_middlename = resp.data.nominee_middlename;
                $scope.txtnominee_lastname = resp.data.nominee_lastname;
                $scope.txtnominee_dob = resp.data.nominee_dob;
                $scope.txtnominee_age = resp.data.nominee_age;
                $scope.txttotallandinacres = resp.data.totallandinacres;
                $scope.txtcultivatedland = resp.data.cultivatedland;
                $scope.txtpreviouscrop = resp.data.previouscrop;
                $scope.txtprposedcrop = resp.data.prposedcrop;
                $scope.cboInstitution = resp.data.institution_gid;
                $scope.cbosamunnatibranchname = resp.data.nearsamunnatiabranch_gid;
                $scope.cbophysicalstatus = resp.data.physicalstatus_gid;
                $scope.cbointernalrating = resp.data.internalrating_gid;

                if (resp.data.contact_status == "Incomplete") {
                    $scope.IndividualSubmit = true;
                    $scope.IndividualUpdate = false;
                }
                else {
                    $scope.IndividualSubmit = false;
                    $scope.IndividualUpdate = true;
                }

                if (resp.data.urn_status == 'Yes') {
                    $scope.URN_yes = true;
                }
                else {
                    $scope.URN_yes = false;
                    $scope.txt_urn = '';
                }
                if (resp.data.othernominee_status == 'Yes') {
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
                if ($scope.cboGroup == null || $scope.cboGroup == '' || $scope.cboGroup == undefined || $scope.cboGroup == 'NA') {
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
                if ($scope.cboInstitution == 'NA') {
                    $scope.groupnameYes = true;
                    $scope.groupnameNo = false;
                }
                else {
                    $scope.groupnameYes = false;
                    $scope.groupnameNo = true;
                }

                unlockUI();
            });

            var url = 'api/MstApplicationEdit/GetPANForm60List';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanform60_list = resp.data.contactpanform60_list;
            });
            var param = {
                contact_gid: $scope.contact_gid
            };
            var url = 'api/MstApplicationEdit/EditPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.panabsencereason_list = resp.data.panabsencereason_list;
                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].check_status == true) {
                        $scope.panabsencereason_list[i].checked = true;
                    }
                }
            });

            var url = 'api/MstApplicationEdit/ContactPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanabsencereason_list = resp.data.contactpanabsencereason_list;
            });

            var params = {
                application_gid: $location.search().application_gid
            }

            var url = 'api/MstApplicationEdit/GetEditProductcharges';
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

        function defaultamountwordschange(input) {
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
            return lswords;
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

        //Edit PAN Absence Reasons
        $scope.edit_pandtl = function () {
            $scope.view_nopanreasons = true;
        }

        $scope.pandtl_update = function () {

            var panabsencereason_selectedList = [];
            angular.forEach($scope.panabsencereason_list, function (val) {
                if (val.checked == true) {
                    var panabsencereason = val.panabsencereason;
                    panabsencereason_selectedList.push(panabsencereason);
                }

            });

            var params = {
                contact_gid: $scope.contact_gid,
                panabsencereason_selectedlist: panabsencereason_selectedList,
            }
            var url = 'api/MstApplicationEdit/UpdatePANAbsenceReasons';
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
            if ($scope.cboGroup == null || $scope.cboGroup == '' || $scope.cboGroup == undefined || $scope.cboGroup == 'NA') {
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
            if ($scope.cboInstitution == 'NA') {
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
                var url = 'api/MstCustomerAdd/GetAge';

                SocketService.getparams(url, params).then(function (resp) {
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
                var url = 'api/MstCustomerAdd/GetAge';

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
                var url = 'api/MstCustomerAdd/GetAge';

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

        $scope.onchangeddobage_individual = function () {

            var params = {
                age: $scope.txtage
            }
            var url = 'api/MstApplicationAdd/GetDOB';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtindividual_dob = Date.parse(resp.data.dob);
            });
        }

        $scope.onchangeddobage_father = function () {
            var params = {
                age: $scope.txtfather_age
            }
            var url = 'api/MstApplicationAdd/GetDOB';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtfather_dob = Date.parse(resp.data.dob);
            });
        }

        $scope.onchangeddobage_mother = function () {
            var params = {
                age: $scope.txtmother_age
            }
            var url = 'api/MstApplicationAdd/GetDOB';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtmother_dob = Date.parse(resp.data.dob);
            });
        }

        $scope.onchangeddobage_spouse = function () {
            var params = {
                age: $scope.txtspouse_age
            }
            var url = 'api/MstApplicationAdd/GetDOB';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtspouse_dob = Date.parse(resp.data.dob);
            });
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
                var txtannual_income = parseInt($scope.txtannual_income.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtannual_income = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
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
                var txtmonthly_income = parseInt($scope.txtmonthly_income.replace(/[\s,]+/g, '').trim());
            }
            $scope.txtmonthly_income = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
        }

        // Numeric to Word - Indian Standard...//

        // function inWords(num) {
        //     var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
        //     var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
        //     var s = num.toString();
        //     s = s.replace(/[\, ]/g, '');
        //     if (s != parseFloat(s)) return '';
        //     if ((num = num.toString()).length > 9) return 'Overflow';
        //     var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
        //     if (!n) return; var str = '';
        //     str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
        //     str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
        //     str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
        //     str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';

        //     str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
        //     return str;
        // }

        $scope.futuredatecheck = function (val) {
            if (val.length >= 10) {
                var parts = val.split("-");
                var finalval = new Date(parts[2], parts[1] - 1, parts[0]);
                var params = {
                    date: finalval.toDateString()
                }
                var url = 'api/MstApplicationAdd/FutureDateCheck';
                SocketService.getparams(url, params).then(function (resp) {
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
                var url = 'api/Kyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result != null) {
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

            if (($scope.cboStakeHolder != undefined && $scope.cboStakeHolder != null) || ($scope.cboStakeHolder != undefined && $scope.cboStakeHolder != null)) {
                var getStakeholderType = "";
                if ($scope.cboStakeHolder != undefined && $scope.cboStakeHolder != "") {
                    getStakeholderType = $scope.cboStakeHolder;
                }
                else {
                    var getStakeholderType = $scope.usertype_list.find(function (v) {
                        return v.usertype_gid === $scope.cboStakeHolder
                    });
                }

                if (getStakeholderType != null && getStakeholderType != "") {
                    var Stakeholdertype = $('#user_type :selected').text();
                    if (getStakeholderType.user_type != "") {
                        lockUI();
                        var pan_no = ($scope.txtpan_no == "" || $scope.txtpan_no == undefined) ? 'No' : $scope.txtpan_no;
                        var params = {
                            pan_no: pan_no,
                            aadhar_no: $scope.txtaadhar_no,
                            institution_gid: 'No',
                            contact_gid: $scope.contact_gid,
                            application_gid: $scope.application_gid,
                            stakeholder_type: Stakeholdertype,
                            panrenewal_flage: $scope.panrenewal_flag,
                            credit_name: 'Individual'
                        }
                        var url = 'api/MstApplicationAdd/GetOnboardAppValidatePANAadhar';
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

            /*    $scope.PANAadhaarLink = function () {
                    if ($scope.txtpan_no.length == 10) {
                        var params = {
                            pan: $scope.txtpan_no
                        }
                        var url = 'api/Kyc/PANAadhaarLink';
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
                } */

            $scope.IDProofValidation = function () {

                if ($scope.cboIndividualProof.individualproof_gid == "INPF2021062300002" && $scope.txtidproof_no.length == 10) {
                    var params = {
                        epic_no: $scope.txtidproof_no
                    }
                    var url = 'api/Kyc/VoterID';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.result != null) {
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
                    var url = 'api/Kyc/PANNumber';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.result != null) {
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
                    var url = 'api/Kyc/DrivingLicenseAuthentication';
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
                    var url = 'api/Kyc/Passport';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.result != null) {
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

            $scope.downloads1 = function (val1, val2, val3) {
                if (val3 == 'N') {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                else {
                    DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                }
        }

            $scope.downloads = function (val1, val2) {

                DownloaddocumentService.Downloaddocument(val1, val2);
            }


            $scope.individualmobileno_add = function () {
                if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_status == undefined) || ($scope.rdbwhatsapp_no == undefined) || ($scope.rdbwhatsapp_no == '') || ($scope.rdbprimary_status == '')) {
                    Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
                }
                else if ($scope.txtmobile_no.length < 10) {
                    Notify.alert('Enter 10 Digit Mobile Number', 'warning');
                }
                else {
                    var params = {
                        mobile_no: $scope.txtmobile_no,
                        primary_status: $scope.rdbprimary_status,
                        whatsapp_no: $scope.rdbwhatsapp_no,
                        contact_gid: $scope.contact_gid,
                    }
                    var url = 'api/MstApplicationEdit/PostIndividualMobileNumber';
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
                        $scope.rdbprimary_status = '';
                        $scope.rdbwhatsapp_no = '';
                    });
                }
            }

            function mobileno_templist() {
                var param = {
                    contact_gid: $scope.contact_gid
                };
                var url = 'api/MstApplicationEdit/GetIndividualMobileNoTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactmobileno_list = resp.data.contactmobileno_list;
                });
            }

            $scope.individualmobileno_edit = function (contact2mobileno_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/editindividualmobileno.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    var params = {
                        contact2mobileno_gid: contact2mobileno_gid
                    }
                    var url = 'api/MstApplicationEdit/EditIndividualMobileNo';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.txteditmobile_no = resp.data.mobile_no;
                        $scope.rdbeditprimary_status = resp.data.primary_status;
                        $scope.rdbeditwhatsapp_no = resp.data.whatsapp_no;
                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.update_mobileno = function () {

                        var params = {
                            mobile_no: $scope.txteditmobile_no,
                            primary_status: $scope.rdbeditprimary_status,
                            whatsapp_no: $scope.rdbeditwhatsapp_no,
                            contact2mobileno_gid: contact2mobileno_gid,
                            contact_gid: $scope.contact_gid,

                        }
                        var url = 'api/MstApplicationEdit/UpdateIndividualMobileNo';
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

            $scope.individualmobileno_delete = function (contact2mobileno_gid) {
                var params =
                {
                    contact2mobileno_gid: contact2mobileno_gid
                }
                var url = 'api/MstApplicationEdit/DeleteIndividualMobileNo';
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

            $scope.individualemail_add = function () {
                if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbemailprimary_status == undefined) || ($scope.rdbemailprimary_status == '')) {

                    Notify.alert('Enter Mail ID / Select Primary Status', 'warning');
                }
                else {
                    var params = {
                        email_address: $scope.txtemail_address,
                        primary_status: $scope.rdbemailprimary_status,
                        contact_gid: $scope.contact_gid,
                    }
                    var url = 'api/MstApplicationEdit/PostIndividualEmailAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            email_templist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.txtemail_address = '';
                        $scope.rdbemailprimary_status = '';
                    });
                }
            }

            function email_templist() {
                var param = {
                    contact_gid: $scope.contact_gid
                };
                var url = 'api/MstApplicationEdit/GetIndividualEmailAddressTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactemail_list = resp.data.contactemail_list;
                });
            }

            $scope.individualemail_edit = function (contact2email_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/individualemailaddressedit.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    var params = {
                        contact2email_gid: contact2email_gid
                    }
                    var url = 'api/MstApplicationEdit/EditIndividualEmailAddress';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txteditemail_address = resp.data.email_address;
                        $scope.rdbeditprimary_status = resp.data.primary_status;
                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.update_emailaddress = function () {

                        var params = {
                            email_address: $scope.txteditemail_address,
                            primary_status: $scope.rdbeditprimary_status,
                            contact2email_gid: contact2email_gid,
                            contact_gid: $scope.contact_gid,
                        }
                        var url = 'api/MstApplicationEdit/UpdateIndividualEmailAddress';
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
                            email_templist();
                        });

                        $modalInstance.close('closed');

                    }
                }
            }

            $scope.individualemail_delete = function (contact2email_gid) {
                var params =
                {
                    contact2email_gid: contact2email_gid
                }
                var url = 'api/MstApplicationEdit/DeleteIndividualEmailAddress';
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

                    email_templist();
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
                        var url = 'api/MstApplicationAdd/GetPostalCodeDetails';

                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.txtcity = resp.data.city;
                            $scope.txttaluka = resp.data.taluka;
                            $scope.txtdistrict = resp.data.district;
                            $scope.txtstate = resp.data.state;

                        });
                    }
                    $scope.getGeoCoding = function () {
                        if ($scope.txtpostal_code == undefined) {
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
                            addresstype_gid: $scope.cboAddressType.address_gid,
                            addresstype_name: $scope.cboAddressType.address_type,
                            primary_status: $scope.rdbprimary_status,
                            addressline1: $scope.txtaddressline1,
                            addressline2: $scope.txtaddressline2,
                            landmark: $scope.txtlandmark,
                            postal_code: $scope.txtpostal_code,
                            taluka: $scope.txttaluka,
                            city: $scope.txtcity,
                            district: $scope.txtdistrict,
                            state: $scope.txtstate,
                            country: $scope.txtcountry,
                            latitude: $scope.txtlatitude,
                            longitude: $scope.txtlongitude,
                            contact_gid: $location.search().lscontact_gid,
                        }
                        var url = 'api/MstApplicationEdit/PostIndividualAddress';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                address_templist();
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

            function address_templist() {
                var param = {
                    contact_gid: $scope.contact_gid
                };
                var url = 'api/MstApplicationEdit/GetIndividualAddressTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactaddress_list = resp.data.contactaddress_list;
                });
            }

            $scope.address_edit = function (contact2address_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/addresstype_edit.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    var url = 'api/AddressType/GetAddressTypeASC';
                    SocketService.get(url).then(function (resp) {
                        $scope.addresstype_list = resp.data.addresstype_list;
                    });

                    var params = {
                        contact2address_gid: contact2address_gid
                    }
                    var url = 'api/MstApplicationEdit/EditIndividualAddress';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.cboAddressType = resp.data.addresstype_gid;

                        $scope.rdbprimary_status = resp.data.primary_status;
                        $scope.txtaddressline1 = resp.data.addressline1;
                        $scope.txtaddressline2 = resp.data.addressline2;
                        $scope.txtlandmark = resp.data.landmark;
                        $scope.txtpostal_code = resp.data.postal_code;
                        $scope.txtcity = resp.data.city;
                        $scope.txttaluka = resp.data.taluka;
                        $scope.txtdistrict = resp.data.district;
                        $scope.txtstate = resp.data.state;
                        $scope.txtcountry = resp.data.country;
                        $scope.txtlatitude = resp.data.latitude;
                        $scope.txtlongitude = resp.data.longitude;

                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                    $scope.txtcountry = "India";
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
                        if ($scope.txtpostal_code == undefined) {
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

                    $scope.address_update = function () {
                        var address_type = $('#address_type :selected').text();

                        var params = {
                            addresstype_gid: $scope.cboAddressType,
                            addresstype_name: address_type,
                            primary_status: $scope.rdbprimary_status,
                            addressline1: $scope.txtaddressline1,
                            addressline2: $scope.txtaddressline2,
                            landmark: $scope.txtlandmark,
                            postal_code: $scope.txtpostal_code,
                            taluka: $scope.txttaluka,
                            city: $scope.txtcity,
                            district: $scope.txtdistrict,
                            state: $scope.txtstate,
                            country: $scope.txtcountry,
                            latitude: $scope.txtlatitude,
                            longitude: $scope.txtlongitude,
                            contact2address_gid: contact2address_gid,
                            contact_gid: $scope.contact_gid,
                        }
                        var url = 'api/MstApplicationEdit/UpdateIndividualAddress';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                address_templist();
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
                var url = 'api/MstApplicationEdit/DeleteIndividualAddress';
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

                    address_templist();
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
                        var url = 'api/MstAppCreditUnderWriting/IndividualProofDocumentUpload'
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
                                idproof_templist();
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            unlockUI();
                        });
                    }
                    else {
                        alert('Please select a file.')
                    }
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

            function idproof_templist() {
                var param = {
                    contact_gid: $scope.contact_gid
                };
                var url = 'api/MstApplicationEdit/GetIndividualProofTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactidproof_list = resp.data.contactidproof_list;
                });
            }

            $scope.idproof_delete = function (contact2idproof_gid) {

                var params = {
                    contact2idproof_gid: contact2idproof_gid
                }
                lockUI();
                var url = 'api/MstApplicationAdd/IndividualProofDelete';
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
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    idproof_templist();
                    unlockUI();
                });
            }

            $scope.individualdocument_upload = function (val, val1, name) {
                if (($scope.cboIndividualDocument.individualdocument_name == null) || ($scope.cboIndividualDocument.individualdocument_name == '')
                    || ($scope.cboIndividualDocument.individualdocument_name == undefined)
                    || ($scope.cboindividualdocumenttype == null) || ($scope.cboindividualdocumenttype == '') || ($scope.cboindividualdocumenttype == undefined)                ) {
                    $("#fileIndividuaDocument").val('');
                    $scope.cboindividualdocumenttype = '';
                    Notify.alert('Kindly Enter the Document Title', 'warning');
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

                    frm.append('document_title', $scope.cboIndividualDocument.individualdocument_name);
                    frm.append('individualdocument_gid', $scope.cboIndividualDocument.individualdocument_gid);
                    frm.append('documenttype_gid', $scope.cboindividualdocumenttype.documenttypes_gid);
                    frm.append('documenttype_name', $scope.cboindividualdocumenttype.documenttype_name);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                }
            }
        //Upload Check
            $scope.individualdocument_add = function () {

                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstApplicationEdit/IndividualDocumentUpload';

                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        unlockUI();
                        $("#fileIndividuaDocument").val('');
                        $scope.upload_list = resp.data.upload_list;

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.cboIndividualDocument = '';
                            $scope.txtindividual_document = '';
                            $scope.uploadfrm = undefined;
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }

                        individualdoc_templist();

                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
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

            function individualdoc_templist() {
                var param = {
                    contact_gid: $scope.contact_gid
                };
                var url = 'api/MstApplicationEdit/GetIndividualDocTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.uploadindividualdoc_list = resp.data.uploadindividualdoc_list;
                });
            }



            $scope.individualdocument_delete = function (contact2document_gid) {

                var params = {
                    contact2document_gid: contact2document_gid
                }
                lockUI();
                var url = 'api/MstApplicationEdit/IndividualDocDelete';
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
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });

                    }
                    individualdoc_templist();
                    unlockUI();
                });
            }



            $scope.individual_Back = function () {
                if (lstab == 'add') {
                    $location.url('app/MstApplicationGeneralAdd?lstab=' + lstab);
                }
                else {
                    $state.go('app.MstApplicationGeneralEdit');
                }
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
                frm.append('project_flag', "Default");


                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstApplicationAdd/PANForm60DocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $("#institutionform60file").val('');
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

                        panform60_templist();

                        unlockUI();
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

            function panform60_templist() {
                var param = {
                    contact_gid: $scope.contact_gid
                };
                var url = 'api/MstApplicationEdit/GetPANForm60TempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.contactpanform60_list;
                });
            }

            $scope.IndividualPANForm60DocumentDelete = function (contact2panform60_gid) {

                var params = {
                    contact2panform60_gid: contact2panform60_gid
                }
                lockUI();
                var url = 'api/MstApplicationAdd/PANForm60Delete';
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
                    var url = 'api/MstApplicationAdd/GetPANForm60List';
                    SocketService.get(url).then(function (resp) {
                        $scope.contactpanform60_list = resp.data.contactpanform60_list;
                    });
                    unlockUI();
                });
            }

            $scope.update_individual = function () {
                var panabsencereasons_checked = false;
                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].checked == true) {
                        panabsencereasons_checked = true;
                        break;
                    }
                }
                if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                    Notify.alert('Kindly Enter URN', 'warning')
                }
                else if ($scope.cboStakeHolder == null || $scope.cboStakeHolder == '' || $scope.cboStakeHolder == undefined) {
                    Notify.alert('Kindly select Stakeholder Type', 'warning')
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

                    var Gender = $('#Gender :selected').text();
                    var Designation = $('#Designation :selected').text();
                    var Stakeholdertype = $('#user_type :selected').text();
                    var MaritalStatus = $('#MaritalStatus :selected').text();
                    var EducationalQualification = $('#EducationalQualification :selected').text();
                    var IncomeType = $('#IncomeType :selected').text();
                    var OwnershipType = $('#OwnershipType :selected').text();
                    var ResidenceType = $('#residencetype_name :selected').text();
                    var propertyinname = $('#propertyin_name :selected').text();
                    var groupname = $('#group_name :selected').text();
                    var institutionname = $('#institution_name :selected').text();
                    var branchname = $('#branch_name :selected').text();
                    var physicalstatus = $('#physical_status :selected').text();
                    var internalratingname = $('#internalrating_name :selected').text();

                    if ($scope.cboGroup == 'NA') {
                        groupname = 'NA';
                    }
                    if ($scope.cboInstitution == 'NA') {
                        institutionname = 'NA';
                    }
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

                        gender_gid: $scope.cboGender,
                        gender_name: Gender,

                        designation_gid: $scope.cboDesignation,
                        designation_name: Designation,

                        pep_status: $scope.rdbpep_status,
                        pepverifieddate: $scope.txtpepverified_date,

                        stakeholdertype_gid: $scope.cboStakeHolder,
                        stakeholder_type: Stakeholdertype,

                        maritalstatus_gid: $scope.cboMaritalStatus,
                        maritalstatus_name: MaritalStatus,

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

                        educationalqualification_gid: $scope.cboEducationalQualification,
                        educationalqualification_name: EducationalQualification,

                        main_occupation: $scope.txtmain_occupation,
                        annual_income: $scope.txtannual_income,
                        monthly_income: $scope.txtmonthly_income,

                        incometype_gid: $scope.cboIncomeType,
                        incometype_name: IncomeType,

                        ownershiptype_gid: $scope.cboOwnershipType,
                        ownershiptype_name: OwnershipType,

                        propertyholder_gid: $scope.cboPropertyin,
                        propertyholder_name: propertyinname,

                        residencetype_gid: $scope.cboResidenceType,
                        residencetype_name: ResidenceType,

                        currentresidence_years: $scope.txtcurrentresidence_years,
                        branch_distance: $scope.txtbranch_distance,

                        contact_gid: $scope.contact_gid,

                        group_gid: $scope.cboGroup,
                        group_name: groupname,
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
                        institution_gid: $scope.cboInstitution,
                        institution_name: institutionname,
                        panabsencereason_selectedlist: panabsencereason_selectedList,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: branchname,
                        physicalstatus_gid: $scope.cbophysicalstatus,
                        physicalstatus_name: physicalstatus,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalratingname,
                        program_gid: $scope.program_gid
                    }
                    var url = 'api/MstApplicationEdit/UpdateIndividual';
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
                                $location.url('app/MstApplicationGeneralAdd?lstab=' + lstab);
                            }
                            else {
                                $state.go('app.MstApplicationGeneralEdit');
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
                else {
                    Notify.alert('Kindly Select One Nominee Status as Yes', 'warning')
                }
            }


            $scope.individual_save = function () {
                var panabsencereasons_checked = false;
                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].checked == true) {
                        panabsencereasons_checked = true;
                        break;
                    }
                }
                if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                    Notify.alert('Kindly Enter URN', 'warning')
                }
                else if ($scope.cboStakeHolder == null || $scope.cboStakeHolder == '' || $scope.cboStakeHolder == undefined) {
                    Notify.alert('Kindly select Stakeholder Type', 'warning')
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
                else {
                    var Gender = $('#Gender :selected').text();
                    var Designation = $('#Designation :selected').text();
                    var Stakeholdertype = $('#user_type :selected').text();
                    var MaritalStatus = $('#MaritalStatus :selected').text();
                    var EducationalQualification = $('#EducationalQualification :selected').text();
                    var IncomeType = $('#IncomeType :selected').text();
                    var OwnershipType = $('#OwnershipType :selected').text();
                    var ResidenceType = $('#residencetype_name :selected').text();
                    var propertyinname = $('#propertyin_name :selected').text();
                    var groupname = $('#group_name :selected').text();
                    var institutionname = $('#institution_name :selected').text();
                    var branchname = $('#branch_name :selected').text();
                    var physicalstatus = $('#physical_status :selected').text();
                    var internalratingname = $('#internalrating_name :selected').text();

                    if ($scope.cboGroup == 'NA') {
                        groupname = 'NA';
                    }
                    if ($scope.cboInstitution == 'NA') {
                        institutionname = 'NA';
                    }
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

                        gender_gid: $scope.cboGender,
                        gender_name: Gender,

                        designation_gid: $scope.cboDesignation,
                        designation_name: Designation,

                        pep_status: $scope.rdbpep_status,
                        pepverifieddate: $scope.txtpepverified_date,

                        stakeholdertype_gid: $scope.cboStakeHolder,
                        stakeholder_type: Stakeholdertype,

                        propertyholder_gid: $scope.cboPropertyin,
                        propertyholder_name: propertyinname,

                        maritalstatus_gid: $scope.cboMaritalStatus,
                        maritalstatus_name: MaritalStatus,

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

                        educationalqualification_gid: $scope.cboEducationalQualification,
                        educationalqualification_name: EducationalQualification,

                        main_occupation: $scope.txtmain_occupation,
                        annual_income: $scope.txtannual_income,
                        monthly_income: $scope.txtmonthly_income,

                        incometype_gid: $scope.cboIncomeType,
                        incometype_name: IncomeType,

                        ownershiptype_gid: $scope.cboOwnershipType,
                        ownershiptype_name: $scope.OwnershipType,

                        residencetype_gid: $scope.cboResidenceType,
                        residencetype_name: ResidenceType,

                        currentresidence_years: $scope.txtcurrentresidence_years,
                        branch_distance: $scope.txtbranch_distance,

                        contact_gid: $scope.contact_gid,

                        group_gid: $scope.cboGroup,
                        group_name: groupname,
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
                        institution_gid: $scope.cboInstitution,
                        institution_name: institutionname,
                        panabsencereason_selectedlist: panabsencereason_selectedList,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: branchname,
                        physicalstatus_gid: $scope.cbophysicalstatus,
                        physicalstatus_name: physicalstatus,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalratingname
                    }
                    var url = 'api/MstApplicationEdit/SaveIndividualEditDtl';
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
                                $location.url('app/MstApplicationGeneralAdd?lstab=' + lstab);
                            }
                            else {
                                $state.go('app.MstApplicationGeneralEdit');
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
                var panabsencereasons_checked = false;

                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].checked == true) {
                        panabsencereasons_checked = true;
                        break;
                    }
                }
                //if ($scope.cboGroup == "") {
                //    Notify.alert('Kindly Select Group Name', 'warning')
                //}
                if ($scope.cboInstitution == '') {
                    Notify.alert('Kindly Select Company Name', 'warning')
                }
                else if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                    Notify.alert('Kindly Enter URN', 'warning')
                }
                else if ($scope.cboStakeHolder == null || $scope.cboStakeHolder == '' || $scope.cboStakeHolder == undefined) {
                    Notify.alert('Kindly select Stakeholder Type', 'warning')
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
                    var Gender = $('#Gender :selected').text();
                    var Designation = $('#Designation :selected').text();
                    var Stakeholdertype = $('#user_type :selected').text();
                    var MaritalStatus = $('#MaritalStatus :selected').text();
                    var EducationalQualification = $('#EducationalQualification :selected').text();
                    var IncomeType = $('#IncomeType :selected').text();
                    var OwnershipType = $('#OwnershipType :selected').text();
                    var ResidenceType = $('#residencetype_name :selected').text();
                    var propertyinname = $('#propertyin_name :selected').text();
                    var groupname = $('#group_name :selected').text();
                    var institutionname = $('#institution_name :selected').text();
                    var branchname = $('#branch_name :selected').text();
                    var physicalstatus = $('#physical_status :selected').text();
                    var internalratingname = $('#internalrating_name :selected').text();

                    if ($scope.cboGroup == 'NA') {
                        groupname = 'NA';
                    }
                    if ($scope.cboInstitution == 'NA') {
                        institutionname = 'NA';
                    }

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

                        gender_gid: $scope.cboGender,
                        gender_name: Gender,

                        designation_gid: $scope.cboDesignation,
                        designation_name: Designation,

                        pep_status: $scope.rdbpep_status,
                        pepverifieddate: $scope.txtpepverified_date,

                        stakeholdertype_gid: $scope.cboStakeHolder,
                        stakeholder_type: Stakeholdertype,

                        propertyholder_gid: $scope.cboPropertyin,
                        propertyholder_name: propertyinname,

                        maritalstatus_gid: $scope.cboMaritalStatus,
                        maritalstatus_name: MaritalStatus,

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

                        educationalqualification_gid: $scope.cboEducationalQualification,
                        educationalqualification_name: EducationalQualification,

                        main_occupation: $scope.txtmain_occupation,
                        annual_income: $scope.txtannual_income,
                        monthly_income: $scope.txtmonthly_income,

                        incometype_gid: $scope.cboIncomeType,
                        incometype_name: IncomeType,

                        ownershiptype_gid: $scope.cboOwnershipType,
                        ownershiptype_name: $scope.OwnershipType,

                        residencetype_gid: $scope.cboResidenceType,
                        residencetype_name: ResidenceType,

                        currentresidence_years: $scope.txtcurrentresidence_years,
                        branch_distance: $scope.txtbranch_distance,

                        contact_gid: $scope.contact_gid,

                        group_gid: $scope.cboGroup,
                        group_name: groupname,
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
                        institution_gid: $scope.cboInstitution,
                        institution_name: institutionname,
                        panabsencereason_selectedlist: panabsencereason_selectedList,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: branchname,
                        physicalstatus_gid: $scope.cbophysicalstatus,
                        physicalstatus_name: physicalstatus,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalratingname,
                        program_gid: $scope.program_gid

                    }
                    var url = 'api/MstApplicationEdit/SubmitIndividualEditDtl';
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
                                $location.url('app/MstApplicationGeneralAdd?lstab=' + lstab);
                            }
                            else {
                                $state.go('app.MstApplicationGeneralEdit');
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
                else {
                    Notify.alert('Kindly Select One Nominee Status as Yes', 'warning')
                }
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
                        var url = 'api/MstApplicationAdd/PostContactEquipmentHolding';
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
                var url = 'api/MstApplicationAdd/DeleteContactEquipmentHolding';
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
                    var url = 'api/MstApplicationAdd/GetContactEquipmentHoldingView';
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
                var params = {
                    contact_gid: $scope.contact_gid
                }
                var url = 'api/MstApplicationEdit/GetEditContactEquipmentHoldingList';
                SocketService.getparams(url, params).then(function (resp) {
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
                        var url = 'api/MstApplicationAdd/PostContactLivestock';
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
                var url = 'api/MstApplicationAdd/DeleteContactLivestock';
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
                    var url = 'api/MstApplicationAdd/GetContactLivestockHoldingView';
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
                var params = {
                    contact_gid: $scope.contact_gid
                }
                var url = 'api/MstApplicationEdit/GetEditContactLivestockList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;

                });
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

            $scope.downloadall = function () {

                for (var i = 0; i < $scope.uploadindividualdoc_list.length; i++) {
                    if ($scope.uploadindividualdoc_list[i].migration_flag == 'N') {
                        //DownloaddocumentService.Downloaddocument(val1, val2);
                        DownloaddocumentService.Downloaddocument($scope.uploadindividualdoc_list[i].document_path, $scope.uploadindividualdoc_list[i].document_name);
                    }
                    else {
                        //DownloaddocumentService.OtherDownloaddocument(val1, val2);
                        DownloaddocumentService.OtherDownloaddocument($scope.uploadindividualdoc_list[i].document_path, $scope.uploadindividualdoc_list[i].document_name, $scope.uploadindividualdoc_list[i].migration_flag );
                    }
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

        $scope.documentviewerindividual = function (val1, val2, val3) {
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

            if (val3 == 'N') {
                DownloaddocumentService.DocumentViewer(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDocumentViewer(val1, val2, val3);
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

