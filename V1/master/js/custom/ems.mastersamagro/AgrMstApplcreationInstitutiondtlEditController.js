(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApplcreationInstitutiondtlEditController', AgrMstApplcreationInstitutiondtlEditController);

    AgrMstApplcreationInstitutiondtlEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstApplcreationInstitutiondtlEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApplcreationInstitutiondtlEditController ';

        $scope.institution_gid = $location.search().lsinstitution_gid;
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

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };

            vm.calender14 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open14 = true;
            };

            vm.calender15 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open15 = true;
            };

            vm.calender20 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open20 = true;
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/AgrMstApplicationAdd/GetIntitutionTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/AgrMstApplication360/licensetypeList';
            SocketService.get(url).then(function (resp) {
                $scope.licensetype_list = resp.data.licensetype_list;
            });

            //var url = 'api/AgrMstApplication360/CompanyDocumentList';
            //SocketService.get(url).then(function (resp) {
            //    $scope.companydocument_list = resp.data.companydocument_list;
            //});

            var url = 'api/AgrMstApplication360/CompanyTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.companytype_list = resp.data.companytype_list;
            });

            var url = 'api/AgrMstApplication360/AssessmentAgencyList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });

            var url = 'api/AgrMstApplication360/AssessmentAgencyRatingList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });

            var url = 'api/AgrMstApplication360/AMLCategoryList';
            SocketService.get(url).then(function (resp) {
                $scope.amlcategory_list = resp.data.amlcategory_list;
            });

            var url = 'api/AgrMstApplication360/BusinessCategoryList';
            SocketService.get(url).then(function (resp) {
                $scope.businesscategory_list = resp.data.businesscategory_list;
            });

            var url = 'api/AgrMstApplication360/GetUserTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.usertype_list = resp.data.usertype_list;
            });

            var url = 'api/AgrMstApplication360/GetDesignationList';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });

            var param = {
                institution_gid: $scope.institution_gid
            };

            var url = 'api/AgrMstApplicationAdd/Institution2bankTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditbankacc_list = resp.data.institution2bankacc_list;

            });

            var url = 'api/AgrMstApplicationEdit/InstitutionGSTList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutiongst_list = resp.data.mstgst_list;
            });

            var url = 'api/AgrMstApplicationEdit/InstitutionMobileNoList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionmobileno_list = resp.data.mstmobileno_list;
            });

            var url = 'api/AgrMstApplicationEdit/InstitutionEmailAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionemailaddress_list = resp.data.mstemailaddress_list;
            });

            var url = 'api/AgrMstApplicationEdit/InstitutionAddressList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionaddress_list = resp.data.mstaddress_list;
            });

            var url = 'api/AgrMstApplicationEdit/InstitutionLicenseList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionlicense_list = resp.data.mstlicense_list;
            });

            var url = 'api/AgrMstApplicationEdit/InstitutionDocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionupload_list = resp.data.institutionupload_list;
            });

            var url = 'api/AgrMstApplicationEdit/InstitutionForm60DocumentList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionform60upload_list = resp.data.institutionupload_list;
            });

          

            var url = 'api/AgrMstApplicationEdit/InstitutionDetailsEdit';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.application_gid= resp.data.application_gid;
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
                $scope.txtlastyear_turnover = resp.data.lastyear_turnover;
                $scope.txtlastyear_turnover_edit = (parseInt($scope.txtlastyear_turnover.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtlastyear_turnover_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount').innerHTML = $scope.lblamountwords;
                $scope.institution_status = resp.data.institution_status;
                $scope.txteditmsmereg = resp.data.msme_registration;
                $scope.txteditlei = resp.data.lglentity_id;
                $scope.txteditleirenewal_date  = resp.data.editlei_renewaldate;
                $scope.txteditkin  = resp.data.kin;
                $scope.rdburn_status = resp.data.urn_status;
                $scope.txt_urn = resp.data.urn;
                $scope.txtrevenue = resp.data.revenue;
                $scope.txtlastyear_turnover_edit = (parseInt($scope.txtrevenue.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtlastyear_turnover_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount2').innerHTML = $scope.lblamountwords;
                $scope.txtprofit = resp.data.profit;
                $scope.txtlastyear_turnover_edit = (parseInt($scope.txtprofit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtlastyear_turnover_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount1').innerHTML = $scope.lblamountwords;
                $scope.txtfixed_asset = resp.data.fixed_assets;
                $scope.txtlastyear_turnover_edit = (parseInt($scope.txtfixed_asset.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtlastyear_turnover_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount3').innerHTML = $scope.lblamountwords;
                $scope.txtsundrydebt_adv = resp.data.sundrydebt_adv;
                $scope.txtlastyear_turnover_edit = (parseInt($scope.txtsundrydebt_adv.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountseperator = (parseInt($scope.txtlastyear_turnover_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                document.getElementById('words_totalamount4').innerHTML = $scope.lblamountwords;
                $scope.txttan_number = resp.data.tan_number;
                $scope.rdbincome_tax = resp.data.incometax_returnsstatus;
                $scope.txtstakeholder_type = resp.data.stakeholder_type;

                if (resp.data.stakeholder_type == "Applicant") {

                    $scope.disabled = true;

                }

                if (resp.data.institution_status == "Incomplete") {
                    $scope.InstitutionSubmit = true;
                    $scope.InstitutionUpdate = false;
                }
                else {
                    $scope.InstitutionSubmit = false;
                    $scope.InstitutionUpdate = true;
                }

                //if (resp.data.urn_status == 'Yes') {
                //    $scope.URN_yes = true;
                //}
                //else {
                //    $scope.URN_yes = false;
                //    $scope.txt_urn = '';
                //}

                unlockUI();

                var params = {
                    application_gid: $scope.application_gid,

                }

                var url = 'api/AgrMstApplicationEdit/GetEditProductcharges';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.program_gid = resp.data.program_gid;

                    var params = {
                        program_gid: $scope.program_gid
                    }
                    lockUI();
                    var url = 'api/AgrMstApplication360/GetInstitutionDocTypeList';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.institutiondoctype_list = resp.data.institutiondoctype_list;
                    });

                });

            });

            var params = {
                institution_gid: $scope.institution_gid,
                tmp_status: false
            }
            var url = 'api/AgrMstApplicationAdd/GetInstitutionRatingList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionratinglist = resp.data.MdlRatingdtl;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });

        }

        $scope.documenttype_change = function () {
            var params = {
                documenttypes_gid: $scope.cbocompanydocumenttype.documenttypes_gid,
                program_gid: $scope.program_gid
            }
            var url = 'api/AgrMstApplication360/CompanyDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.companydocument_list = resp.data.companydocument_list;
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
                $location.url('app/AgrMstApplicationHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
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
            var url = 'api/AgrMstApplicationAdd/GetGSTState';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtgst_state = resp.data.gst_state;
            });
        }

        $scope.onchangebusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txtbusinessstart_date
            }
            var url = 'api/AgrMstbuyer/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtyearin_business = resp.data.year_business;
                $scope.txtmonthsin_business = resp.data.month_business;
            });
        }

        $scope.getTAN = function () {
            if ($scope.txttan_number.length == 10 || txttan_number == '' || txttan_number == undefined || txttan_number == null) {

                var params = {
                    tan_no: $scope.txttan_number
                }

                var url = 'api/AgrKyc/TANCompanyauthetication';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if(resp.data.result != null) {
                        if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                            $scope.tanvalidation = true;
    
                        } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                            $scope.tanvalidation = false;
                            Notify.alert('TAN is not verified..!', 'warning');
    
                        } 
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });

            } else {
                Notify.alert(resp.data.message, 'warning')
            }
        }

        $scope.futuredatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/AgrMstApplicationAdd/FutureDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                }
            });
        }
 
        $scope.getPANbasedGST = function () {
            if ($scope.txtpan_number != undefined && $scope.txtpan_number.length == 10) {
                if ($scope.institutiongst_list != null) {
                    var paramsdel =
                    {
                        institution_gid: $scope.institution_gid
                    }
                    var url = 'api/AgrMstApplicationAdd/DeleteGSTInstitution';
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
                var url = 'api/AgrKyc/GSTSBPAN';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var param = {
                            pan: $scope.txtpan_number
                        }
                        var url = 'api/AgrKyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if(resp.data.result != null) {
                                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                    $scope.txtcompany_name = resp.data.result.name;
                                }
                            }
                            else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
 
                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/AgrMstApplicationAdd/PostInstitutionGSTList';
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
                                $scope.txtcompany_name = resp.data.result.name;
                                $scope.panvalidation = true;
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
					var pan_no = ($scope.txtpan_number =="" || $scope.txtpan_number ==undefined) ? 'No': $scope.txtpan_number;
                    var params = {
                        pan_no: pan_no,
                        aadhar_no: 'No',
                        institution_gid: $scope.institution_gid,
                        application_gid: $scope.application_gid,
                        stakeholder_type: $scope.cboStakeholdertype.user_type
                    }
                    var url = 'api/AgrMstApplicationAdd/GetOnboardAppValidatePANAadhar';
                    SocketService.post(url, params).then(function (resp) {
                        $scope.lblcreated_by = resp.data.lscreatedby_name;
                        unlockUI();
                        if (resp.data.status == true) {
                            if (resp.data.panoraadhar =="PAN")
                                $scope.Alreadyaddedpanaadhar = true;
                            else
                                $scope.Alreadyaddedpanaadhar = false;
                        }
                        else {
                            $scope.Alreadyaddedpanaadhar = false;
                        }
                    });
                }
                else
                    $scope.Alreadyaddedpanaadhar = false;
            }
        }

        function license_list() {
            var param = {
                institution_gid: $scope.institution_gid
            };
            var url = 'api/AgrMstApplicationEdit/InstitutionLicenseTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionlicense_list = resp.data.mstlicense_list;

            });
        }

        function institutiongstlist() {
            var param = {
                institution_gid: $scope.institution_gid
            };
            var url = 'api/AgrMstApplicationEdit/InstitutionGSTTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutiongst_list = resp.data.mstgst_list;
            });
        }

        function institutionmobilenolist() {
            var param = {
                institution_gid: $scope.institution_gid
            };
            var url = 'api/AgrMstApplicationEdit/InstitutionMobileNoTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionmobileno_list = resp.data.mstmobileno_list;
            });
        }

        function institutionmail_list() {
            var param = {
                institution_gid: $scope.institution_gid
            };
            var url = 'api/AgrMstApplicationEdit/InstitutionEmailAddressTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionemailaddress_list = resp.data.mstemailaddress_list;
            });
        }

        function institutionaddresslist() {
            var param = {
                institution_gid: $scope.institution_gid
            };
            var url = 'api/AgrMstApplicationEdit/InstitutionAddressTmpList';
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
                var url = 'api/AgrMstApplicationAdd/PostInstitutionGST';
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
                var url = 'api/AgrMstApplicationAdd/EditInstitutionGST';
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
                    var url = 'api/AgrMstApplicationAdd/GetGSTState';

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
                    var url = 'api/AgrMstApplicationAdd/UpdateInstitutionGST';
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
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionGST';
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
                    frm.append('project_flag', "documentformatonly");
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
                var url = 'api/AgrMstApplicationEdit/InstitutionEditForm_60DocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    lockUI();
                    var params = {
                        institution_gid: $scope.institution_gid
                    }

                    var url = "api/AgrMstApplicationEdit/InstitutionEditForm60TmpList";
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
            var url = 'api/AgrMstApplicationEdit/InstitutionEditForm_60DocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    institution_gid: $scope.institution_gid
                }

                var url = "api/AgrMstApplicationEdit/InstitutionEditForm60TmpList";
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
                var url = 'api/AgrMstApplicationAdd/PostInstitutionMobileNo';
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
                var url = 'api/AgrMstApplicationAdd/EditInstitutionMobileNo';
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
                    var url = 'api/AgrMstApplicationAdd/UpdateInstitutionMobileNo';
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
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionMobileNo';
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
                var url = 'api/AgrMstApplicationAdd/PostInstitutionEmailAddress';
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
                var url = 'api/AgrMstApplicationAdd/EditInstitutionEmailAddress';
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
                    var url = 'api/AgrMstApplicationAdd/UpdateInstitutionEmailAddress';
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
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionEmailAddress';
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
            }
        }

        
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

                    var url = 'api/AgrMstApplicationAdd/PostInstitutionAddressDetail';
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
                var url = 'api/AgrMstAddressType/GetAddressTypeASC';
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
                var url = 'api/AgrMstApplicationAdd/EditInstitutionAddressDetail';
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
                    var url = 'api/AgrMstApplicationAdd/UpdateInstitutionAddressDetail';
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
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionAddressDetail';
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

        $scope.InstitutionDocumentUpload = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cboDocumentName == null) || ($scope.cboDocumentName == '') || ($scope.cboDocumentName == undefined)
                || ($scope.cbocompanydocumenttype == null) || ($scope.cbocompanydocumenttype == '') || ($scope.cbocompanydocumenttype == undefined))
            {
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
                frm.append('documenttype_gid', $scope.cbocompanydocumenttype.documenttypes_gid);
                frm.append('documenttype_name', $scope.cbocompanydocumenttype.documenttype_name);
                frm.append('document_id', $scope.txtdocument_id);
                frm.append('institution_gid', $scope.institution_gid);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
            }
        }

        $scope.institutionDoc_upload = function () {

            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/AgrMstApplicationEdit/InstitutionEditDocumentUpload';

                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    var params = {
                        institution_gid: $scope.institution_gid
                    }

                    var url = "api/AgrMstApplicationEdit/InstitutionEditDocumentTmpList";
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
            var url = 'api/AgrMstApplicationEdit/InstitutionEditDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                var params = {
                    institution_gid: $scope.institution_gid
                }

                var url = "api/AgrMstApplicationEdit/InstitutionEditDocumentTmpList";
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

        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        // Institution License Add

        $scope.licensetype_add = function () {

            if (($scope.cboLicenseType.licensetype_name == '') || ($scope.cboLicenseType.licensetype_name == undefined) || ($scope.txtnumber == '') || ($scope.txtnumber == undefined) || ($scope.txtissue_date == '') || ($scope.txtissue_date == undefined) || ($scope.txtexpiry_date == '') || ($scope.txtexpiry_date == undefined)) {
                Notify.alert('Select License Type / Enter Number / Select Issue Date / Select Expiry Date', 'warning');
            }
            else if ($scope.txtissue_date > $scope.txtexpiry_date) {
                Notify.alert('Expiry Date Is Less Then Issued Date', 'warning');
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
                var url = 'api/AgrMstApplicationAdd/PostInstitutionLicenseDetail';
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

                var url = 'api/AgrMstApplication360/licensetypeList';
                SocketService.get(url).then(function (resp) {
                    $scope.licensetype_list = resp.data.licensetype_list;
                });

                var params = {
                    institution2licensedtl_gid: institution2licensedtl_gid
                }
                var url = 'api/AgrMstApplicationAdd/EditInstitutionLicenseDetail';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cboeditLicenseType = resp.data.licensetype_gid;
                    $scope.txteditnumber = resp.data.license_number;

                    $scope.txteditissue_date = (resp.data.licenseissue_date);
                    $scope.txteditExpiryDate = (resp.data.licenseexpiry_date);
                });

                $scope.calender01 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open01 = true;
                };

                $scope.calender02 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    $scope.open02 = true;
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

                $scope.update_licensetype = function () {


                    //var issue_date = $scope.txteditissue_date.split('-');
                    //// Please pay attention to the month (parts[1]); JavaScript counts months from 0:
                    //// January - 0, February - 1, etc.
                    //var issue_date = new Date(parts[0], parts[1] - 1, parts[2]);

                    //var expiry_date = $scope.txteditExpiryDate.split('-');
                    //// Please pay attention to the month (parts[1]); JavaScript counts months from 0:
                    //// January - 0, February - 1, etc.
                    //var expiry_date = new Date(parts[0], parts[1] - 1, parts[2]);

                    //var issue_date = new Date($scope.txteditissue_date);
                    //var expiry_date = new Date($scope.txteditExpiryDate);

                    //if ($scope.txteditissue_date > $scope.txteditExpiryDate) {
                    //    alert('Expiry Date Is Less Then Issued Date', 'warning');
                    //}
                    var IssueDate = $scope.txteditissue_date;
                    var ExpiryDate = $scope.txteditExpiryDate;

                    var checkissuedate = containsSpecialChars($scope.txteditissue_date);
                    if (checkissuedate == false) {
                        var d = new Date($scope.txteditissue_date); 
                        IssueDate = Date.parse(d.getFullYear() + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + ("0" + d.getDate()).slice(-2));
                    }
                    else {
                        IssueDate = Date.parse(IssueDate.split("-").reverse().join("-"));
                    }
                    var checkdate = containsSpecialChars($scope.txteditExpiryDate);
                    if (checkdate == false) {
                        var d = new Date($scope.txteditExpiryDate);
                        ExpiryDate = Date.parse(d.getFullYear() + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + ("0" + d.getDate()).slice(-2));
                    }
                    else {
                        ExpiryDate = Date.parse(ExpiryDate.split("-").reverse().join("-"));
                    }
                    
                    if (IssueDate > ExpiryDate) {
                        alert('Expiry Date Is Less Then Issued Date', 'warning');
                    }
                    else {
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
                        var url = 'api/AgrMstApplicationAdd/UpdateInstitutionLicenseDetail';
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
                                alert(resp.data.message, {
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

        function containsSpecialChars(str) {
            try {
                const specialChars = str.match(/-/gi);
                return true;
            }
            catch (e) {
                return false;
            }
        }

        $scope.deleteinstitution_licensetype = function (institution2licensedtl_gid) {
            var params = {
                institution2licensedtl_gid: institution2licensedtl_gid
            }
            var url = 'api/AgrMstApplicationAdd/DeleteInstitutionLicenseDetail';
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



        $scope.update_institutiondtl = function (program_gid) {

            license_list();
            creditbankacc_list();

            //     var startdate_array=$scope.txtstart_date.split("-");
            //     var enddate_array=$scope.txtend_date.split("-");

            //  /*   var start_date = Date.parse($scope.txtstart_date);
            //     var end_date = Date.parse($scope.txtend_date); */

            //   var start_date = new Date(startdate_array[2] + "/" + (startdate_array[1] + 1) + "/" + startdate_array[0]);
            //   var end_date = new Date(enddate_array[2] + "/" + (enddate_array[1] + 1) + "/" + enddate_array[0]);

            //var start_date = new Date($scope.txtstart_date);
            //var end_date = new Date($scope.txtend_date);

            var start_date = $scope.txtstart_date;
            var end_date = $scope.txtend_date;

            var checkissuedate = containsSpecialChars($scope.txtstart_date);
            if (checkissuedate == false) {
                var d = new Date($scope.txtstart_date);
                start_date = Date.parse(d.getFullYear() + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + ("0" + d.getDate()).slice(-2));
            }
            else {
                start_date = Date.parse(start_date.split("-").reverse().join("-"));
            }
            var checkdate = containsSpecialChars($scope.txtend_date);
            if (checkdate == false) {
                var d = new Date($scope.txtend_date);
                end_date = Date.parse(d.getFullYear() + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + ("0" + d.getDate()).slice(-2));
            }
            else {
                end_date = Date.parse(end_date.split("-").reverse().join("-"));
            }

            //if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
            //    Notify.alert('Kindly Enter URN', 'warning')
            //}
            //else
                if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
                Notify.alert('Kindly select Stakeholder Type', 'warning')
            }          
            else if (start_date > end_date) {
                Notify.alert('Company End Date  Is Less Then Start Date ', 'warning');
            }
            else if ($scope.institutionlicense_list == '' || $scope.institutionlicense_list == undefined || $scope.institutionlicense_list == null) {
                Notify.alert('Add Atleast one License detail', 'warning');
            }
            //else if ($scope.creditbankacc_list == '' || $scope.creditbankacc_list == undefined || $scope.creditbankacc_list == null) {
            //    Notify.alert('Add Atleast one Bank Account detail', 'warning');
            //}
            else if ($scope.Alreadyaddedpanaadhar == true) {
                Notify.alert('PAN number is already approved, you cannot add', 'warning')
                }
               
            //else if ($scope.institutionupload_list == null) {
            //    Notify.alert("Kindly upload the document", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            //else if ($scope.rdbprimaryaddress == 'No') {
            //    Notify.alert("Kindly enter the License details", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            //else if ($scope.institutionlicense_list == null) {
            //    Notify.alert("Kindly enter the License details", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            else {
                var companytypename = $('#companytype_name :selected').text();
                var usertype = $('#user_type :selected').text();
                var assessmentagencyname = $('#assessmentagency_name :selected').text();
                var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                var amlcategoryname = $('#amlcategory_name :selected').text();
                var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();

                      if ($scope.institutiongst_list != null) {
                        var Gstflag = 'Yes';
                    }
                    else if ($scope.institutiongst_list == null || $scope.institutiongst_list == '' || $scope.institutiongst_list == undefined) {
                        var Gstflag = 'No';
                    }

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
                    stakeholdertype_name: $scope.txtstakeholder_type,
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
                    tan_number: $scope.txttan_number,
                    incometax_returnsstatus: $scope.rdbincome_tax,
                    revenue: $scope.txtrevenue,
                    profit: $scope.txtprofit,
                    fixed_assets: $scope.txtfixed_asset,
                    sundrydebt_adv: $scope.txtsundrydebt_adv,
                    lastyear_turnover: $scope.txtlastyear_turnover,
                    institution_gid: $scope.institution_gid,
                    urn_status: $scope.rdburn_status,
                    urn: $scope.txt_urn,
                    Gstflag: Gstflag,
                    program_gid: program_gid
                }
                var url = 'api/AgrMstApplicationEdit/UpdateInstitutionDtl';
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
                            $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                        }
                        else {
                            $state.go('app.AgrMstApplicationGeneralEdit');
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
                $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
            }
            else {
                $state.go('app.AgrMstApplicationGeneralEdit');
            }
        }

        $scope.overallsubmit_application = function () {
            if (lstab == 'add') {
                $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
            }
            else {
                $state.go('app.AgrMstApplicationGeneralEdit');
            }
        }

        $scope.save_institution = function () {

            license_list();

            //if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
            //    Notify.alert('Kindly Enter URN', 'warning')
            //}
            //else
                if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
                Notify.alert('Kindly select Stakeholder Type', 'warning')
            }
            else if ($scope.txtstart_date > $scope.txtend_date) {
                Notify.alert('Company End Date  Is Less Then Start Date ', 'warning');
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
                    tan_number: $scope.txttan_number,
                    incometax_returnsstatus: $scope.rdbincome_tax,
                    revenue: $scope.txtrevenue,
                    profit: $scope.txtprofit,
                    fixed_assets: $scope.txtfixed_asset,
                    sundrydebt_adv: $scope.txtsundrydebt_adv,
                    lastyear_turnover: $scope.txtlastyear_turnover,
                    institution_gid: $scope.institution_gid,
                    urn_status: $scope.rdburn_status,
                    urn: $scope.txt_urn,
                }
                var url = 'api/AgrMstApplicationEdit/SaveInstitutionEditDtl';
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
                            $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                        }
                        else {
                            $state.go('app.AgrMstApplicationGeneralEdit');
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

        function creditbankacc_list() {
            var param = {
                institution_gid: $scope.institution_gid
            };
            var url = 'api/AgrMstApplicationAdd/Institution2bankTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditbankacc_list = resp.data.institution2bankacc_list;

            });
        }

        //$scope.creditbankacctdtl_edit = function (institution2bankdtl_gid) {
        //    $location.url('app/AgrMstCompanyBankAccountDtlEdit?institution_gid=' + $scope.institution_gid + '&institution2bankdtl_gid=' + institution2bankdtl_gid + '&lstab=' + lstab);
        //}

        $scope.submit_institution = function (program_gid) {

            license_list();
            creditbankacc_list();
            //if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
            //    Notify.alert('Kindly Enter URN', 'warning')
            //}
            //else
                if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
                Notify.alert('Kindly select Stakeholder Type', 'warning')
            }
            else if ($scope.txtstart_date > $scope.txtend_date) {
                Notify.alert('Company End Date  Is Less Then Start Date ', 'warning');
            }
            else if ($scope.institutionlicense_list == '' || $scope.institutionlicense_list == undefined || $scope.institutionlicense_list == null) {
                Notify.alert('Add Atleast one License detail', 'warning');
            }
            // else if ($scope.creditbankacc_list == '' || $scope.creditbankacc_list == undefined || $scope.creditbankacc_list == null) {
            //     Notify.alert('Add Atleast one Bank Account detail', 'warning');
            // }
            else if ($scope.Alreadyaddedpanaadhar == true) {
                Notify.alert('PAN number is already approved, you cannot add', 'warning')
            }

            //else if ($scope.institutionupload_list == null) {
            //    Notify.alert("Kindly upload the document", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            //else if ($scope.institutionlicense_list == null) {
            //    Notify.alert("Kindly enter the License details", {
            //        status: 'warning',
            //        pos: 'top-center',
            //        timeout: 3000
            //    });
            //}
            else {
                var companytypename = $('#companytype_name :selected').text();
                var usertype = $('#user_type :selected').text();
                var assessmentagencyname = $('#assessmentagency_name :selected').text();
                var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                var amlcategoryname = $('#amlcategory_name :selected').text();
                var businesscategoryname = $('#businesscategory_name :selected').text();
                var designationtype = $('#designation_type :selected').text();

                       if ($scope.institutiongst_list != null) {
                        var Gstflag = 'Yes';
                    }
                    else if ($scope.institutiongst_list == null || $scope.institutiongst_list == '' || $scope.institutiongst_list == undefined) {
                        var Gstflag = 'No';
                    }

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
                    tan_number: $scope.txttan_number,
                    incometax_returnsstatus: $scope.rdbincome_tax,
                    revenue: $scope.txtrevenue,
                    profit: $scope.txtprofit,
                    fixed_assets: $scope.txtfixed_asset,
                    sundrydebt_adv: $scope.txtsundrydebt_adv,
                    lastyear_turnover: $scope.txtlastyear_turnover,
                    institution_gid: $scope.institution_gid,
                    urn_status: $scope.rdburn_status,
                    urn: $scope.txt_urn,
                    Gstflag: Gstflag,
                    program_gid: program_gid
                }
                var url = 'api/AgrMstApplicationEdit/SubmitInstitutionEditDtl';
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
                            $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                        }
                        else {
                            $state.go('app.AgrMstApplicationGeneralEdit');
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

        function institutionratinglist() {
            var params = {
                institution_gid: $scope.institution_gid,
                tmp_status: "both"
            }
            var url = 'api/AgrMstApplicationAdd/GetInstitutionRatingList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionratinglist = resp.data.MdlRatingdtl;
            });
        }

        $scope.ratingdtl_add = function () {

            var lsassessmentagency_gid = '', lsassessmentagency_name = '', lsassessmentagencyrating_gid = '', lsassessmentagencyrating_name = '';

            if ($scope.cboCreditAssessmentagency != undefined || $scope.cboCreditAssessmentagency != null) {
                lsassessmentagency_gid = $scope.cboCreditAssessmentagency.assessmentagency_gid;
                lsassessmentagency_name = $scope.cboCreditAssessmentagency.assessmentagency_name;
            }

            if ($scope.cboAssessmentRating != undefined || $scope.cboAssessmentRating != null) {
                lsassessmentagencyrating_gid = $scope.cboAssessmentRating.assessmentagencyrating_gid;
                lsassessmentagencyrating_name = $scope.cboAssessmentRating.assessmentagencyrating_name;
            }

            var params = {
                institution_gid: $scope.institution_gid,
                application_gid: $scope.application_gid,
                creditrating_agencygid: lsassessmentagency_gid,
                creditrating_agencyname: lsassessmentagency_name,
                creditrating_gid: lsassessmentagencyrating_gid,
                creditrating_name: lsassessmentagencyrating_name,
                assessed_on: $scope.txtratingason_date,
                creditrating_link: $scope.txtcreditratinglink,
                tmpadd_status: true
            }
            var url = 'api/AgrMstApplicationAdd/PostRatingdtl';
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
                institutionratinglist();
                $scope.cboCreditAssessmentagency = '';
                $scope.cboAssessmentRating = '';
                $scope.txtratingason_date = '';
                $scope.txtcreditratinglink = '';
            });
        }

        $scope.ratingdtl_view = function (creditrating_link) {
            var modalInstance = $modal.open({
                templateUrl: '/institutionratingdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtcreditrating_link = creditrating_link;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.ratingdtl_delete = function (institution2ratingdetail_gid) {
            var params = {
                institution2ratingdetail_gid: institution2ratingdetail_gid
            }
            var url = 'api/AgrMstApplicationAdd/DeleteRatingDtl';
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
                institutionratinglist();
            });
        }

        $scope.txtprofit_change = function () {
            var input = document.getElementById('profit').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtprofit = "";
            }
            else {
                $scope.txtprofit = output;
                document.getElementById('words_totalamount1').innerHTML = lswords_totalamount;
            }
        }

        $scope.txtrevenue_change = function () {
            var input = document.getElementById('revenue').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtrevenue = "";
            }
            else {
                $scope.txtrevenue = output;
                document.getElementById('words_totalamount2').innerHTML = lswords_totalamount;
            }
        }

        $scope.txtfixed_assetchange = function () {
            var input = document.getElementById('fixed_asset').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtfixed_asset = "";
            }
            else {
                $scope.txtfixed_asset = output;
                document.getElementById('words_totalamount3').innerHTML = lswords_totalamount;
            }
        }

        $scope.txtsundrydebt_advchange = function () {
            var input = document.getElementById('sundrydebt_adv').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtsundrydebt_adv = "";
            }
            else {
                $scope.txtsundrydebt_adv = output;
                document.getElementById('words_totalamount4').innerHTML = lswords_totalamount;
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
                    if(resp.data.result != null) {
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
                        } 
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }
        $scope.change = function () {
            $scope.txtjointacctholder_name = '';
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
                    if(resp.data.result != null) {
                        if (resp.data.result.bankTxnStatus != "" && resp.data.result.bankTxnStatus != null) {
                            $scope.bankaccvalidation = true;
                            $scope.txtacctholder_name = resp.data.result.accountName;
    
                        } else if (resp.data.result.bankTxnStatus == "" || resp.data.result.bankTxnStatus == null) {
                            $scope.bankaccvalidation = false;
                            Notify.alert('Bank Account is not verified..!', 'warning');
                            $scope.txtacctholder_name = '';
                        } 
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning')
                    }
                });
            }
        }

        $scope.add_creditbankacctdtl = function () {

            if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') ||
                ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
            ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.rdbprimarystatus == undefined) || ($scope.rdbprimarystatus == '' || ($scope.txtBank_Address == undefined) ||
                ($scope.txtBank_Address == '') || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == ''))) {
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
                        var params = {
                            application_gid: $scope.application_gid,
                            institution_gid: $scope.institution_gid,
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
                        var url = 'api/AgrMstApplicationAdd/PostInstitutionBank';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $scope.creditbankacc_list = resp.data.creditbankacc_list;
                                $scope.credituploaddocument_list = null;
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
                            $scope.rdbprimarystatus = '';
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
                            //activate();
                            creditbankacc_list();
                        });
                    }
                }
                else {
                    var params = {
                        application_gid: $scope.application_gid,
                        institution_gid: $scope.institution_gid,                            
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
                    var url = 'api/AgrMstApplicationAdd/PostInstitutionBank';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.creditbankacc_list = resp.data.institution2bankacc_list;

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
                        $scope.rdbprimarystatus = '';
                        creditbankacc_list();

                    });
                }

            }
        }

        $scope.creditbankacctdtl_delete = function (institution2bankdtl_gid, institution_gid) {
            var params = {
                institution2bankdtl_gid: institution2bankdtl_gid,
                institution_gid: institution_gid
            }
            var url = 'api/AgrMstApplicationAdd/DeleteinstitutionBankAcc';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.creditbankacc_list = resp.data.creditbankacc_list;
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
                    activate();
                }


            });
        }

        $scope.creditbankacctdtl_edit = function (institution2bankdtl_gid) {
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
                    institution2bankdtl_gid: institution2bankdtl_gid
                }

                var url = 'api/AgrMstApplicationAdd/EditGetCreditBankAccDtl';

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
                            if(resp.data.result != null) {
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
                                } 
                            }
                            else {
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
                        ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
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
                                    institution2bankdtl_gid: institution2bankdtl_gid,
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
                                var url = 'api/AgrMstApplicationAdd/UpdateInstitutionBankAccDtl';
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
                                });
                                $modalInstance.close('closed');
                            }
                        }
                        else {
                            var bankaccounttype_name = $('#AccountType :selected').text();
                            var params = {
                                institution2bankdtl_gid: institution2bankdtl_gid,
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
                            var url = 'api/AgrMstApplicationAdd/UpdateInstitutionBankAccDtl';
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
                            });
                            $modalInstance.close('closed');
                        }
                    }
                }

            }
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.institutionupload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.institutionupload_list[i].document_path, $scope.institutionupload_list[i].document_name);
            }
        }

        //doc viewer company edit

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
                        institution2branch_gid: institution2branch_gid,
                        institution_gid: $location.search().lsinstitution_gid
                    }
                    lockUI();
                    var url = 'api/AgrMstApplicationEdit/UpdateGSTHeadOffice';
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
                };
            }

        }


    }
})();

