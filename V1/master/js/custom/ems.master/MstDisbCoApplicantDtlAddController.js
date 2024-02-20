(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDisbCoApplicantDtlAddController', MstDisbCoApplicantDtlAddController);

    MstDisbCoApplicantDtlAddController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstDisbCoApplicantDtlAddController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDisbCoApplicantDtlAddController';

        var farmercontact_gid = $location.search().farmercontact_gid;
        var application_gid = $location.search().application_gid;
        var disbursement_to = $location.search().disbursement_to;
        var customer_urn = $location.search().customer_urn;
        var lsgeneratelsa_gid = $location.search().lsgeneratelsa_gid;
        var application2sanction_gid = $location.search().application2sanction_gid;
        var productsubproduct_gid = $location.search().productsubproduct_gid;
        var lspage = $location.search().lspage;
        var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;

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

            $scope.application_gid = $location.search().lsapplication_gid;

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

            var url = 'api/AddressType/GetAddressTypeASC';
            SocketService.get(url).then(function (resp) {
                $scope.addresstype_list = resp.data.addresstype_list;
            });

            var url = 'api/MstCreditOpsApplication/GetCoapplicantPANForm60List';
            SocketService.get(url).then(function (resp) {
                $scope.contactpanform60_list = resp.data.Coapplicantcontactpanform60_list;
                $scope.contactpanform60_list = '';
            });

            var url = 'api/MstApplicationAdd/PANAbsenceReasonList';
            SocketService.get(url).then(function (resp) {
                $scope.panabsencereason_list = resp.data.panabsencereason_list;
            });

            var url = 'api/MstCreditOpsApplication/GetCoApplicantTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstApplication360/GenderList';
            SocketService.get(url).then(function (resp) {
                $scope.gender_list = resp.data.application_list;
            });

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

            var url = 'api/MstApplication360/IndividualDocumentListDis';
            SocketService.get(url).then(function (resp) {
                $scope.individualdocument_list = resp.data.application_list;
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

            $scope.havenotpan = false;
            $scope.havepan = false;
            $scope.view_nopanreasons = false;
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

        $scope.IFSCValidation = function () {

            if ($scope.txtIFSC_Code.length == 11) {
                var params = {
                    ifsc: $scope.txtIFSC_Code
                }
                lockUI();
                var url = 'api/CADKyc/IfscVerification';
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
                var url = 'api/CADKyc/PANNumber';
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

        
        $scope.coapplicantdoc_documentviewer = function (val1, val2) {
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

        $scope.individualdocument_upload = function (val, val1, name) {
            lockUI();
            if (($scope.cboIndividualDocument == null) || ($scope.cboIndividualDocument == '') || ($scope.cboIndividualDocument == undefined)) {
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
                frm.append('farmercontact_gid', farmercontact_gid);
                frm.append('application_gid', application_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstCreditOpsApplication/CoapplicantContactDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $("#fileIndividuaDocument").val('');
                        $scope.cboIndividualDocument = '';
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

                        var url = 'api/MstCreditOpsApplication/GetCoapplicantContactDocList';
                        SocketService.get(url).then(function (resp) {
                            $scope.uploadindividualdoc_list = resp.data.coapplicantuploadindividualdoc_list;
                            $scope.lblfilename = resp.data.filename;
                            $scope.lblfilepath = resp.data.filepath;
                        });

                        unlockUI();
                    });
                }
                else {
                    alert('Please select a file.')
                }
            }
        }

        $scope.downloads1 = function (val1, val2) {
          
                DownloaddocumentService.Downloaddocument(val1, val2);
            
        }

        $scope.individualdocument_delete = function (coapplicantcontact2document_gid) {

            var params = {
                coapplicantcontact2document_gid: coapplicantcontact2document_gid
            }
            lockUI();
            var url = 'api/MstCreditOpsApplication/CoapplicantContactDocDelete';
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
                var url = 'api/MstCreditOpsApplication/GetCoapplicantContactDocList';
                SocketService.get(url).then(function (resp) {
                    $scope.uploadindividualdoc_list = resp.data.coapplicantuploadindividualdoc_list;
                });
                unlockUI();
            });
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
            frm.append('project_flag', "Default");
            frm.append('farmercontact_gid', farmercontact_gid);
            frm.append('application_gid', application_gid);


            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstCreditOpsApplication/CoapplicantPANForm60DocumentUpload';
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

                    var url = 'api/MstCreditOpsApplication/GetCoapplicantPANForm60List';
                    SocketService.get(url).then(function (resp) {
                        $scope.contactpanform60_list = resp.data.Coapplicantcontactpanform60_list;

                    });

                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }

        }

        $scope.IndividualPANForm60DocumentDelete = function (coapplicantcontact2panform60_gid) {

            var params = {
                coapplicantcontact2panform60_gid: coapplicantcontact2panform60_gid
            }
            lockUI();
            var url = 'api/MstCreditOpsApplication/CoapplicantPANForm60Delete';
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
                var url = 'api/MstCreditOpsApplication/GetCoapplicantPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.Coapplicantcontactpanform60_list;
                });
                unlockUI();
            });
        }

        $scope.form60_documentviewer = function (val1, val2) {
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

        $scope.form60_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }   
       
        $scope.Coapplicantcontact_submit = function () {
            var lsgender_gid = '';
            var lsgender_name = '';
            var lsdesignation_gid = '';
            var lsdesignation_name = '';
            var lsmaritalstatus_gid = '';
            var lsmaritalstatus_name = '';
            var lseducationalqualification_gid = '';
            var lseducationalqualification_name = '';
            var lsaddress_gid = '';
            var lsaddress_type = '';
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
            if ($scope.cboMaritalStatus != undefined || $scope.cboMaritalStatus != null) {
                lsmaritalstatus_gid = $scope.cboMaritalStatus.maritalstatus_gid;
                lsmaritalstatus_name = $scope.cboMaritalStatus.maritalstatus_name;
            }
            if ($scope.cboEducationalQualification != undefined || $scope.cboEducationalQualification != null) {
                lseducationalqualification_gid = $scope.cboEducationalQualification.educationalqualification_gid;
                lseducationalqualification_name = $scope.cboEducationalQualification.educationalqualification_name;
            }
            if ($scope.cboaddresstype != undefined || $scope.cboaddresstype != null) {
                lsaddress_gid = $scope.cboaddresstype.address_gid;
                lsaddress_type = $scope.cboaddresstype.address_type;
            }

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
                var application_gid = $location.search().application_gid;
                var params = {
                    farmercontact_gid:farmercontact_gid,
                    application_gid: application_gid,
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
                    currentresidence_years: $scope.txtcurrentresidence_years,
                    branch_distance: $scope.txtbranch_distance,
                    fathernominee_status: $scope.rdbfathernominee_status,
                    mothernominee_status: $scope.rdbmothernominee_status,
                    spousenominee_status: $scope.rdbspousenominee_status,
                    panabsencereason_selectedlist: panabsencereason_selectedList,
                    mobile_no: $scope.txtmobile_no,
                    primary_status: $scope.rdbprimary_no,
                    whatsapp_no: $scope.rdbwhatsapp_no,
                    email_address: $scope.txtemail_address,
                    emailprimary_status: $scope.rdbprimary_email,
                    addresstype_gid: lsaddress_gid,
                    addresstype_name: lsaddress_type,
                    addressprimary_status: $scope.rdbprimaryaddress,
                    addressline1: $scope.txtaddressline1,
                    addressline2: $scope.txtaddressline2,
                    landmark: $scope.txtLand_Mark,
                    postal_code: $scope.txtpostal_code,
                    city: $scope.txtcity,
                    taluka: $scope.txttaluka,
                    district: $scope.txtdistrict,
                    state: $scope.txtstate,
                    country: $scope.txtcountry,
                    latitude: $scope.txtlatitude,
                    longitude: $scope.txtlongitude,
                    ifsc_code: $scope.txtIFSC_Code,
                    bank_name: $scope.txtBank_Name,
                    branch_name: $scope.txtBranch_Name,
                    branch_address: $scope.txtBank_Address,
                    micr_code: $scope.txtMICR_Code,
                    bankaccount_number: $scope.txtbankacct_no,
                    confirmbankaccount_number: $scope.txtconfirmbankacct_no,
                    accountholder_name: $scope.txtacctholder_name
                }
                var url = 'api/MstCreditOpsApplication/SubmitCoapplicantContactDtlAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var farmercontact_gid = $location.search().farmercontact_gid;
                        var application_gid = $location.search().application_gid;
                        var disbursement_to = $location.search().disbursement_to;
                        var customer_urn = $location.search().customer_urn;
                        if (lspage == 'RMDisbursementEdit') {
                           $location.url('app/MstRMDisbursementRequestEdit?farmercontact_gid=' + farmercontact_gid + '&application_gid=' + application_gid + '&disbursement_to=' + disbursement_to + '&customer_urn=' + customer_urn + '&lsgeneratelsa_gid=' + lsgeneratelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&productsubproduct_gid=' + productsubproduct_gid + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid);
                        }
                        else { 
                           $location.url('app/MstRMInitiateDisbursement?farmercontact_gid=' + farmercontact_gid + '&application_gid=' + application_gid + '&disbursement_to=' + disbursement_to + '&customer_urn=' + customer_urn + '&lsgeneratelsa_gid=' + lsgeneratelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&productsubproduct_gid=' + productsubproduct_gid);
                        }
                     }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        //$location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
                    }

                });
            }
            else {
                Notify.alert('Kindly Select One Nominee Status as Yes', 'warning')
            }
        }

        $scope.Back = function () {
            var farmercontact_gid = $location.search().farmercontact_gid;
            var application_gid = $location.search().application_gid;
            var disbursement_to = $location.search().disbursement_to;
            var customer_urn = $location.search().customer_urn;
            var rmdisbursementrequest_gid = $location.search().rmdisbursementrequest_gid;
            if (lspage == 'RMDisbursementEdit') {
                $location.url('app/MstRMDisbursementRequestEdit?farmercontact_gid=' + farmercontact_gid + '&application_gid=' + application_gid + '&disbursement_to=' + disbursement_to + '&customer_urn=' + customer_urn + '&lsgeneratelsa_gid=' + lsgeneratelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&productsubproduct_gid=' + productsubproduct_gid + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid);
            }
            else {
                $location.url('app/MstRMInitiateDisbursement?farmercontact_gid=' + farmercontact_gid + '&application_gid=' + application_gid + '&disbursement_to=' + disbursement_to + '&customer_urn=' + customer_urn + '&lsgeneratelsa_gid=' + lsgeneratelsa_gid + '&application2sanction_gid=' + application2sanction_gid + '&productsubproduct_gid=' + productsubproduct_gid + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid);
            }
            
        }

    }
})();