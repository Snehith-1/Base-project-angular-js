(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprOnboardInfoEditcontroller', AgrMstSuprOnboardInfoEditcontroller);

    AgrMstSuprOnboardInfoEditcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstSuprOnboardInfoEditcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprOnboardInfoEditcontroller';

        activate();

        function activate() {
            $scope.companynameYes = true;

            //var selectedIndex = $location.search().selectedIndex;
            //var FromRM = $location.search().FromRM;
            //var application_gid = $location.search().application_gid;
            //$scope.application_gid = $location.search().application_gid;
            //var institution_gid = $location.search().institution_gid;
            //$scope.institution_gid = $location.search().institution_gid;
            //var contact_gid = $location.search().contact_gid;
            //$scope.contact_gid = $location.search().contact_gid;
            //var lsApp = $location.search().lsApp;
            $scope.cboVernacularLanguage = [];
            $scope.showgen = true;
            //$scope.bankaccvalidation = "";
            var searchObject = cmnfunctionService.decryptURL($location.search().hash);
            var selectedIndex = searchObject.selectedIndex;
            var FromRM = searchObject.FromRM;
            var application_gid = searchObject.application_gid;
            $scope.application_gid = searchObject.application_gid;
            var institution_gid = searchObject.institution_gid;
            $scope.institution_gid = searchObject.institution_gid;
            var contact_gid = searchObject.contact_gid;
            $scope.contact_gid = searchObject.contact_gid;
            var lsApp = searchObject.lsApp;

            if (FromRM != null && lsApp != null) {

                $scope.gendev = false;
                $scope.indivdiv = false;
                $scope.instdiv = false;

            }

            else {

                $scope.gendev = true;
                $scope.indivdiv = true;
                $scope.instdiv = true;

            }
            $scope.editinstituion = false;

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

            // Calender Popup... //

            vm.calender12 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open12 = true;
            };

            // Calender Popup... //

            vm.calender01 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open01 = true;
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

            var url = 'api/AgrMstSupplierOnboard/suprronboardTmpClear';
            SocketService.get(url).then(function (resp) {

            });

            

        }

        function miniactivater() {
            $scope.showapprovalsubmitbtn = false;
            var param = {
                application_gid: $scope.application_gid
            };

            lockUI();
            var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualSummaryEdit';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.individual_list = resp.data.BuyerOnboardcicindividual_list;
                    if ($scope.individual_list != null) {
                        var getapplicant = $scope.individual_list.find(function (v) { return v.stakeholder_type === "Applicant" });
                        if (getapplicant.contact_gid != null) {
                            $scope.showapprovalsubmitbtn = true;
                        }
                    }
                }
                else {
                    unlockUI();
                }
            });
            lockUI();
            var url = 'api/AgrMstSupplierOnboardEdit/GetInstitutionSummaryEdit';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.institution_list = resp.data.BuyerOnboardcicinstitution_list;
                    if ($scope.institution_list != null) {
                        var getapplicant = $scope.institution_list.find(function (v) { return v.stakeholder_type === "Applicant" });
                        if (getapplicant.institution_gid != null) {
                            $scope.showapprovalsubmitbtn = true;
                        }
                    }
                }
                else {
                    unlockUI();
                }
            });

    }

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.GetBuyergeneral = function () {
            $scope.showapprovalsubmitbtn = false;
            var url = 'api/AgrMstApplicationAdd/GetDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
                $scope.vernacularlang_list = resp.data.vernacularlang_list;
                $scope.constitutionlist = resp.data.constitutionlist;
                $scope.designationlist = resp.data.designationlist;
                $scope.program_list = resp.data.program_list;
                $scope.productname_list = resp.data.productname_list;
            });

            var url = 'api/AgrMstApplicationAdd/GetGeneticCodeList';
            SocketService.get(url).then(function (resp) {
                $scope.genetic_list = resp.data.genetic_list;
            });

            var url = 'api/AgrMstBuyerOnboard/GetBuyerSupplierType';
            SocketService.get(url).then(function (resp) {
                $scope.suppliertype_list = resp.data.BuyerSupplierType_List;
            });

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/AgrMstSupplierOnboardEdit/GetGeneralInfoEdit';
            SocketService.getparams(url, param).then(function (resp) {
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
                $scope.approval_submittedflag = resp.data.approval_submittedflag;

                if ($scope.application_gid != null) {
                    $scope.gendev = false;
                    $scope.gendtldev = true;
                    $scope.indivdiv = true;
                    $scope.instdiv = true;
                }

                if ($scope.approval_submittedflag == "Y") {
                    $scope.hidesubmitbtn = true;
                }
                else {
                    $scope.hidesubmitbtn = false;
                }

            });
            unlockUI();
            lockUI();
            var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualSummaryEdit';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.individual_list = resp.data.BuyerOnboardcicindividual_list;
                    if ($scope.individual_list != null) {
                        var getapplicant = $scope.individual_list.find(function (v) { return v.stakeholder_type === "Applicant" });                                         
                        if (getapplicant.contact_gid != null) {
                            $scope.showapprovalsubmitbtn = true;                         
                        }
                    }
                }
                else {
                    unlockUI();
                }
            });
            lockUI();
            var url = 'api/AgrMstSupplierOnboardEdit/GetInstitutionSummaryEdit';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.institution_list = resp.data.BuyerOnboardcicinstitution_list;
                    if ($scope.institution_list != null) {
                        var getapplicant = $scope.institution_list.find(function (v) { return v.stakeholder_type === "Applicant"});                      
                        if (getapplicant.institution_gid != null) {
                            $scope.showapprovalsubmitbtn = true;                        
                        }
                    }
                }
                else {
                    unlockUI();
                }
            });
        }


        $scope.GetBuyerinstitution = function () {
            $scope.Alreadyaddedpanaadhar = false;
            var url = 'api/AgrMstApplication360/CompanyTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.companytype_list = resp.data.companytype_list;
            });

            var url = 'api/AgrMstApplication360/GetUserTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.usertype_list = resp.data.usertype_list;
            });

            var url = 'api/AgrMstApplication360/AMLCategoryList';
            SocketService.get(url).then(function (resp) {
                $scope.amlcategory_list = resp.data.amlcategory_list;
            });

            var url = 'api/AgrMstApplication360/BusinessCategoryList';
            SocketService.get(url).then(function (resp) {
                $scope.businesscategory_list = resp.data.businesscategory_list;
            });

            var url = 'api/AgrMstApplication360/AssessmentAgencyList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });

            var url = 'api/AgrMstApplication360/AssessmentAgencyRatingList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });

            var url = 'api/AgrMstApplication360/GetDesignationList';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });

            var url = 'api/AgrMstApplication360/CompanyOnboardDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.companydocument_list = resp.data.companydocument_list;
            });

            var url = 'api/AgrMstApplication360/licensetypeList';
            SocketService.get(url).then(function (resp) {
                $scope.licensetype_list = resp.data.licensetype_list;
            });


            $scope.bankaccvalidation = null;
            $scope.ifscvalidation = '';

        }


        $scope.GetBuyerindividual = function () {
            $scope.AlreadyaddedIndividualaadhar = false;
            $scope.AlreadyaddedIndividualpan = false;
            var params = {
                application_gid: $scope.application_gid
            }

            var url = 'api/AgrMstSupplierOnboard/GetCompanyList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionlist = resp.data.institutionlist;
            });
            // For Edit Page - No need to call 
            if ($scope.editmdtab == true) {
            }
            else {
                var url = 'api/AgrMstSupplierOnboard/PANAbsenceReasonList';
                SocketService.get(url).then(function (resp) {
                    $scope.panabsencereason_list = resp.data.BuyerOnboardpanabsencereason_list;
                });
            }
            
            var url = 'api/AgrMstApplication360/GetMaritalStatusList';
            SocketService.get(url).then(function (resp) {
                $scope.maritalstatus_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/EducationalQualificationList';
            SocketService.get(url).then(function (resp) {
                $scope.educationalqualification_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/GetDesignationList';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
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

            var url = 'api/AgrMstApplication360/GenderList';
            SocketService.get(url).then(function (resp) {
                $scope.gender_list = resp.data.application_list;
            });

            //var url = 'api/AgrMstApplication360/GetUserTypeList';
            //SocketService.get(url).then(function (resp) {
            //    $scope.usertype_list = resp.data.usertype_list;
            //});

            var url = 'api/AgrMstApplication360/GetUserTypeNoApplicantList';
            SocketService.get(url).then(function (resp) {
                $scope.usertypenoapplicant_list = resp.data.usertype_list;
            });

            var url = 'api/AgrMstApplication360/ResidenceTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.residencetype_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/IndividualOnboardDocumentList';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.individualdocument_list = resp.data.application_list;
            });

            var url = 'api/AgrMstApplication360/GetPropertyinNameList';
            SocketService.get(url).then(function (resp) {
                $scope.propertyin_list = resp.data.application_list;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });
        }


        $scope.productgen_change = function (cboproduct_name) {
            var params = {
                product_gid: cboproduct_name.product_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetSectorcategory';
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
                variety_gid: cbovariety_name.variety_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetVarietyDtl';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.product_gid = resp.data.product_gid;
                $scope.variety_gid = resp.data.variety_gid;
                $scope.variety_name = resp.data.variety_name;
                $scope.txtbotanical_name = resp.data.botanical_name;
                $scope.txtalternative_name = resp.data.alternative_name;
                $scope.txthsn_code = resp.data.hsn_code;
            });
        }

        $scope.OngenchangeVertical = function (cbovertical) {
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

        $scope.productgendtl_add = function () {
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
                var url = 'api/AgrMstSupplierOnboard/PostProductDetailAdd';
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

            var url = 'api/AgrMstSupplierOnboard/GetProductDetailList';
            SocketService.get(url).then(function (resp) {
                $scope.mstproduct_list = resp.data.mstBuyerOnboardproduct_list;
            });
        }

        $scope.product_delete = function (application2product_gid) {
            var params =
                {
                    application2product_gid: application2product_gid
                }
            var url = 'api/AgrMstSupplierOnboard/DeleteProductDetail';
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

        $scope.generalmobileno_add = function () {
            if (($scope.txtgeneralmobileno == undefined) || ($scope.txtgeneralmobileno == '') || ($scope.rdbprimarymobile_no == undefined) || ($scope.rdbprimarywhatsapp_no == undefined) || ($scope.rdbprimarymobile_no == '') || ($scope.rdbprimarywhatsapp_no == '')) {
                Notify.alert('Enter Mobile Number / Select Primary Status', 'warning');
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
                var url = 'api/AgrMstSupplierOnboard/PostMobileNo';
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
                    $scope.rdbprimarymobile_no = ''
                    $scope.rdbprimarymobile_no == false;
                    $scope.rdbprimarywhatsapp_no = '';
                    generalmobilenolist();
                });
            }
        }

        function generalmobilenolist() {

            var url = 'api/AgrMstSupplierOnboard/GetAppMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.generalmobileno_list = resp.data.mstBuyerOnboardmobileno_list;
            });
        }

        $scope.generalmobileno_delete = function (application2contact_gid) {
            var params =
                {
                    application2contact_gid: application2contact_gid
                }
            var url = 'api/AgrMstSupplierOnboard/DeleteMobileNo';
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
                Notify.alert('Enter Mail ID / Select Primary Status', 'warning');
            }
            else {
                var params = {
                    email_address: $scope.txtgeneralmail_id,
                    primary_emailaddress: $scope.rdbgeneralmaildetails
                }
                var url = 'api/AgrMstSupplierOnboard/PostEmailAddress';
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
                    $scope.rdbgeneralmaildetails = '';
                    generalmail_list();
                });
            }
        }

        function generalmail_list() {
            var url = 'api/AgrMstSupplierOnboard/GetAppEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.generalmaildetails_list = resp.data.mstBuyerOnboardemailaddress_list;
            });
        }

        $scope.generalmail_delete = function (application2email_gid) {
            var params =
                {
                    application2email_gid: application2email_gid
                }
            var url = 'api/AgrMstSupplierOnboard/DeleteEmailAddress';
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
                Notify.alert('Select Genetic Code / Select Status / Enter Genetic Code Remarks', 'warning');
            }
            else {
                var params = {
                    geneticcode_gid: $scope.cboGeneticCode.geneticcode_gid,
                    geneticcode_name: $scope.cboGeneticCode.geneticcode_name,
                    genetic_status: $scope.rdbStatus,
                    genetic_remarks: $scope.txtgenetic_remarks
                }
                var url = 'api/AgrMstSupplierOnboard/PostGeneticCode';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.mstgeneticcode_list = resp.data.mstBuyerOnboardgeneticcode_list;
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
            var url = 'api/AgrMstSupplierOnboard/DeleteGenetic';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.mstgeneticcode_list = resp.data.mstBuyerOnboardgeneticcode_list;
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

            if (($scope.cboVernacularLanguage == undefined) || ($scope.cboVernacularLanguage == '') || ($scope.cboVernacularLanguage == null)) {

                Notify.alert('Select Vernacular Language ', 'warning');
            }
            else if (($scope.rdbassociate == 'Yes') && (($scope.cbosa_idname == undefined) || ($scope.cbosa_idname == '') || ($scope.cbosa_idname == null))) {
                Notify.alert('Kindly Add SAM Associate ID / Name  ', 'warning');
            }
            else if ($scope.mstproduct_list == null || $scope.mstproduct_list == undefined || $scope.mstproduct_list == "") {
                Notify.alert('Atleast One Record should be added in Product Details', 'warning')
            }
            else {
                var lsvertical_gid = '';
                var lsvertical_name = '';
                var lsconstitution_gid = '';
                var lsconstitution_name = '';
                var lsbusinessunit_gid = '';
                var lsbusinessunit_name = '';
                var lsname = '';
                var lsassociatemaster_gid = '';
                var lsdesignation_gid = '';
                var lsdesignation_type = '';
                var lsprogram_name = '';
                var lsprogram_gid = '';
                var lsproduct_name = '';
                var lsproduct_gid = '';
                var lsvariety_name = '';
                var lsvariety_gid = '';
                var lsbuyersuppliertype_gid = '';
                var lsbuyersuppliertype_name = '';

                if ($scope.cbovertical != undefined || $scope.cbovertical != null) {
                    lsvertical_gid = $scope.cbovertical.vertical_gid;
                    lsvertical_name = $scope.cbovertical.vertical_name;
                }

                if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                    lsconstitution_gid = $scope.cboConstitution;
                    var getconstitutionname = $scope.constitutionlist.find(function (v) { return v.constitution_gid === $scope.cboConstitution });
                    if (getconstitutionname != null && getconstitutionname != "")
                        lsconstitution_name = getconstitutionname.constitution_name;

                }

                if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                    lsdesignation_gid = $scope.cboDesignation;
                    var getdesginationname = $scope.designation_list.find(function (v) { return v.designation_gid === $scope.cboDesignation });
                    if (getdesginationname != null && getdesginationname != "")
                        lsdesignation_type = getdesginationname.designation_type;
                }
                if ($scope.cbosa_idname != undefined || $scope.cbosa_idname != null) {

                    lsassociatemaster_gid = $scope.cbosa_idname;
                    var getassociatemastername = $scope.associatemaster_list.find(function (v) { return v.designation_gid === $scope.cbosa_idname });
                    if (getassociatemastername != null && getassociatemastername != "")
                        lsname = getassociatemastername.name;
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
                if ($scope.suppliertypeadd != undefined || $scope.suppliertypeadd != null) {
                    lsbuyersuppliertype_gid = $scope.suppliertypeadd.buyersuppliertype_gid;
                    lsbuyersuppliertype_name = $scope.suppliertypeadd.buyersuppliertype_name;
                }
                var params = {
                    customer_urn: $scope.txtcustomer_URN,
                    customer_name: $scope.txtcustomer_name,
                    vertical_gid: lsvertical_gid,
                    vertical_name: lsvertical_name,
                    constitution_gid: lsconstitution_gid,
                    constitution_name: lsconstitution_name,
                    sa_status: $scope.rdbassociate,
                    saname_gid: lsassociatemaster_gid,
                    sa_name: lsname,
                    vernacularlanguage_list: $scope.cboVernacularLanguage,
                    contactpersonfirst_name: $scope.txtfirst_name,
                    contactpersonmiddle_name: $scope.txtmiddle_name,
                    contactpersonlast_name: $scope.txtlast_name,
                    designation_gid: lsdesignation_gid,
                    designation_type: lsdesignation_type,
                    landline_no: $scope.txtlandline_no,
                    //program_gid: lsprogram_gid,
                    //program_name: lsprogram_name,
                    product_gid: lsproduct_gid,
                    product_name: lsproduct_name,
                    variety_gid: lsvariety_gid,
                    variety_name: lsvariety_name,
                    sector_name: $scope.txtsector_name,
                    category_name: $scope.txtcategory_name,
                    botanical_name: $scope.txtbotanical_name,
                    alternative_name: $scope.txtalternative_name,
                    buyersuppliertype_gid: lsbuyersuppliertype_gid,
                    buyersuppliertype_name: lsbuyersuppliertype_name,

                }
                var url = 'api/AgrMstSupplierOnboard/SubmitGeneralDtl';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        $scope.application_gid = resp.data.application_gid;
                        Notify.alert('General Information Submitted Successfully', {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        window.scrollTo(0, 0);
                        if ($scope.application_gid != null) {
                            $scope.gendev = false;
                            $scope.gendtldev = true;
                            $scope.indivdiv = true;
                            $scope.instdiv = true;
                            GeneralInfo_list();
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

        function GeneralInfo_list() {
            var url = 'api/AgrMstSupplierOnboard/GetGeneralInfo';
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

                if ($scope.application_gid != null) {
                    $scope.gendev = false;
                    $scope.gendtldev = true;
                    $scope.indivdiv = true;
                    $scope.instdiv = true;
                }
            });
        }

        $scope.onselectedurn_yes = function () {
            if ($scope.rdbinsturn_status == 'Yes') {
                $scope.URN_yes = true;
            }
            else {
                $scope.URN_yes = false;
                $scope.txt_urn = '';
            }
        }

        $scope.onselectedediturn_yes = function () {
            if ($scope.rdbeditinsturn_status == 'Yes') {
                $scope.URN_yes = true;
            }
            else {
                $scope.URN_yes = false;
                $scope.txteditinst_urn = '';
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

        $scope.onchangeeditbusinessstartdate = function () {
            var params = {
                businessstart_date: $scope.txteditbusinessstart_date
            }
            var url = 'api/AgrMstbuyer/GetYearsAndMonthsInBusiness';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtedityearin_business = resp.data.year_business;
                $scope.txteditmonthsin_business = resp.data.month_business;
            });
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

        $scope.getTANAdd = function () {
            if ($scope.txttan_number.length == 10 || txttan_number == '' || txttan_number == undefined || txttan_number == null) {

                var params = {
                    tan_no: $scope.txttan_number
                }

                var url = 'api/AgrSuprKyc/TANCompanyauthetication';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.tanvalidation = true;
                        Notify.alert('TAN is Verified..!', 'success');

                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.tanvalidation = false;
                        Notify.alert('TAN is not verified..!', 'warning');

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });

            } else {
                Notify.alert(resp.data.message, 'warning')
            }
        }

        $scope.getTAN = function () {
            if ($scope.txtedittan_number.length == 10 || txtedittan_number == '' || txtedittan_number == undefined || txtedittan_number == null) {

                var params = {
                    tan_no: $scope.txtedittan_number
                }

                var url = 'api/AgrSuprKyc/TANCompanyauthetication';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.tanvalidation = true;
                        Notify.alert('TAN is Verified..!', 'success');

                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.tanvalidation = false;
                        Notify.alert('TAN is not verified..!', 'warning');

                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });

            } else {
                Notify.alert(resp.data.message, 'warning')
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

        //supplierpanissue 
        
        $scope.getPANbasedGST = function (institution_gid) {
            if ($scope.txtpan_number.length == 10) {
                $scope.Alreadyaddedpanaadhar = false;
                if ($scope.institutiongst_list != null) {
                    var paramsdel =
                    {
                        institution_gid: institution_gid
                    }
                    var url = 'api/AgrMstSupplierOnboard/DeleteGSTInstitution';
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
                var url = 'api/AgrSuprKyc/GSTSBPAN';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.statusCode == 101) {
                        $scope.panvalidation = true;
                        const GstArray = resp.data.result;

                        var param = {
                            pan: $scope.txtpan_number
                        }
                        var url = 'api/AgrSuprKyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.txtinstcompany_name = resp.data.result.name;
                            } else {
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });


                        var params = {
                            GSTArray: GstArray
                        }

                        var url = 'api/AgrMstSupplierOnboard/PostInstitutionGSTList';
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
                        var url = 'api/AgrSuprKyc/PANNumber';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                            unlockUI();
                            if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                $scope.txtinstcompany_name = resp.data.result.name;
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
                    var params = {
                        pan_no: $scope.txtpan_number,
                        aadhar_no: 'No',
                        institution_gid: institution_gid,
                        onboard_gid: $scope.application_gid,
						stakeholder_type: $scope.cboStakeholdertype.user_type
                    }
                    var url = 'api/AgrMstSupplierOnboard/GetOnboardValidatePANAadhar';
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
                else
                    $scope.Alreadyaddedpanaadhar = false;
            }
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


        function creditbankacc_list() {
            var url = 'api/AgrMstSupplierOnboard/GetInstitutionBankAccDtl';
            SocketService.get(url).then(function (resp) {
                $scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;

            });
        }

        $scope.creditbankacctdtl_add = function (institution2bankdtl_gid) {
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

                var url = 'api/AgrMstSupplierOnboard/EditGetCreditBankAccDtl';

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
                        var url = 'api/AgrSuprKyc/IfscVerification';
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
                    if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') ||
                        ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
                        ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                        ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') ||  ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
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
                                    primary_status: $scope.rdbprimarystatus,

                                }
                                var url = 'api/AgrMstSupplierOnboard/UpdateInstitutionBankAccDtl';
                                lockUI();
                                SocketService.post(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {
                                        Notify.alert(resp.data.message, {
                                            status: 'success',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                        creditbankacc_list();
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
                                primary_status: $scope.rdbprimarystatus,

                            }
                            var url = 'api/AgrMstSupplierOnboard/UpdateInstitutionBankAccDtl';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    creditbankacc_list();

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

                var url = 'api/AgrMstSupplierOnboard/EditGetIndividualBankAccDtl';

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
                        var url = 'api/AgrSuprKyc/IfscVerification';
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
                    if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') ||
                        ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
                        ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                        ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.rdbprimarystatus == undefined) || ($scope.rdbprimarystatus == '')  || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
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
                                    primary_status: $scope.rdbprimarystatus,

                                }
                                var url = 'api/AgrMstSupplierOnboard/UpdateIndividualBankAccDtl';
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
                                primary_status: $scope.rdbprimarystatus,

                            }
                            var url = 'api/AgrMstSupplierOnboard/UpdateIndividualBankAccDtl';
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


        $scope.add_indbankacctdtl = function () {

            if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') ||
                ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
            ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.rdbprimarystatus == undefined) || ($scope.rdbprimarystatus == '')  || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                if ($scope.rdbJoint_Account == 'Yes') {

                    if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                        Notify.alert('Enter Joint Account Holder Name', 'warning');
                    }
                    else {
                        var params = {
                            //application_gid: $scope.application_gid,
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
                        var url = 'api/AgrMstSupplierOnboard/PostIndividualBank';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
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
                            //activate();
                            indbankacc_list();
                        });
                    }
                }
                else {
                    var params = {
                        //application_gid: application_gid,
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
                    var url = 'api/AgrMstSupplierOnboard/PostIndividualBank';
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

        //what if I 
        function indbankacc_list() {
            var url = 'api/AgrMstSupplierOnboard/GetIndividualBankAccDtl';
            SocketService.get(url).then(function (resp) {
                $scope.creditbankacc_list = resp.data.BuyerOnboardIndividual2bankacc_list;

            });
        }


        $scope.indbankacctdtl_delete = function (contact2bankdtl_gid, contact_gid) {
            var params = {
                contact2bankdtl_gid: contact2bankdtl_gid,
                contact_gid: contact_gid
            }
            var url = 'api/AgrMstSupplierOnboard/DeleteIndividualBankAcc';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    //$scope.creditbankacc_list = resp.data.BuyerOnboardIndividual2bankacc_list;
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

        //$scope.indeditbankacctdtl_delete = function (contact2bankdtl_gid, contact_gid) {
        //    var params = {
        //        contact2bankdtl_gid: contact2bankdtl_gid,
        //        contact_gid: contact_gid
        //    }
        //    var url = 'api/AgrMstSupplierOnboard/DeleteIndividualBankAcc';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        unlockUI();
        //        if (resp.data.status == true) {
        //            $scope.creditbankacc_list = resp.data.BuyerOnboardIndividual2bankacc_list;
        //            Notify.alert(resp.data.message, {
        //                status: 'success',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            indbankacctmp_list();

        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //        }


        //    });
        //}



        function license_list() {
            var url = 'api/AgrMstSupplierOnboard/GetInstitutionLicenseList';
            SocketService.get(url).then(function (resp) {
                $scope.institutionlicense_list = resp.data.mstBuyerOnboardlicense_list;

            });
        }

        function institutiongstlist() {
            var url = 'api/AgrMstSupplierOnboard/GetInstitutionGSTList';
            SocketService.get(url).then(function (resp) {
                $scope.institutiongst_list = resp.data.mstBuyerOnboardgst_list;
            });
        }

        function institutionmobilenolist() {
            var url = 'api/AgrMstSupplierOnboard/GetInstitutionMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.institutionmobileno_list = resp.data.mstBuyerOnboardmobileno_list;
            });
        }

        function institutionmail_list() {
            var url = 'api/AgrMstSupplierOnboard/GetInstitutionEmailAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.institutionmaildetails_list = resp.data.mstBuyerOnboardemailaddress_list;
            });
        }

        function institutionaddress_list() {
            var url = 'api/AgrMstSupplierOnboard/GetInstitutionAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.institutionaddresslist = resp.data.mstBuyerOnboardaddress_list;
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
                var url = 'api/AgrMstSupplierOnboard/PostInstitutionGST';
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
            var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionGST';
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

        $scope.verify_gstverification = function (gst_no, institution2branch_gid) {
            $scope.gst_no = gst_no;
            var params = {
                gstin: gst_no,
                institution2branch_gid: institution2branch_gid,
                application_gid: $scope.application_gid
            }
            lockUI();

            var url = 'api/AgrSuprKyc/GSTVerification';
            SocketService.post(url, params).then(function (resp) {

                unlockUI();
                //list();
                if (resp.data.result.gstin != "" && resp.data.result.gstin != undefined) {
                    $scope.gstverification = true;
                }
                else if (resp.data.result.gstin == "" || resp.data.result.gstin == undefined) {
                    $scope.gstverification = false;
                    $scope.gstverify_status = 'notverify';
                } else {
                    Notify.alert(resp.data.message, 'warning')
                }

                institutiongstlist();
            });
        }


        $scope.gstaddressview = function (institution2branch_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/gstaddressview.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    institution2branch_gid: institution2branch_gid,
                }

                var url = 'api/AgrMstSuprAPIVerifications/GSTVerificationViewDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();

                    $scope.gstin = resp.data.result.gstin;


                    $scope.legalname = resp.data.result.lgnm;
                    $scope.tradename = resp.data.result.tradeNam;
                    $scope.status = resp.data.result.sts;
                    $scope.constitutionofbusiness = resp.data.result.ctb;

                    var nba = resp.data.result.nba;
                    var natureofbusiness = "";
                    for (var i = 0; i < nba.length; i++) {
                        natureofbusiness = natureofbusiness.concat(nba[i], ",");
                    }
                    natureofbusiness = natureofbusiness.replace(/,\s*$/, "");
                    $scope.nob = natureofbusiness;

                    $scope.taxpayertype = resp.data.result.dty;
                    $scope.compliancerating = resp.data.result.cmpRt;

                    $scope.centraljusridiction = resp.data.result.ctj;
                    $scope.centraljusridiction_code = resp.data.result.ctjCd;
                    $scope.statejurisdiction = resp.data.result.stj;
                    $scope.statejurisdiction_code = resp.data.result.stjCd;



                    $scope.dateofregistration = resp.data.result.rgdt;
                    $scope.dateofcancellation = resp.data.result.cxdt;
                    $scope.lastupdateddate = resp.data.result.lstupdt;

                    $scope.contact_mobnum = resp.data.result.contacted.mobNum;
                    $scope.contact_email = resp.data.result.contacted.email;
                    $scope.contact_name = resp.data.result.contacted.name;

                    $scope.contactdetail = "defined";
                    if ($scope.contact_mobnum == null && $scope.contact_email == null && $scope.contact_name == null) {
                        $scope.contactdetail = null;
                    }




                    $scope.pradr_adr = resp.data.result.pradr.adr;
                    $scope.pradr_ntr = resp.data.result.pradr.ntr;
                    $scope.pradr_em = resp.data.result.pradr.em;
                    $scope.pradr_mb = resp.data.result.pradr.mb;
                    $scope.pradr_lastUpdatedDate = resp.data.result.pradr.lastUpdatedDate;


                    $scope.adadr_list = resp.data.result.adadr;

                    $scope.additionaladdress = "defined";
                    if ($scope.adadr_list.length == 0) {
                        $scope.additionaladdress = null;
                    }

                });



                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }


        function institutionratinglist() {
            var params = {
                institution_gid: $scope.institution_gid,
                tmp_status: true
            }
            var url = 'api/AgrMstSupplierOnboard/GetInstitutionRatingList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionratinglist = resp.data.MdlBuyerOnboardRatingdtl;
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
            var url = 'api/AgrMstSupplierOnboard/PostRatingdtl';
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

        $scope.ratingdtl_delete = function (institution2ratingdetail_gid) {
            var params = {
                institution2ratingdetail_gid: institution2ratingdetail_gid
            }
            var url = 'api/AgrMstSupplierOnboard/DeleteRatingDtl';
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
                var url = 'api/AgrMstSupplierOnboard/PostInstitutionMobileNo';
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
            var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionMobileNo';
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
                var url = 'api/AgrMstSupplierOnboard/PostInstitutionEmailAddress';
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
            var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionEmailAddress';
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
                    var url = 'api/AgrMstSupplierOnboard/PostInstitutionAddressDetail';
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
            var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionAddressDetail';
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

        // Institution License Add

        $scope.institutionlicenseadd = function () {

            if (($scope.cboLicenseType == '') || ($scope.cboLicenseType == undefined) || ($scope.txtnumber == '') || ($scope.txtnumber == undefined) || ($scope.txtissue_date == '') || ($scope.txtissue_date == undefined) || ($scope.txtexpiry_date == '') || ($scope.txtexpiry_date == undefined)) {
                Notify.alert('Kindly Fill All Mandatory Fields', 'warning');
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
                    licenseexpiry_date: $scope.txtexpiry_date

                }
                var url = 'api/AgrMstSupplierOnboard/PostInstitutionLicenseDetail';
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
            var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionLicenseDetail';
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


        $scope.InstitutionDocumentUpload = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbocompanydocumentname == null) || ($scope.cbocompanydocumentname == '') || ($scope.cbocompanydocumentname == undefined)) {
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
                frm.append('document_id', $scope.txtdocument_id);
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    lockUI();
                    var url = 'api/AgrMstSupplierOnboard/InstitutionDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $scope.institutionupload_list = resp.data.BuyerOnboardinstitutionupload_list;
                        unlockUI();

                        $("#institutionfile").val('');
                        $scope.cbocompanydocumentname = "";
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

        $scope.institutiondocument_delete = function (institution2documentupload_gid) {
            lockUI();
            var params = {
                institution2documentupload_gid: institution2documentupload_gid
            }
            var url = 'api/AgrMstSupplierOnboard/InstitutionDocumentDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionupload_list = resp.data.BuyerOnboardinstitutionupload_list;
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
                    var url = 'api/AgrMstSupplierOnboard/UpdateGSTHeadOffice';
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


        $scope.IFSCValidation = function () {

            if ($scope.txtIFSC_Code.length == 11) {
                var params = {
                    ifsc: $scope.txtIFSC_Code
                }
                lockUI();
                var url = 'api/AgrSuprKyc/IfscVerification';
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
        $scope.change = function () {
            $scope.txtjointacctholder_name = '';
        }
        $scope.BankAccValidation = function () {
            if ($scope.txtbankacct_no == $scope.txtconfirmbankacct_no) {
                var params = {
                    ifsc: $scope.txtIFSC_Code,
                    accountNumber: $scope.txtconfirmbankacct_no
                }
                var url = 'api/AgrSuprKyc/BankAccVerification';
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

        $scope.add_creditbankacctdtl = function () {

            if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') ||
                ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
            ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                if ($scope.rdbJoint_Account == 'Yes') {

                    if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                        Notify.alert('Enter Joint Account Holder Name', 'warning');
                    }
                    else {
                        var params = {
                            //application_gid: $scope.application_gid,
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
                        var url = 'api/AgrMstSupplierOnboard/PostInstitutionBank';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                //$scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;
                                $scope.credituploaddocument_list = null;
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                creditbankacc_list();
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
                            //activate();
                            creditbankacc_list();
                        });
                    }
                }
                else {
                    var params = {
                        //application_gid: application_gid,
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
                    var url = 'api/AgrMstSupplierOnboard/PostInstitutionBank';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            //$scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            creditbankacc_list();
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
            var url = 'api/AgrMstSupplierOnboard/DeleteinstitutionBankAcc';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    creditbankacc_list();
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

        $scope.creditbankacctdtl_delete = function (institution2bankdtl_gid, institution_gid) {
            var params = {
                institution2bankdtl_gid: institution2bankdtl_gid,
                institution_gid: institution_gid
            }
            var url = 'api/AgrMstSupplierOnboard/DeleteinstitutionBankAcc';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    creditbankacc_list();
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


     


        $scope.submit_institution = function () {

            license_list();

            try {
                if ($scope.txtleirenewal_date.split("-"))
                    $scope.txtleirenewal_date = $scope.txtleirenewal_date.split("-").reverse().join("-");
            }
            catch (e) { $scope.txtleirenewal_date = $scope.txtleirenewal_date  }

            //if ($scope.rdbinsturn_status == 'Yes' && ($scope.txtinst_urn == '' || $scope.txtinst_urn == undefined || $scope.txtinst_urn == null)) {
            //    Notify.alert('Kindly Enter URN', 'warning')
            //}
            //else
                if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
                Notify.alert('Kindly select Stakeholder Type', 'warning')
            }
            else if ($scope.txtstart_date > $scope.txtend_date) {
                Notify.alert('Company End Date Is Less Then Start Date', 'warning');
            }
            else if ($scope.institutionlicense_list == '' || $scope.institutionlicense_list == undefined || $scope.institutionlicense_list == null) {
                Notify.alert('Add Atleast one License detail', 'warning');
                }
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
                if ($scope.cboinstDesignation != undefined || $scope.cboinstDesignation != null) {
                    lsdesignation_gid = $scope.cboinstDesignation.designation_gid;
                    lsdesignation_type = $scope.cboinstDesignation.designation_type;
                }
                    if ($scope.institutiongst_list != null) {
                        var Gstflag = 'Yes';
                    }
                    else if ($scope.institutiongst_list == null || $scope.institutiongst_list == '' || $scope.institutiongst_list == undefined) {
                        var Gstflag = 'No';
                    }
                var params = {
                    company_name: $scope.txtinstcompany_name,
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
                    contactperson_firstname: $scope.txtinstfirst_name,
                    contactperson_middlename: $scope.txtmiddle_name,
                    contactperson_lastname: $scope.txtlast_name,
                    designation_gid: lsdesignation_gid,
                    designation: lsdesignation_type,
                    start_date: $scope.txtstart_date,
                    end_date: $scope.txtend_date,
                    escrow: $scope.rdbescrow,
                    tan_number: $scope.txttan_number,
                    incometax_returnsstatus: $scope.rdbincome_tax,
                    revenue: $scope.txtrevenue,
                    profit: $scope.txtprofit,
                    fixed_assets: $scope.txtfixed_asset,
                    sundrydebt_adv: $scope.txtsundrydebt_adv,
                    lastyear_turnover: $scope.txtlastyear_turnover,
                    urn_status: $scope.rdbinsturn_status,
                    urn: $scope.txtinst_urn,
                    msme_registration:$scope.txtmsmereg,
                    lglentity_id:$scope.txtlei,
                    lei_renewaldate:$scope.txtleirenewal_date,
                    kin:$scope.txtkin,
                    application_gid: $scope.application_gid,
                    Gstflag: Gstflag
                }
                var url = 'api/AgrMstSupplierOnboardEdit/SubmitInstitutionDtlEdit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        overallgeneralsummary();
                        $scope.txtinstcompany_name = '';
                        $scope.txtincorporation_date = '';
                        $scope.txtbusinessstart_date = '';
                        $scope.txtyearin_business = '';
                        $scope.txtmonthsin_business = '';
                        $scope.txtpan_number = '';
                        $scope.txtcin_no = '';
                        $scope.txtofficialtelephone_number = '';
                        $scope.txtofficial_mailid = '';
                        $scope.txtratingason_date = '';
                        $scope.txtinstfirst_name = '';
                        $scope.txtmiddle_name = '';
                        $scope.txtlast_name = '';
                        $scope.txtstart_date = '';
                        $scope.txtend_date = '';
                        $scope.rdbescrow = '';
                        $scope.txttan_number = '';
                        $scope.rdbincome_tax = '';
                        $scope.txtrevenue = '';
                        $scope.txtprofit = '';
                        $scope.txtfixed_asset = '';
                        $scope.txtsundrydebt_adv = '';
                        $scope.txtlastyear_turnover = '';
                        $scope.rdbinsturn_status = '';
                        $scope.txtinst_urn = '';
                        $scope.cboeditCompanytype = '';
                        $scope.cboeditStakeholdertype = '';
                        $scope.cboeditCreditAssessmentagency = '';
                        $scope.cboeditAssessmentRating = '';
                        $scope.cboeditAMLCategory = '';
                        $scope.cboeditBusinesscategory = '';
                        $scope.cboeditinstDesignation = '';
                        $scope.institutiongst_list = null;
                        $scope.institutionratinglist = null;
                        $scope.institutionmobileno_list = null;
                        $scope.institutionmaildetails_list = null;
                        $scope.institutionaddresslist = null;
                        $scope.creditbankacc_list = null;
                        $scope.institutionupload_list = null;
                        $scope.institutionlicense_list = null;


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


        $scope.save_institution = function () {

            license_list();
            try {
                if ($scope.txtleirenewal_date.split("-"))
                    $scope.txtleirenewal_date = $scope.txtleirenewal_date.split("-").reverse().join("-");
            }
            catch (e) { $scope.txtleirenewal_date = $scope.txtleirenewal_date  }
            //if ($scope.rdbinsturn_status == 'Yes' && ($scope.txtinst_urn == '' || $scope.txtinst_urn == undefined || $scope.txtinst_urn == null)) {
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
                if ($scope.cboinstDesignation != undefined || $scope.cboinstDesignation != null) {
                    lsdesignation_gid = $scope.cboinstDesignation.designation_gid;
                    lsdesignation_type = $scope.cboinstDesignation.designation_type;
                }

                var params = {
                    company_name: $scope.txtinstcompany_name,
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
                    contactperson_firstname: $scope.txtinstfirst_name,
                    contactperson_middlename: $scope.txtmiddle_name,
                    contactperson_lastname: $scope.txtlast_name,
                    designation_gid: lsdesignation_gid,
                    designation: lsdesignation_type,
                    start_date: $scope.txtstart_date,
                    end_date: $scope.txtend_date,
                    escrow: $scope.rdbescrow,
                    tan_number: $scope.txttan_number,
                    incometax_returnsstatus: $scope.rdbincome_tax,
                    revenue: $scope.txtrevenue,
                    profit: $scope.txtprofit,
                    fixed_assets: $scope.txtfixed_asset,
                    sundrydebt_adv: $scope.txtsundrydebt_adv,
                    lastyear_turnover: $scope.txtlastyear_turnover,
                    urn_status: $scope.rdbinsturn_status,
                    urn: $scope.txtinst_urn,
                    msme_registration:$scope.txtmsmereg,
                    lglentity_id:$scope.txtlei,
                    lei_renewaldate:$scope.txtleirenewal_date,
                    kin:$scope.txtkin,
                    application_gid: $scope.application_gid,
                }

                var url = 'api/AgrMstSupplierOnboardEdit/SaveInstitutionDtl';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        overallgeneralsummary();
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

        $scope.change_pan = function (cbopanstatus) {
            if ($scope.cbopanstatus == 'Customer Submitting PAN') {
                $scope.havepan = true;
                $scope.havenotpan = false;
                angular.forEach($scope.panabsencereason_list, function (val) {
                    val.checked = false;
                });
                var url = 'api/AgrMstSupplierOnboard/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.BuyerOnboardcontactpanform60_list;
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
            if ($scope.panabsencereason_list != null) {
                angular.forEach($scope.panabsencereason_list, function (val) {

                    if (val.checked == true) {
                        var panabsencereason = val.panabsencereason;
                        panabsencereason_selectedList.push(panabsencereason);
                    }

                });
            }
            var params = {
                panabsencereason_selectedlist: panabsencereason_selectedList,
            }
            var url = 'api/AgrMstSupplierOnboard/PostPANAbsenceReasons';
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
                var url = 'api/AgrMstSupplierOnboard/PANForm60DocumentUpload';
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

                    var url = 'api/AgrMstSupplierOnboard/GetPANForm60List';
                    SocketService.get(url).then(function (resp) {
                        $scope.contactpanform60_list = resp.data.BuyerOnboardcontactpanform60_list;
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
            var url = 'api/AgrMstSupplierOnboard/PANForm60Delete';
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
                var url = 'api/AgrMstSupplierOnboard/GetPANForm60List';
                SocketService.get(url).then(function (resp) {
                    $scope.contactpanform60_list = resp.data.BuyerOnboardcontactpanform60_list;
                });
                unlockUI();
            });
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
                var url = 'api/AgrMstSupplierOnboard/MobileNumberAdd';
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
                    var url = 'api/AgrMstSupplierOnboard/GetMobileNoList';
                    SocketService.get(url).then(function (resp) {
                        $scope.contactmobileno_list = resp.data.BuyerOnboardcontactmobileno_list;

                    });
                    $scope.txtmobile_no = '';
                    $scope.rdbprimary_no = '';
                    $scope.rdbwhatsapp_no = '';
                });

            }

        }

        $scope.Indivmobileno_delete = function (contact2mobileno_gid) {
            var params =
                {
                    contact2mobileno_gid: contact2mobileno_gid
                }
            var url = 'api/AgrMstSupplierOnboard/MobileNoDelete';
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
                var url = 'api/AgrMstSupplierOnboard/GetMobileNoList';
                SocketService.get(url).then(function (resp) {
                    $scope.contactmobileno_list = resp.data.BuyerOnboardcontactmobileno_list;

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
                var url = 'api/AgrMstSupplierOnboard/EmailAddressAdd';
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
                    var url = 'api/AgrMstSupplierOnboard/GetEmailAddressList';
                    SocketService.get(url).then(function (resp) {
                        $scope.contactemail_list = resp.data.BuyerOnboardcontactemail_list;

                    });
                    $scope.txtemail_address = '';
                    $scope.rdbprimary_email = '';
                });

            }

        }

        $scope.Indivemailaddress_delete = function (contact2email_gid) {
            var params =
                {
                    contact2email_gid: contact2email_gid
                }
            var url = 'api/AgrMstSupplierOnboard/EmailAddressDelete';
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
                var url = 'api/AgrMstSupplierOnboard/GetEmailAddressList';
                SocketService.get(url).then(function (resp) {
                    $scope.contactemail_list = resp.data.BuyerOnboardcontactemail_list;

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
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/AgrMstSupplierOnboard/IndividualProofDocumentUpload';
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

                        var url = 'api/AgrMstSupplierOnboard/GetIndividualProofList';
                        SocketService.get(url).then(function (resp) {
                            $scope.contactidproof_list = resp.data.contactBuyerOnboardidproof_list;
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
            var url = 'api/AgrMstSupplierOnboard/IndividualProofDelete';
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
                var url = 'api/AgrMstSupplierOnboard/GetIndividualProofList';
                SocketService.get(url).then(function (resp) {
                    $scope.contactidproof_list = resp.data.contactBuyerOnboardidproof_list;
                });
                unlockUI();
            });
        }

        function individualaddress_list() {
            var url = 'api/AgrMstSupplierOnboard/GetAddressList';
            SocketService.get(url).then(function (resp) {
                $scope.contactindividualaddress_list = resp.data.BuyerOnboardcontactaddress_list;
            });
        }

        $scope.address_add = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addresstype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg',

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
                    var url = 'api/AgrMstSupplierOnboard/AddressAdd';
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
            var url = 'api/AgrMstSupplierOnboard/AddressDelete';
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
                frm.append('project_flag', "documentformatonly");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/AgrMstSupplierOnboard/IndividualDocumentUpload';
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

                        var url = 'api/AgrMstSupplierOnboard/GetIndividualDocList';
                        SocketService.get(url).then(function (resp) {
                            $scope.uploadindividualdoc_list = resp.data.uploadBuyerOnboardindividualdoc_list;
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
            var url = 'api/AgrMstSupplierOnboard/IndividualDocDelete';
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
                var url = 'api/AgrMstSupplierOnboard/GetIndividualDocList';
                SocketService.get(url).then(function (resp) {
                    $scope.uploadindividualdoc_list = resp.data.uploadBuyerOnboardindividualdoc_list;
                });
                unlockUI();
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

        $scope.onselectedindurn_yes = function () {
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

        $scope.onchangeddobage_individual = function () {
            var params = {
                age: $scope.txtage
            }
            var url = 'api/AgrMstApplicationAdd/GetDOB';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtindividual_dob = Date.parse(resp.data.dob);
            });
        }

        $scope.onchangeddobage_father = function () {
            var params = {
                age: $scope.txtfather_age
            }
            var url = 'api/AgrMstApplicationAdd/GetDOB';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtfather_dob = Date.parse(resp.data.dob);
            });
        }

        $scope.onchangeddobage_mother = function () {
            var params = {
                age: $scope.txtmother_age
            }
            var url = 'api/AgrMstApplicationAdd/GetDOB';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtmother_dob = Date.parse(resp.data.dob);
            });
        }

        $scope.onchangeddobage_spouse = function () {
            var params = {
                age: $scope.txtspouse_age
            }
            var url = 'api/AgrMstApplicationAdd/GetDOB';

            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtspouse_dob = Date.parse(resp.data.dob);
            });
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

        

        $scope.PANValidation = function () {
            if ($scope.txtpan_no.length == 10) {
                $scope.AlreadyaddedIndividualpanaadhar = false;
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/AgrSuprKyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.panvalidation = true;
                        var parts = resp.data.result.name.split(" ");
                        if (parts.length == 3) {
                            $scope.txtindfirst_name = parts[0];
                            $scope.txtindmiddle_name = parts[1];
                            $scope.txtindlast_name = parts[2];
                        } else {
                            $scope.txtindfirst_name = parts[0];
                            $scope.txtindlast_name = parts[1];
                        }
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.panvalidation = false;
                        Notify.alert('PAN is not verified..!', 'warning');
                        $scope.txtindfirst_name = '';
                        $scope.txtindmiddle_name = '';
                        $scope.txtindlast_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
            }
        }

        $scope.PANAddIndividualValidation = function (contact_gid) {
            if ($scope.txtpan_no != undefined && $scope.txtpan_no.length == 10) {
                $scope.AlreadyaddedIndividualpanaadhar = false;
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/AgrSuprKyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.panvalidation = true;
                        var parts = resp.data.result.name.split(" ");
                        if (parts.length == 3) {
                            $scope.txtindfirst_name = parts[0];
                            $scope.txtindmiddle_name = parts[1];
                            $scope.txtindlast_name = parts[2];
                        } else {
                            $scope.txtindfirst_name = parts[0];
                            $scope.txtindlast_name = parts[1];
                        }
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.panvalidation = false;
                        Notify.alert('PAN is not verified..!', 'warning');
                        $scope.txtindfirst_name = '';
                        $scope.txtindmiddle_name = '';
                        $scope.txtindlast_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
               
            }

            if (($scope.cboindStakeholderType != undefined && $scope.cboindStakeholderType != null) || ($scope.cboindStakeholderType != undefined && $scope.cboindStakeholderType != null)) {
                var getStakeholderType = "";
                if ($scope.cboindStakeholderType != undefined && $scope.cboindStakeholderType != "") {
                    getStakeholderType = $scope.cboindStakeholderType;
                }
                else {
                    var getStakeholderType = $scope.usertypenoapplicant_list.find(function (v) {
                        return v.usertype_gid === $scope.cboindStakeholderType
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
                            contact_gid: contact_gid,
                            onboard_gid: $scope.application_gid,
                            stakeholder_type: getStakeholderType.user_type
                        }
                        var url = 'api/AgrMstSupplierOnboard/GetOnboardValidatePANAadhar';
                        SocketService.post(url, params).then(function (resp) {
                            $scope.lblcreated_by = resp.data.lscreatedby_name;
                            unlockUI();
                            if (resp.data.status == true) {
                                if (resp.data.panoraadhar == "PAN") {
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


        //supplier basic issue

        $scope.PANIndividualValidation = function (contact_gid) {
            if ($scope.txtpan_no !=undefined && $scope.txtpan_no.length == 10) {
                $scope.AlreadyaddedIndividualpanaadhar = false;
                var params = {
                    pan: $scope.txtpan_no
                }
                var url = 'api/AgrSuprKyc/PANNumber';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                        $scope.panvalidation = true;
                        var parts = resp.data.result.name.split(" ");
                        if (parts.length == 3) {
                            $scope.txtindfirst_name = parts[0];
                            $scope.txtindmiddle_name = parts[1];
                            $scope.txtindlast_name = parts[2];
                        } else {
                            $scope.txtindfirst_name = parts[0];
                            $scope.txtindlast_name = parts[1];
                        }
                    } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                        $scope.panvalidation = false;
                        Notify.alert('PAN is not verified..!', 'warning');
                        $scope.txtindfirst_name = '';
                        $scope.txtindmiddle_name = '';
                        $scope.txtindlast_name = '';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                });
 
            }

            if (($scope.cboStakeholderType != undefined && $scope.cboStakeholderType != null) || ($scope.cboStakeHolderType != undefined && $scope.cboStakeHolderType != null)) {
                var getStakeholderType = "";
                if ($scope.cboStakeHolderType != undefined && $scope.cboStakeHolderType != "") {
                    getStakeholderType = $scope.cboStakeHolderType;
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
                            contact_gid: contact_gid,
                            onboard_gid: $scope.application_gid,
                            stakeholder_type: getStakeholderType.user_type
                        }
                        var url = 'api/AgrMstSupplierOnboard/GetOnboardValidatePANAadhar';
                        SocketService.post(url, params).then(function (resp) {
                            $scope.lblcreated_by = resp.data.lscreatedby_name;
                            unlockUI();
                            if (resp.data.status == true) {
                                if (resp.data.panoraadhar == "PAN") {
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

    $scope.Drivingid_dob =  function (){

        if(($scope.cboIndividualProof.individualproof_name == "Driving License" ) && ($scope.txtidproof_dob != null || $scope.txtidproof_dob != '' || $scope.txtidproof_dob != undefined)){
            // Notify.alert('Kindly Enter Date of Birth', 'warning');
            
            $scope.drivingPassportid_dob=false;
        }
        else {
            
        }
    }

    $scope.Passportid_dob =  function (){

        if(($scope.cboIndividualProof.individualproof_name == "Passport" ) && ($scope.txtidproof_dob != null || $scope.txtidproof_dob != '' || $scope.txtidproof_dob != undefined) && ($scope.txtfile_no != null || $scope.txtfile_no != '' || $scope.txtfile_no != undefined)){
            // Notify.alert('Kindly Enter Date of Birth', 'warning');
            
            $scope.drivingPassportid_dob=false;
        }
        else {
            
        }
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
            if ($scope.panabsencereason_list != null) {
                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].checked == true) {
                        panabsencereasons_checked = true;
                        break;
                    }
                }
            }
            if ($scope.cboGender != undefined || $scope.cboGender != null) {
                lsgender_gid = $scope.cboGender.gender_gid;
                lsgender_name = $scope.cboGender.gender_name;
            }
            if ($scope.cboindDesignation != undefined || $scope.cboindDesignation != null) {
                lsdesignation_gid = $scope.cboindDesignation.designation_gid;
                lsdesignation_name = $scope.cboindDesignation.designation_type;
            }
            if ($scope.cboindStakeholderType != undefined || $scope.cboindStakeholderType != null) {
                lsstakeholdertype_gid = $scope.cboindStakeholderType.usertype_gid;
                lsstakeholdertype_name = $scope.cboindStakeholderType.user_type;
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
                    if ($scope.panabsencereason_list != null) {
                        angular.forEach($scope.panabsencereason_list, function (val) {

                            if (val.checked == true) {
                                var panabsencereason = val.panabsencereason;
                                panabsencereason_selectedList.push(panabsencereason);
                            }

                        });
                    }
                var params = {
                    pan_status: $scope.cbopanstatus,
                    pan_no: $scope.txtpan_no,
                    aadhar_no: $scope.txtaadhar_no,
                    first_name: $scope.txtindfirst_name,
                    middle_name: $scope.txtindmiddle_name,
                    last_name: $scope.txtindlast_name,
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
                    application_gid: $scope.application_gid,
                    panabsencereason_selectedList: panabsencereason_selectedList
                }
                var url = 'api/AgrMstSupplierOnboardEdit/IndividualSave';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        overallgeneralsummary();
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
            if ($scope.panabsencereason_list != null) {
                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].checked == true) {
                        panabsencereasons_checked = true;
                        break;
                    }
                }
            }
            if ($scope.cboGender != undefined || $scope.cboGender != null) {
                lsgender_gid = $scope.cboGender.gender_gid;
                lsgender_name = $scope.cboGender.gender_name;
            }
            if ($scope.cboindDesignation != undefined || $scope.cboindDesignation != null) {
                lsdesignation_gid = $scope.cboindDesignation.designation_gid;
                lsdesignation_name = $scope.cboindDesignation.designation_type;
            }
            if ($scope.cboindStakeholderType != undefined || $scope.cboindStakeholderType != null) {
                lsstakeholdertype_gid = $scope.cboindStakeholderType.usertype_gid;
                lsstakeholdertype_name = $scope.cboindStakeholderType.user_type;
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
                if (($scope.rdbfathernominee_status == '' && $scope.rdbmothernominee_status == '' && $scope.rdbspousenominee_status == '' && $scope.rdbothernominee_status == '') ||
                $scope.rdbfathernominee_status == undefined && $scope.rdbmothernominee_status == undefined && $scope.rdbspousenominee_status == undefined && $scope.rdbothernominee_status == undefined) {
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
                    if ($scope.panabsencereason_list != null) {
                angular.forEach($scope.panabsencereason_list, function (val) {

                    if (val.checked == true) {
                        var panabsencereason = val.panabsencereason;
                        panabsencereason_selectedList.push(panabsencereason);
                    }

                });
                        }
                var params = {
                    pan_status: $scope.cbopanstatus,
                    pan_no: $scope.txtpan_no,
                    aadhar_no: $scope.txtaadhar_no,
                    first_name: $scope.txtindfirst_name,
                    middle_name: $scope.txtindmiddle_name,
                    last_name: $scope.txtindlast_name,
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
                    application_gid: $scope.application_gid,
                    panabsencereason_selectedlist: panabsencereason_selectedList
                }
                var url = 'api/AgrMstSupplierOnboardEdit/IndividualSubmit';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        overallgeneralsummary();
                        $scope.cbopanstatus = '';
                        $scope.txtpan_no = '';
                        $scope.txtaadhar_no = '';
                        $scope.txtindfirst_name = '';
                        $scope.txtindmiddle_name = '';
                        $scope.txtindlast_name = '';
                        $scope.txtindividual_dob = '';
                        $scope.txtage = '';
                        $scope.rdbpep_status = '';
                        $scope.txtpepverified_date = '';
                        $scope.txtfather_firstname = '';
                        $scope.txtfather_middlename = '';
                        $scope.txtfather_lastname = '';
                        $scope.txtfather_dob = '';
                        $scope.txtfather_age = '';
                        $scope.txtmother_firstname = '';
                        $scope.txtmother_middlename = '';
                        $scope.txtmother_lastname = '';
                        $scope.txtmother_dob = '';
                        $scope.txtmother_age = '';
                        $scope.txtspouse_firstname = '';
                        $scope.txtspouse_middlename = '';
                        $scope.txtspouse_lastname = '';
                        $scope.txtspouse_dob = '';
                        $scope.txtspouse_age = '';
                        $scope.txtmain_occupation = '';
                        $scope.txtannual_income = '';
                        $scope.txtmonthly_income = '';
                        $scope.txtcurrentresidence_years = '';
                        $scope.txtbranch_distance = '';
                        $scope.txtprofile = '';
                        $scope.rdburn_status = '';
                        $scope.txt_urn = '';
                        $scope.rdbfathernominee_status = '';
                        $scope.rdbmothernominee_status = '';
                        $scope.rdbspousenominee_status = '';
                        $scope.rdbothernominee_status = '';
                        $scope.txtrelationshiptype = '';
                        $scope.txtnomineefirst_name = '';
                        $scope.txtnominee_middlename = '';
                        $scope.txtnominee_lastname = '';
                        $scope.txtnominee_dob = '';
                        $scope.txtnominee_age = '';
                        $scope.txttotallandinacres = '';
                        $scope.txtcultivatedland = '';
                        $scope.txtpreviouscrop = '';
                        $scope.txtprposedcrop = '';
                        $scope.cboGender = '';
                        $scope.cboindDesignation = '';
                        $scope.cboStakeholderType = '';
                        $scope.cboMaritalStatus = '';
                        $scope.cboEducationalQualification = '';
                        $scope.cboOwnershipType = '';
                        $scope.cboIncomeType = '';
                        $scope.cboResidenceType = '';
                        $scope.cboPropertyin = '';
                        $scope.cboGroup = '';
                        $scope.cboInstitution = '';
                        panabsencereason_selectedList = null;
                        $scope.contactpanform60_list = null;
                        $scope.contactmobileno_list = null;
                        $scope.contactemail_list = null;
                        $scope.contactidproof_list = null;
                        $scope.contactindividualaddress_list = null;
                        $scope.uploadindividualdoc_list = null;

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


        $scope.back = function () {
            $state.go('app.AgrMstCustomerOnboardingSummary');
        }

        function overallgeneralsummary() {
            lockUI();
            $scope.showapprovalsubmitbtn = false;
            var param = {
                application_gid: $scope.application_gid
            };
            var url = 'api/AgrMstSupplierOnboardEdit/GetGeneralInfoEdit';
            SocketService.getparams(url, param).then(function (resp) {
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
                $scope.approval_submittedflag = resp.data.approval_submittedflag;

                if ($scope.application_gid != null) {
                    $scope.gendev = false;
                    $scope.gendtldev = true;
                    $scope.indivdiv = true;
                    $scope.instdiv = true;
                }

                if ($scope.approval_submittedflag == "Y") {
                    $scope.hidesubmitbtn = true;
                }

                else {
                    $scope.hidesubmitbtn = false;
                }

                unlockUI();
            });
            lockUI();
            var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualSummaryEdit';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.individual_list = resp.data.BuyerOnboardcicindividual_list;
                    if ($scope.individual_list != null) {
                        var getapplicant = $scope.individual_list.find(function (v) { return v.stakeholder_type === "Applicant"  });                      
                        if ( getapplicant.contact_gid != null) {
                            $scope.showapprovalsubmitbtn = true;                      
                        }
                    }
                }
                else {
                    unlockUI();
                }
            });
            lockUI();
            var url = 'api/AgrMstSupplierOnboardEdit/GetInstitutionSummaryEdit';
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.institution_list = resp.data.BuyerOnboardcicinstitution_list;
                    if ($scope.institution_list != null) {
                        var getapplicant = $scope.institution_list.find(function (v) { return v.stakeholder_type === "Applicant"  });                   
                        if (getapplicant.institution_gid != null) {
                            $scope.showapprovalsubmitbtn = true;                       
                        }
                    }
                }
                else {
                    unlockUI();
                }
            });
            $scope.selectedBuyerIndex = 0;
            $scope.cbopanstatus = '';
            $scope.txtpan_no = '';
            $scope.txtaadhar_no = '';
            $scope.txtindfirst_name = '';
            $scope.txtindmiddle_name = '';
            $scope.txtindlast_name = '';
            $scope.txtindividual_dob = '';
            $scope.txtage = '';
            $scope.rdbpep_status = '';
            $scope.txtpepverified_date = '';
            $scope.txtfather_firstname = '';
            $scope.txtfather_middlename = '';
            $scope.txtfather_lastname = '';
            $scope.txtfather_dob = '';
            $scope.txtfather_age = '';
            $scope.txtmother_firstname = '';
            $scope.txtmother_middlename = '';
            $scope.txtmother_lastname = '';
            $scope.txtmother_dob = '';
            $scope.txtmother_age = '';
            $scope.txtspouse_firstname = '';
            $scope.txtspouse_middlename = '';
            $scope.txtspouse_lastname = '';
            $scope.txtspouse_dob = '';
            $scope.txtspouse_age = '';
            $scope.txtmain_occupation = '';
            $scope.txtannual_income = '';
            $scope.txtmonthly_income = '';
            $scope.txtcurrentresidence_years = '';
            $scope.txtbranch_distance = '';
            $scope.txtprofile = '';
            $scope.rdburn_status = '';
            $scope.txt_urn = '';
            $scope.rdbfathernominee_status = '';
            $scope.rdbmothernominee_status = '';
            $scope.rdbspousenominee_status = '';
            $scope.rdbothernominee_status = '';
            $scope.txtrelationshiptype = '';
            $scope.txtnomineefirst_name = '';
            $scope.txtnominee_middlename = '';
            $scope.txtnominee_lastname = '';
            $scope.txtnominee_dob = '';
            $scope.txtnominee_age = '';
            $scope.txttotallandinacres = '';
            $scope.txtcultivatedland = '';
            $scope.txtpreviouscrop = '';
            $scope.txtprposedcrop = '';
            $scope.cboGender = '';
            $scope.cboindDesignation = '';
            $scope.cboStakeHolderType = '';
            $scope.cboMaritalStatus = '';
            $scope.cboEducationalQualification = '';
            $scope.cboOwnershipType = '';
            $scope.cboIncomeType = '';
            $scope.cboResidenceType = '';
            $scope.cboPropertyOwner = '';
            $scope.cboGroup = '';
            $scope.cboInstitution = '';
            panabsencereason_selectedList = null;
            $scope.contactpanform60_list = null;
            $scope.contactmobileno_list = null;
            $scope.contactemail_list = null;
            $scope.contactidproof_list = null;
            $scope.contactindividualaddress_list = null;
            $scope.uploadindividualdoc_list = null;
            $scope.txtinstcompany_name = '';
            $scope.txtincorporation_date = '';
            $scope.txtbusinessstart_date = '';
            $scope.txtyearin_business = '';
            $scope.txtmonthsin_business = '';
            $scope.txtpan_number = '';
            $scope.txtcin_no = '';
            $scope.txtofficialtelephone_number = '';
            $scope.txtofficial_mailid = '';
            $scope.txtratingason_date = '';
            $scope.txtinstfirst_name = '';
            $scope.txtmiddle_name = '';
            $scope.txtlast_name = '';
            $scope.txtstart_date = '';
            $scope.txtend_date = '';
            $scope.rdbescrow = '';
            $scope.txttan_number = '';
            $scope.rdbincome_tax = '';
            $scope.txtrevenue = '';
            $scope.txtprofit = '';
            $scope.txtfixed_asset = '';
            $scope.txtsundrydebt_adv = '';
            $scope.txtlastyear_turnover = '';
            $scope.rdbinsturn_status = '';
            $scope.txtinst_urn = '';
            $scope.cboeditCompanytype = '';
            $scope.cboeditStakeholdertype = '';
            $scope.cboeditCreditAssessmentagency = '';
            $scope.cboeditAssessmentRating = '';
            $scope.cboeditAMLCategory = '';
            $scope.cboeditBusinesscategory = '';
            $scope.cboeditinstDesignation = '';
            $scope.cboindDesignation = '';
            $scope.cboeditCompanytype = '';
            $scope.cboStakeholdertype = '';
            $scope.cboCreditAssessmentagency = '';
            $scope.cboAssessmentRating = '';
            $scope.cboAMLCategory = '';
            $scope.cboBusinesscategory = '';
            $scope.cboinstDesignation = '';
            $scope.cboStakeholderType = '';
            $scope.institutiongst_list = null;
            $scope.institutionratinglist = null;
            $scope.institutionmobileno_list = null;
            $scope.institutionmaildetails_list = null;
            $scope.institutionaddresslist = null;
            $scope.creditbankacc_list = null;
            $scope.institutionupload_list = null;
            $scope.institutionlicense_list = null;
        }

        $scope.Submittoapproval = function () {
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSupplierOnboard/GetSupplierOnboardSubmitApproval';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/AgrMstCustomerOnboardingSummary');
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }


        $scope.Editgeneral = function (application_gid, application_status, product_gid, variety_gid) {

            $scope.selectedBuyerIndex = 0;
            $scope.editgeneral = true;
            $scope.editmdtab = true;
            $scope.gendev = true;
            $scope.gendtldev = false;

            var url = 'api/AgrMstApplicationAdd/GetGeneticCodeList';
            SocketService.get(url).then(function (resp) {
                $scope.geneticcode_list = resp.data.genetic_list;
            });

            var url = 'api/AgrMstApplicationAdd/GetDropDown';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.vertical_list = resp.data.vertical_list;
                $scope.verticaltaggs_list = resp.data.verticaltaggs_list;
                $scope.constitution_list = resp.data.constitutionlist;
                $scope.businessunit_list = resp.data.businessunitlist;
                $scope.associatemaster_list = resp.data.associatemasterlist;
                $scope.designation_list = resp.data.designationlist;
                $scope.creditgrouplist = resp.data.creditgrouplist;
                $scope.productname_list = resp.data.productname_list;
            });

            var params = {
                product_gid: $scope.product_gid
            }
            var url = 'api/AgrMstApplicationAdd/GetSectorcategory';
            unlockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.varietyname_list = resp.data.varietyname_list;
            });

            var url = 'api/AgrMstBuyerOnboard/GetBuyerSupplierType';
            SocketService.get(url).then(function (resp) {
                $scope.suppliertype_list = resp.data.BuyerSupplierType_List;
            });

            var param = {
                application_gid: $scope.application_gid
            };
            var url = 'api/AgrMstApplicationEdit/GetApplicationBasicDetailsTempClear';
            SocketService.get(url).then(function (resp) {
            });


            var url = 'api/AgrMstSupplierOnboardEdit/EditAppBasicDetail';
            unlockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txtcustomer_urn = resp.data.customer_urn;
                $scope.txtcustomer_name = resp.data.customer_name;
                $scope.cbovertical = resp.data.vertical_gid;
                $scope.suppliertypeadd = resp.data.buyersuppliertype_gid;
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

            var url = 'api/AgrMstSupplierOnboardEdit/GetAppMobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstmobileno_list = resp.data.mstBuyerOnboardmobileno_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/GetAppEmailAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstemailaddress_list = resp.data.mstBuyerOnboardemailaddress_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/GetAppGeneticCodeTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstgeneticcode_list = resp.data.mstBuyerOnboardgeneticcode_list;

            });

            var param = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSupplierOnboardEdit/GetAppProductTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.mstproduct_list = resp.data.mstBuyerOnboardproduct_list;
            });

            $scope.rdbgenetic_status = "No";


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
                    product_gid: cboproduct_name.product_gid
                }
                var url = 'api/AgrMstApplicationAdd/GetSectorcategory';
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
                    variety_gid: cbovariety_name.variety_gid
                }
                var url = 'api/AgrMstApplicationAdd/GetVarietyDtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.product_gid = resp.data.product_gid;
                    $scope.variety_gid = resp.data.variety_gid;
                    $scope.variety_name = resp.data.variety_name;
                    $scope.txtbotanical_name = resp.data.botanical_name;
                    $scope.txtalternative_name = resp.data.alternative_name;
                    $scope.txthsn_code = resp.data.hsn_code;
                });
            }

            $scope.genmobileno_add = function () {
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
                    var url = 'api/AgrMstSupplierOnboard/PostMobileNo';
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
                var url = 'api/AgrMstSupplierOnboardEdit/GetAppMobileNoTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.mstmobileno_list = resp.data.mstBuyerOnboardmobileno_list;
                });
            }


            $scope.mobileno_delete = function (application2contact_gid) {
                var params =
                    {
                        application2contact_gid: application2contact_gid
                    }
                var url = 'api/AgrMstSupplierOnboard/DeleteMobileNo';
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
                    var url = 'api/AgrMstSupplierOnboard/PostEmailAddress';
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
                var url = 'api/AgrMstSupplierOnboardEdit/GetAppEmailAddressTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.mstemailaddress_list = resp.data.mstBuyerOnboardemailaddress_list;
                });
            }


            $scope.emailaddress_delete = function (application2email_gid) {
                var params =
                    {
                        application2email_gid: application2email_gid
                    }
                var url = 'api/AgrMstSupplierOnboard/DeleteEmailAddress';
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
                    var url = 'api/AgrMstSupplierOnboard/PostGeneticCode';
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
                var url = 'api/AgrMstSupplierOnboardEdit/GetAppGeneticCodeTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.mstgeneticcode_list = resp.data.mstBuyerOnboardgeneticcode_list;
                });
            }



            $scope.geneticcode_delete = function (application2geneticcode_gid) {
                var params =
                   {
                       application2geneticcode_gid: application2geneticcode_gid
                   }
                var url = 'api/AgrMstSupplierOnboard/DeleteGenetic';
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

                if (($scope.cboVernacularLanguage == undefined) || ($scope.cboVernacularLanguage == '') || ($scope.cboVernacularLanguage == null)) {

                    Notify.alert('Select Vernacular Language ', 'warning');
                }
                else if (($scope.rdbsa_status == 'Yes') && (($scope.cbosa_idname == undefined) || ($scope.cbosa_idname == '') || ($scope.cbosa_idname == null))) {
                    Notify.alert('Kindly Add SAM Associate ID / Name  ', 'warning');
                }
                else if ($scope.mstproduct_list == null || $scope.mstproduct_list == undefined || $scope.mstproduct_list == "") {
                    Notify.alert('Atleast One Record should be added in Product Details', 'warning')
                }
                else {

                    var lsvertical_gid = '';
                    var lsvertical_name = '';
                    var lsconstitution_gid = '';
                    var lsconstitution_name = '';
                    var lsbusinessunit_gid = '';
                    var lsbusinessunit_name = '';
                    var lsname = '';
                    var lsassociatemaster_gid = '';
                    var lsdesignation_gid = '';
                    var lsdesignation_type = '';
                    var lsprogram_name = '';
                    var lsprogram_gid = '';
                    var lsproduct_name = '';
                    var lsproduct_gid = '';
                    var lsvariety_name = '';
                    var lsvariety_gid = '';
                    var lscreditgroup_gid = '';
                    var lscreditgroup_name = '';
                    var lsbuyersuppliertype_gid = '';
                    var lsbuyersuppliertype_name = '';

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

                    if ($scope.cbovertical != undefined || $scope.cbovertical != null) {
                        lsvertical_gid = $scope.cbovertical;
                        var getvertical_name = $scope.vertical_list.find(function (v) { return v.vertical_gid === $scope.cbovertical });
                        if (getvertical_name != null && getvertical_name != "")
                            lsvertical_name = getvertical_name.vertical_name;
                    }

                    if ($scope.cboConstitution != undefined || $scope.cboConstitution != null) {
                        lsconstitution_gid = $scope.cboConstitution;
                        var getconstitutionname = $scope.constitutionlist.find(function (v) { return v.constitution_gid === $scope.cboConstitution });
                        if (getconstitutionname != null && getconstitutionname != "")
                            lsconstitution_name = getconstitutionname.constitution_name;

                    }

                    if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                        lsdesignation_gid = $scope.cboDesignation;
                        var getdesginationname = $scope.designation_list.find(function (v) { return v.designation_gid === $scope.cboDesignation });
                        if (getdesginationname != null && getdesginationname != "")
                            lsdesignation_type = getdesginationname.designation_type;
                    }
                    if ($scope.cbosa_idname != undefined || $scope.cbosa_idname != null) {

                        lsassociatemaster_gid = $scope.cbosa_idname;
                        var getassociatemastername = $scope.associatemaster_list.find(function (v) { return v.designation_gid === $scope.cbosa_idname });
                        if (getassociatemastername != null && getassociatemastername != "")
                            lsname = getassociatemastername.name;
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
                    if ($scope.suppliertypeadd != undefined || $scope.suppliertypeadd != null) {
                        lsbuyersuppliertype_gid = $scope.suppliertypeadd;
                        var getbuyersuppliertype_name = $scope.suppliertype_list.find(function (v) { return v.buyersuppliertype_gid === $scope.suppliertypeadd });
                        if (getbuyersuppliertype_name != null && getbuyersuppliertype_name != "")
                            lsbuyersuppliertype_name = getbuyersuppliertype_name.buyersuppliertype_name;
                    }
                    
                    var params = {
                        customer_urn: $scope.txtcustomer_urn,
                        customer_name: $scope.txtcustomer_name,
                        vertical_gid: $scope.cbovertical,
                        vertical_name: lsvertical_name,
                        constitution_gid: $scope.cboConstitution,
                        constitution_name: lsconstitution_name,
                        sa_status: $scope.rdbsa_status,
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
                        onboarding_status: $scope.rdbOnboarding,
                        buyersuppliertype_gid: lsbuyersuppliertype_gid,
                        buyersuppliertype_name: lsbuyersuppliertype_name,
                    }
                    var url = 'api/AgrMstSupplierOnboardEdit/UpdateAppBasicDetail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            overallgeneralsummary();
                            //if (lstab == 'add') {
                            //    $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                            //}
                            //else {
                            //    $state.go('app.AgrMstApplicationGeneralEdit');
                            //}
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
                    var url = 'api/AgrMstSupplierOnboard/PostProductDetailAdd';
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
                var url = 'api/AgrMstSupplierOnboardEdit/GetAppProductTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.mstproduct_list = resp.data.mstBuyerOnboardproduct_list;
                });
            }

            $scope.product_delete = function (application2product_gid) {
                var params =
                    {
                        application2product_gid: application2product_gid
                    }
                var url = 'api/AgrMstSupplierOnboard/DeleteProductDetail';
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

        //magic magic magic

        $scope.Editindividual = function (contact_gid) {
            $scope.validatecontact_gid = contact_gid;
            $scope.selectedBuyerIndex = 2;
            $scope.editindividual = true;
            $scope.editmdtab = true;
            $scope.instdiv = false;

            var param = {
                contact_gid: contact_gid
            };

            var url = 'api/AgrMstSupplierOnboardEdit/EditIndividual';

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
                $scope.txtpan_no = resp.data.pan_no;
                $scope.txtaadhar_no = resp.data.aadhar_no;
                $scope.txtindfirst_name = resp.data.first_name;
                $scope.txtindmiddle_name = resp.data.middle_name;
                $scope.txtindlast_name = resp.data.last_name;
                $scope.txtindividual_dob = resp.data.individual_dob;
                $scope.txtage = resp.data.age;
                $scope.cboGender = resp.data.gender_gid;
                $scope.cboindDesignation = resp.data.designation_gid;
                $scope.cboEducationalQualification = resp.data.educationalqualification_gid;

                $scope.txtmain_occupation = resp.data.main_occupation;
                $scope.txtannual_income = resp.data.annual_income;
                if ($scope.txtannual_income != null && $scope.txtannual_income != undefined && $scope.txtannual_income != "") {
                    $scope.txtmarketValue_edit = (parseInt($scope.txtannual_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtmarketValue_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_annualincomeedit').innerHTML = $scope.lblamountwords;
                }
                $scope.txtmonthly_income = resp.data.monthly_income;
                if ($scope.txtmonthly_income != null && $scope.txtmonthly_income != undefined && $scope.txtmonthly_income != "") {
                    $scope.txtmarketValue_edit = (parseInt($scope.txtmonthly_income.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtmarketValue_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_monthlyincomeedit').innerHTML = $scope.lblamountwords;
                }
                $scope.cboIncomeType = resp.data.incometype_gid;
                $scope.rdbpep_status = resp.data.pep_status;
                $scope.txtpepverified_date = resp.data.pepverified_date;
                $scope.cboMaritalStatus = resp.data.maritalstatus_gid;

                $scope.cboStakeholderType = resp.data.stakeholdertype_gid;


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

                if (resp.data.stakeholdertype_name == "Applicant") {
                    $scope.stk_hldr = true;
                }

                else {
                    $scope.stk_hldr = false;
                }

                if (resp.data.contact_status == "Incomplete") {
                    $scope.IndividualSubmit = true;
                    $scope.editindividual = false;
                }
                else {
                    $scope.IndividualSubmit = false;
                    $scope.editindividual = true;
                }

                //if (resp.data.urn_status == 'Yes') {
                //    $scope.URN_yes = true;
                //}
                //else {
                //    $scope.URN_yes = false;
                //    $scope.txt_urn = '';
                //}
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

            function defaultamountwordschange(input) {
                var str1 = input.replace(/,/g, '');
                var str = Math.round(str1);
                var output = Number(str).toLocaleString('en-IN');
                var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
                return lswords;
            }

            var url = 'api/AgrMstSupplierOnboard/Individual2bankTmpList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.creditbankacc_list = resp.data.BuyerOnboardIndividual2bankacc_list;

            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });


            var url = 'api/AgrMstSupplierOnboardEdit/GetPANForm60List';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanform60_list = resp.data.BuyerOnboardcontactpanform60_list;
            });
            var param = {
                contact_gid: contact_gid
            };
            var url = 'api/AgrMstSupplierOnboardEdit/EditPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.panabsencereason_list = resp.data.BuyerOnboardpanabsencereason_list;
                for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                    if ($scope.panabsencereason_list[i].check_status == true) {
                        $scope.panabsencereason_list[i].checked = true;
                    }
                }
            });

            var url = 'api/AgrMstSupplierOnboardEdit/ContactPANAbsenceReasonList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactpanabsencereason_list = resp.data.BuyerOnboardcontactpanabsencereason_list;
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

            var url = 'api/AgrMstApplication360/IndividualOnboardDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.individualdocument_list = resp.data.application_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualMobileNoTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactmobileno_list = resp.data.BuyerOnboardcontactmobileno_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualEmailAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactemail_list = resp.data.BuyerOnboardcontactemail_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualAddressTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactaddress_list = resp.data.BuyerOnboardcontactaddress_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualProofTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.contactidproof_list = resp.data.contactBuyerOnboardidproof_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualDocTempList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.uploadindividualdoc_list = resp.data.uploadBuyerOnboardindividualdoc_list;
            });

            $scope.txtannualincome_change = function () {
                var input = document.getElementById('editannual_income').value;
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
                    document.getElementById('words_annualincomeedit').innerHTML = lswords_annualincome;
                }
            }

            $scope.txtmonthlyincome_change = function () {
                var input = document.getElementById('monthly_incomeedit').value;
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
                    document.getElementById('words_monthlyincomeedit').innerHTML = lswords_monthlyincome;
                }
            }

            $scope.EditIndivmobileno_delete = function (contact2mobileno_gid) {
                var params =
                    {
                        contact2mobileno_gid: contact2mobileno_gid
                    }
                var url = 'api/AgrMstSupplierOnboard/MobileNoDelete';
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
                    mobileno_indtemplist();
                });
    
            }


            $scope.edit_indbankacctdtl = function () {

                if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') ||
                    ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
                ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                    ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.rdbprimarystatus == undefined) || ($scope.rdbprimarystatus == '') ||  ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
                    Notify.alert('Enter All Mandatory Fields', 'warning');
                }
                else {
                    if ($scope.rdbJoint_Account == 'Yes') {

                        if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                            Notify.alert('Enter Joint Account Holder Name', 'warning');
                        }
                        else {
                            var params = {
                                //application_gid: $scope.application_gid,
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
                                primary_status: $scope.rdbprimarystatus,
                                contact_gid: contact_gid
                            }
                            var url = 'api/AgrMstSupplierOnboard/PostIndividualBank';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    //$scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;
                                    $scope.credituploaddocument_list = null;
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    indbankacctmp_list();
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
                                //activate();
                                indbankacctmp_list();
                            });
                        }
                    }
                    else {
                        var params = {
                            //application_gid: application_gid,
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
                            primary_status: $scope.rdbprimarystatus,
                            contact_gid: contact_gid
                        }
                        var url = 'api/AgrMstSupplierOnboard/PostIndividualBank';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                //$scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });

                                indbankacctmp_list();
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
                            indbankacctmp_list();

                        });
                    }

                }
            }

            $scope.indeditbankacctdtl_delete = function (contact2bankdtl_gid, contact_gid) {
                var params = {
                    contact2bankdtl_gid: contact2bankdtl_gid,
                    contact_gid: contact_gid
                }
                var url = 'api/AgrMstSupplierOnboard/DeleteIndividualBankAcc';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        //$scope.creditbankacc_list = resp.data.BuyerOnboardIndividual2bankacc_list;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        indbankacctmp_list();

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

            $scope.indbankacctdtl_edit = function (contact2bankdtl_gid) {
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

                    var url = 'api/AgrMstSupplierOnboard/EditGetIndividualBankAccDtl';

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
                            var url = 'api/AgrSuprKyc/IfscVerification';
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
                        if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') ||
                           ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
                       ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                            ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.rdbprimarystatus == undefined) || ($scope.rdbprimarystatus == '') ||  ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
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
                                        primary_status: $scope.rdbprimarystatus,
                                        contact_gid: contact_gid

                                    }
                                    var url = 'api/AgrMstSupplierOnboard/UpdateIndividualBankAccDtl';
                                    lockUI();
                                    SocketService.post(url, params).then(function (resp) {
                                        unlockUI();
                                        if (resp.data.status == true) {

                                            Notify.alert(resp.data.message, {
                                                status: 'success',
                                                pos: 'top-center',
                                                timeout: 3000
                                            });
                                            indbankacctmp_list();
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
                                    primary_status: $scope.rdbprimarystatus,
                                    contact_gid: contact_gid

                                }
                                var url = 'api/AgrMstSupplierOnboard/UpdateIndividualBankAccDtl';
                                lockUI();
                                SocketService.post(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {

                                        Notify.alert(resp.data.message, {
                                            status: 'success',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                        indbankacctmp_list();
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


            function indbankacctmp_list() {

                var param = {
                    contact_gid: contact_gid
                };

                var url = 'api/AgrMstSupplierOnboard/Individual2bankTmpList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.creditbankacc_list = resp.data.BuyerOnboardIndividual2bankacc_list;

                });

            }


            $scope.Indivmobileno_edit = function () {

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
                        whatsapp_no: $scope.rdbwhatsapp_no,
                        contact_gid: contact_gid
                    }
                    var url = 'api/AgrMstSupplierOnboard/MobileNumberAdd';
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

                        mobileno_indtemplist();
                        $scope.txtmobile_no = '';
                        $scope.rdbprimary_no = '';
                        $scope.rdbwhatsapp_no = '';
                    });
                }
            }

            function mobileno_indtemplist() {
                var param = {
                    contact_gid: contact_gid
                };
                var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualMobileNoTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactmobileno_list = resp.data.BuyerOnboardcontactmobileno_list;
                });
            }


            $scope.individualemail_add = function () {
                if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_email == undefined) || ($scope.rdbprimary_email == '')) {
                    Notify.alert('Enter Email Address/Select Primary Status', 'warning');
                }
                else {


                    var params = {
                        email_address: $scope.txtemail_address,
                        primary_status: $scope.rdbprimary_email,
                        contact_gid: contact_gid
                    }
                    var url = 'api/AgrMstSupplierOnboard/EmailAddressAdd';
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
                    contact_gid: contact_gid
                };
                var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualEmailAddressTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactemail_list = resp.data.BuyerOnboardcontactemail_list;
                });
            }

            $scope.individualemail_delete = function (contact2email_gid) {
                var params =
                   {
                       contact2email_gid: contact2email_gid
                   }
                var url = 'api/AgrMstSupplierOnboard/EmailAddressDelete';
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

            $scope.indivaddress_edit = function () {
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
                            longitude: $scope.txtlongitude,
                            contact_gid: contact_gid
                        }
                        var url = 'api/AgrMstSupplierOnboard/AddressAdd';
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

            $scope.Indivaddress_delete = function (contact2address_gid) {
                var params =
                   {
                       contact2address_gid: contact2address_gid
                   }
                var url = 'api/AgrMstSupplierOnboardEdit/DeleteIndividualAddress';
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



            function address_templist() {
                var param = {
                    contact_gid: contact_gid
                };
                var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualAddressTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactaddress_list = resp.data.BuyerOnboardcontactaddress_list;
                });
            }


            $scope.address_delete = function (contact2address_gid) {
                var params =
                   {
                       contact2address_gid: contact2address_gid
                   }
                var url = 'api/AgrMstSupplierOnboardEdit/DeleteIndividualAddress';
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

            $scope.update_individual = function () {
                var panabsencereasons_checked = false;
                if ($scope.panabsencereason_list != null) {
                    for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                        if ($scope.panabsencereason_list[i].checked == true) {
                            panabsencereasons_checked = true;
                            break;
                        }
                    }
                }
                //if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                //    Notify.alert('Kindly Enter URN', 'warning')
                //}
                //else
                    if ($scope.cboStakeholderType == null || $scope.cboStakeholderType == '' || $scope.cboStakeholderType == undefined) {
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
                    else if ($scope.Alreadyaddedpanaadhar == true) {
                        Notify.alert('PAN number is already approved, you cannot add', 'warning')
                    }
                    //else if ($scope.uploadindividualdoc_list == null) {
                    //    Notify.alert("Kindly upload the document", {
                    //        status: 'warning',
                    //        pos: 'top-center',
                    //        timeout: 3000
                    //    });
                    //}
                else if ($scope.rdbfathernominee_status == 'Yes' || $scope.rdbmothernominee_status == 'Yes' || $scope.rdbspousenominee_status == 'Yes' || $scope.rdbothernominee_status == 'Yes') {

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

                    if ($scope.cboGender != undefined || $scope.cboGender != null) {
                        lsgender_gid = $scope.cboGender;
                        var getgender_name = $scope.gender_list.find(function (v) { return v.gender_gid === $scope.cboGender });
                        if (getgender_name != null && getgender_name != "")
                            lsgender_name = getgender_name.gender_name;
                    }


                    if ($scope.cboindDesignation != undefined || $scope.cboindDesignation != null) {
                        lsdesignation_gid = $scope.cboindDesignation;
                        var getdesginationname = $scope.designation_list.find(function (v) { return v.designation_gid === $scope.cboindDesignation });
                        if (getdesginationname != null && getdesginationname != "")
                            lsdesignation_name = getdesginationname.designation_type;
                    }

                    if ($scope.cboStakeholderType != undefined || $scope.cboStakeholderType != null) {
                        lsstakeholdertype_gid = $scope.cboStakeholderType;
                        var getStakeholderType = $scope.usertypenoapplicant_list.find(function (v) { return v.usertype_gid === $scope.cboStakeholderType });
                        if (getStakeholderType != null && getStakeholderType != "")
                            lsstakeholdertype_name = getStakeholderType.user_type;
                    }

                    if ($scope.cboMaritalStatus != undefined || $scope.cboMaritalStatus != null) {
                        lsmaritalstatus_gid = $scope.cboMaritalStatus;
                        var getmaritalstatus = $scope.maritalstatus_list.find(function (v) { return v.maritalstatus_gid === $scope.cboMaritalStatus });
                        if (getmaritalstatus != null && getmaritalstatus != "")
                            lsmaritalstatus_name = getmaritalstatus.maritalstatus_name;
                    }

                    if ($scope.cboEducationalQualification != undefined || $scope.cboEducationalQualification != null) {
                        lseducationalqualification_gid = $scope.cboEducationalQualification;
                        var getQualification = $scope.educationalqualification_list.find(function (v) { return v.educationalqualification_gid === $scope.cboEducationalQualification });
                        if (getQualification != null && getQualification != "")
                            lseducationalqualification_name = getQualification.educationalqualification_name;
                    }

                    if ($scope.cboOwnershipType != undefined || $scope.cboOwnershipType != null) {
                        lsownershiptype_gid = $scope.cboOwnershipType;
                        var getownershiptype = $scope.ownershiptype_list.find(function (v) { return v.ownershiptype_gid === $scope.cboOwnershipType });
                        if (getownershiptype != null && getownershiptype != "")
                            lsownershiptype_name = getownershiptype.ownershiptype_name;
                    }

                    if ($scope.cboIncomeType != undefined || $scope.cboIncomeType != null) {
                        lsincometype_gid = $scope.cboIncomeType;
                        var getincometype = $scope.incometype_list.find(function (v) { return v.incometype_gid === $scope.cboIncomeType });
                        if (getincometype != null && getincometype != "")
                            lsincometype_name = getincometype.incometype_name;
                    }


                    if ($scope.cboResidenceType != undefined || $scope.cboResidenceType != null) {
                        lsresidencetype_gid = $scope.cboResidenceType;
                        var getResidenceType = $scope.residencetype_list.find(function (v) { return v.residencetype_gid === $scope.cboResidenceType });
                        if (getResidenceType != null && getResidenceType != "")
                            lsresidencetype_name = getResidenceType.residencetype_name;
                    }

                    if ($scope.cboInstitution != undefined || $scope.cboInstitution != null) {
                        lsinstitution_gid = $scope.cboInstitution;
                        var getInstitution = $scope.institutionlist.find(function (v) { return v.institution_gid === $scope.cboInstitution });
                        if (getInstitution != null && getInstitution != "")
                            institutionname = getInstitution.institution_name;
                    }

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
                        first_name: $scope.txtindfirst_name,
                        middle_name: $scope.txtindmiddle_name,
                        last_name: $scope.txtindlast_name,
                        individual_dob: $scope.txtindividual_dob,
                        age: $scope.txtage,

                        gender_gid: $scope.cboGender,
                        gender_name: lsgender_name,

                        pep_status: $scope.rdbpep_status,
                        pepverifieddate: $scope.txtpepverified_date,

                        stakeholdertype_gid: $scope.cboStakeholderType,
                        stakeholder_type: lsstakeholdertype_name,

                        maritalstatus_gid: $scope.cboMaritalStatus,
                        maritalstatus_name: lsmaritalstatus_name,

                        designation_gid: $scope.cboindDesignation,
                        designation_name: lsdesignation_name,


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
                        educationalqualification_name: lseducationalqualification_name,

                        main_occupation: $scope.txtmain_occupation,
                        annual_income: $scope.txtannual_income,
                        monthly_income: $scope.txtmonthly_income,

                        incometype_gid: $scope.cboIncomeType,
                        incometype_name: lsincometype_name,

                        ownershiptype_gid: $scope.cboOwnershipType,
                        ownershiptype_name: lsownershiptype_name,

                        propertyholder_gid: $scope.cboPropertyin,
                        propertyholder_name: lspropertyin_name,

                        residencetype_gid: $scope.cboResidenceType,
                        residencetype_name: lsresidencetype_name,

                        currentresidence_years: $scope.txtcurrentresidence_years,
                        branch_distance: $scope.txtbranch_distance,

                        contact_gid: contact_gid,
                        application_gid: $scope.application_gid,
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
                        application_gid: $scope.application_gid,
                        panabsencereason_selectedlist: panabsencereason_selectedList
                    }
                    var url = 'api/AgrMstSupplierOnboardEdit/UpdateIndividual';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            overallgeneralsummary();
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
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/AgrMstSupplierOnboard/IndividualProofDocumentUpload'
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            $("#file1").val('');
                            unlockUI();
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $("#file1").val('');
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


            $scope.idproofdocument_uploadedit = function (val, val1, name) {
                lockUI();
                if (($scope.cboIndividualProof == null) || ($scope.cboIndividualProof == '') || ($scope.cboIndividualProof == undefined) || ($scope.txtidproof_no == null) || ($scope.txtidproof_no == '') || ($scope.txtidproof_no == undefined)) {
                    $("#file1").val('');
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
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/AgrMstSupplierOnboard/IndividualProofDocumentUpload'
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            $("#file1").val('');
                            unlockUI();
                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $("#file1").val('');
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

            $scope.idproofedit_delete = function (contact2idproof_gid) {

                var params = {
                    contact2idproof_gid: contact2idproof_gid
                }
                lockUI();
                var url = 'api/AgrMstSupplierOnboard/IndividualProofDelete';
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
                    idproof_templist();
                    unlockUI();
                });
            }
    


            function idproof_templist() {
                var param = {
                    contact_gid: contact_gid
                };
                var url = 'api/AgrMstSupplierOnboardEdit/GetIndividualProofTempList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.contactidproof_list = resp.data.contactBuyerOnboardidproof_list;
                });
            }


            $scope.individualedit_save = function () {
                var panabsencereasons_checked = false;
                if ($scope.panabsencereason_list != null) {
                    for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                        if ($scope.panabsencereason_list[i].checked == true) {
                            panabsencereasons_checked = true;
                            break;
                        }
                    }
                }
                //if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                //    Notify.alert('Kindly Enter URN', 'warning')
                //}
                //else
                    if ($scope.cboStakeholderType == null || $scope.cboStakeholderType == '' || $scope.cboStakeholderType == undefined) {
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

                    if ($scope.cboGender != undefined || $scope.cboGender != null) {
                        lsgender_gid = $scope.cboGender;
                        var getgender_name = $scope.gender_list.find(function (v) { return v.gender_gid === $scope.cboGender });
                        if (getgender_name != null && getgender_name != "")
                            lsgender_name = getgender_name.gender_name;
                    }


                    if ($scope.cboindDesignation != undefined || $scope.cboindDesignation != null) {
                        lsdesignation_gid = $scope.cboindDesignation;
                        var getdesginationname = $scope.designation_list.find(function (v) { return v.designation_gid === $scope.cboindDesignation });
                        if (getdesginationname != null && getdesginationname != "")
                            lsdesignation_name = getdesginationname.designation_type;
                    }

                    if ($scope.cboStakeholderType != undefined || $scope.cboStakeholderType != null) {
                        lsstakeholdertype_gid = $scope.cboStakeholderType;
                        var getStakeholderType = $scope.usertypenoapplicant_list.find(function (v) { return v.usertype_gid === $scope.cboStakeholderType });
                        if (getStakeholderType != null && getStakeholderType != "")
                            lsstakeholdertype_name = getStakeholderType.user_type;
                    }

                    if ($scope.cboMaritalStatus != undefined || $scope.cboMaritalStatus != null) {
                        lsmaritalstatus_gid = $scope.cboMaritalStatus;
                        var getmaritalstatus = $scope.maritalstatus_list.find(function (v) { return v.maritalstatus_gid === $scope.cboMaritalStatus });
                        if (getmaritalstatus != null && getmaritalstatus != "")
                            lsmaritalstatus_name = getmaritalstatus.maritalstatus_name;
                    }

                    if ($scope.cboEducationalQualification != undefined || $scope.cboEducationalQualification != null) {
                        lseducationalqualification_gid = $scope.cboEducationalQualification;
                        var getQualification = $scope.educationalqualification_list.find(function (v) { return v.educationalqualification_gid === $scope.cboEducationalQualification });
                        if (getQualification != null && getQualification != "")
                            lseducationalqualification_name = getQualification.educationalqualification_name;
                    }

                    if ($scope.cboOwnershipType != undefined || $scope.cboOwnershipType != null) {
                        lsownershiptype_gid = $scope.cboOwnershipType;
                        var getownershiptype = $scope.ownershiptype_list.find(function (v) { return v.ownershiptype_gid === $scope.cboOwnershipType });
                        if (getownershiptype != null && getownershiptype != "")
                            lsownershiptype_name = getownershiptype.ownershiptype_name;
                    }

                    if ($scope.cboIncomeType != undefined || $scope.cboIncomeType != null) {
                        lsincometype_gid = $scope.cboIncomeType;
                        var getincometype = $scope.incometype_list.find(function (v) { return v.incometype_gid === $scope.cboIncomeType });
                        if (getincometype != null && getincometype != "")
                            lsincometype_name = getincometype.incometype_name;
                    }


                    if ($scope.cboResidenceType != undefined || $scope.cboResidenceType != null) {
                        lsresidencetype_gid = $scope.cboResidenceType;
                        var getResidenceType = $scope.residencetype_list.find(function (v) { return v.residencetype_gid === $scope.cboResidenceType });
                        if (getResidenceType != null && getResidenceType != "")
                            lsresidencetype_name = getResidenceType.residencetype_name;
                    }

                    if ($scope.cboInstitution != undefined || $scope.cboInstitution != null) {
                        lsinstitution_gid = $scope.cboInstitution;
                        var getInstitution = $scope.institutionlist.find(function (v) { return v.institution_gid === $scope.cboInstitution });
                        if (getInstitution != null && getInstitution != "")
                            lsinstitution_name = getInstitution.institution_name;
                    }

                    //if ($scope.cboPropertyin != undefined || $scope.cboPropertyin != null) {
                    //    lspropertyin_gid = $scope.cboPropertyin;
                    //    var getpropertyin = $scope.propertyin_list.find(function (v) { return v.propertyin_gid === $scope.cboPropertyin });
                    //    if (getpropertyin != null && getpropertyin != "")
                    //        lspropertyin_name = getpropertyin.propertyin_name;
                    //}

                    if ($scope.cboGroup != undefined || $scope.cboGroup != null) {
                        lsgroup_gid = $scope.cboGroup.group_gid;
                        lsgroup_name = $scope.cboGroup.group_name;
                    }
                    //if ($scope.cboInstitution != undefined || $scope.cboInstitution != null) {
                    //    lsinstitution_gid = $scope.cboInstitution.institution_gid;
                    //    lsinstitution_name = $scope.cboInstitution.institution_name;
                    //}


                    if ($scope.cboGroup == 'NA') {
                        groupname = 'NA';
                    }
                    if ($scope.cboInstitution == 'NA') {
                        institutionname = 'NA';
                    }
                        var panabsencereason_selectedList = [];
                        if ($scope.panabsencereason_list != null) {
                    angular.forEach($scope.panabsencereason_list, function (val) {
                        if (val.checked == true) {
                            var panabsencereason = val.panabsencereason;
                            panabsencereason_selectedList.push(panabsencereason);
                        }

                    });
                    }
                    var params = {
                        pan_status: $scope.cbopanstatus,
                        pan_no: $scope.txtpan_no,
                        aadhar_no: $scope.txtaadhar_no,
                        first_name: $scope.txtindfirst_name,
                        middle_name: $scope.txtindmiddle_name,
                        last_name: $scope.txtindlast_name,
                        individual_dob: $scope.txtindividual_dob,
                        age: $scope.txtage,

                        gender_gid: $scope.cboGender,
                        gender_name: lsgender_name,

                        designation_gid: $scope.cboindDesignation,
                        designation_name: lsdesignation_name,

                        pep_status: $scope.rdbpep_status,
                        pepverifieddate: $scope.txtpepverified_date,

                        stakeholdertype_gid: $scope.cboStakeholderType,
                        stakeholder_type: lsstakeholdertype_name,

                        propertyholder_gid: $scope.cboPropertyin,
                        propertyholder_name: lspropertyin_name,

                        maritalstatus_gid: $scope.cboMaritalStatus,
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

                        educationalqualification_gid: $scope.cboEducationalQualification,
                        educationalqualification_name: lseducationalqualification_name,

                        main_occupation: $scope.txtmain_occupation,
                        annual_income: $scope.txtannual_income,
                        monthly_income: $scope.txtmonthly_income,
                        incometype_gid: $scope.cboIncomeType,
                        incometype_name: lsincometype_name,

                        ownershiptype_gid: $scope.cboOwnershipType,
                        ownershiptype_name: lsownershiptype_name,

                        residencetype_gid: $scope.cboResidenceType,
                        residencetype_name: lsresidencetype_name,

                        currentresidence_years: $scope.txtcurrentresidence_years,
                        branch_distance: $scope.txtbranch_distance,

                        contact_gid: contact_gid,

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
                        institution_name: lsinstitution_name,
                        application_gid: $scope.application_gid,
                        panabsencereason_selectedlist: panabsencereason_selectedList
                    }
                    var url = 'api/AgrMstSupplierOnboardEdit/SaveIndividualEditDtl';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //if (lstab == 'add') {
                            //    $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                            //}
                            //else {
                            //    $state.go('app.AgrMstApplicationGeneralEdit');
                            //}
                            overallgeneralsummary();
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

            $scope.individualedit_submit = function () {
                var panabsencereasons_checked = false;
                if ($scope.panabsencereason_list != null) {
                    for (var i = 0; i < $scope.panabsencereason_list.length; i++) {
                        if ($scope.panabsencereason_list[i].checked == true) {
                            panabsencereasons_checked = true;
                            break;
                        }
                    }
                }
                //if ($scope.cboGroup == "") {
                //    Notify.alert('Kindly Select Group Name', 'warning')
                //}
                //if ($scope.cboInstitution == '') {
                //    Notify.alert('Kindly Select Company Name', 'warning')
                //}
                //else
                //    if ($scope.rdburn_status == 'Yes' && ($scope.txt_urn == '' || $scope.txt_urn == undefined || $scope.txt_urn == null)) {
                //    Notify.alert('Kindly Enter URN', 'warning')
                //}
                //    else
                if ($scope.cboStakeholderType == null || $scope.cboStakeholderType == '' || $scope.cboStakeholderType == undefined) {
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
                else if ($scope.AlreadyaddedIndividualaadhar == true || $scope.AlreadyaddedIndividualpan == true) {
                    if ($scope.AlreadyaddedIndividualaadhar == true && $scope.AlreadyaddedIndividualpan == true) {
                        Notify.alert('PAN & Aadhar number is already approved, you cannot add', 'warning')
                    }
                    else if ($scope.AlreadyaddedIndividualaadhar == true && $scope.AlreadyaddedIndividualpan == false)
                        Notify.alert('Aadhar number is already approved, you cannot add', 'warning')
                    else
                        Notify.alert('PAN number is already approved, you cannot add', 'warning')
                }
                    //else if ($scope.uploadindividualdoc_list == null) {
                    //    Notify.alert("Kindly upload the document", {
                    //        status: 'warning',
                    //        pos: 'top-center',
                    //        timeout: 3000
                    //    });
                    //}
                else if ($scope.rdbfathernominee_status == 'Yes' || $scope.rdbmothernominee_status == 'Yes' || $scope.rdbspousenominee_status == 'Yes' || $scope.rdbothernominee_status == 'Yes') {
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

                    if ($scope.cboGender != undefined || $scope.cboGender != null) {
                        lsgender_gid = $scope.cboGender;
                        var getgender_name = $scope.gender_list.find(function (v) { return v.gender_gid === $scope.cboGender });
                        if (getgender_name != null && getgender_name != "")
                            lsgender_name = getgender_name.gender_name;
                    }


                    if ($scope.cboindDesignation != undefined || $scope.cboindDesignation != null) {
                        lsdesignation_gid = $scope.cboindDesignation;
                        var getdesginationname = $scope.designation_list.find(function (v) { return v.designation_gid === $scope.cboindDesignation });
                        if (getdesginationname != null && getdesginationname != "")
                            lsdesignation_name = getdesginationname.designation_type;
                    }

                    if ($scope.cboStakeholderType != undefined || $scope.cboStakeholderType != null) {
                        lsstakeholdertype_gid = $scope.cboStakeholderType;
                        var getStakeholderType = $scope.usertypenoapplicant_list.find(function (v) { return v.usertype_gid === $scope.cboStakeholderType });
                        if (getStakeholderType != null && getStakeholderType != "")
                            lsstakeholdertype_name = getStakeholderType.user_type;
                    }

                    if ($scope.cboMaritalStatus != undefined || $scope.cboMaritalStatus != null) {
                        lsmaritalstatus_gid = $scope.cboMaritalStatus;
                        var getmaritalstatus = $scope.maritalstatus_list.find(function (v) { return v.maritalstatus_gid === $scope.cboMaritalStatus });
                        if (getmaritalstatus != null && getmaritalstatus != "")
                            lsmaritalstatus_name = getmaritalstatus.maritalstatus_name;
                    }

                    if ($scope.cboEducationalQualification != undefined || $scope.cboEducationalQualification != null) {
                        lseducationalqualification_gid = $scope.cboEducationalQualification;
                        var getQualification = $scope.educationalqualification_list.find(function (v) { return v.educationalqualification_gid === $scope.cboEducationalQualification });
                        if (getQualification != null && getQualification != "")
                            lseducationalqualification_name = getQualification.educationalqualification_name;
                    }

                    if ($scope.cboOwnershipType != undefined || $scope.cboOwnershipType != null) {
                        lsownershiptype_gid = $scope.cboOwnershipType;
                        var getownershiptype = $scope.ownershiptype_list.find(function (v) { return v.ownershiptype_gid === $scope.cboOwnershipType });
                        if (getownershiptype != null && getownershiptype != "")
                            lsownershiptype_name = getownershiptype.ownershiptype_name;
                    }

                    if ($scope.cboIncomeType != undefined || $scope.cboIncomeType != null) {
                        lsincometype_gid = $scope.cboIncomeType;
                        var getincometype = $scope.incometype_list.find(function (v) { return v.incometype_gid === $scope.cboIncomeType });
                        if (getincometype != null && getincometype != "")
                            lsincometype_name = getincometype.incometype_name;
                    }


                    if ($scope.cboResidenceType != undefined || $scope.cboResidenceType != null) {
                        lsresidencetype_gid = $scope.cboResidenceType;
                        var getResidenceType = $scope.residencetype_list.find(function (v) { return v.residencetype_gid === $scope.cboResidenceType });
                        if (getResidenceType != null && getResidenceType != "")
                            lsresidencetype_name = getResidenceType.residencetype_name;
                    }

                    //if ($scope.cboPropertyin != undefined || $scope.cboPropertyin != null) {
                    //    lspropertyin_gid = $scope.cboPropertyin;
                    //    var getpropertyin = $scope.propertyin_list.find(function (v) { return v.propertyin_gid === $scope.cboPropertyin });
                    //    if (getpropertyin != null && getpropertyin != "")
                    //        lspropertyin_name = getpropertyin.propertyin_name;
                    //}

                    if ($scope.cboInstitution != undefined || $scope.cboInstitution != null) {
                        lsinstitution_gid = $scope.cboInstitution;
                        var getInstitution = $scope.institutionlist.find(function (v) { return v.institution_gid === $scope.cboInstitution });
                        if (getInstitution != null && getInstitution != "")
                            lsinstitution_name = getInstitution.institution_name;
                    }


                    if ($scope.cboGroup == 'NA') {
                        groupname = 'NA';
                    }
                    if ($scope.cboInstitution == 'NA') {
                        institutionname = 'NA';
                    }

                    var panabsencereason_selectedList = [];
                    if ($scope.panabsencereason_list != null) {
                        angular.forEach($scope.panabsencereason_list, function (val) {
                            if (val.checked == true) {
                                var panabsencereason = val.panabsencereason;
                                panabsencereason_selectedList.push(panabsencereason);
                            }

                        });
                    }
                    var params = {
                        pan_status: $scope.cbopanstatus,
                        pan_no: $scope.txtpan_no,
                        aadhar_no: $scope.txtaadhar_no,
                        first_name: $scope.txtindfirst_name,
                        middle_name: $scope.txtindmiddle_name,
                        last_name: $scope.txtindlast_name,
                        individual_dob: $scope.txtindividual_dob,
                        age: $scope.txtage,

                        gender_gid: $scope.cboGender,
                        gender_name: lsgender_name,

                        designation_gid: $scope.cboindDesignation,
                        designation_name: lsdesignation_name,

                        pep_status: $scope.rdbpep_status,
                        pepverifieddate: $scope.txtpepverified_date,

                        stakeholdertype_gid: $scope.cboStakeholderType,
                        stakeholder_type: lsstakeholdertype_name,

                        propertyholder_gid: $scope.cboPropertyin,
                        propertyholder_name: lspropertyin_name,

                        maritalstatus_gid: $scope.cboMaritalStatus,
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

                        educationalqualification_gid: $scope.cboEducationalQualification,
                        educationalqualification_name: lseducationalqualification_name,

                        main_occupation: $scope.txtmain_occupation,
                        annual_income: $scope.txtannual_income,
                        monthly_income: $scope.txtmonthly_income,

                        incometype_gid: $scope.cboIncomeType,
                        incometype_name: lsincometype_name,

                        ownershiptype_gid: $scope.cboOwnershipType,
                        ownershiptype_name: $scope.OwnershipType,

                        residencetype_gid: $scope.cboResidenceType,
                        residencetype_name: lsresidencetype_name,

                        currentresidence_years: $scope.txtcurrentresidence_years,
                        branch_distance: $scope.txtbranch_distance,

                        contact_gid: contact_gid,

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
                        institution_name: lsinstitution_name,
                        application_gid: $scope.application_gid,
                        panabsencereason_selectedlist: panabsencereason_selectedList

                    }
                    var url = 'api/AgrMstSupplierOnboardEdit/SubmitIndividualEditDtl';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //if (lstab == 'add') {
                            //    $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                            //}
                            //else {
                            //    $state.go('app.AgrMstApplicationGeneralEdit');
                            //}
                            overallgeneralsummary();

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
            $scope.IndividualPANDocumentUploadEdit = function (val, val1, name) {
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
                    var url = 'api/AgrMstSupplierOnboard/PANForm60DocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $("#Editpanform60file").val('');
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $("#Editpanform60file").val('');
                            $scope.txtindividualpanform60_document = ''
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        var param = {
                            contact_gid: contact_gid
                        };
                        var url = 'api/AgrMstSupplierOnboard/GetEditPANForm60List';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.contactpanform60_list = resp.data.BuyerOnboardcontactpanform60_list;
                        });

                        unlockUI();
                    });
                }
                else {
                    alert('Please select a file.')
                }

            }

            $scope.IndividualEditPANForm60DocumentDelete = function (contact2panform60_gid) {

                var params = {
                    contact2panform60_gid: contact2panform60_gid
                }
                lockUI();
                var url = 'api/AgrMstSupplierOnboard/PANForm60Delete';
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
                    var param = {
                        contact_gid: contact_gid
                    };
                    var url = 'api/AgrMstSupplierOnboard/GetEditPANForm60List';
                    SocketService.getparams(url,param).then(function (resp) {
                        $scope.contactpanform60_list = resp.data.BuyerOnboardcontactpanform60_list;
                    });

                    unlockUI();
                });
            }
    

            $scope.individualdocument_uploadEdit = function (val, val1, name) {
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
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        var url = 'api/AgrMstSupplierOnboard/IndividualDocumentUpload';
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
                            var param = {
                                contact_gid: contact_gid
                            };
                            var url = 'api/AgrMstSupplierOnboard/GetIndividualDocListEdit';
                            SocketService.getparams(url, param).then(function (resp) {
                                $scope.uploadindividualdoc_list = resp.data.uploadBuyerOnboardindividualdoc_list;
                            });

                            unlockUI();
                        });
                    }
                    else {
                        alert('Please select a file.')
                    }
                }
            }

            $scope.individualeditdocument_delete = function (contact2document_gid) {

                var params = {
                    contact2document_gid: contact2document_gid
                }
                lockUI();
                var url = 'api/AgrMstSupplierOnboard/IndividualDocDelete';
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
                    var param = {
                        contact_gid: contact_gid
                    };
                    var url = 'api/AgrMstSupplierOnboard/GetIndividualDocListEdit';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.uploadindividualdoc_list = resp.data.uploadBuyerOnboardindividualdoc_list;
                    });
                    unlockUI();
                });
            }
    

        }

        $scope.addcompany_dtl = function () {
            $scope.selectedBuyerIndex = 1;
            $scope.editmdtab = false;
            $scope.bankaccvalidation = null;
            $scope.ifscvalidation = '';
        }

        $scope.addindividual_dtl = function () {
            $scope.selectedBuyerIndex = 2;
            $scope.editmdtab = false;
        }


        $scope.Editinstitution = function (institution_gid) {
            $scope.validateinstitution_gid = institution_gid;
            $scope.selectedBuyerIndex = 1;
            $scope.editinstituion = true;
            $scope.editmdtab = true;
            $scope.indivdiv = false;
            $scope.bankaccvalidation = null;
            $scope.ifscvalidation = '';

            var url = 'api/AgrMstApplication360/CompanyTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.companytype_list = resp.data.companytype_list;
            });

            var url = 'api/AgrMstApplication360/GetUserTypeList';
            SocketService.get(url).then(function (resp) {
                $scope.usertype_list = resp.data.usertype_list;
            });

            var url = 'api/AgrMstApplication360/AMLCategoryList';
            SocketService.get(url).then(function (resp) {
                $scope.amlcategory_list = resp.data.amlcategory_list;
            });

            var url = 'api/AgrMstApplication360/BusinessCategoryList';
            SocketService.get(url).then(function (resp) {
                $scope.businesscategory_list = resp.data.businesscategory_list;
            });

            var url = 'api/AgrMstApplication360/AssessmentAgencyList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagency_list = resp.data.assessmentagency_list;
            });

            var url = 'api/AgrMstApplication360/AssessmentAgencyRatingList';
            SocketService.get(url).then(function (resp) {
                $scope.assessmentagencyrating_list = resp.data.assessmentagencyrating_list;
            });

            var url = 'api/AgrMstApplication360/GetDesignationList';
            SocketService.get(url).then(function (resp) {
                $scope.designation_list = resp.data.designation_list;
            });

            var url = 'api/AgrTrnAppCreditUnderWriting/GetCreditAccountType';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.accounttype_list = resp.data.creditbankacc_list;
            });

            var url = 'api/AgrMstApplication360/CompanyOnboardDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.companydocument_list = resp.data.companydocument_list;
            });

            var url = 'api/AgrMstApplication360/licensetypeList';
            SocketService.get(url).then(function (resp) {
                $scope.licensetype_list = resp.data.licensetype_list;
            });

            var param = {
                institution_gid: institution_gid
            };

            var url = 'api/AgrMstSupplierOnboardEdit/InstitutionDetailsEdit';

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.txteditinstcompany_name = resp.data.company_name;
                $scope.txteditincorporation_date = resp.data.editdate_incorporation;
                $scope.txteditbusinessstart_date = resp.data.editbusinessstart_date;
                $scope.txtedityearin_business = resp.data.year_business;
                $scope.txteditmonthsin_business = resp.data.month_business;
                $scope.txteditpan_number = resp.data.companypan_no;
                $scope.txteditcin_no = resp.data.cin_no;
                $scope.txteditofficialtelephone_number = resp.data.official_telephoneno;
                $scope.txteditofficial_mailid = resp.data.official_mailid;
                $scope.cboeditCompanytype = resp.data.companytype_gid;
                $scope.cboeditStakeholdertype = resp.data.stakeholdertype_gid;
                $scope.cboeditCreditAssessmentagency = resp.data.assessmentagency_gid;
                $scope.cboeditAssessmentRating = resp.data.assessmentagencyrating_gid;
                $scope.txteditratingason_date = resp.data.editratingas_on;
                $scope.cboeditAMLCategory = resp.data.amlcategory_gid;
                $scope.cboeditBusinesscategory = resp.data.businesscategory_gid;
                $scope.txteditinstfirst_name = resp.data.contactperson_firstname;
                $scope.txteditinstmiddle_name = resp.data.contactperson_middlename;
                $scope.txteditinstlast_name = resp.data.contactperson_lastname;
                $scope.cboeditinstDesignation = resp.data.designation_gid;
                $scope.txteditstart_date = resp.data.editstart_date;
                $scope.txteditend_date = resp.data.editend_date;
                $scope.txteditmsmereg = resp.data.msme_registration;
                $scope.txteditlei = resp.data.lglentity_id;
                $scope.txteditleirenewal_date  = resp.data.editlei_renewaldate;
                $scope.txteditkin  = resp.data.kin;
                $scope.rdbeditescrow = resp.data.escrow;
                $scope.txtlastyear_turnover = resp.data.lastyear_turnover;
                if ($scope.txtlastyear_turnover != null && $scope.txtlastyear_turnover != undefined && $scope.txtlastyear_turnover != "") {
                    $scope.txtmarketValue_edit = (parseInt($scope.txtlastyear_turnover.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtmarketValue_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamountedit').innerHTML = $scope.lblamountwords;
                }
                $scope.institution_status = resp.data.institution_status;
                $scope.txtrevenue = resp.data.revenue;
                if ($scope.txtrevenue != null && $scope.txtrevenue != undefined && $scope.txtrevenue != "") {
                    $scope.txtmarketValue_edit = (parseInt($scope.txtrevenue.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtmarketValue_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount2edit').innerHTML = $scope.lblamountwords;
                }

                $scope.rdbeditinsturn_status = resp.data.urn_status;
                $scope.txteditinst_urn = resp.data.urn;
                $scope.txtprofit = resp.data.profit;
                if ($scope.txtprofit != null && $scope.txtprofit != undefined && $scope.txtprofit != "") {
                    $scope.txtmarketValue_edit = (parseInt($scope.txtprofit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtmarketValue_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount1edit').innerHTML = $scope.lblamountwords;
                }
                $scope.txtfixed_asset = resp.data.fixed_assets;
                if ($scope.txtfixed_asset != null && $scope.txtfixed_asset != undefined && $scope.txtfixed_asset != "") {
                    $scope.txtmarketValue_edit = (parseInt($scope.txtfixed_asset.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtmarketValue_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount3edit').innerHTML = $scope.lblamountwords;
                }
                $scope.txtsundrydebt_adv = resp.data.txtsundrydebt_adv;
                if ($scope.txtsundrydebt_adv != null && $scope.txtsundrydebt_adv != undefined && $scope.txtsundrydebt_adv != "") {
                    $scope.txtmarketValue_edit = (parseInt($scope.txtsundrydebt_adv.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountseperator = (parseInt($scope.txtmarketValue_edit.replace(/[^\d]+/gi, '')) || 0).toLocaleString('en-IN');
                    $scope.lblamountwords = defaultamountwordschange($scope.lblamountseperator);
                    document.getElementById('words_totalamount4edit').innerHTML = $scope.lblamountwords;
                }
                $scope.txtedittan_number = resp.data.tan_number;
                $scope.rdbincome_tax = resp.data.incometax_returnsstatus;
                $scope.stakeholder_type = resp.data.stakeholder_type;

                if (resp.data.stakeholder_type == "Applicant") {
                    $scope.stkhldr = true;
                }

                else {
                    $scope.stkhldr = false;
                }

                if (resp.data.institution_status == "Incomplete") {
                    $scope.InstitutionSubmit = true;
                    $scope.editinstituion = false;
                }
                else {
                    $scope.InstitutionSubmit = false;
                    $scope.editinstituion = true;
                }

                //if (resp.data.urn_status == 'Yes') {
                //    $scope.URN_yes = true;
                //}
                //else {
                //    $scope.URN_yes = false;
                //    $scope.txt_urn = '';
                //}

                unlockUI();
            });

            function defaultamountwordschange(input) {
                var str1 = input.replace(/,/g, '');
                var str = Math.round(str1);
                var output = Number(str).toLocaleString('en-IN');
                var lswords = cmnfunctionService.fnConvertNumbertoWord(str);
                return lswords;
            }

            var params = {
                institution_gid: institution_gid,
                tmp_status: false
            }
            var url = 'api/AgrMstSupplierOnboard/GetInstitutionRatingList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionratinglist = resp.data.MdlBuyerOnboardRatingdtl;
            });

            var url = 'api/AgrMstSupplierOnboard/Institution2bankTmpList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;

            });

            var url = 'api/AgrMstSupplierOnboardEdit/InstitutionGSTList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutiongst_list = resp.data.mstBuyerOnboardgst_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/InstitutionMobileNoList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionmobileno_list = resp.data.mstBuyerOnboardmobileno_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/InstitutionEmailAddressList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionemailaddress_list = resp.data.mstBuyerOnboardemailaddress_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/InstitutionAddressList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionaddress_list = resp.data.mstBuyerOnboardaddress_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/InstitutionLicenseList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionlicense_list = resp.data.mstBuyerOnboardlicense_list;
            });

            var url = 'api/AgrMstSupplierOnboardEdit/InstitutionDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.institutionupload_list = resp.data.institutionupload_list;
            });


            $scope.txtlastyear_turnoverchange = function () {
                var input = document.getElementById('idlastyear_turnover').value;
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
                    document.getElementById('words_totalamountedit').innerHTML = lswords_totalamount;
                }
            }

            $scope.txtrevenue_change = function () {
                var input = document.getElementById('editrevenue').value;
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
                    document.getElementById('words_totalamount2edit').innerHTML = lswords_totalamount;
                }
            }

            $scope.txtfixed_assetchange = function () {
                var input = document.getElementById('editfixed_asset').value;
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
                    document.getElementById('words_totalamount3edit').innerHTML = lswords_totalamount;
                }
            }

            $scope.txtsundrydebt_advchange = function () {
                var input = document.getElementById('editsundrydebt_adv').value;
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
                    document.getElementById('words_totalamount4edit').innerHTML = lswords_totalamount;
                }
            }

            $scope.txtprofit_change = function () {
                var input = document.getElementById('editprofit').value;
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
                    document.getElementById('words_totalamount1edit').innerHTML = lswords_totalamount;
                }
            }



            //$scope.crediteditbankacctdtl_delete = function (institution2bankdtl_gid, institution_gid) {
            //    var params = {
            //        institution2bankdtl_gid: institution2bankdtl_gid,
            //        institution_gid: institution_gid
            //    }
            //    var url = 'api/AgrMstSupplierOnboard/DeleteinstitutionBankAcc';
            //    SocketService.getparams(url, params).then(function (resp) {
            //        unlockUI();
            //        if (resp.data.status == true) {
            //            Notify.alert(resp.data.message, {
            //                status: 'success',
            //                pos: 'top-center',
            //                timeout: 3000
            //            });
            //            bankTmp_list();
            //        }
            //        else {
            //            Notify.alert(resp.data.message, {
            //                status: 'warning',
            //                pos: 'top-center',
            //                timeout: 3000
            //            });
            //            bankTmp_list();
            //        }


            //    });
            //}

            $scope.InstitutionEditDocumentUpload = function (val, val1, name) {
                if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbocompanydocumentname == null) || ($scope.cbocompanydocumentname == '') || ($scope.cbocompanydocumentname == undefined)) {
                    $("#institutioneditfile").val('');
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
                    frm.append('document_id', $scope.txtdocument_id);
                    frm.append('project_flag', "documentformatonly");
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        lockUI();
                        var url = 'api/AgrMstSupplierOnboard/InstitutionDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            //$scope.institutionupload_list = resp.data.BuyerOnboardinstitutionupload_list;
                            unlockUI();

                            $("#institutioneditfile").val('');
                            $scope.cbocompanydocumentname = "";
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
                            companydocument_list();
                        });
                    }
                    else {
                        alert('Document is not Available..!');
                        return;
                    }
                }
            }

            function companydocument_list() {

                var param = {
                    institution_gid: institution_gid
                };
                var url = 'api/AgrMstSupplierOnboard/Getcompanyeditdocumentlist';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutionupload_list = resp.data.BuyerOnboardinstitutionupload_list;
                });
            }

            function licenseedit_list() {
                var param = {
                    institution_gid: institution_gid
                };
                var url = 'api/AgrMstSupplierOnboardEdit/InstitutionLicenseList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutionlicense_list = resp.data.mstBuyerOnboardlicense_list;

                });
            }

            //suppliergeteditPANbasedGST

            $scope.geteditPANbasedGST = function (institution_gid) {
                if ($scope.txteditpan_number.length == 10) {
                    $scope.Alreadyaddedpanaadhar = false;
                    if ($scope.institutiongst_list != null) {
                        var paramsdel =
                        {
                            institution_gid: institution_gid
                        }
                        var url = 'api/AgrMstSupplierOnboard/DeleteGSTInstitution';
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
                        pan: $scope.txteditpan_number
                    }
                    var url = 'api/AgrSuprKyc/GSTSBPAN';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.statusCode == 101) {
                            $scope.panvalidation = true;
                            const GstArray = resp.data.result;

                            var param = {
                                pan: $scope.txteditpan_number
                            }
                            var url = 'api/AgrSuprKyc/PANNumber';
                            lockUI();
                            SocketService.post(url, param).then(function (resp) {
                                unlockUI();
                                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                    $scope.txteditinstcompany_name = resp.data.result.name;
                                } else {
                                    Notify.alert(resp.data.message, 'warning')
                                }
                            });


                            var params = {
                                GSTArray: GstArray
                            }

                            var url = 'api/AgrMstSupplierOnboard/PostInstitutionGSTList';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    institutioneditgstlist();
                                }
                                else {
                                    Notify.alert('Error occured while adding the fetched GST Details..!', 'warning');
                                }

                            });

                        } else if (resp.data.statusCode == 103) {
                            var param = {
                                pan: $scope.txteditpan_number
                            }
                            var url = 'api/AgrSuprKyc/PANNumber';
                            lockUI();
                            SocketService.post(url, param).then(function (resp) {
                                unlockUI();
                                if (resp.data.result.name != "" && resp.data.result.name != undefined) {
                                    $scope.txteditinstcompany_name = resp.data.result.name;
                                    $scope.panvalidation = true;
                                    institutioneditgstlist();
                                } else if (resp.data.result.name == "" || resp.data.result.name == undefined) {
                                    $scope.panvalidation = false;
                                    Notify.alert('PAN is not verified..!', 'warning');
                                    institutioneditgstlist();
                                } else {
                                    Notify.alert(resp.data.message, 'warning')
                                }

                            });

                        } else {
                            Notify.alert(resp.data.message, 'warning')
                        }
                    });
                    if ($scope.cboeditStakeholdertype != undefined || $scope.cboeditStakeholdertype != null) {
                        var getStakeholderType = $scope.usertype_list.find(function (v) {
                            return v.usertype_gid === $scope.cboeditStakeholdertype
                        });
                        if (getStakeholderType != null && getStakeholderType != "") {
                            if (getStakeholderType.user_type != "") {
                                lockUI();
                                var params = {
                                    pan_no: $scope.txteditpan_number,
                                    aadhar_no: 'No',
                                    institution_gid: institution_gid,
                                    onboard_gid: $scope.application_gid,
									stakeholder_type: getStakeholderType.user_type
                                }
                                var url = 'api/AgrMstSupplierOnboard/GetOnboardValidatePANAadhar';
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
                        else {
                            $scope.Alreadyaddedpanaadhar = false;
                        }
                    }

                }
            }


            function institutioneditgstlist() {
                var param = {
                    institution_gid: institution_gid
                };
                var url = 'api/AgrMstSupplierOnboardEdit/InstitutionGSTList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutiongst_list = resp.data.mstBuyerOnboardgst_list;
                });
            }

            function institutioneditmobilenolist() {
                var param = {
                    institution_gid: institution_gid
                };
                var url = 'api/AgrMstSupplierOnboardEdit/InstitutionMobileNoList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutionmobileno_list = resp.data.mstBuyerOnboardmobileno_list;
                });
            }

            function institutioneditmail_list() {
                var param = {
                    institution_gid: institution_gid
                };
                var url = 'api/AgrMstSupplierOnboardEdit/InstitutionEmailAddressList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutionemailaddress_list = resp.data.mstBuyerOnboardemailaddress_list;
                });
            }

            function institutioneditaddresslist() {
                var param = {
                    institution_gid: institution_gid
                };
                var url = 'api/AgrMstSupplierOnboardEdit/InstitutionAddressList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.institutionaddress_list = resp.data.mstBuyerOnboardaddress_list;
                });
            }

            $scope.institutioneditmail_delete = function (institution2email_gid) {
                var params =
                    {
                        institution2email_gid: institution2email_gid
                    }
                var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionEmailAddress';
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

                    institutioneditmail_list();
                });

            }

            $scope.deleteeditinstitution_address = function (institution2address_gid) {
                var params =
                    {
                        institution2address_gid: institution2address_gid
                    }
                var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionAddressDetail';
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
                    institutioneditaddresslist();
                });

            }

            $scope.institutioneditmobileno_delete = function (institution2mobileno_gid) {
                var params =
                    {
                        institution2mobileno_gid: institution2mobileno_gid
                    }
                var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionMobileNo';
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
                    institutioneditmobilenolist();
                });

            }

            $scope.institutioneditdocument_delete = function (institution2documentupload_gid) {
                lockUI();
                var params = {
                    institution2documentupload_gid: institution2documentupload_gid
                }
                var url = 'api/AgrMstSupplierOnboard/InstitutionDocumentDelete';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.institutionupload_list = resp.data.BuyerOnboardinstitutionupload_list;
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

            $scope.deleteeditinstitution_address = function (institution2address_gid) {
                var params =
                    {
                        institution2address_gid: institution2address_gid
                    }
                var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionAddressDetail';
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
                    institutioneditaddresslist();
                });

            }

            $scope.gstnumberedit_add = function () {
                if (($scope.rdbgstregistered == undefined) || ($scope.rdbgstregistered == '') || ($scope.txtgst_state == undefined) || ($scope.txtgst_state == '') || ($scope.txtgst_number == undefined) || ($scope.txtgst_number == '')) {
                    Notify.alert('Select GST Registered Status/ Select GST State / Enter GST Number', 'warning');
                }
                else {
                    var params = {
                        gst_state: $scope.txtgst_state,
                        gst_no: $scope.txtgst_number,
                        gst_registered: $scope.rdbgstregistered
                    }
                    var url = 'api/AgrMstSupplierOnboard/PostInstitutionGST';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            institutioneditgstlist();
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

            $scope.institutioneditmobileno_add = function () {
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
                        whatsapp_no: $scope.rdbprimarywhatsapp_no,
                        institution_gid: institution_gid
                    }
                    var url = 'api/AgrMstSupplierOnboard/PostInstitutionMobileNo';
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
                        institutioneditmobilenolist();
                        $scope.txtmobile_no = '';
                        $scope.rdbprimarymobile_no = '';
                        $scope.rdbprimarywhatsapp_no = '';
                    });
                }
            }

            $scope.institutioneditmaildetails_add = function () {
                if (($scope.txtinstitutionmail_address == undefined) || ($scope.txtinstitutionmail_address == '') || ($scope.rdbinstitutiomaildetails == undefined)) {
                    Notify.alert('Enter Mail ID / Select Primary Status', 'warning');
                }
                else {
                    var params = {
                        email_address: $scope.txtinstitutionmail_address,
                        primary_status: $scope.rdbinstitutiomaildetails,
                        institution_gid: institution_gid
                    }
                    var url = 'api/AgrMstSupplierOnboard/PostInstitutionEmailAddress';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            institutioneditmail_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        institutioneditmail_list();
                    });
                    $scope.txtinstitutionmail_address = '';
                    $scope.rdbinstitutiomaildetails = '';
                }
            }

            $scope.licensetypeedit_add = function () {

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
                        institution_gid: institution_gid
                    }
                    var url = 'api/AgrMstSupplierOnboard/PostInstitutionLicenseDetail';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            licenseedit_list();
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


            $scope.addaddress = function () {
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
                            longitude: $scope.txtlongitude,
                            institution_gid: institution_gid
                        }

                        var url = 'api/AgrMstSupplierOnboard/PostInstitutionAddressDetail';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {

                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                institutioneditaddresslist();
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

            function containsSpecialChars(str) {
                try {
                    const specialChars = str.match(/-/gi);
                    return true;
                }
                catch (e) {
                    return false;
                }
            }

            $scope.creditbankacctdtladd = function () {

                if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') ||
                    ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
                ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                    ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.rdbprimarystatus == undefined) || ($scope.rdbprimarystatus == '') || ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
                    Notify.alert('Enter All Mandatory Fields', 'warning');
                }
                else {
                    if ($scope.rdbJoint_Account == 'Yes') {

                        if (($scope.txtjointacctholder_name == undefined) || ($scope.txtjointacctholder_name == '')) {
                            Notify.alert('Enter Joint Account Holder Name', 'warning');
                        }
                        else {
                            var params = {
                                //application_gid: $scope.application_gid,
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
                                primary_status: $scope.rdbprimarystatus,
                                institution_gid: institution_gid
                            }
                            var url = 'api/AgrMstSupplierOnboard/PostInstitutionBank';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    //$scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;
                                    $scope.credituploaddocument_list = null;
                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    bankTmp_list();
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
                                //activate();
                                bankTmp_list();
                            });
                        }
                    }
                    else {
                        var params = {
                            //application_gid: application_gid,
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
                            primary_status: $scope.rdbprimarystatus,
                            institution_gid: institution_gid
                        }
                        var url = 'api/AgrMstSupplierOnboard/PostInstitutionBank';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                //$scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;

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
                            bankTmp_list();

                        });
                    }

                }
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

                    var url = 'api/AgrMstSupplierOnboard/EditGetCreditBankAccDtl';

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
                            var url = 'api/AgrSuprKyc/IfscVerification';
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
                        if (($scope.txtIFSC_Code == undefined) || ($scope.txtIFSC_Code == '') || ($scope.txtbankacct_no == undefined) || ($scope.txtbankacct_no == '') || ($scope.txtBank_Address == undefined) || ($scope.txtBank_Address == '') ||
                           ($scope.txtconfirmbankacct_no == undefined) || ($scope.txtconfirmbankacct_no == '') || ($scope.txtacctholder_name == undefined) || ($scope.txtacctholder_name == '') ||
                       ($scope.cboAccountType == undefined) || ($scope.cboAccountType == '') || ($scope.rdbJoint_Account == undefined) || ($scope.rdbJoint_Account == '') ||
                            ($scope.rdbCheque_Book == undefined) || ($scope.rdbCheque_Book == '') || ($scope.rdbprimarystatus == undefined) || ($scope.rdbprimarystatus == '') ||  ($scope.txtBranch_Name == undefined) || ($scope.txtBranch_Name == '')) {
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
                                        primary_status: $scope.rdbprimarystatus,
                                        institution_gid: institution_gid
                                    }
                                    var url = 'api/AgrMstSupplierOnboard/UpdateInstitutionBankAccDtl';
                                    lockUI();
                                    SocketService.post(url, params).then(function (resp) {
                                        unlockUI();
                                        if (resp.data.status == true) {

                                            Notify.alert(resp.data.message, {
                                                status: 'success',
                                                pos: 'top-center',
                                                timeout: 3000
                                            });
                                            bankTmp_list();
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
                                var url = 'api/AgrMstSupplierOnboard/UpdateInstitutionBankAccDtl';
                                lockUI();
                                SocketService.post(url, params).then(function (resp) {
                                    unlockUI();
                                    if (resp.data.status == true) {

                                        Notify.alert(resp.data.message, {
                                            status: 'success',
                                            pos: 'top-center',
                                            timeout: 3000
                                        });
                                        bankTmp_list();
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


            $scope.crediteditbankacctdtl_delete = function (institution2bankdtl_gid, institution_gid) {
                var params = {
                    institution2bankdtl_gid: institution2bankdtl_gid,
                    institution_gid: institution_gid
                }
                var url = 'api/AgrMstSupplierOnboard/DeleteinstitutionBankAcc';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        //$scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;
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
                        bankTmp_list();
                    }


                });
            }


            function bankTmp_list() {

                var param = {
                    institution_gid: institution_gid
                };

                var url = 'api/AgrMstSupplierOnboard/Institution2bankTmpList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.creditbankacc_list = resp.data.BuyerOnboardinstitution2bankacc_list;

                });

            }

            $scope.institutioneditgst_delete = function (institution2branch_gid) {
                var params =
                    {
                        institution2branch_gid: institution2branch_gid
                    }
                var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionGST';
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
                    institutioneditgstlist();
                });

            }

            $scope.headoffice_confirm = function (institution2branch_gid, validateinstitution_gid) {
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
                            institution_gid: validateinstitution_gid
                        }
                        lockUI();
                        var url = 'api/AgrMstSupplierOnboardEdit/UpdateGSTHeadOffice';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                                institutioneditgstlist();
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


            $scope.verify_editgstverification = function (gst_no, institution2branch_gid) {
                $scope.gst_no = gst_no;
                var params = {
                    gstin: gst_no,
                    institution2branch_gid: institution2branch_gid,
                    application_gid: $scope.application_gid
                }
                lockUI();

                var url = 'api/AgrSuprKyc/GSTVerification';
                SocketService.post(url, params).then(function (resp) {

                    unlockUI();
                    //list();
                    if (resp.data.result.gstin != "" && resp.data.result.gstin != undefined) {
                        $scope.gstverification = true;
                    }
                    else if (resp.data.result.gstin == "" || resp.data.result.gstin == undefined) {
                        $scope.gstverification = false;
                        $scope.gstverify_status = 'notverify';
                    } else {
                        Notify.alert(resp.data.message, 'warning')
                    }

                    institutioneditgstlist();
                });
            }


            $scope.gsteditaddressview = function (institution2branch_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/gstaddressview.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'lg'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    var params = {
                        institution2branch_gid: institution2branch_gid,
                    }

                    var url = 'api/AgrMstSuprAPIVerifications/GSTVerificationViewDetails';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();

                        $scope.gstin = resp.data.result.gstin;


                        $scope.legalname = resp.data.result.lgnm;
                        $scope.tradename = resp.data.result.tradeNam;
                        $scope.status = resp.data.result.sts;
                        $scope.constitutionofbusiness = resp.data.result.ctb;

                        var nba = resp.data.result.nba;
                        var natureofbusiness = "";
                        for (var i = 0; i < nba.length; i++) {
                            natureofbusiness = natureofbusiness.concat(nba[i], ",");
                        }
                        natureofbusiness = natureofbusiness.replace(/,\s*$/, "");
                        $scope.nob = natureofbusiness;

                        $scope.taxpayertype = resp.data.result.dty;
                        $scope.compliancerating = resp.data.result.cmpRt;

                        $scope.centraljusridiction = resp.data.result.ctj;
                        $scope.centraljusridiction_code = resp.data.result.ctjCd;
                        $scope.statejurisdiction = resp.data.result.stj;
                        $scope.statejurisdiction_code = resp.data.result.stjCd;



                        $scope.dateofregistration = resp.data.result.rgdt;
                        $scope.dateofcancellation = resp.data.result.cxdt;
                        $scope.lastupdateddate = resp.data.result.lstupdt;

                        $scope.contact_mobnum = resp.data.result.contacted.mobNum;
                        $scope.contact_email = resp.data.result.contacted.email;
                        $scope.contact_name = resp.data.result.contacted.name;

                        $scope.contactdetail = "defined";
                        if ($scope.contact_mobnum == null && $scope.contact_email == null && $scope.contact_name == null) {
                            $scope.contactdetail = null;
                        }




                        $scope.pradr_adr = resp.data.result.pradr.adr;
                        $scope.pradr_ntr = resp.data.result.pradr.ntr;
                        $scope.pradr_em = resp.data.result.pradr.em;
                        $scope.pradr_mb = resp.data.result.pradr.mb;
                        $scope.pradr_lastUpdatedDate = resp.data.result.pradr.lastUpdatedDate;


                        $scope.adadr_list = resp.data.result.adadr;

                        $scope.additionaladdress = "defined";
                        if ($scope.adadr_list.length == 0) {
                            $scope.additionaladdress = null;
                        }

                    });



                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };


                }

            }



            $scope.submit_editinstitution = function () {

                licenseedit_list();

                try {
                    if ($scope.txteditleirenewal_date.split("-"))
                        $scope.txteditleirenewal_date = $scope.txteditleirenewal_date.split("-").reverse().join("-");
                }
                catch (e) { $scope.txteditleirenewal_date = $scope.txteditleirenewal_date  }

                //if ($scope.rdbeditinsturn_status == 'Yes' && ($scope.txteditinst_urn == '' || $scope.txteditinst_urn == undefined || $scope.txteditinst_urn == null)) {
                //    Notify.alert('Kindly Enter URN', 'warning')
                //}
                //else
                    if ($scope.cboeditStakeholdertype == null || $scope.cboeditStakeholdertype == '' || $scope.cboeditStakeholdertype == undefined) {
                    Notify.alert('Kindly select Stakeholder Type', 'warning')
                }
                else if ($scope.txteditstart_date > $scope.txteditend_date) {
                    Notify.alert('Company End Date Is Less Then Start Date', 'warning');
                }
                else if ($scope.institutionlicense_list == '' || $scope.institutionlicense_list == undefined || $scope.institutionlicense_list == null) {
                    Notify.alert('Add Atleast one License detail', 'warning');
                    }
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
                    var lsstakeholdertype_name = '';
                    var lsstakeholdertype_gid = '';


                    var companytypename = $('#companytype_name :selected').text();
                    var usertype = $('#user_type :selected').text();
                    var assessmentagencyname = $('#assessmentagency_name :selected').text();
                    var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                    var amlcategoryname = $('#amlcategory_name :selected').text();
                    var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();


                    if ($scope.cboeditinstDesignation != undefined || $scope.cboeditinstDesignation != null) {
                        lsdesignation_gid = $scope.cboeditinstDesignation;
                        var getdesginationname = $scope.designation_list.find(function (v) {
                            return v.designation_gid === $scope.cboeditinstDesignation
                        });
                        if (getdesginationname != null && getdesginationname != "")
                            lsdesignation_type = getdesginationname.designation_type;
                    }

                    if ($scope.cboeditStakeholdertype != undefined || $scope.cboeditStakeholdertype != null) {
                        lsstakeholdertype_gid = $scope.cboeditStakeholdertype;
                        var getStakeholderType = $scope.usertype_list.find(function (v) {
                            return v.usertype_gid === $scope.cboeditStakeholdertype
                        });
                        if (getStakeholderType != null && getStakeholderType != "")
                            lsstakeholdertype_name = getStakeholderType.user_type;
                    }

                    if ($scope.cboeditCompanytype != undefined || $scope.cboeditCompanytype != null) {
                        lscompanytype_gid = $scope.cboeditCompanytype;
                        var getcboCompanytype = $scope.companytype_list.find(function (v) {
                            return v.companytype_gid === $scope.cboeditCompanytype
                        });
                        if (getcboCompanytype != null && getcboCompanytype != "")
                            lscompanytype_name = getcboCompanytype.companytype_name;
                    }

                    if ($scope.cboeditAMLCategory != undefined || $scope.cboeditAMLCategory != null) {
                        lsamlcategory_gid = $scope.cboeditAMLCategory;
                        var getcboeditAMLCategory = $scope.amlcategory_list.find(function (v) {
                            return v.amlcategory_gid === $scope.cboeditAMLCategory
                        });
                        if (getcboeditAMLCategory != null && getcboeditAMLCategory != "")
                            lsamlcategory_name = getcboeditAMLCategory.amlcategory_name;
                    }

                    if ($scope.cboeditBusinesscategory != undefined || $scope.cboeditBusinesscategory != null) {
                        lsbusinesscategory_gid = $scope.cboeditBusinesscategory;
                        var getbusinesscategoryname = $scope.businesscategory_list.find(function (v) {
                            return v.businesscategory_gid === $scope.cboeditBusinesscategory
                        });
                        if (getbusinesscategoryname != null && getbusinesscategoryname != "")
                            lsbusinesscategory_name = getbusinesscategoryname.businesscategory_name;
                    }
                        if ($scope.institutiongst_list != null) {
                            var Gstflag = 'Yes';
                        }
                        else if ($scope.institutiongst_list == null || $scope.institutiongst_list == '' || $scope.institutiongst_list == undefined) {
                            var Gstflag = 'No';
                        }
                    var params = {
                        company_name: $scope.txteditinstcompany_name,
                        dateincorporation: $scope.txteditincorporation_date,
                        businessstart_date: $scope.txteditbusinessstart_date,
                        year_business: $scope.txtedityearin_business,
                        month_business: $scope.txteditmonthsin_business,
                        companypan_no: $scope.txteditpan_number,
                        cin_no: $scope.txteditcin_no,
                        official_telephoneno: $scope.txteditofficialtelephone_number,
                        official_mailid: $scope.txteditofficial_mailid,
                        companytype_gid: $scope.cboeditCompanytype,
                        companytype_name: lscompanytype_name,
                        stakeholdertype_gid: $scope.cboeditStakeholdertype,
                        stakeholder_type: lsstakeholdertype_name,
                        assessmentagency_gid: $scope.cboeditCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboeditAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txteditratingason_date,
                        amlcategory_gid: $scope.cboeditAMLCategory,
                        amlcategory_name: lsamlcategory_name,
                        businesscategory_gid: $scope.cboeditBusinesscategory,
                        businesscategory_name: lsbusinesscategory_name,
                        contactperson_firstname: $scope.txteditinstfirst_name,
                        contactperson_middlename: $scope.txteditinstmiddle_name,
                        contactperson_lastname: $scope.txteditinstlast_name,
                        designation_gid: $scope.cboeditinstDesignation,
                        designation: lsdesignation_type,
                        startdate: $scope.txteditstart_date,
                        enddate: $scope.txteditend_date,
                        escrow: $scope.rdbeditescrow,
                        tan_number: $scope.txtedittan_number,
                        incometax_returnsstatus: $scope.rdbincome_tax,
                        revenue: $scope.txtrevenue,
                        profit: $scope.txtprofit,
                        fixed_assets: $scope.txtfixed_asset,
                        sundrydebt_adv: $scope.txtsundrydebt_adv,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        urn_status: $scope.rdbeditinsturn_status,
                        urn: $scope.txteditinst_urn,
                        msme_registration:$scope.txteditmsmereg,
                        lglentity_id:$scope.txteditlei,
                        lei_renewaldate:$scope.txteditleirenewal_date,
                        kin:$scope.txteditkin,
                        institution_gid: institution_gid,
                        application_gid: $scope.application_gid,
                        Gstflag: Gstflag
                    }
                    var url = 'api/AgrMstSupplierOnboardEdit/SubmitInstitutionEditDtl';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            overallgeneralsummary();
                            $scope.txteditinstcompany_name = '';
                            $scope.txteditincorporation_date = '';
                            $scope.txteditbusinessstart_date = '';
                            $scope.txtedityearin_business = '';
                            $scope.txteditmonthsin_business = '';
                            $scope.txteditpan_number = '';
                            $scope.txteditcin_no = '';
                            $scope.txteditofficialtelephone_number = '';
                            $scope.txteditofficial_mailid = '';
                            $scope.txteditratingason_date = '';
                            $scope.txteditinstfirst_name = '';
                            $scope.txteditmiddle_name = '';
                            $scope.txteditlast_name = '';
                            $scope.txteditstart_date = '';
                            $scope.txteditend_date = '';
                            $scope.rdbediteditescrow = '';
                            $scope.txtedittan_number = '';
                            $scope.rdbeditincome_tax = '';
                            $scope.txteditrevenue = '';
                            $scope.txteditprofit = '';
                            $scope.txteditfixed_asset = '';
                            $scope.txteditsundrydebt_adv = '';
                            $scope.txteditlastyear_turnover = '';
                            $scope.rdbeditinsturn_status = '';
                            $scope.txteditinst_urn = '';
                            $scope.cboCompanytype = '';
                            $scope.cboStakeholdertype = '';
                            $scope.cboCreditAssessmentagency = '';
                            $scope.cboAssessmentRating = '';
                            $scope.cboAMLCategory = '';
                            $scope.cboBusinesscategory = '';
                            $scope.cboinstDesignation = '';
                            $scope.institutiongst_list = null;
                            $scope.institutionratinglist = null;
                            $scope.institutionmobileno_list = null;
                            $scope.institutionmaildetails_list = null;
                            $scope.institutionaddresslist = null;
                            $scope.creditbankacc_list = null;
                            $scope.institutionupload_list = null;
                            $scope.institutionlicense_list = null;


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

            $scope.update_institution = function () {

                licenseedit_list();

                try {
                    if ($scope.txteditleirenewal_date.split("-"))
                        $scope.txteditleirenewal_date = $scope.txteditleirenewal_date.split("-").reverse().join("-");
                }
                catch (e) { $scope.txteditleirenewal_date = $scope.txteditleirenewal_date  }

                var start_date = $scope.txteditstart_date;
                var end_date = $scope.txteditend_date;

                var checkissuedate = containsSpecialChars($scope.txteditstart_date);
                if (checkissuedate == false) {
                    var d = new Date($scope.txteditstart_date);
                    start_date = Date.parse(d.getFullYear() + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + ("0" + d.getDate()).slice(-2));
                }
                else {
                    start_date = Date.parse(start_date.split("-").reverse().join("-"));
                }
                var checkdate = containsSpecialChars($scope.txteditend_date);
                if (checkdate == false) {
                    var d = new Date($scope.txteditend_date);
                    end_date = Date.parse(d.getFullYear() + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" + ("0" + d.getDate()).slice(-2));
                }
                else {
                    end_date = Date.parse(end_date.split("-").reverse().join("-"));
                }

                //if ($scope.rdburn_status == 'Yes' && ($scope.txtedit_urn == '' || $scope.txtedit_urn == undefined || $scope.txtedit_urn == null)) {
                //    Notify.alert('Kindly Enter URN', 'warning')
                //}
                //else
                    if ($scope.cboeditStakeholdertype == null || $scope.cboeditStakeholdertype == '' || $scope.cboeditStakeholdertype == undefined) {
                    Notify.alert('Kindly select Stakeholder Type', 'warning')
                }
                else if (start_date > end_date) {
                    Notify.alert('Company End Date  Is Less Then Start Date ', 'warning');
                }
                else if ($scope.institutionlicense_list == '' || $scope.institutionlicense_list == undefined || $scope.institutionlicense_list == null) {
                    Notify.alert('Add Atleast one License detail', 'warning');
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
                    var lsstakeholdertype_name = '';
                    var lsstakeholdertype_gid = '';


                    var companytypename = $('#companytype_name :selected').text();
                    var usertype = $('#user_type :selected').text();
                    var assessmentagencyname = $('#assessmentagency_name :selected').text();
                    var assessmentagencyratingname = $('#assessmentagencyrating_name :selected').text();
                    var amlcategoryname = $('#amlcategory_name :selected').text();
                    var businesscategoryname = $('#businesscategory_name :selected').text();
                    var designationtype = $('#designation_type :selected').text();


                    if ($scope.cboeditinstDesignation != undefined || $scope.cboeditinstDesignation != null) {
                        lsdesignation_gid = $scope.cboeditinstDesignation;
                        var getdesginationname = $scope.designation_list.find(function (v) {
                            return v.designation_gid === $scope.cboeditinstDesignation
                        });
                        if (getdesginationname != null && getdesginationname != "")
                            lsdesignation_type = getdesginationname.designation_type;
                    }

                    if ($scope.cboeditStakeholdertype != undefined || $scope.cboeditStakeholdertype != null) {
                        lsstakeholdertype_gid = $scope.cboeditStakeholdertype;
                        var getStakeholderType = $scope.usertype_list.find(function (v) {
                            return v.usertype_gid === $scope.cboeditStakeholdertype
                        });
                        if (getStakeholderType != null && getStakeholderType != "")
                            lsstakeholdertype_name = getStakeholderType.user_type;
                    }

                    if ($scope.cboeditCompanytype != undefined || $scope.cboeditCompanytype != null) {
                        lscompanytype_gid = $scope.cboeditCompanytype;
                        var getcboCompanytype = $scope.companytype_list.find(function (v) {
                            return v.companytype_gid === $scope.cboeditCompanytype
                        });
                        if (getcboCompanytype != null && getcboCompanytype != "")
                            lscompanytype_name = getcboCompanytype.companytype_name;
                    }

                    if ($scope.cboeditAMLCategory != undefined || $scope.cboeditAMLCategory != null) {
                        lsamlcategory_gid = $scope.cboeditAMLCategory;
                        var getcboeditAMLCategory = $scope.amlcategory_list.find(function (v) {
                            return v.amlcategory_gid === $scope.cboeditAMLCategory
                        });
                        if (getcboeditAMLCategory != null && getcboeditAMLCategory != "")
                            lsamlcategory_name = getcboeditAMLCategory.amlcategory_name;
                    }

                    if ($scope.cboeditBusinesscategory != undefined || $scope.cboeditBusinesscategory != null) {
                        lsbusinesscategory_gid = $scope.cboeditBusinesscategory;
                        var getbusinesscategoryname = $scope.businesscategory_list.find(function (v) {
                            return v.businesscategory_gid === $scope.cboeditBusinesscategory
                        });
                        if (getbusinesscategoryname != null && getbusinesscategoryname != "")
                            lsbusinesscategory_name = getbusinesscategoryname.businesscategory_name;
                        }

                        if ($scope.institutiongst_list != null) {
                            var Gstflag = 'Yes';
                        }
                        else if ($scope.institutiongst_list == null || $scope.institutiongst_list == '' || $scope.institutiongst_list == undefined) {
                            var Gstflag = 'No';
                        }

                    var params = {
                        company_name: $scope.txteditinstcompany_name,
                        dateincorporation: $scope.txteditincorporation_date,
                        businessstart_date: $scope.txteditbusinessstart_date,
                        year_business: $scope.txtedityearin_business,
                        month_business: $scope.txteditmonthsin_business,
                        companypan_no: $scope.txteditpan_number,
                        cin_no: $scope.txteditcin_no,
                        official_telephoneno: $scope.txteditofficialtelephone_number,
                        official_mailid: $scope.txteditofficial_mailid,
                        companytype_gid: $scope.cboeditCompanytype,
                        companytype_name: lscompanytype_name,
                        stakeholdertype_gid: $scope.cboeditStakeholdertype,
                        stakeholder_type: lsstakeholdertype_name,
                        assessmentagency_gid: $scope.cboeditCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboeditAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txteditratingason_date,
                        amlcategory_gid: $scope.cboeditAMLCategory,
                        amlcategory_name: lsamlcategory_name,
                        businesscategory_gid: $scope.cboeditBusinesscategory,
                        businesscategory_name: lsbusinesscategory_name,
                        contactperson_firstname: $scope.txteditinstfirst_name,
                        contactperson_middlename: $scope.txteditinstmiddle_name,
                        contactperson_lastname: $scope.txteditinstlast_name,
                        designation_gid: $scope.cboeditinstDesignation,
                        designation: lsdesignation_type,
                        startdate: $scope.txteditstart_date,
                        enddate: $scope.txteditend_date,
                        escrow: $scope.rdbeditescrow,
                        tan_number: $scope.txtedittan_number,
                        incometax_returnsstatus: $scope.rdbincome_tax,
                        revenue: $scope.txtrevenue,
                        profit: $scope.txtprofit,
                        fixed_assets: $scope.txtfixed_asset,
                        sundrydebt_adv: $scope.txtsundrydebt_adv,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        urn_status: $scope.rdbeditinsturn_status,
                        urn: $scope.txteditinst_urn,
                        msme_registration:$scope.txteditmsmereg,
                        lglentity_id:$scope.txteditlei,
                        lei_renewaldate:$scope.txteditleirenewal_date,
                        kin:$scope.txteditkin,
                        institution_gid: institution_gid,
                        application_gid: $scope.application_gid,
                        Gstflag: Gstflag
                    }
                    var url = 'api/AgrMstSupplierOnboardEdit/UpdateInstitutionDtl';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            //$location.url('app/AgrMstbyrOnboardInfoEdit?application_gid=' + $scope.application_gid);
                            overallgeneralsummary();
                            //if (lstab == 'add') {
                            //    $location.url('app/AgrMstApplicationGeneralAdd?lstab=' + lstab);
                            //}
                            //else {
                            //    $state.go('app.AgrMstApplicationGeneralEdit');
                            //}
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

            $scope.institutioneditlicense_delete = function (institution2licensedtl_gid) {
                var params = {
                    institution2licensedtl_gid: institution2licensedtl_gid
                }
                var url = 'api/AgrMstSupplierOnboard/DeleteInstitutionLicenseDetail';
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
                    licenseedit_list();
                });

            }


            $scope.save_editinstitution = function () {

                licenseedit_list();

                try {
                    if ($scope.txteditleirenewal_date.split("-"))
                        $scope.txteditleirenewal_date = $scope.txteditleirenewal_date.split("-").reverse().join("-");
                }
                catch (e) { $scope.txteditleirenewal_date = $scope.txteditleirenewal_date  }

                //make this happen
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
                var lsstakeholdertype_name = '';
                var lsstakeholdertype_gid = '';

                //if ($scope.rdbediturn_status == 'Yes' && ($scope.txtedit_urn == '' || $scope.txtedit_urn == undefined || $scope.txtedit_urn == null)) {
                //    Notify.alert('Kindly Enter URN', 'warning')
                //}
                //else
                    if ($scope.cboeditStakeholdertype == null || $scope.cboeditStakeholdertype == '' || $scope.cboeditStakeholdertype == undefined) {
                    Notify.alert('Kindly select Stakeholder Type', 'warning')
                }
                else if ($scope.txteditstart_date > $scope.txteditend_date) {
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

                        if ($scope.cboeditinstDesignation != undefined || $scope.cboeditinstDesignation != null) {
                            lsdesignation_gid = $scope.cboeditinstDesignation;
                            var getdesginationname = $scope.designation_list.find(function (v) {
                                return v.designation_gid === $scope.cboeditinstDesignation
                            });
                            if (getdesginationname != null && getdesginationname != "")
                                lsdesignation_type = getdesginationname.designation_type;
                        }

                        if ($scope.cboeditStakeholdertype != undefined || $scope.cboeditStakeholdertype != null) {
                            lsstakeholdertype_gid = $scope.cboeditStakeholdertype;
                            var getStakeholderType = $scope.usertype_list.find(function (v) {
                                return v.usertype_gid === $scope.cboeditStakeholdertype
                            });
                            if (getStakeholderType != null && getStakeholderType != "")
                                lsstakeholdertype_name = getStakeholderType.user_type;
                        }

                        if ($scope.cboeditCompanytype != undefined || $scope.cboeditCompanytype != null) {
                            lscompanytype_gid = $scope.cboeditCompanytype;
                            var getcboCompanytype = $scope.companytype_list.find(function (v) {
                                return v.companytype_gid === $scope.cboeditCompanytype
                            });
                            if (getcboCompanytype != null && getcboCompanytype != "")
                                lscompanytype_name = getcboCompanytype.companytype_name;
                        }

                        if ($scope.cboeditAMLCategory != undefined || $scope.cboeditAMLCategory != null) {
                            lsamlcategory_gid = $scope.cboeditAMLCategory;
                            var getcboeditAMLCategory = $scope.amlcategory_list.find(function (v) {
                                return v.amlcategory_gid === $scope.cboeditAMLCategory
                            });
                            if (getcboeditAMLCategory != null && getcboeditAMLCategory != "")
                                lsamlcategory_name = getcboeditAMLCategory.amlcategory_list;
                        }

                        if ($scope.cboeditBusinesscategory != undefined || $scope.cboeditBusinesscategory != null) {
                            lsbusinesscategory_gid = $scope.cboeditBusinesscategory;
                            var getbusinesscategoryname = $scope.businesscategory_list.find(function (v) {
                                return v.businesscategory_gid === $scope.cboeditBusinesscategory
                            });
                            if (getbusinesscategoryname != null && getbusinesscategoryname != "")
                                lsbusinesscategory_name = getbusinesscategoryname.businesscategory_list;
                        }

                    var params = {
                        company_name: $scope.txteditinstcompany_name,
                        dateincorporation: $scope.txteditincorporation_date,
                        businessstart_date: $scope.txteditbusinessstart_date,
                        year_business: $scope.txtedityearin_business,
                        month_business: $scope.txteditmonthsin_business,
                        companypan_no: $scope.txteditpan_number,
                        cin_no: $scope.txteditcin_no,
                        official_telephoneno: $scope.txteditofficialtelephone_number,
                        official_mailid: $scope.txteditofficial_mailid,
                        companytype_gid: $scope.cboeditCompanytype,
                        companytype_name: lscompanytype_name,
                        stakeholdertype_gid: $scope.cboeditStakeholdertype,
                        stakeholder_type: lsstakeholdertype_name,
                        assessmentagency_gid: $scope.cboeditCreditAssessmentagency,
                        assessmentagency_name: assessmentagencyname,
                        assessmentagencyrating_gid: $scope.cboeditAssessmentRating,
                        assessmentagencyrating_name: assessmentagencyratingname,
                        ratingason: $scope.txteditratingason_date,
                        amlcategory_gid: $scope.cboeditAMLCategory,
                        amlcategory_name: lsamlcategory_name,
                        businesscategory_gid: $scope.cboeditBusinesscategory,
                        businesscategory_name: lsbusinesscategory_name,
                        contactperson_firstname: $scope.txteditinstfirst_name,
                        contactperson_middlename: $scope.txteditinstmiddle_name,
                        contactperson_lastname: $scope.txteditinstlast_name,
                        designation_gid: $scope.cboeditinstDesignation,
                        designation: lsdesignation_type,
                        startdate: $scope.txteditstart_date,
                        enddate: $scope.txteditend_date,
                        escrow: $scope.rdbeditescrow,
                        tan_number: $scope.txtedittan_number,
                        incometax_returnsstatus: $scope.rdbincome_tax,
                        revenue: $scope.txtrevenue,
                        profit: $scope.txtprofit,
                        fixed_assets: $scope.txtfixed_asset,
                        sundrydebt_adv: $scope.txtsundrydebt_adv,
                        lastyear_turnover: $scope.txtlastyear_turnover,
                        urn_status: $scope.rdbeditinsturn_status,
                        urn: $scope.txteditinst_urn,
                        msme_registration:$scope.txteditmsmereg,
                        lglentity_id:$scope.txteditlei,
                        lei_renewaldate:$scope.txteditleirenewal_date,
                        kin:$scope.txteditkin,
                        institution_gid: institution_gid,
                        application_gid: $scope.application_gid,
                    }
                    var url = 'api/AgrMstSupplierOnboardEdit/SaveInstitutionEditDtl';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            overallgeneralsummary();
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

        }

        $scope.DeleteInstitution = function (institution_gid) {
            var params = {
                institution_gid: institution_gid,
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSupplierOnboard/Deleteinstitution';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                   // $scope.institution_list = resp.data.cicinstitution_list;
                    miniactivater();
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

        $scope.Deleteindividual = function (contact_gid) {
            var params = {
                contact_gid: contact_gid,
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSupplierOnboard/Deleteindividual';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                   // $scope.individual_list = resp.data.cicindividual_list;
                    miniactivater();
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

        $scope.AadharIndividualValidation = function (contact_gid) {
            var getStakeholderType = "";
            if ($scope.cboindStakeholderType != undefined && $scope.cboindStakeholderType != "") {
                getStakeholderType = $scope.cboindStakeholderType;
            }
            else {
                var getStakeholderType = $scope.usertype_list.find(function (v) {
                    return v.usertype_gid === $scope.cboindStakeholderType
                });
            } 
            if (getStakeholderType != null && getStakeholderType != "") {
                 $scope.AlreadyaddedIndividualpanaadhar = false;
                lockUI();
                var params = {
                    pan_no: 'No',
                    aadhar_no: $scope.txtaadhar_no,
                    institution_gid: 'No',
                    contact_gid: contact_gid,
                    onboard_gid: $scope.application_gid,
                    stakeholder_type: getStakeholderType.user_type
                }
                var url = 'api/AgrMstSupplierOnboard/GetOnboardValidatePANAadhar';
                SocketService.post(url, params).then(function (resp) {
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
        $scope.downloadall_4 = function () {
            for (var i = 0; i < $scope.contactpanform60_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.contactpanform60_list[i].document_path, $scope.contactpanform60_list[i].document_name);
            }
        }
        $scope.downloadall_5 = function () {
            for (var i = 0; i < $scope.contactidproof_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.contactidproof_list[i].document_path, $scope.contactidproof_list[i].document_name);
            }
        }
        $scope.downloadall_6 = function () {
            for (var i = 0; i < $scope.uploadindividualdoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.uploadindividualdoc_list[i].document_path, $scope.uploadindividualdoc_list[i].document_name);
            }
        }
        $scope.downloadall_8 = function () {
            for (var i = 0; i < $scope.institutionupload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.institutionupload_list[i].document_path, $scope.institutionupload_list[i].document_name);
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






    }

})();