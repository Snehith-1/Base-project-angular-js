(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplcreationInstitutiondtlEditController', MstApplcreationInstitutiondtlEditController);

    MstApplcreationInstitutiondtlEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function MstApplcreationInstitutiondtlEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplcreationInstitutiondtlEditController ';

        $scope.institution_gid = $location.search().lsinstitution_gid;
        $scope.application_gid = $location.search().application_gid;
        var lstab = $location.search().lstab;
        activate();
        function activate() {
            // Calender Popup... //

            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open6 = true;
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
            // Calender Popup... //

            vm.calender9 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open9 = true;
            };
            // Calender Popup... //

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

            vm.calender06 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open06 = true;
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

            var url = 'api/MstApplicationAdd/GetIntitutionTempClear';
            SocketService.get(url).then(function (resp) {
            });

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

            var url = 'api/MstApplication360/GetDesignationList';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });


            var param = {
                institution_gid: $scope.institution_gid,
                Type: 'Institution',

            };
            var url = 'api/MstApplicationAdd/GetRenewalAppValidatePANAadhar';
            /* SocketService.getparams(url, param).then(function (resp) {*/
            SocketService.post(url, param).then(function (resp) {
                $scope.panrenewal_flag = resp.data.panrenewal_flag;
            });

            var url = 'api/MstApplicationEdit/InstitutionGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutiongst_list = resp.data.mstgst_list;
            });

            var url = 'api/MstApplicationEdit/InstitutionMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionmobileno_list = resp.data.mstmobileno_list;
            });

            var url = 'api/MstApplicationEdit/InstitutionEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionemailaddress_list = resp.data.mstemailaddress_list;
            });

            var url = 'api/MstApplicationEdit/InstitutionAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionaddress_list = resp.data.mstaddress_list;
            });

            var url = 'api/MstApplicationEdit/InstitutionLicenseList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionlicense_list = resp.data.mstlicense_list;
            });

            var url = 'api/MstApplicationEdit/InstitutionDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionupload_list = resp.data.institutionupload_list;
            });

            var url = 'api/MstApplicationEdit/InstitutionForm60DocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionform60upload_list = resp.data.institutionupload_list;
            });

            var url = 'api/MstApplicationEdit/GetInstitutionEquipmentHoldingList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstequipmentholding_list = resp.data.mstequipmentholding_list;
            });

            var url = 'api/MstApplicationEdit/GetInstitutionLivestockList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstlivestockholding_list = resp.data.mstlivestockholding_list;
            });

            var url = 'api/MstApplicationEdit/GetInstitutionReceivableList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstreceivable_list = resp.data.mstreceivable_list;
            });


            var url = 'api/MstApplicationEdit/InstitutionDetailsEdit';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtrenewaldue_date = resp.data.renewaldue_date ;
                $scope.txtmsme_regi_no = resp.data.msme_regi_no;
                $scope.txtkin_no = resp.data.kin_no;
                $scope.txtlei_no = resp.data.lei_no;

                $scope.txtcompany_name = resp.data.company_name;
                $scope.txtincorporation_date = resp.data.editdate_incorporation;
                $scope.txtbusinessstart_date = resp.data.editbusinessstart_date;
                $scope.txtyearin_business = resp.data.year_business;
                $scope.txtmonthsin_business = resp.data.month_business;
                $scope.txtpan_number = resp.data.companypan_no;
                $scope.txtcin_no = resp.data.cin_no;
                $scope.txtofficialtelephone_number = resp.data.official_telephoneno;
                $scope.txtofficial_mailid = resp.data.official_mailid;
                $scope.cboCompanytype = resp.data.companytype_gid;
                $scope.cboStakeholdertype = resp.data.stakeholdertype_gid;
                $scope.cboCreditAssessmentagency = resp.data.assessmentagency_gid;
                $scope.cboAssessmentRating = resp.data.assessmentagencyrating_gid;
                $scope.txtratingason_date = resp.data.editratingas_on;
                $scope.cboAMLCategory = resp.data.amlcategory_gid;
                $scope.cboBusinesscategory = resp.data.businesscategory_gid;
                $scope.txtfirst_name = resp.data.contactperson_firstname;
                $scope.txtmiddle_name = resp.data.contactperson_middlename;
                $scope.txtlast_name = resp.data.contactperson_lastname;
                $scope.cboDesignation = resp.data.designation_gid;
                $scope.txtstart_date = resp.data.editstart_date;
                $scope.txtend_date = resp.data.editend_date;
                $scope.rdbescrow = resp.data.escrow;
                $scope.txtsamplelastyear_turnover = resp.data.lastyear_turnover;
                $scope.txtlastyear_turnover = (parseInt($scope.txtsamplelastyear_turnover.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtlastyear_turnover.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount').innerHTML = $scope.lblamountwords;
                $scope.institution_status = resp.data.institution_status;
                $scope.rdburn_status = resp.data.urn_status;
                $scope.txt_urn = resp.data.urn;
                $scope.cbosamunnatibranchname = resp.data.nearsamunnatiabranch_gid;
                $scope.txtudhayam_registration = resp.data.udhayam_registration;
                $scope.txttan_number = resp.data.tan_number;
                $scope.txtbusiness_description = resp.data.business_description;
                $scope.cbotanstatename = resp.data.tanstate_gid;
                $scope.cbointernalrating = resp.data.internalrating_gid;
                $scope.txtsales = resp.data.sales;
                $scope.txtpurchase = resp.data.purchase;
                $scope.txtcredit_summation = resp.data.credit_summation;
                $scope.txtcheque_bounce = resp.data.cheque_bounce;
                $scope.txtnumberof_boardmeetings = resp.data.numberof_boardmeetings;
                $scope.txtfarmer_count = resp.data.farmer_count;
                $scope.txtcrop_cycle = resp.data.crop_cycle;
                $scope.rdbcalamities_prone = resp.data.calamities_prone;

                $scope.city_list = resp.data.cityedit_list;
                $scope.cbofpocityname = [];
                if (resp.data.fpocity_list != null) {
                    var count = resp.data.fpocity_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.city_list.map(function (x) { return x.city_gid; }).indexOf(resp.data.fpocity_list[i].city_gid);
                        $scope.cbofpocityname.push($scope.city_list[indexs]);
                        $scope.$parent.cbofpocityname = $scope.cbofpocityname;
                    }
                }

                if (resp.data.institution_status == "Incomplete") {
                    $scope.InstitutionSubmit = true;
                    $scope.InstitutionUpdate = false;
                }
                else {
                    $scope.InstitutionSubmit = false;
                    $scope.InstitutionUpdate = true;
                }

                if (resp.data.urn_status == 'Yes') {
                    $scope.URN_yes = true;
                }
                else {
                    $scope.URN_yes = false;
                    $scope.txt_urn = '';
                }

                unlockUI();
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
                var url = 'api/MstApplication360/GetInstitutionDocTypeList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.institutiondoctype_list = resp.data.institutiondoctype_list;
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


        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/MstApplicationHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
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
               /* $scope.Alreadyaddedpanaadhar = false;*/
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
                        if ($scope.panrenewal_flag == 'N') {
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
                                    $scope.txtcompany_name = '';
                                    Notify.alert('PAN is not verified..!', 'warning');
                                    institutiongstlist();
                                } else {
                                    Notify.alert(resp.data.message, 'warning')
                                }

                            });
                        }
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }

            if ($scope.panrenewal_flag == 'N') {
                if ($scope.cboStakeholdertype.user_type != "") {
                    lockUI();
                    var usertype = $('#user_type :selected').text();
                    var pan_no = ($scope.txtpan_number == "" || $scope.txtpan_number == undefined) ? 'No' : $scope.txtpan_number;
                    var params = {
                        pan_no: pan_no,
                        aadhar_no: 'No',
                        institution_gid: $scope.institution_gid,
                        application_gid: $scope.application_gid,
                        stakeholder_type: usertype,
                        panrenewal_flage: $scope.panrenewal_flag,
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

            function license_list() {
                var param = {
                    institution_gid: $scope.institution_gid
                };
                var url = 'api/MstApplicationEdit/InstitutionLicenseTmpList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutionlicense_list = resp.data.mstlicense_list;

                });
            }

            function institutiongstlist() {
                var param = {
                    institution_gid: $scope.institution_gid
                };
                var url = 'api/MstApplicationEdit/InstitutionGSTTmpList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutiongst_list = resp.data.mstgst_list;
                });
            }

            function institutionmobilenolist() {
                var param = {
                    institution_gid: $scope.institution_gid
                };
                var url = 'api/MstApplicationEdit/InstitutionMobileNoTmpList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutionmobileno_list = resp.data.mstmobileno_list;
                });
            }

            function institutionmail_list() {
                var param = {
                    institution_gid: $scope.institution_gid
                };
                var url = 'api/MstApplicationEdit/InstitutionEmailAddressTmpList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutionemailaddress_list = resp.data.mstemailaddress_list;
                });
            }

            function institutionaddresslist() {
                var param = {
                    institution_gid: $scope.institution_gid
                };
                var url = 'api/MstApplicationEdit/InstitutionAddressTmpList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutionaddress_list = resp.data.mstaddress_list;
                });
            }


            $scope.gstnumber_add = function () {
                if (($scope.rdbgstregistered == undefined) || ($scope.rdbgstregistered == '') || ($scope.txtgst_state == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_number == undefined) || ($scope.txtgst_number == '')) {
                    Notify.alert('Select GST Registered Status/ Select GST State / Enter GST Number', 'warning');
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
                            institutiongstlist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $scope.txtgst_number = '';
                    $scope.rdbgstregistered = '';
                    $scope.txtgst_state = '';
                }
            }

            $scope.edit_gstdetails = function (institution2branch_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/GSTdetailedit.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    var params = {
                        institution2branch_gid: institution2branch_gid
                    }
                    var url = 'api/MstApplicationAdd/EditInstitutionGST';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.rdbgstregistered = resp.data.gst_registered;
                        $scope.txteditgst_state = resp.data.gst_state;
                        $scope.txteditgst_number = resp.data.gst_no;
                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.onchangeeditgst_number = function () {
                        var gst_number = $scope.txteditgst_number;
                        var params = {
                            gst_code: gst_number.substring(0, 2)
                        }
                        var url = 'api/MstApplicationAdd/GetGSTState';

                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.txteditgst_state = resp.data.gst_state;
                        });
                    }

                    $scope.update_gst = function () {
                        var params = {
                            gst_state: $scope.txteditgst_state,
                            gst_no: $scope.txteditgst_number,
                            gst_registered: $scope.rdbgstregistered,
                            institution2branch_gid: institution2branch_gid,
                            institution_gid: $location.search().lsinstitution_gid
                        }
                        var url = 'api/MstApplicationAdd/UpdateInstitutionGST';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                institutiongstlist();
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

                        $modalInstance.close('closed');

                    }
                }
            }

            $scope.deletegst_details = function (institution2branch_gid) {
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
                        institutiongstlist();
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

            $scope.Form60DocumentUpload = function () {
                lockUI();
                var fi = document.getElementById('institutionform60file');
                if (fi.files.length > 0) {
                    var frm = new FormData();
                    for (var i = 0; i <= fi.files.length - 1; i++) {

                        frm.append(fi.files[i].name, fi.files[i])
                        $scope.uploadfrm = frm;
                        var fname = fi.files.item(i).name;
                        var fsize = fi.files.item(i).size;

                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "documentformatonly");
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
                    var url = 'api/MstApplicationEdit/InstitutionEditForm_60DocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        lockUI();
                        var params = {
                            institution_gid: $scope.institution_gid
                        }

                        var url = "api/MstApplicationEdit/InstitutionEditForm60TmpList";
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.institutionform60upload_list = resp.data.institutionupload_list;
                        });

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
                    alert('Please select a file.')
                }
            }

            $scope.uploaddocumentform60cancel = function (institution2form60documentupload_gid) {
                lockUI();
                var params = {
                    institution2form60documentupload_gid: institution2form60documentupload_gid,
                }
                var url = 'api/MstApplicationEdit/InstitutionEditForm_60DocumentDelete';
                SocketService.getparams(url, params).then(function (resp) {
                    var params = {
                        institution_gid: $scope.institution_gid
                    }

                    var url = "api/MstApplicationEdit/InstitutionEditForm60TmpList";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.institutionform60upload_list = resp.data.institutionupload_list;
                    });

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

            $scope.downloadsform60 = function (val1, val2) {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }

            $scope.institutionmobileno_add = function () {
                if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbprimarywhatsapp_no == undefined)) {
                    Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
                }
                else if ($scope.txtmobile_no.length < 10) {
                    Notify.alert('Enter 10 Digit Mobile Number', 'warning');
                }
                else {
                    var params = {
                        mobile_no: $scope.txtmobile_no,
                        primary_status: $scope.rdbprimarymobile_no,
                        whatsapp_no: $scope.rdbprimarywhatsapp_no,
                        institution_gid: $scope.institution_gid
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
                            institutionmobilenolist();
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
                    $scope.txtmobile_no = '';
                    $scope.rdbprimarymobile_no = '';
                    $scope.rdbprimarywhatsapp_no = '';
                }
            }

            $scope.institutionmobileno_edit = function (institution2mobileno_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/editinstitutionmobileno.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    var params = {
                        institution2mobileno_gid: institution2mobileno_gid
                    }
                    var url = 'api/MstApplicationAdd/EditInstitutionMobileNo';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.txteditmobile_no = resp.data.mobile_no;
                        $scope.rdbeditprimarymobile_no = resp.data.primary_status;
                        $scope.rdbeditwhatsappmobile_no = resp.data.whatsapp_no;
                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.update_mobileno = function () {
                        var params = {
                            mobile_no: $scope.txteditmobile_no,
                            primary_status: $scope.rdbeditprimarymobile_no,
                            whatsapp_no: $scope.rdbeditwhatsappmobile_no,
                            institution2mobileno_gid: institution2mobileno_gid,
                            institution_gid: $location.search().lsinstitution_gid,
                        }
                        var url = 'api/MstApplicationAdd/UpdateInstitutionMobileNo';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                institutionmobilenolist();
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
                        institutionmobilenolist();
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

            $scope.institutionmaildetails_add = function () {
                if (($scope.txtinstitution_emailaddress == undefined) || ($scope.txtinstitution_emailaddress == '') || ($scope.rdbemailprimarymobile_no == undefined)) {
                    Notify.alert('Enter Mail ID / Select Primary Status', 'warning');
                }
                else {
                    var params = {
                        email_address: $scope.txtinstitution_emailaddress,
                        primary_status: $scope.rdbemailprimarymobile_no,
                        institution_gid: $scope.institution_gid
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
                            institutionmail_list();
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
                    $scope.txtinstitution_emailaddress = '';
                    $scope.rdbemailprimarymobile_no = '';
                }
            }

            $scope.institutionemailaddress_edit = function (institution2email_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/institutionemailaddressedit.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    var params = {
                        institution2email_gid: institution2email_gid
                    }
                    var url = 'api/MstApplicationAdd/EditInstitutionEmailAddress';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txteditemail_address = resp.data.email_address;
                        $scope.rdbeditprimary_emailaddress = resp.data.primary_status;
                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.update_emailaddress = function () {

                        var params = {
                            email_address: $scope.txteditemail_address,
                            primary_status: $scope.rdbeditprimary_emailaddress,
                            institution2email_gid: institution2email_gid,
                            institution_gid: $location.search().lsinstitution_gid,
                        }
                        var url = 'api/MstApplicationAdd/UpdateInstitutionEmailAddress';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                institutionmail_list();
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

            $scope.institutionmaildetails_delete = function (institution2email_gid) {
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
                        institutionmail_list();
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
                    var txtlastyear_turnover = parseInt($scope.txtlastyear_turnover.replace(/[\s,]+/g, '').trim());
                }
                $scope.txtlastyear_turnover = (parseInt(input.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN')
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

            $scope.addaddress = function () {
                var modalInstance = $modal.open({
                    templateUrl: '/addaddresstype.html',
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
                    $scope.addressSubmit = function () {

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
                            longitude: $scope.txtlongitude,
                            institution_gid: $location.search().lsinstitution_gid
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
                                institutionaddresslist();
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

            $scope.editinstitution_address = function (institution2address_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/editaddresstype.html',
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
                    var url = 'api/customer/state';
                    SocketService.get(url).then(function (resp) {
                        $scope.state_list = resp.data.state_list;
                    });

                    var params = {
                        institution2address_gid: institution2address_gid
                    }
                    var url = 'api/MstApplicationAdd/EditInstitutionAddressDetail';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.cboaddresstype_GID = resp.data.address_typegid;
                        $scope.rdbprimaryaddress = resp.data.primary_status;
                        $scope.txtaddressline1 = resp.data.addressline1;
                        $scope.txtaddressline2 = resp.data.addressline2;
                        $scope.txtLand_Mark = resp.data.landmark;
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
                    $scope.update_address = function () {
                        var addresstype = $('#address_type :selected').text();
                        var params = {
                            address_typegid: $scope.cboaddresstype_GID,
                            address_type: addresstype,
                            primary_status: $scope.rdbprimaryaddress,
                            addressline1: $scope.txtaddressline1,
                            addressline2: $scope.txtaddressline2,
                            postal_code: $scope.txtpostal_code,
                            taluka: $scope.txttaluka,
                            city: $scope.txtcity,
                            state: $scope.txtstate,
                            district: $scope.txtdistrict,
                            country: $scope.txtcountry,
                            latitude: $scope.txtlatitude,
                            longitude: $scope.txtlongitude,
                            landmark: $scope.txtLand_Mark,
                            institution2address_gid: institution2address_gid,
                            institution_gid: $location.search().lsinstitution_gid
                        }
                        var url = 'api/MstApplicationAdd/UpdateInstitutionAddressDetail';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                institutionaddresslist();
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
                        institutionaddresslist();
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

            $scope.InstitutionDocumentUpload = function (val, val1, name) {
                if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined)
                    || ($scope.cboDocumentName == null) || ($scope.cboDocumentName == '') || ($scope.cboDocumentName == undefined)
                    || ($scope.cbocompanydocumenttype == null) || ($scope.cbocompanydocumenttype == '') || ($scope.cbocompanydocumenttype == undefined)) {
                    $("#file").val('');
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
                    frm.append('document_title', $scope.cboDocumentName.companydocument_name);
                    frm.append('companydocument_gid', $scope.cboDocumentName.companydocument_gid);
                    frm.append('document_id', $scope.txtdocument_id);
                    frm.append('institution_gid', $scope.institution_gid);
                    frm.append('documenttype_gid', $scope.cbocompanydocumenttype.documenttypes_gid);
                    frm.append('documenttype_name', $scope.cbocompanydocumenttype.documenttype_name);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                }
            }

            $scope.institutionDoc_upload = function () {

                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/MstApplicationEdit/InstitutionEditDocumentUpload';

                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        var params = {
                            institution_gid: $scope.institution_gid
                        }

                        var url = "api/MstApplicationEdit/InstitutionEditDocumentTmpList";
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.institutionupload_list = resp.data.institutionupload_list;
                        });

                        unlockUI();

                        $("#file").val('');
                        $scope.cboDocumentName = "";
                        $scope.txtdocument_id = "";
                        $scope.cbocompanydocumenttype = "";
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


            $scope.upload_documentcancel = function (institution2documentupload_gid) {
                lockUI();
                var params = {
                    institution2documentupload_gid: institution2documentupload_gid
                }
                var url = 'api/MstApplicationEdit/InstitutionEditDocumentDelete';
                SocketService.getparams(url, params).then(function (resp) {
                    var params = {
                        institution_gid: $scope.institution_gid
                    }

                    var url = "api/MstApplicationEdit/InstitutionEditDocumentTmpList";
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.institutionupload_list = resp.data.institutionupload_list;
                    });

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

            $scope.doc_downloads = function (val1, val2, val3) {

                if (val3 == 'N') {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                else {
                    DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
                }
        }

        $scope.documentviewer = function (val1, val2, val3) {
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

            // Institution License Add

            $scope.licensetype_add = function () {

                if (($scope.cboLicenseType.licensetype_name == '') || ($scope.cboLicenseType.licensetype_name == undefined) || ($scope.txtnumber == '') || ($scope.txtnumber == undefined) || ($scope.txtissue_date == '') || ($scope.txtissue_date == undefined) || ($scope.txtexpiry_date == '') || ($scope.txtexpiry_date == undefined)) {
                    Notify.alert('Select License Type / Enter Number / Select Issue Date / Select Expiry Date', 'warning');
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
                        licenseexpiry_date: $scope.txtexpiry_date,
                        institution_gid: $scope.institution_gid
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
                            license_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $scope.cboLicenseType = '';
                        $scope.txtnumber = '';
                        $scope.txtissue_date = '';
                        $scope.txtexpiry_date = '';
                    });
                }
            }

            $scope.editinstitution_licensetype = function (institution2licensedtl_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/institutionlicensetypedit.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {

                    var url = 'api/MstApplication360/licensetypeList';
                    SocketService.get(url).then(function (resp) {
                        $scope.licensetype_list = resp.data.licensetype_list;
                    });

                    var params = {
                        institution2licensedtl_gid: institution2licensedtl_gid
                    }
                    var url = 'api/MstApplicationAdd/EditInstitutionLicenseDetail';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.cboeditLicenseType = resp.data.licensetype_gid;
                        $scope.txteditnumber = resp.data.license_number;

                        $scope.txteditissue_date = new Date(resp.data.licenseissue_dateedit);
                        $scope.txteditExpiryDate = new Date(resp.data.licenseexpiry_dateedit);
                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.update_licensetype = function () {
                        var licensetypename = $('#licensetype_name :selected').text();
                        var params = {
                            licensetype_gid: $scope.cboeditLicenseType,
                            licensetype_name: licensetypename,
                            license_number: $scope.txteditnumber,
                            licenseissue_date: $scope.txteditissue_date,
                            licenseexpiry_date: $scope.txteditExpiryDate,
                            institution2licensedtl_gid: institution2licensedtl_gid,
                            institution_gid: $location.search().lsinstitution_gid
                        }
                        var url = 'api/MstApplicationAdd/UpdateInstitutionLicenseDetail';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                license_list();
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

            $scope.deleteinstitution_licensetype = function (institution2licensedtl_gid) {
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
                        license_list();
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

            $scope.update_institutiondtl = function () {
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
                else if ($scope.institutiongst_list != null) {
                    var Gstflag = 'Yes';
                    var companytypename = $('#companytype_name :selected').text();
                    var usertype = $('#user_type :selected').text();
                    var assessmentagencyname = $('#assessmentagency_name :selected').text();
                    var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                    var amlcategoryname = $('#amlcategory_name :selected').text();
                    var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();
                    var samunnatibranch_name = $('#samunnatibranch_name :selected').text();
                    var state_name = $('#state_name :selected').text();
                    var internalrating_name = $('#internalrating_name :selected').text();

                    var params = {
                        Renewaldue_date: $scope.txtrenewaldue_date,
                        kin_no: $scope.txtkin_no,
                        lei_no: $scope.txtlei_no,
                        msme_regi_no: $scope.txtmsme_regi_no,
                        company_name: $scope.txtcompany_name,
                        dateincorporation: $scope.txtincorporation_date,
                        businessstart_date: $scope.txtbusinessstart_date,
                        year_business: $scope.txtyearin_business,
                        month_business: $scope.txtmonthsin_business,
                        companypan_no: $scope.txtpan_number,
                        cin_no: $scope.txtcin_no,
                        official_telephoneno: $scope.txtofficialtelephone_number,
                        official_mailid: $scope.txtofficial_mailid,
                        companytype_gid: $scope.cboCompanytype,
                        companytype_name: companytypename,
                        stakeholdertype_gid: $scope.cboStakeholdertype,
                        stakeholder_type: usertype,
                        assessmentagency_gid: $scope.cboCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txtratingason_date,
                        amlcategory_gid: $scope.cboAMLCategory,
                        amlcategory_name: amlcategoryname,
                        businesscategory_gid: $scope.cboBusinesscategory,
                        businesscategory_name: businesscategoryname,
                        contactperson_firstname: $scope.txtfirst_name,
                        contactperson_middlename: $scope.txtmiddle_name,
                        contactperson_lastname: $scope.txtlast_name,
                        designation_gid: $scope.cboDesignation,
                        designation: designationtype,
                        startdate: $scope.txtstart_date,
                        enddate: $scope.txtend_date,
                        escrow: $scope.rdbescrow,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        institution_gid: $scope.institution_gid,
                        urn_status: $scope.rdburn_status,
                        urn: $scope.txt_urn,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: samunnatibranch_name,
                        udhayam_registration: $scope.txtudhayam_registration,
                        tan_number: $scope.txttan_number,
                        business_description: $scope.txtbusiness_description,
                        tanstate_gid: $scope.cbotanstatename,
                        tanstate_name: state_name,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalrating_name,
                        sales: $scope.txtsales,
                        purchase: $scope.txtpurchase,
                        credit_summation: $scope.txtcredit_summation,
                        cheque_bounce: $scope.txtcheque_bounce,
                        numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                        farmer_count: $scope.txtfarmer_count,
                        crop_cycle: $scope.txtcrop_cycle,
                        fpocity_list: $scope.$parent.cbofpocityname,
                        calamities_prone: $scope.rdbcalamities_prone,
                        Gstflag: Gstflag,
                        program_gid: $scope.program_gid
                    }
                    var url = 'api/MstApplicationEdit/UpdateInstitutionDtl';
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
                else if ($scope.institutiongst_list == null || $scope.institutiongst_list == '' || $scope.institutiongst_list == undefined) {
                    var Gstflag = 'No';
                    var companytypename = $('#companytype_name :selected').text();
                    var usertype = $('#user_type :selected').text();
                    var assessmentagencyname = $('#assessmentagency_name :selected').text();
                    var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                    var amlcategoryname = $('#amlcategory_name :selected').text();
                    var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();
                    var samunnatibranch_name = $('#samunnatibranch_name :selected').text();
                    var state_name = $('#state_name :selected').text();
                    var internalrating_name = $('#internalrating_name :selected').text();

                    var params = {
                        company_name: $scope.txtcompany_name,
                        dateincorporation: $scope.txtincorporation_date,
                        businessstart_date: $scope.txtbusinessstart_date,
                        year_business: $scope.txtyearin_business,
                        month_business: $scope.txtmonthsin_business,
                        companypan_no: $scope.txtpan_number,
                        cin_no: $scope.txtcin_no,
                        official_telephoneno: $scope.txtofficialtelephone_number,
                        official_mailid: $scope.txtofficial_mailid,
                        companytype_gid: $scope.cboCompanytype,
                        companytype_name: companytypename,
                        stakeholdertype_gid: $scope.cboStakeholdertype,
                        stakeholder_type: usertype,
                        assessmentagency_gid: $scope.cboCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txtratingason_date,
                        amlcategory_gid: $scope.cboAMLCategory,
                        amlcategory_name: amlcategoryname,
                        businesscategory_gid: $scope.cboBusinesscategory,
                        businesscategory_name: businesscategoryname,
                        contactperson_firstname: $scope.txtfirst_name,
                        contactperson_middlename: $scope.txtmiddle_name,
                        contactperson_lastname: $scope.txtlast_name,
                        designation_gid: $scope.cboDesignation,
                        designation: designationtype,
                        startdate: $scope.txtstart_date,
                        enddate: $scope.txtend_date,
                        escrow: $scope.rdbescrow,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        institution_gid: $scope.institution_gid,
                        urn_status: $scope.rdburn_status,
                        urn: $scope.txt_urn,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: samunnatibranch_name,
                        udhayam_registration: $scope.txtudhayam_registration,
                        tan_number: $scope.txttan_number,
                        business_description: $scope.txtbusiness_description,
                        tanstate_gid: $scope.cbotanstatename,
                        tanstate_name: state_name,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalrating_name,
                        sales: $scope.txtsales,
                        purchase: $scope.txtpurchase,
                        credit_summation: $scope.txtcredit_summation,
                        cheque_bounce: $scope.txtcheque_bounce,
                        numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                        farmer_count: $scope.txtfarmer_count,
                        crop_cycle: $scope.txtcrop_cycle,
                        fpocity_list: $scope.$parent.cbofpocityname,
                        calamities_prone: $scope.rdbcalamities_prone,
                        Gstflag: Gstflag,
                        program_gid: $scope.program_gid
                    }
                    var url = 'api/MstApplicationEdit/UpdateInstitutionDtl';
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
                    var companytypename = $('#companytype_name :selected').text();
                    var usertype = $('#user_type :selected').text();
                    var assessmentagencyname = $('#assessmentagency_name :selected').text();
                    var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                    var amlcategoryname = $('#amlcategory_name :selected').text();
                    var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();
                    var samunnatibranch_name = $('#samunnatibranch_name :selected').text();
                    var state_name = $('#state_name :selected').text();
                    var internalrating_name = $('#internalrating_name :selected').text();

                    var params = {
                        company_name: $scope.txtcompany_name,
                        dateincorporation: $scope.txtincorporation_date,
                        businessstart_date: $scope.txtbusinessstart_date,
                        year_business: $scope.txtyearin_business,
                        month_business: $scope.txtmonthsin_business,
                        companypan_no: $scope.txtpan_number,
                        cin_no: $scope.txtcin_no,
                        official_telephoneno: $scope.txtofficialtelephone_number,
                        official_mailid: $scope.txtofficial_mailid,
                        companytype_gid: $scope.cboCompanytype,
                        companytype_name: companytypename,
                        stakeholdertype_gid: $scope.cboStakeholdertype,
                        stakeholder_type: usertype,
                        assessmentagency_gid: $scope.cboCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txtratingason_date,
                        amlcategory_gid: $scope.cboAMLCategory,
                        amlcategory_name: amlcategoryname,
                        businesscategory_gid: $scope.cboBusinesscategory,
                        businesscategory_name: businesscategoryname,
                        contactperson_firstname: $scope.txtfirst_name,
                        contactperson_middlename: $scope.txtmiddle_name,
                        contactperson_lastname: $scope.txtlast_name,
                        designation_gid: $scope.cboDesignation,
                        designation: designationtype,
                        startdate: $scope.txtstart_date,
                        enddate: $scope.txtend_date,
                        escrow: $scope.rdbescrow,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        institution_gid: $scope.institution_gid,
                        urn_status: $scope.rdburn_status,
                        urn: $scope.txt_urn,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: samunnatibranch_name,
                        udhayam_registration: $scope.txtudhayam_registration,
                        tan_number: $scope.txttan_number,
                        business_description: $scope.txtbusiness_description,
                        tanstate_gid: $scope.cbotanstatename,
                        tanstate_name: state_name,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalrating_name,
                        sales: $scope.txtsales,
                        purchase: $scope.txtpurchase,
                        credit_summation: $scope.txtcredit_summation,
                        cheque_bounce: $scope.txtcheque_bounce,
                        numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                        farmer_count: $scope.txtfarmer_count,
                        crop_cycle: $scope.txtcrop_cycle,
                        fpocity_list: $scope.$parent.cbofpocityname,
                        calamities_prone: $scope.rdbcalamities_prone,
                        program_gid: $scope.program_gid
                    }
                    var url = 'api/MstApplicationEdit/UpdateInstitutionDtl';
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

            $scope.institutiondtl_Back = function () {
                if (lstab == 'add') {
                    $location.url('app/MstApplicationGeneralAdd?lstab=' + lstab);
                }
                else {
                    $state.go('app.MstApplicationGeneralEdit');
                }
            }

            $scope.overallsubmit_application = function () {
                if (lstab == 'add') {
                    $location.url('app/MstApplicationGeneralAdd?lstab=' + lstab);
                }
                else {
                    $state.go('app.MstApplicationGeneralEdit');
                }
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
                    var companytypename = $('#companytype_name :selected').text();
                    var usertype = $('#user_type :selected').text();
                    var assessmentagencyname = $('#assessmentagency_name :selected').text();
                    var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                    var amlcategoryname = $('#amlcategory_name :selected').text();
                    var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();
                    var samunnatibranch_name = $('#samunnatibranch_name :selected').text();
                    var state_name = $('#state_name :selected').text();
                    var internalrating_name = $('#internalrating_name :selected').text();

                    if ($scope.$parent.cbofpocityname == '') {
                        var fpocity_list = null;
                    }
                    else {
                        var fpocity_list = $scope.$parent.cbofpocityname;
                    }

                    var params = {
                        Renewaldue_date: $scope.txtrenewaldue_date,
                        kin_no: $scope.txtkin_no,
                        lei_no: $scope.txtlei_no,
                        msme_regi_no: $scope.txtmsme_regi_no,
                        company_name: $scope.txtcompany_name,
                        dateincorporation: $scope.txtincorporation_date,
                        businessstart_date: $scope.txtbusinessstart_date,
                        year_business: $scope.txtyearin_business,
                        month_business: $scope.txtmonthsin_business,
                        companypan_no: $scope.txtpan_number,
                        cin_no: $scope.txtcin_no,
                        official_telephoneno: $scope.txtofficialtelephone_number,
                        official_mailid: $scope.txtofficial_mailid,
                        companytype_gid: $scope.cboCompanytype,
                        companytype_name: companytypename,
                        stakeholdertype_gid: $scope.cboStakeholdertype,
                        stakeholder_type: usertype,
                        assessmentagency_gid: $scope.cboCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txtratingason_date,
                        amlcategory_gid: $scope.cboAMLCategory,
                        amlcategory_name: amlcategoryname,
                        businesscategory_gid: $scope.cboBusinesscategory,
                        businesscategory_name: businesscategoryname,
                        contactperson_firstname: $scope.txtfirst_name,
                        contactperson_middlename: $scope.txtmiddle_name,
                        contactperson_lastname: $scope.txtlast_name,
                        designation_gid: $scope.cboDesignation,
                        designation: designationtype,
                        startdate: $scope.txtstart_date,
                        enddate: $scope.txtend_date,
                        escrow: $scope.rdbescrow,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        institution_gid: $scope.institution_gid,
                        urn_status: $scope.rdburn_status,
                        urn: $scope.txt_urn,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: samunnatibranch_name,
                        udhayam_registration: $scope.txtudhayam_registration,
                        tan_number: $scope.txttan_number,
                        business_description: $scope.txtbusiness_description,
                        tanstate_gid: $scope.cbotanstatename,
                        tanstate_name: state_name,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalrating_name,
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
                    var url = 'api/MstApplicationEdit/SaveInstitutionEditDtl';
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

            $scope.submit_institution = function () {
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
                else if ($scope.institutiongst_list != null) {
                    var Gstflag = 'Yes';
                    var companytypename = $('#companytype_name :selected').text();
                    var usertype = $('#user_type :selected').text();
                    var assessmentagencyname = $('#assessmentagency_name :selected').text();
                    var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                    var amlcategoryname = $('#amlcategory_name :selected').text();
                    var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();
                    var samunnatibranch_name = $('#samunnatibranch_name :selected').text();
                    var state_name = $('#state_name :selected').text();
                    var internalrating_name = $('#internalrating_name :selected').text();

                    var params = {
                        Renewaldue_date: $scope.txtrenewaldue_date,
                        kin_no: $scope.txtkin_no,
                        lei_no: $scope.txtlei_no,
                        msme_regi_no: $scope.txtmsme_regi_no,
                        company_name: $scope.txtcompany_name,
                        dateincorporation: $scope.txtincorporation_date,
                        businessstart_date: $scope.txtbusinessstart_date,
                        year_business: $scope.txtyearin_business,
                        month_business: $scope.txtmonthsin_business,
                        companypan_no: $scope.txtpan_number,
                        cin_no: $scope.txtcin_no,
                        official_telephoneno: $scope.txtofficialtelephone_number,
                        official_mailid: $scope.txtofficial_mailid,
                        companytype_gid: $scope.cboCompanytype,
                        companytype_name: companytypename,
                        stakeholdertype_gid: $scope.cboStakeholdertype,
                        stakeholder_type: usertype,
                        assessmentagency_gid: $scope.cboCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txtratingason_date,
                        amlcategory_gid: $scope.cboAMLCategory,
                        amlcategory_name: amlcategoryname,
                        businesscategory_gid: $scope.cboBusinesscategory,
                        businesscategory_name: businesscategoryname,
                        contactperson_firstname: $scope.txtfirst_name,
                        contactperson_middlename: $scope.txtmiddle_name,
                        contactperson_lastname: $scope.txtlast_name,
                        designation_gid: $scope.cboDesignation,
                        designation: designationtype,
                        startdate: $scope.txtstart_date,
                        enddate: $scope.txtend_date,
                        escrow: $scope.rdbescrow,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        institution_gid: $scope.institution_gid,
                        urn_status: $scope.rdburn_status,
                        urn: $scope.txt_urn,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: samunnatibranch_name,
                        udhayam_registration: $scope.txtudhayam_registration,
                        tan_number: $scope.txttan_number,
                        business_description: $scope.txtbusiness_description,
                        tanstate_gid: $scope.cbotanstatename,
                        tanstate_name: state_name,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalrating_name,
                        sales: $scope.txtsales,
                        purchase: $scope.txtpurchase,
                        credit_summation: $scope.txtcredit_summation,
                        cheque_bounce: $scope.txtcheque_bounce,
                        numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                        farmer_count: $scope.txtfarmer_count,
                        crop_cycle: $scope.txtcrop_cycle,
                        fpocity_list: $scope.$parent.cbofpocityname,
                        calamities_prone: $scope.rdbcalamities_prone,
                        Gstflag: Gstflag,
                        program_gid: $scope.program_gid
                    }
                    var url = 'api/MstApplicationEdit/SubmitInstitutionEditDtl';
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
                else if ($scope.institutiongst_list == null || $scope.institutiongst_list == '' || $scope.institutiongst_list == undefined) {
                    var Gstflag = 'No';
                    var companytypename = $('#companytype_name :selected').text();
                    var usertype = $('#user_type :selected').text();
                    var assessmentagencyname = $('#assessmentagency_name :selected').text();
                    var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                    var amlcategoryname = $('#amlcategory_name :selected').text();
                    var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();
                    var samunnatibranch_name = $('#samunnatibranch_name :selected').text();
                    var state_name = $('#state_name :selected').text();
                    var internalrating_name = $('#internalrating_name :selected').text();

                    var params = {
                        company_name: $scope.txtcompany_name,
                        dateincorporation: $scope.txtincorporation_date,
                        businessstart_date: $scope.txtbusinessstart_date,
                        year_business: $scope.txtyearin_business,
                        month_business: $scope.txtmonthsin_business,
                        companypan_no: $scope.txtpan_number,
                        cin_no: $scope.txtcin_no,
                        official_telephoneno: $scope.txtofficialtelephone_number,
                        official_mailid: $scope.txtofficial_mailid,
                        companytype_gid: $scope.cboCompanytype,
                        companytype_name: companytypename,
                        stakeholdertype_gid: $scope.cboStakeholdertype,
                        stakeholder_type: usertype,
                        assessmentagency_gid: $scope.cboCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txtratingason_date,
                        amlcategory_gid: $scope.cboAMLCategory,
                        amlcategory_name: amlcategoryname,
                        businesscategory_gid: $scope.cboBusinesscategory,
                        businesscategory_name: businesscategoryname,
                        contactperson_firstname: $scope.txtfirst_name,
                        contactperson_middlename: $scope.txtmiddle_name,
                        contactperson_lastname: $scope.txtlast_name,
                        designation_gid: $scope.cboDesignation,
                        designation: designationtype,
                        startdate: $scope.txtstart_date,
                        enddate: $scope.txtend_date,
                        escrow: $scope.rdbescrow,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        institution_gid: $scope.institution_gid,
                        urn_status: $scope.rdburn_status,
                        urn: $scope.txt_urn,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: samunnatibranch_name,
                        udhayam_registration: $scope.txtudhayam_registration,
                        tan_number: $scope.txttan_number,
                        business_description: $scope.txtbusiness_description,
                        tanstate_gid: $scope.cbotanstatename,
                        tanstate_name: state_name,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalrating_name,
                        sales: $scope.txtsales,
                        purchase: $scope.txtpurchase,
                        credit_summation: $scope.txtcredit_summation,
                        cheque_bounce: $scope.txtcheque_bounce,
                        numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                        farmer_count: $scope.txtfarmer_count,
                        crop_cycle: $scope.txtcrop_cycle,
                        fpocity_list: $scope.$parent.cbofpocityname,
                        calamities_prone: $scope.rdbcalamities_prone,
                        Gstflag: Gstflag,
                        program_gid: $scope.program_gid
                    }
                    var url = 'api/MstApplicationEdit/SubmitInstitutionEditDtl';
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
                    var companytypename = $('#companytype_name :selected').text();
                    var usertype = $('#user_type :selected').text();
                    var assessmentagencyname = $('#assessmentagency_name :selected').text();
                    var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                    var amlcategoryname = $('#amlcategory_name :selected').text();
                    var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();
                    var samunnatibranch_name = $('#samunnatibranch_name :selected').text();
                    var state_name = $('#state_name :selected').text();
                    var internalrating_name = $('#internalrating_name :selected').text();

                    var params = {
                        company_name: $scope.txtcompany_name,
                        dateincorporation: $scope.txtincorporation_date,
                        businessstart_date: $scope.txtbusinessstart_date,
                        year_business: $scope.txtyearin_business,
                        month_business: $scope.txtmonthsin_business,
                        companypan_no: $scope.txtpan_number,
                        cin_no: $scope.txtcin_no,
                        official_telephoneno: $scope.txtofficialtelephone_number,
                        official_mailid: $scope.txtofficial_mailid,
                        companytype_gid: $scope.cboCompanytype,
                        companytype_name: companytypename,
                        stakeholdertype_gid: $scope.cboStakeholdertype,
                        stakeholder_type: usertype,
                        assessmentagency_gid: $scope.cboCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txtratingason_date,
                        amlcategory_gid: $scope.cboAMLCategory,
                        amlcategory_name: amlcategoryname,
                        businesscategory_gid: $scope.cboBusinesscategory,
                        businesscategory_name: businesscategoryname,
                        contactperson_firstname: $scope.txtfirst_name,
                        contactperson_middlename: $scope.txtmiddle_name,
                        contactperson_lastname: $scope.txtlast_name,
                        designation_gid: $scope.cboDesignation,
                        designation: designationtype,
                        startdate: $scope.txtstart_date,
                        enddate: $scope.txtend_date,
                        escrow: $scope.rdbescrow,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        institution_gid: $scope.institution_gid,
                        urn_status: $scope.rdburn_status,
                        urn: $scope.txt_urn,
                        nearsamunnatiabranch_gid: $scope.cbosamunnatibranchname,
                        nearsamunnatiabranch_name: samunnatibranch_name,
                        udhayam_registration: $scope.txtudhayam_registration,
                        tan_number: $scope.txttan_number,
                        business_description: $scope.txtbusiness_description,
                        tanstate_gid: $scope.cbotanstatename,
                        tanstate_name: state_name,
                        internalrating_gid: $scope.cbointernalrating,
                        internalrating_name: internalrating_name,
                        sales: $scope.txtsales,
                        purchase: $scope.txtpurchase,
                        credit_summation: $scope.txtcredit_summation,
                        cheque_bounce: $scope.txtcheque_bounce,
                        numberof_boardmeetings: $scope.txtnumberof_boardmeetings,
                        farmer_count: $scope.txtfarmer_count,
                        crop_cycle: $scope.txtcrop_cycle,
                        fpocity_list: $scope.$parent.cbofpocityname,
                        calamities_prone: $scope.rdbcalamities_prone,
                        program_gid: $scope.program_gid
                    }
                    var url = 'api/MstApplicationEdit/SubmitInstitutionEditDtl';
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
                var params = {
                    institution_gid: $scope.institution_gid
                }
                var url = 'api/MstApplicationEdit/GetEditInstitutionEquipmentHoldingList';
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
                var params = {
                    institution_gid: $scope.institution_gid
                }
                var url = 'api/MstApplicationEdit/GetEditInstitutionLivestockList';
                SocketService.getparams(url, params).then(function (resp) {
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
                    $scope.institution_gid = $location.search().lsinstitution_gid;
                    $scope.headoffice_submit = function () {
                        var params = {
                            institution2branch_gid: institution2branch_gid,
                            institution_gid: $scope.institution_gid
                        }
                        lockUI();
                        var url = 'api/MstApplicationEdit/UpdateGSTHeadOffice';
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
                var params = {
                    institution_gid: $scope.institution_gid
                }
                var url = 'api/MstApplicationEdit/GetEditInstitutionReceivableList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mstreceivable_list = resp.data.mstreceivable_list;

                });
            }
            $scope.downloadall = function () {
                for (var i = 0; i < $scope.institutionupload_list.length; i++) {
                    if ($scope.institutionupload_list[i].migration_flag == 'N') {
                        //DownloaddocumentService.Downloaddocument(val1, val2);
                        DownloaddocumentService.Downloaddocument($scope.institutionupload_list[i].document_path, $scope.institutionupload_list[i].document_name);
                    }
                    else {
                        //DownloaddocumentService.OtherDownloaddocument(val1, val2);
                        DownloaddocumentService.OtherDownloaddocument($scope.institutionupload_list[i].document_path, $scope.institutionupload_list[i].document_name, $scope.institutionupload_list[i].migration_flag);
                    }


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

