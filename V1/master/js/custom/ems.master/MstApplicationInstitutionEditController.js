(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplicationInstitutionEditController', MstApplicationInstitutionEditController);

    MstApplicationInstitutionEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstApplicationInstitutionEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplicationInstitutionEditController';

        lockUI();
        activate();
        var application_gid = $location.search().lsapplication_gid;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;
        function activate() {

            $scope.application_gid = $location.search().lsapplication_gid;
            $scope.amount_validation = true;

            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open6 = true;
            };
            vm.calender06 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open06 = true;
            };
            vm.calender01 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open01 = true;
            };
            // Calender Popup... //

            vm.calender7 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open7 = true;
            };
            // Calender Popup... //


            vm.calender8 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open8 = true;
            };
            vm.calender10 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open10 = true;
            };


            // Calender Popup... //

            vm.calender11 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open11 = true;
            };
            // Calender Popup... //

            vm.calender12 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open12 = true;
            };

            vm.calender099 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open099 = true;
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
            var url = 'api/MstApplicationEdit/EditProceed';
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

            var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
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

            var params = {
                application_gid: $scope.application_gid
            }

            //var url = 'api/MstApplicationAdd/GetIndividualTempClear';
            //SocketService.get(url).then(function (resp) {
            //});
            var url = 'api/MstApplicationAdd/GetIntitutionTempClear';
            SocketService.get(url).then(function (resp) {
            });
            //Individual DropDown List
            var url = 'api/MstApplication360/GenderList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.gender_list = resp.data.application_list;
            });

            var url = 'api/MstApplication360/GetDesignationList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.designation_list = resp.data.designation_list;
            });

            var url = 'api/MstApplication360/GetUserTypeList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.usertype_list = resp.data.usertype_list;
            });

            var url = 'api/MstApplication360/GetMaritalStatusList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
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

            //Institution Drop Down list
            var url = 'api/MstApplication360/licensetypeList';
            SocketService.get(url).then(function (resp) {
                $scope.licensetype_list = resp.data.licensetype_list;
            });
                        
            var url = 'api/MstApplication360/CompanyTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.companytype_list = resp.data.companytype_list;
            });

            var url = 'api/MstApplication360/AssessmentAgencyList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });

            var url = 'api/MstApplication360/AssessmentAgencyRatingList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });

            var url = 'api/MstApplication360/AMLCategoryList';
            SocketService.get(url).then(function (resp) {
                $scope.amlcategory_list = resp.data.amlcategory_list;
            });

            var url = 'api/MstApplication360/BusinessCategoryList';
            SocketService.get(url).then(function (resp) {
                $scope.businesscategory_list = resp.data.businesscategory_list;
            });

            var url = 'api/MstApplication360/GetUserTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.usertype_list = resp.data.usertype_list;
            });

            var url = 'api/MstApplication360/GetDesignationList';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });

            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            var url = 'api/MstApplication360/GetInternalRatingList';
            SocketService.get(url).then(function (resp) {
                $scope.internalrating_list = resp.data.internalrating_list;
            });

            var url = 'api/MstApplication360/GetSamunnatiBranchList';
            SocketService.get(url).then(function (resp) {
                $scope.samunnatibranch_list = resp.data.samunnatibranch_list;
            });

            var url = 'api/MstApplication360/GetCityList';
            SocketService.get(url).then(function (resp) {
                $scope.city_list = resp.data.city_list;
            });
                     

            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/MstApplicationEdit/GetEditProductcharges';
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
                var url = 'api/MstApplication360/GetInstitutionDocTypeList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.institutiondoctype_list = resp.data.institutiondoctype_list;
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

        }

        $scope.InstitutionDocumentUpload = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined)
                || ($scope.cbocompanydocumentname == null) || ($scope.cbocompanydocumentname == '') || ($scope.cbocompanydocumentname == undefined)
                || ($scope.cbocompanydocumenttype == null) || ($scope.cbocompanydocumenttype == '') || ($scope.cbocompanydocumenttype == undefined)) {
                $("#institutionfile").val('');
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

                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbocompanydocumentname.companydocument_name);
                frm.append('companydocument_gid', $scope.cbocompanydocumentname.companydocument_gid);
                frm.append('documenttype_gid', $scope.cbocompanydocumenttype.documenttypes_gid);
                frm.append('documenttype_name', $scope.cbocompanydocumenttype.documenttype_name);
                frm.append('document_id', $scope.txtdocument_id);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstApplicationAdd/InstitutionDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.institutionupload_list = resp.data.institutionupload_list;
                        unlockUI();

                        $("#institutionfile").val('');
                        $scope.cbocompanydocumentname = "";
                        $scope.cbocompanydocumenttype = "";
                        $scope.txtdocument_id = "";
                        $scope.uploadfrm = undefined;

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
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
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


        //Institution

        $scope.onchangegst_number = function () {
            var gst_number = $scope.txtgst_number;
            var params = {
                gst_code: gst_number.substring(0, 2)
            }
            var url = 'api/MstApplicationAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }


        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            var url = 'api/Mstbuyer/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyearin_business = resp.data.year_business;
                $scope.txtmonthsin_business = resp.data.month_business;
            });
        }

        $scope.futuredatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/MstApplicationAdd/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                }
            });
        }

        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_number.length == 10) {
                $scope.Alreadyaddedpanaadhar = false;
                if ($scope.institutiongst_list != null) {
                    var paramsdel =
                    {
                        institution_gid: $scope.institution_gid
                    }
                    var url = 'api/MstApplicationAdd/DeleteGSTInstitution';
                    lockUI();
                    SocketService.getparams(url, paramsdel).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                        }
                        else {
                            Notify.alert('Error occured while deleting the existing GST Details..!', 'warning');
                        }
                    });
                }
                var params = {
                    pan: $scope.txtpan_number
                }
                var url = 'api/Kyc/GSTSBPAN';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var param = {
                            pan: $scope.txtpan_number
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.txtcompany_name = resp.data.result.name;
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });


                        var params = {
                            GSTArray: GstArray
                        }
                        var url = 'api/MstApplicationAdd/PostInstitutionGSTList';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                institutiongstlist();
                            }
                            else {
                                Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                            }

                        });

                    } else if (resp.data.statusCode == 103) {

                        var param = {
                            pan: $scope.txtpan_number
                        }
                        var url = 'api/Kyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.panvalidation = true;
                                $scope.txtcompany_name = resp.data.result.name;
                                institutiongstlist();
                            } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                $scope.panvalidation = false;

                                Notify.alert('PAN is not verified..!', 'warning');
                                institutiongstlist();
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }

                        });

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
                if ($scope.cboStakeholdertype.user_type != "") {
                    lockUI();
                    var pan_no = ($scope.txtpan_number == "" || $scope.txtpan_number == undefined) ? 'No' : $scope.txtpan_number;
                    var params = {
                        pan_no: pan_no,
                        aadhar_no: 'No',
                        institution_gid: $scope.institution_gid,
                        application_gid: application_gid,
                        stakeholder_type: $scope.cboStakeholdertype.user_type,
                        panrenewal_flage: 'N',
                        credit_name: 'Institution'
                    }
                    var url = 'api/MstApplicationAdd/GetOnboardAppValidatePANAadhar';
                    SocketService.post(url, params).then(function (resp) {
                        $scope.lblcreated_by = resp.data.lscreatedby_name;
                        unlockUI();
                        if (resp.data.status == true) {
                            if (resp.data.panoraadhar == "PAN")
                                $scope.Alreadyaddedpanaadhar = true;
                            else
                                $scope.Alreadyaddedpanaadhar = false;
                        }
                        else {
                            $scope.Alreadyaddedpanaadhar = false;
                        }
                    });
                }
            }
        }
                $scope.UDYAMValidation = function () {
                    if ($scope.txtudhayam_registration.length == 19) {
                        var params = {
                            udyamreg_no: $scope.txtudhayam_registration
                        }
                        var url = 'api/Kyc/UDYAMNumber';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.result != null) {
                                if (resp.data.result.udyamRegistrationNo != "" && resp.data.result.udyamRegistrationNo != undefined) {
                                    $scope.udyamvalidation = true;
                                } else if (resp.data.result.udyamRegistrationNo == "" || resp.data.result.udyamRegistrationNo == undefined) {
                                    $scope.udyamvalidation = false;
                                    Notify.alert('UDYAM Registration Number is not verified..!', 'warning');
                                }
                            }
                            else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
                    }
                }


                $scope.InstitutionForm60DocumentUpload = function (val, val1, name) {

                    var item = {

                        file: val[0]
                    };
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");
                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        return false;
                    }
                    var frm = new FormData();
                    frm.append('fileupload', item.file);

                    frm.append('document_name', $scope.documentname);
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        lockUI();
                        var url = 'api/MstApplicationAdd/InstitutionForm_60DocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            $scope.institutionform60upload_list = resp.data.institutionupload_list;
                            unlockUI();

                            $("#institutionform60file").val('');
                            $scope.txtinstitutionform60_document = "";
                            $scope.uploadfrm = undefined;

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
                            unlockUI();
                        });
                    }
                    else {
                        alert('Document is not Available..!');
                        return;
                    }
                }

                $scope.institutionuploaddocumentcancel = function (institution2form60documentupload_gid) {
                    lockUI();
                    var params = {
                        institution2form60documentupload_gid: institution2form60documentupload_gid
                    }
                    var url = 'api/MstApplicationAdd/InstitutionForm_60DocumentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.institutionform60upload_list = resp.data.institutionupload_list;
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
                        unlockUI();
                    });
                }

                $scope.downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.save_institution = function () {
                    if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                        Notify.alert('Kindly Enter URN', 'warning')
                    }
                    else if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
                        Notify.alert('Kindly select Stakeholder Type', 'warning')
                    }
                    else if ($scope.txtstart_date > $scope.txtend_date) {
                        Notify.alert('Company End Date  Is Less Than Start Date ', 'warning');
                    }
                    else if ($scope.Alreadyaddedpanaadhar == true) {
                        Notify.alert('PAN number is already approved, you cannot add', 'warning')
                    }
                    else {
                        var lscompanytype_gid = '';
                        var lscompanytype_name = '';
                        var lsusertype_gid = '';
                        var lsuser_type = '';
                        var lsassessmentagency_gid = '';
                        var lsassessmentagency_name = '';
                        var lsassessmentagencyrating_gid = '';
                        var lsassessmentagencyrating_name = '';
                        var lsbusinesscategory_gid = '';
                        var lsbusinesscategory_name = '';
                        var lsdesignation_gid = '';
                        var lsdesignation_type = '';
                        var lsamlcategory_gid = '';
                        var lsamlcategory_name = '';
                        var lsnearsamunnatiabranch_gid = '';
                        var lsnearsamunnatiabranch_name = '';
                        var lsstate_gid = '';
                        var lsstate_name = '';
                        var lsinternalrating_gid = '';
                        var lsinternalrating_name = '';

                        if ($scope.cboCompanytype != undefined || $scope.cboCompanytype != null) {
                            lscompanytype_gid = $scope.cboCompanytype.companytype_gid;
                            lscompanytype_name = $scope.cboCompanytype.companytype_name;
                        }
                        if ($scope.cboStakeholdertype != undefined || $scope.cboStakeholdertype != null) {
                            lsusertype_gid = $scope.cboStakeholdertype.usertype_gid;
                            lsuser_type = $scope.cboStakeholdertype.user_type;
                        }
                        if ($scope.cboCreditAssessmentagency != undefined || $scope.cboCreditAssessmentagency != null) {
                            lsassessmentagency_gid = $scope.cboCreditAssessmentagency.assessmentagency_gid;
                            lsassessmentagency_name = $scope.cboCreditAssessmentagency.assessmentagency_name;
                        }
                        if ($scope.cboAssessmentRating != undefined || $scope.cboAssessmentRating != null) {
                            lsassessmentagencyrating_gid = $scope.cboAssessmentRating.assessmentagencyrating_gid;
                            lsassessmentagencyrating_name = $scope.cboAssessmentRating.assessmentagencyrating_name;
                        }
                        if ($scope.cboAMLCategory != undefined || $scope.cboAMLCategory != null) {
                            lsamlcategory_gid = $scope.cboAMLCategory.amlcategory_gid;
                            lsamlcategory_name = $scope.cboAMLCategory.amlcategory_name;
                        }
                        if ($scope.cboBusinesscategory != undefined || $scope.cboBusinesscategory != null) {
                            lsbusinesscategory_gid = $scope.cboBusinesscategory.businesscategory_gid;
                            lsbusinesscategory_name = $scope.cboBusinesscategory.businesscategory_name;
                        }
                        if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                            lsdesignation_gid = $scope.cboDesignation.designation_gid;
                            lsdesignation_type = $scope.cboDesignation.designation_type;
                        }
                        if ($scope.cbosamunnatibranchname != undefined || $scope.cbosamunnatibranchname != null) {
                            lsnearsamunnatiabranch_gid = $scope.cbosamunnatibranchname.branch_gid;
                            lsnearsamunnatiabranch_name = $scope.cbosamunnatibranchname.branch_name;
                        }
                        if ($scope.cbotanstatename != undefined || $scope.cbotanstatename != null) {
                            lsstate_gid = $scope.cbotanstatename.state_gid;
                            lsstate_name = $scope.cbotanstatename.state_name;
                        }
                        if ($scope.cbointernalrating != undefined || $scope.cbotanstatename != null) {
                            lsinternalrating_gid = $scope.cbointernalrating.internalrating_gid;
                            lsinternalrating_name = $scope.cbointernalrating.internalrating_name;
                        }

                        if ($scope.$parent.cbocityname == '') {
                            var fpocity_list = null;
                        }
                        else {
                            var fpocity_list = $scope.$parent.cbocityname;
                        }

                        var params = {
                            renewaldue_date: $scope.txtrenewaldue_date,
                            kin_no: $scope.txtkin_no,
                            lei_no: $scope.txtlei_no,
                            msme_regi_no: $scope.txtmsme_regi_no,
                            company_name: $scope.txtcompany_name,
                            date_incorporation: $scope.txtincorporation_date,
                            businessstartdate: $scope.txtbusinessstart_date,
                            year_business: $scope.txtyearin_business,
                            month_business: $scope.txtmonthsin_business,
                            companypan_no: $scope.txtpan_number,
                            cin_no: $scope.txtcin_no,
                            official_telephoneno: $scope.txtofficialtelephone_number,
                            official_mailid: $scope.txtofficial_mailid,
                            companytype_gid: lscompanytype_gid,
                            companytype_name: lscompanytype_name,
                            stakeholdertype_gid: lsusertype_gid,
                            stakeholder_type: lsuser_type,
                            assessmentagency_gid: lsassessmentagency_gid,
                            assessmentagency_name: lsassessmentagency_name,
                            assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                            assessmentagencyrating_name: lsassessmentagencyrating_name,
                            ratingas_on: $scope.txtratingason_date,
                            amlcategory_gid: lsamlcategory_gid,
                            amlcategory_name: lsamlcategory_name,
                            businesscategory_gid: lsbusinesscategory_gid,
                            businesscategory_name: lsbusinesscategory_name,
                            contactperson_firstname: $scope.txtfirst_name,
                            contactperson_middlename: $scope.txtmiddle_name,
                            contactperson_lastname: $scope.txtlast_name,
                            designation_gid: lsdesignation_gid,
                            designation: lsdesignation_type,
                            start_date: $scope.txtstart_date,
                            end_date: $scope.txtend_date,
                            escrow: $scope.rdbescrow,
                            lastyear_turnover: $scope.txtlastyear_turnover,
                            application_gid: $scope.application_gid,
                            urn_status: $scope.rdburn_status,
                            urn: $scope.txt_urn,
                            nearsamunnatiabranch_gid: lsnearsamunnatiabranch_gid,
                            nearsamunnatiabranch_name: lsnearsamunnatiabranch_name,
                            udhayam_registration: $scope.txtudhayam_registration,
                            tan_number: $scope.txttan_number,
                            business_description: $scope.txtbusiness_description,
                            tanstate_gid: lsstate_gid,
                            tanstate_name: lsstate_name,
                            internalrating_gid: lsinternalrating_gid,
                            internalrating_name: lsinternalrating_name,
                            sales: $scope.txtsales,
                            purchase: $scope.txtpurchase,
                            credit_summation: $scope.txtcredit_summation,
                            cheque_bounce: $scope.txtcheque_bounce,
                            numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                            farmer_count: $scope.txtfarmer_count,
                            crop_cycle: $scope.txtcrop_cycle,
                            fpocity_list: fpocity_list,
                            calamities_prone: $scope.rdbcalamities_prone
                        }
                        var url = 'api/MstApplicationEdit/SaveInstitutionDtlAdd';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $scope.hide_generalsummary = false;
                            $scope.show_generalform = false;
                        });
                    }
                }

                $scope.submit_institution = function () {
                    if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                        Notify.alert('Kindly Enter URN', 'warning')
                    }
                    else if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
                        Notify.alert('Kindly select Stakeholder Type', 'warning')
                    }
                    else if ($scope.txtstart_date > $scope.txtend_date) {
                        Notify.alert('Company End Date Is Less Than Start Date ', 'warning');
                    }
                    else if ($scope.Alreadyaddedpanaadhar == true) {
                        Notify.alert('PAN number is already approved, you cannot add', 'warning')
                    }
                    else if ($scope.institutiongst_list != null) {
                        var Gstflag = 'Yes';
                        var lscompanytype_gid = '';
                        var lscompanytype_name = '';
                        var lsusertype_gid = '';
                        var lsuser_type = '';
                        var lsassessmentagency_gid = '';
                        var lsassessmentagency_name = '';
                        var lsassessmentagencyrating_gid = '';
                        var lsassessmentagencyrating_name = '';
                        var lsbusinesscategory_gid = '';
                        var lsbusinesscategory_name = '';
                        var lsdesignation_gid = '';
                        var lsdesignation_type = '';
                        var lsamlcategory_gid = '';
                        var lsamlcategory_name = '';
                        var lsnearsamunnatiabranch_gid = '';
                        var lsnearsamunnatiabranch_name = '';
                        var lsstate_gid = '';
                        var lsstate_name = '';
                        var lsinternalrating_gid = '';
                        var lsinternalrating_name = '';

                        if ($scope.cboCompanytype != undefined || $scope.cboCompanytype != null) {
                            lscompanytype_gid = $scope.cboCompanytype.companytype_gid;
                            lscompanytype_name = $scope.cboCompanytype.companytype_name;
                        }
                        if ($scope.cboStakeholdertype != undefined || $scope.cboStakeholdertype != null) {
                            lsusertype_gid = $scope.cboStakeholdertype.usertype_gid;
                            lsuser_type = $scope.cboStakeholdertype.user_type;
                        }
                        if ($scope.cboCreditAssessmentagency != undefined || $scope.cboCreditAssessmentagency != null) {
                            lsassessmentagency_gid = $scope.cboCreditAssessmentagency.assessmentagency_gid;
                            lsassessmentagency_name = $scope.cboCreditAssessmentagency.assessmentagency_name;
                        }
                        if ($scope.cboAssessmentRating != undefined || $scope.cboAssessmentRating != null) {
                            lsassessmentagencyrating_gid = $scope.cboAssessmentRating.assessmentagencyrating_gid;
                            lsassessmentagencyrating_name = $scope.cboAssessmentRating.assessmentagencyrating_name;
                        }
                        if ($scope.cboAMLCategory != undefined || $scope.cboAMLCategory != null) {
                            lsamlcategory_gid = $scope.cboAMLCategory.amlcategory_gid;
                            lsamlcategory_name = $scope.cboAMLCategory.amlcategory_name;
                        }
                        if ($scope.cboBusinesscategory != undefined || $scope.cboBusinesscategory != null) {
                            lsbusinesscategory_gid = $scope.cboBusinesscategory.businesscategory_gid;
                            lsbusinesscategory_name = $scope.cboBusinesscategory.businesscategory_name;
                        }
                        if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                            lsdesignation_gid = $scope.cboDesignation.designation_gid;
                            lsdesignation_type = $scope.cboDesignation.designation_type;
                        }
                        if ($scope.cbosamunnatibranchname != undefined || $scope.cbosamunnatibranchname != null) {
                            lsnearsamunnatiabranch_gid = $scope.cbosamunnatibranchname.branch_gid;
                            lsnearsamunnatiabranch_name = $scope.cbosamunnatibranchname.branch_name;
                        }
                        if ($scope.cbotanstatename != undefined || $scope.cbotanstatename != null) {
                            lsstate_gid = $scope.cbotanstatename.state_gid;
                            lsstate_name = $scope.cbotanstatename.state_name;
                        }
                        if ($scope.cbointernalrating != undefined || $scope.cbotanstatename != null) {
                            lsinternalrating_gid = $scope.cbointernalrating.internalrating_gid;
                            lsinternalrating_name = $scope.cbointernalrating.internalrating_name;
                        }

                        var params = {
                            renewaldue_date: $scope.txtrenewaldue_date,
                            kin_no: $scope.txtkin_no,
                            lei_no: $scope.txtlei_no,
                            msme_regi_no: $scope.txtmsme_regi_no,
                            company_name: $scope.txtcompany_name,
                            date_incorporation: $scope.txtincorporation_date,
                            businessstartdate: $scope.txtbusinessstart_date,
                            year_business: $scope.txtyearin_business,
                            month_business: $scope.txtmonthsin_business,
                            companypan_no: $scope.txtpan_number,
                            cin_no: $scope.txtcin_no,
                            official_telephoneno: $scope.txtofficialtelephone_number,
                            official_mailid: $scope.txtofficial_mailid,
                            companytype_gid: lscompanytype_gid,
                            companytype_name: lscompanytype_name,
                            stakeholdertype_gid: lsusertype_gid,
                            stakeholder_type: lsuser_type,
                            assessmentagency_gid: lsassessmentagency_gid,
                            assessmentagency_name: lsassessmentagency_name,
                            assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                            assessmentagencyrating_name: lsassessmentagencyrating_name,
                            ratingas_on: $scope.txtratingason_date,
                            amlcategory_gid: lsamlcategory_gid,
                            amlcategory_name: lsamlcategory_name,
                            businesscategory_gid: lsbusinesscategory_gid,
                            businesscategory_name: lsbusinesscategory_name,
                            contactperson_firstname: $scope.txtfirst_name,
                            contactperson_middlename: $scope.txtmiddle_name,
                            contactperson_lastname: $scope.txtlast_name,
                            designation_gid: lsdesignation_gid,
                            designation: lsdesignation_type,
                            start_date: $scope.txtstart_date,
                            end_date: $scope.txtend_date,
                            escrow: $scope.rdbescrow,
                            lastyear_turnover: $scope.txtlastyear_turnover,
                            application_gid: $scope.application_gid,
                            urn_status: $scope.rdburn_status,
                            urn: $scope.txt_urn,
                            nearsamunnatiabranch_gid: lsnearsamunnatiabranch_gid,
                            nearsamunnatiabranch_name: lsnearsamunnatiabranch_name,
                            udhayam_registration: $scope.txtudhayam_registration,
                            tan_number: $scope.txttan_number,
                            business_description: $scope.txtbusiness_description,
                            tanstate_gid: lsstate_gid,
                            tanstate_name: lsstate_name,
                            internalrating_gid: lsinternalrating_gid,
                            internalrating_name: lsinternalrating_name,
                            sales: $scope.txtsales,
                            purchase: $scope.txtpurchase,
                            credit_summation: $scope.txtcredit_summation,
                            cheque_bounce: $scope.txtcheque_bounce,
                            numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                            farmer_count: $scope.txtfarmer_count,
                            crop_cycle: $scope.txtcrop_cycle,
                            fpocity_list: $scope.$parent.cbocityname,
                            calamities_prone: $scope.rdbcalamities_prone,
                            Gstflag: Gstflag,
                            program_gid: $scope.program_gid
                        }
                        var url = 'api/MstApplicationEdit/SubmitInstitutionDtlAdd';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $scope.hide_generalsummary = false;
                            $scope.show_generalform = false;
                        });

                    }
                    else if ($scope.institutiongst_list == null || $scope.institutiongst_list == '' || $scope.institutiongst_list == undefined) {
                        var Gstflag = 'No';
                        var lscompanytype_gid = '';
                        var lscompanytype_name = '';
                        var lsusertype_gid = '';
                        var lsuser_type = '';
                        var lsassessmentagency_gid = '';
                        var lsassessmentagency_name = '';
                        var lsassessmentagencyrating_gid = '';
                        var lsassessmentagencyrating_name = '';
                        var lsbusinesscategory_gid = '';
                        var lsbusinesscategory_name = '';
                        var lsdesignation_gid = '';
                        var lsdesignation_type = '';
                        var lsamlcategory_gid = '';
                        var lsamlcategory_name = '';
                        var lsnearsamunnatiabranch_gid = '';
                        var lsnearsamunnatiabranch_name = '';
                        var lsstate_gid = '';
                        var lsstate_name = '';
                        var lsinternalrating_gid = '';
                        var lsinternalrating_name = '';

                        if ($scope.cboCompanytype != undefined || $scope.cboCompanytype != null) {
                            lscompanytype_gid = $scope.cboCompanytype.companytype_gid;
                            lscompanytype_name = $scope.cboCompanytype.companytype_name;
                        }
                        if ($scope.cboStakeholdertype != undefined || $scope.cboStakeholdertype != null) {
                            lsusertype_gid = $scope.cboStakeholdertype.usertype_gid;
                            lsuser_type = $scope.cboStakeholdertype.user_type;
                        }
                        if ($scope.cboCreditAssessmentagency != undefined || $scope.cboCreditAssessmentagency != null) {
                            lsassessmentagency_gid = $scope.cboCreditAssessmentagency.assessmentagency_gid;
                            lsassessmentagency_name = $scope.cboCreditAssessmentagency.assessmentagency_name;
                        }
                        if ($scope.cboAssessmentRating != undefined || $scope.cboAssessmentRating != null) {
                            lsassessmentagencyrating_gid = $scope.cboAssessmentRating.assessmentagencyrating_gid;
                            lsassessmentagencyrating_name = $scope.cboAssessmentRating.assessmentagencyrating_name;
                        }
                        if ($scope.cboAMLCategory != undefined || $scope.cboAMLCategory != null) {
                            lsamlcategory_gid = $scope.cboAMLCategory.amlcategory_gid;
                            lsamlcategory_name = $scope.cboAMLCategory.amlcategory_name;
                        }
                        if ($scope.cboBusinesscategory != undefined || $scope.cboBusinesscategory != null) {
                            lsbusinesscategory_gid = $scope.cboBusinesscategory.businesscategory_gid;
                            lsbusinesscategory_name = $scope.cboBusinesscategory.businesscategory_name;
                        }
                        if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                            lsdesignation_gid = $scope.cboDesignation.designation_gid;
                            lsdesignation_type = $scope.cboDesignation.designation_type;
                        }
                        if ($scope.cbosamunnatibranchname != undefined || $scope.cbosamunnatibranchname != null) {
                            lsnearsamunnatiabranch_gid = $scope.cbosamunnatibranchname.branch_gid;
                            lsnearsamunnatiabranch_name = $scope.cbosamunnatibranchname.branch_name;
                        }
                        if ($scope.cbotanstatename != undefined || $scope.cbotanstatename != null) {
                            lsstate_gid = $scope.cbotanstatename.state_gid;
                            lsstate_name = $scope.cbotanstatename.state_name;
                        }
                        if ($scope.cbointernalrating != undefined || $scope.cbotanstatename != null) {
                            lsinternalrating_gid = $scope.cbointernalrating.internalrating_gid;
                            lsinternalrating_name = $scope.cbointernalrating.internalrating_name;
                        }

                        var params = {
                            company_name: $scope.txtcompany_name,
                            date_incorporation: $scope.txtincorporation_date,
                            businessstartdate: $scope.txtbusinessstart_date,
                            year_business: $scope.txtyearin_business,
                            month_business: $scope.txtmonthsin_business,
                            companypan_no: $scope.txtpan_number,
                            cin_no: $scope.txtcin_no,
                            official_telephoneno: $scope.txtofficialtelephone_number,
                            official_mailid: $scope.txtofficial_mailid,
                            companytype_gid: lscompanytype_gid,
                            companytype_name: lscompanytype_name,
                            stakeholdertype_gid: lsusertype_gid,
                            stakeholder_type: lsuser_type,
                            assessmentagency_gid: lsassessmentagency_gid,
                            assessmentagency_name: lsassessmentagency_name,
                            assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                            assessmentagencyrating_name: lsassessmentagencyrating_name,
                            ratingas_on: $scope.txtratingason_date,
                            amlcategory_gid: lsamlcategory_gid,
                            amlcategory_name: lsamlcategory_name,
                            businesscategory_gid: lsbusinesscategory_gid,
                            businesscategory_name: lsbusinesscategory_name,
                            contactperson_firstname: $scope.txtfirst_name,
                            contactperson_middlename: $scope.txtmiddle_name,
                            contactperson_lastname: $scope.txtlast_name,
                            designation_gid: lsdesignation_gid,
                            designation: lsdesignation_type,
                            start_date: $scope.txtstart_date,
                            end_date: $scope.txtend_date,
                            escrow: $scope.rdbescrow,
                            lastyear_turnover: $scope.txtlastyear_turnover,
                            application_gid: $scope.application_gid,
                            urn_status: $scope.rdburn_status,
                            urn: $scope.txt_urn,
                            nearsamunnatiabranch_gid: lsnearsamunnatiabranch_gid,
                            nearsamunnatiabranch_name: lsnearsamunnatiabranch_name,
                            udhayam_registration: $scope.txtudhayam_registration,
                            tan_number: $scope.txttan_number,
                            business_description: $scope.txtbusiness_description,
                            tanstate_gid: lsstate_gid,
                            tanstate_name: lsstate_name,
                            internalrating_gid: lsinternalrating_gid,
                            internalrating_name: lsinternalrating_name,
                            sales: $scope.txtsales,
                            purchase: $scope.txtpurchase,
                            credit_summation: $scope.txtcredit_summation,
                            cheque_bounce: $scope.txtcheque_bounce,
                            numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                            farmer_count: $scope.txtfarmer_count,
                            crop_cycle: $scope.txtcrop_cycle,
                            fpocity_list: $scope.$parent.cbocityname,
                            calamities_prone: $scope.rdbcalamities_prone,
                            Gstflag: Gstflag,
                            program_gid: $scope.program_gid
                        }
                        var url = 'api/MstApplicationEdit/SubmitInstitutionDtlAdd';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $scope.hide_generalsummary = false;
                            $scope.show_generalform = false;
                        });
                    }
                    else {
                        var lscompanytype_gid = '';
                        var lscompanytype_name = '';
                        var lsusertype_gid = '';
                        var lsuser_type = '';
                        var lsassessmentagency_gid = '';
                        var lsassessmentagency_name = '';
                        var lsassessmentagencyrating_gid = '';
                        var lsassessmentagencyrating_name = '';
                        var lsbusinesscategory_gid = '';
                        var lsbusinesscategory_name = '';
                        var lsdesignation_gid = '';
                        var lsdesignation_type = '';
                        var lsamlcategory_gid = '';
                        var lsamlcategory_name = '';
                        var lsnearsamunnatiabranch_gid = '';
                        var lsnearsamunnatiabranch_name = '';
                        var lsstate_gid = '';
                        var lsstate_name = '';
                        var lsinternalrating_gid = '';
                        var lsinternalrating_name = '';

                        if ($scope.cboCompanytype != undefined || $scope.cboCompanytype != null) {
                            lscompanytype_gid = $scope.cboCompanytype.companytype_gid;
                            lscompanytype_name = $scope.cboCompanytype.companytype_name;
                        }
                        if ($scope.cboStakeholdertype != undefined || $scope.cboStakeholdertype != null) {
                            lsusertype_gid = $scope.cboStakeholdertype.usertype_gid;
                            lsuser_type = $scope.cboStakeholdertype.user_type;
                        }
                        if ($scope.cboCreditAssessmentagency != undefined || $scope.cboCreditAssessmentagency != null) {
                            lsassessmentagency_gid = $scope.cboCreditAssessmentagency.assessmentagency_gid;
                            lsassessmentagency_name = $scope.cboCreditAssessmentagency.assessmentagency_name;
                        }
                        if ($scope.cboAssessmentRating != undefined || $scope.cboAssessmentRating != null) {
                            lsassessmentagencyrating_gid = $scope.cboAssessmentRating.assessmentagencyrating_gid;
                            lsassessmentagencyrating_name = $scope.cboAssessmentRating.assessmentagencyrating_name;
                        }
                        if ($scope.cboAMLCategory != undefined || $scope.cboAMLCategory != null) {
                            lsamlcategory_gid = $scope.cboAMLCategory.amlcategory_gid;
                            lsamlcategory_name = $scope.cboAMLCategory.amlcategory_name;
                        }
                        if ($scope.cboBusinesscategory != undefined || $scope.cboBusinesscategory != null) {
                            lsbusinesscategory_gid = $scope.cboBusinesscategory.businesscategory_gid;
                            lsbusinesscategory_name = $scope.cboBusinesscategory.businesscategory_name;
                        }
                        if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                            lsdesignation_gid = $scope.cboDesignation.designation_gid;
                            lsdesignation_type = $scope.cboDesignation.designation_type;
                        }
                        if ($scope.cbosamunnatibranchname != undefined || $scope.cbosamunnatibranchname != null) {
                            lsnearsamunnatiabranch_gid = $scope.cbosamunnatibranchname.branch_gid;
                            lsnearsamunnatiabranch_name = $scope.cbosamunnatibranchname.branch_name;
                        }
                        if ($scope.cbotanstatename != undefined || $scope.cbotanstatename != null) {
                            lsstate_gid = $scope.cbotanstatename.state_gid;
                            lsstate_name = $scope.cbotanstatename.state_name;
                        }
                        if ($scope.cbointernalrating != undefined || $scope.cbotanstatename != null) {
                            lsinternalrating_gid = $scope.cbointernalrating.internalrating_gid;
                            lsinternalrating_name = $scope.cbointernalrating.internalrating_name;
                        }

                        var params = {
                            company_name: $scope.txtcompany_name,
                            date_incorporation: $scope.txtincorporation_date,
                            businessstartdate: $scope.txtbusinessstart_date,
                            year_business: $scope.txtyearin_business,
                            month_business: $scope.txtmonthsin_business,
                            companypan_no: $scope.txtpan_number,
                            cin_no: $scope.txtcin_no,
                            official_telephoneno: $scope.txtofficialtelephone_number,
                            official_mailid: $scope.txtofficial_mailid,
                            companytype_gid: lscompanytype_gid,
                            companytype_name: lscompanytype_name,
                            stakeholdertype_gid: lsusertype_gid,
                            stakeholder_type: lsuser_type,
                            assessmentagency_gid: lsassessmentagency_gid,
                            assessmentagency_name: lsassessmentagency_name,
                            assessmentagencyrating_gid: lsassessmentagencyrating_gid,
                            assessmentagencyrating_name: lsassessmentagencyrating_name,
                            ratingas_on: $scope.txtratingason_date,
                            amlcategory_gid: lsamlcategory_gid,
                            amlcategory_name: lsamlcategory_name,
                            businesscategory_gid: lsbusinesscategory_gid,
                            businesscategory_name: lsbusinesscategory_name,
                            contactperson_firstname: $scope.txtfirst_name,
                            contactperson_middlename: $scope.txtmiddle_name,
                            contactperson_lastname: $scope.txtlast_name,
                            designation_gid: lsdesignation_gid,
                            designation: lsdesignation_type,
                            start_date: $scope.txtstart_date,
                            end_date: $scope.txtend_date,
                            escrow: $scope.rdbescrow,
                            lastyear_turnover: $scope.txtlastyear_turnover,
                            application_gid: $scope.application_gid,
                            urn_status: $scope.rdburn_status,
                            urn: $scope.txt_urn,
                            nearsamunnatiabranch_gid: lsnearsamunnatiabranch_gid,
                            nearsamunnatiabranch_name: lsnearsamunnatiabranch_name,
                            udhayam_registration: $scope.txtudhayam_registration,
                            tan_number: $scope.txttan_number,
                            business_description: $scope.txtbusiness_description,
                            tanstate_gid: lsstate_gid,
                            tanstate_name: lsstate_name,
                            internalrating_gid: lsinternalrating_gid,
                            internalrating_name: lsinternalrating_name,
                            sales: $scope.txtsales,
                            purchase: $scope.txtpurchase,
                            credit_summation: $scope.txtcredit_summation,
                            cheque_bounce: $scope.txtcheque_bounce,
                            numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                            farmer_count: $scope.txtfarmer_count,
                            crop_cycle: $scope.txtcrop_cycle,
                            fpocity_list: $scope.$parent.cbocityname,
                            calamities_prone: $scope.rdbcalamities_prone,
                            program_gid: $scope.program_gid
                        }
                        var url = 'api/MstApplicationEdit/SubmitInstitutionDtlAdd';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            $scope.hide_generalsummary = false;
                            $scope.show_generalform = false;
                        });
                    }
                }



                $scope.txtlastyear_turnoverchange = function () {
                    var input = document.getElementById('lastyear_turnover').value;
                    var str = input.replace(/,/g, '');
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamount = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtlastyear_turnover = "";
                    }
                    else {
                        $scope.txtlastyear_turnover = output;
                        document.getElementById('words_totalamount').innerHTML = lswords_totalamount;
                    }
                }

                function license_list() {
                    var url = 'api/MstApplicationAdd/GetInstitutionLicenseList';
                    SocketService.get(url).then(function (resp) {
                        $scope.institutionlicense_list = resp.data.mstlicense_list;

                    });
                }

                function institutiongstlist() {
                    var url = 'api/MstApplicationAdd/GetInstitutionGSTList';
                    SocketService.get(url).then(function (resp) {
                        $scope.institutiongst_list = resp.data.mstgst_list;
                    });
                }

                function institutionmobilenolist() {
                    var url = 'api/MstApplicationAdd/GetInstitutionMobileNoList';
                    SocketService.get(url).then(function (resp) {
                        $scope.institutionmobileno_list = resp.data.mstmobileno_list;
                    });
                }

                function institutionmail_list() {
                    var url = 'api/MstApplicationAdd/GetInstitutionEmailAddressList';
                    SocketService.get(url).then(function (resp) {
                        $scope.institutionmaildetails_list = resp.data.mstemailaddress_list;
                    });
                }

                function institutionaddress_list() {
                    var url = 'api/MstApplicationAdd/GetInstitutionAddressList';
                    SocketService.get(url).then(function (resp) {
                        $scope.institutionaddresslist = resp.data.mstaddress_list;
                    });
                }

                $scope.institutiongst_add = function () {
                    if (($scope.rdbgstregistered == undefined) || ($scope.rdbgstregistered == '') || ($scope.txtgst_state == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_number == undefined) || ($scope.txtgst_number == '')) {
                        Notify.alert('Enter GST State / Select GST Registered Status / GST Number', 'warning');
                    }
                    else {
                        var params = {
                            gst_state: $scope.txtgst_state,
                            gst_no: $scope.txtgst_number,
                            gst_registered: $scope.rdbgstregistered
                        }
                        var url = 'api/MstApplicationAdd/PostInstitutionGST';
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
                            institutiongstlist();
                            $scope.txtgst_state = '';
                            $scope.txtgst_number = '';
                            $scope.rdbgstregistered = '';
                        });
                    }
                }

                $scope.institutiongst_delete = function (institution2branch_gid) {
                    var params =
                    {
                        institution2branch_gid: institution2branch_gid
                    }
                    var url = 'api/MstApplicationAdd/DeleteInstitutionGST';
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
                        institutiongstlist();
                    });

                }

                $scope.institutionmobileno_add = function () {
                    if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbprimarywhatsapp_no == undefined) || ($scope.rdbprimarymobile_no == '') || ($scope.rdbprimarywhatsapp_no == '')) {
                        Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
                    }
                    else if ($scope.txtmobile_no.length < 10) {
                        Notify.alert('Enter 10 Digit Mobile Number', 'warning');
                    }
                    else {
                        var params = {
                            mobile_no: $scope.txtmobile_no,
                            primary_status: $scope.rdbprimarymobile_no,
                            whatsapp_no: $scope.rdbprimarywhatsapp_no
                        }
                        var url = 'api/MstApplicationAdd/PostInstitutionMobileNo';
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
                            institutionmobilenolist();
                            $scope.txtmobile_no = '';
                            $scope.rdbprimarymobile_no = '';
                            $scope.rdbprimarywhatsapp_no = '';
                        });
                    }
                }

                $scope.institutionmobileno_delete = function (institution2mobileno_gid) {
                    var params =
                    {
                        institution2mobileno_gid: institution2mobileno_gid
                    }
                    var url = 'api/MstApplicationAdd/DeleteInstitutionMobileNo';
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
                        institutionmobilenolist();
                    });

                }

                $scope.add_institutiomaildetails = function () {
                    if (($scope.txtinstitutionmail_address == undefined) || ($scope.txtinstitutionmail_address == '') || ($scope.rdbinstitutiomaildetails == undefined) || ($scope.rdbinstitutiomaildetails == '')) {
                        Notify.alert('Enter Mail ID / Select Primary Status', 'warning');
                    }
                    else {
                        var params = {
                            email_address: $scope.txtinstitutionmail_address,
                            primary_status: $scope.rdbinstitutiomaildetails
                        }
                        var url = 'api/MstApplicationAdd/PostInstitutionEmailAddress';
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
                            institutionmail_list();
                            $scope.txtinstitutionmail_address = '';
                            $scope.rdbinstitutiomaildetails = '';
                        });
                    }
                }

                $scope.institutionmail_delete = function (institution2email_gid) {
                    var params =
                    {
                        institution2email_gid: institution2email_gid
                    }
                    var url = 'api/MstApplicationAdd/DeleteInstitutionEmailAddress';
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

                        institutionmail_list();
                    });

                }


                $scope.addinstitutionaddress = function () {
                    var modalInstance = $modal.open({
                        templateUrl: '/institutionaddresstype.html',
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

                        $scope.txtcountry = "India";
                        $scope.institutionaddressSubmit = function () {

                            var params = {
                                address_typegid: $scope.cboaddresstype.address_gid,
                                address_type: $scope.cboaddresstype.address_type,
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
                            var url = 'api/MstApplicationAdd/PostInstitutionAddressDetail';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    institutionaddress_list();
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

                $scope.deleteinstitution_address = function (institution2address_gid) {
                    var params =
                    {
                        institution2address_gid: institution2address_gid
                    }
                    var url = 'api/MstApplicationAdd/DeleteInstitutionAddressDetail';
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
                        institutionaddress_list();
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
                        var url = 'api/GoogleMapsAPI/GetPlaceImage';
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

            //$scope.InstitutionDocumentUpload = function (val, val1, name) {
            //    if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbocompanydocumentname == null) || ($scope.cbocompanydocumentname == '') || ($scope.cbocompanydocumentname == undefined)) {
            //        $("#institutionfile").val('');
            //        Notify.alert('Kindly Enter the Document Title', 'warning');
            //    }
            //    else {
            //        var frm = new FormData();

            //        for (var i = 0; i < val.length; i++) {
            //            var item = {
            //                name: val[i].name,
            //                file: val[i]
            //            };
            //            frm.append('fileupload', item.file);
            //            frm.append('file_name', item.name);
            //            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");
            //            if (IsValidExtension == false) {
            //                Notify.alert("File format is not supported..!", {
            //                    status: 'danger',
            //                    pos: 'top-center',
            //                    timeout: 3000
            //                });

            //                return false;
            //            }
            //        }

            //        frm.append('document_name', $scope.documentname);
            //        frm.append('document_title', $scope.cbocompanydocumentname.companydocument_name);
            //        frm.append('companydocument_gid', $scope.cbocompanydocumentname.companydocument_gid);
            //        frm.append('document_id', $scope.txtdocument_id);
            //        frm.append('project_flag', "Default");
            //        $scope.uploadfrm = frm;
            //        if ($scope.uploadfrm != undefined) {
            //            lockUI();
            //            var url = 'api/MstApplicationAdd/InstitutionDocumentUpload';
            //            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

            //                $scope.institutionupload_list = resp.data.institutionupload_list;
            //                unlockUI();

            //                $("#institutionfile").val('');
            //                $scope.cbocompanydocumentname = "";
            //                $scope.txtdocument_id = "";
            //                $scope.uploadfrm = undefined;

            //                if (resp.data.status == true) {
            //                    Notify.alert(resp.data.message, {
            //                        status: 'success',
            //                        pos: 'top-center',
            //                        timeout: 3000
            //                    });
            //                }
            //                else {
            //                    Notify.alert(resp.data.message, {
            //                        status: 'warning',
            //                        pos: 'top-center',
            //                        timeout: 3000
            //                    });
            //                }
            //                unlockUI();
            //            });
            //        }
            //        else {
            //            alert('Document is not Available..!');
            //            return;
            //        }
            //    }
            //}


                // Institution License Add

                $scope.institutionlicenseadd = function () {

                    if (($scope.cboLicenseType == '') || ($scope.cboLicenseType == undefined) || ($scope.txtnumber == '') || ($scope.txtnumber == undefined) || ($scope.txtissue_date == '') || ($scope.txtissue_date == undefined) || ($scope.txtexpiry_date == '') || ($scope.txtexpiry_date == undefined)) {
                        Notify.alert('Kindly Fill All Mandatory Fields', 'warning');
                    }
                    else if ($scope.txtissue_date > $scope.txtexpiry_date) {
                        Notify.alert('Expiry Date Is Less Than Issued Date', 'warning');
                    }
                    else {
                        var params = {
                            licensetype_gid: $scope.cboLicenseType.licensetype_gid,
                            licensetype_name: $scope.cboLicenseType.licensetype_name,
                            license_number: $scope.txtnumber,
                            licenseissue_date: $scope.txtissue_date,
                            licenseexpiry_date: $scope.txtexpiry_date

                        }
                        var url = 'api/MstApplicationAdd/PostInstitutionLicenseDetail';
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
                            license_list();
                            $scope.cboLicenseType = '';
                            $scope.txtnumber = '';
                            $scope.txtissue_date = '';
                            $scope.txtexpiry_date = '';
                        });
                    }
                }

                // Institution license Delete

                $scope.institutionlicense_delete = function (institution2licensedtl_gid) {
                    var params = {
                        institution2licensedtl_gid: institution2licensedtl_gid
                    }
                    var url = 'api/MstApplicationAdd/DeleteInstitutionLicenseDetail';
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
                        license_list();
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

                $scope.institutiondocument_delete = function (institution2documentupload_gid) {
                    lockUI();
                    var params = {
                        institution2documentupload_gid: institution2documentupload_gid
                    }
                    var url = 'api/MstApplicationAdd/InstitutionDocumentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.institutionupload_list = resp.data.institutionupload_list;
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
                        unlockUI();
                    });
                }

                $scope.general_Tab = function () {
                    var application_gid = $scope.application_gid;
                    var application_status = $scope.application_status;
                    if ($scope.application_status == 'Completed') {
                        $location.url('app/MstApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
                    }
                    else {
                        $scope.Company_dtls = true;
                    }
                }

                $scope.company_add = function () {
                    var application_gid = $scope.application_gid;
                    var application_status = $scope.application_status;
                    if ($scope.application_status == 'Completed') {
                        $location.url('app/MstApplicationInstitutionEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
                    }
                    else {
                        $scope.Institution_dtls = true;
                    }
                }

                $scope.Individual_add = function () {
                    var application_gid = $scope.application_gid;
                    var application_status = $scope.application_status;
                    if ($scope.application_status == 'Completed') {
                        $location.url('app/MstApplicationIndividualEdit?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                    }
                    else {
                        $scope.Individual_dtls = true;
                    }
                }

                $scope.Group_add = function () {
                    var application_gid = $scope.application_gid;
                    var application_status = $scope.application_status;
                    if ($scope.application_status == 'Completed') {
                        $location.url('app/MstApplicationGroupEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
                    }
                    else {
                        $scope.Group_dtls = true;
                    }
                }
                $scope.Hypothecation_add = function () {
                    var application_gid = $scope.application_gid;
                    var applicant_type = $scope.applicant_type;

                    if ($scope.applicant_type == null || $scope.applicant_type == '') {
                        $scope.Hypothecation_dtls = true;
                    }
                    else {
                        $location.url('app/MstAppEditHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
                    }
                }
                $scope.EconomicCapital_add = function () {
                    var application_gid = $scope.application_gid;
                    var application_status = $scope.application_status;
                    if ($scope.application_status == 'Completed') {
                        $location.url('app/MstApplicationSocialTradeCapitalEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
                    }
                    else {
                        $scope.EconomicCapital_dtls = true;
                    }
                }

                $scope.BureauUpdates_add = function () {
                    var application_gid = $scope.application_gid;
                    var application_status = $scope.application_status;
                    if ($scope.application_status == 'Completed') {
                        $location.url('app/MstApplicationEditCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
                    }
                    else {
                        $scope.BureauUpdates_dtls = true;
                    }
                }

                $scope.ProductCharges_add = function () {
                    var application_gid = $scope.application_gid;
                    var applicant_type = $scope.applicant_type;
                    if ($scope.applicant_type == null || $scope.applicant_type == '') {
                        $scope.ProductCharges_dtls = true;
                    }
                    else {
                        $location.url('app/MstApplicationProductChargesEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
                    }
                }
                $scope.OverallLimit_add = function () {
                    var application_gid = $scope.application_gid;
                    var applicant_type = $scope.applicant_type;

                    if ($scope.applicant_type == null || $scope.applicant_type == '') {
                        $scope.Hypothecation_dtls = true;
                    }
                    else {
                        $location.url('app/MstAppEditOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
                    }
                }
                $scope.ProductCharges_add = function () {
                    var application_gid = $scope.application_gid;
                    var applicant_type = $scope.applicant_type;

                    if ($scope.applicant_type == null || $scope.applicant_type == '') {
                        $scope.Hypothecation_dtls = true;
                    }
                    else {
                        $location.url('app/MstAppEditProductAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
                    }
                }
                $scope.ServiceCharges_add = function () {
                    var application_gid = $scope.application_gid;
                    var applicant_type = $scope.applicant_type;

                    if ($scope.applicant_type == null || $scope.applicant_type == '') {
                        $scope.Hypothecation_dtls = true;
                    }
                    else {
                        $location.url('app/MstAppEditChargeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
                    }
                }


                $scope.doneclick = function () {
                    lockUI();
                    var params = {
                        application_gid: $scope.application_gid
                    }
                    var url = 'api/MstApplicationEdit/EditProceed';
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

                    var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
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
                    var params = {
                        application_gid: application_gid
                    }
                    var url = 'api/MstApplicationEdit/EditAppProceed';
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
                        $state.go('app.MstApplicationCreationSummary');
                    });

                }

                $scope.submit = function () {
                    lockUI();
                    var params = {
                        application_gid: $scope.application_gid
                    }
                    var url = 'api/MstApplicationEdit/EditAppReProceed';
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
                        $state.go('app.MstApplicationCreationSummary');
                    });

                }

                $scope.Back = function () {
                    $state.go('app.MstApplicationCreationSummary');
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

                        var url = 'api/MstApplicationAdd/FnKycDcoumentValidation';
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


                        var url = 'api/MstApplicationAdd/GetApprovalHierarchyChangeList';
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
                            var url = 'api/MstApplicationAdd/UpdateApprovalHierarchyChange';
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
                            var url = 'api/MstApplicationAdd/PostInstitutionEquipmentHolding';
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

                $scope.deleteequipment_holding = function (institution2equipment_gid) {
                    var params = {
                        institution2equipment_gid: institution2equipment_gid
                    }
                    var url = 'api/MstApplicationAdd/DeleteInstitutionEquipmentHolding';
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

                $scope.equipment_View = function (institution2equipment_gid) {
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
                            institution2equipment_gid: institution2equipment_gid
                        }
                        var url = 'api/MstApplicationAdd/GetEquipmentHoldingView';
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
                    var url = 'api/MstApplicationAdd/GetInstitutionEquipmentHoldingList';
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
                            var url = 'api/MstApplicationAdd/PostInstitutionLivestock';
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

                $scope.deletelivestock_holding = function (institution2livestock_gid) {
                    var params = {
                        institution2livestock_gid: institution2livestock_gid
                    }
                    var url = 'api/MstApplicationAdd/DeleteInstitutionLivestock';
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

                $scope.livestock_View = function (institution2livestock_gid) {
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
                            institution2livestock_gid: institution2livestock_gid
                        }
                        var url = 'api/MstApplicationAdd/GetLivestockHoldingView';
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
                    var url = 'api/MstApplicationAdd/GetInstitutionLivestockList';
                    SocketService.get(url).then(function (resp) {
                        $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;

                    });
                }

                $scope.headoffice_confirm = function (institution2branch_gid) {
                    var modalInstance = $modal.open({
                        templateUrl: '/HeadOfficeConfirmation.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.headoffice_submit = function () {
                            var params = {
                                institution2branch_gid: institution2branch_gid
                            }
                            lockUI();
                            var url = 'api/MstApplicationAdd/UpdateGSTHeadOffice';
                            SocketService.post(url, params).then(function (resp) {
                                if (resp.data.status == true) {
                                    unlockUI();
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                    institutiongstlist();
                                }
                                else {
                                    alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $modalInstance.close('closed');
                                    unlockUI();
                                }
                            });
                        }

                        $scope.ok = function () {
                            $modalInstance.close('closed');
                            activate();
                        };


                    }

                }

                $scope.add_receivable = function () {
                    var modalInstance = $modal.open({
                        templateUrl: '/Receivableadd.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'lg'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {

                        // Calender Popup... //
                        $scope.calenderopen16 = function ($event) {
                            $event.preventDefault();
                            $event.stopPropagation();
                            $scope.open16 = true;
                        };
                        $scope.formats = ['dd-MM-yyyy'];
                        $scope.format = $scope.formats[0];
                        $scope.dateOptions = {
                            formatYear: 'yy',
                            startingDay: 1
                        };

                        $scope.receivable_submit = function () {
                            var params = {
                                receivable_date: $scope.txtreceivable_date,
                                onetothirty_days: $scope.txtonetothirty_days,
                                thirtyonetosixty_days: $scope.txtthirtyonetosixty_days,
                                sixtyonetoninety_days: $scope.txtsixtyonetoninety_days,
                                ninety_days: $scope.txtninety_days
                            }
                            var url = 'api/MstApplicationAdd/PostInstitutionReceivable';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    receivable_list();
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

                $scope.delete_receivable = function (institution2receivable_gid) {
                    var params = {
                        institution2receivable_gid: institution2receivable_gid
                    }
                    var url = 'api/MstApplicationAdd/DeleteInstitutionReceivable';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            receivable_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        receivable_list();
                    });

                }

                function receivable_list() {
                    var url = 'api/MstApplicationAdd/GetInstitutionReceivableList';
                    SocketService.get(url).then(function (resp) {
                        $scope.mstreceivable_list = resp.data.mstreceivable_list;

                    });
                }

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.institutionupload_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.institutionupload_list[i].document_path, $scope.institutionupload_list[i].document_name);
                    }
                }

        $scope.documenttype_change = function () {
                            var params = {
                                documenttypes_gid: $scope.cbocompanydocumenttype.documenttypes_gid,
                                program_gid: $scope.program_gid
                            }
                            var url = 'api/MstApplication360/CompanyDocumentList';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.companydocument_list = resp.data.companydocument_list;
                            });
                        }            
                }
    
})();
