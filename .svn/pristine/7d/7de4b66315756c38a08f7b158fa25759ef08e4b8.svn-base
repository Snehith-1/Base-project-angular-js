(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstApplicationCreationEditController', MstApplicationCreationEditController);

    MstApplicationCreationEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstApplicationCreationEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstApplicationCreationEditController';
       
       
        activate();
        function activate() { 

            $scope.application_gid = localStorage.getItem("application_gid");
            $scope.amount_validation = true;
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.calender01 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open01 = true;
            };
            vm.calender10 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open10 = true;
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

            vm.calender13 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open13 = true;
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

            vm.calender16 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open16 = true;
            };

            vm.calender17 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open17 = true;
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
            //var url = 'api/MstApplicationEdit/PostApplicationEditTemp';
            //SocketService.post(url, params).then(function (resp) {
            //});
            var url = 'api/MstApplicationAdd/GetIndividualTempClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstApplicationAdd/GetIntitutionTempClear';
            SocketService.get(url).then(function (resp) {
            });
            //Individual DropDown List
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

            var url = 'api/MstApplication360/IndividualDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.individualdocument_list = resp.data.application_list;
            });
            //Institution Drop Down list
            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            var url = 'api/MstApplication360/licensetypeList';
            SocketService.get(url).then(function (resp) {
                $scope.licensetype_list = resp.data.licensetype_list;
            });

            var url = 'api/MstApplication360/CompanyDocumentList';
            SocketService.get(url).then(function (resp) {
                $scope.companydocument_list = resp.data.companydocument_list;
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
           
            var url = 'api/MstApplicationEdit/GetBasicDetailsSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.basicdetails_list = resp.data.basicdetails_list;
            });


            var url = 'api/MstApplicationEdit/GetIndividualSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.contact_list = resp.data.contact_list;
            });

            var url = 'api/MstApplicationAdd/GetInstitutionEditSummary';
            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
                $scope.institution_list = resp.data.institution_list;
                unlockUI();
            });

            var url = 'api/MstApplicationEdit/GetEditProductcharges';
            SocketService.getparams(url, params).then(function (resp) {
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
                var url = 'api/MstApplication360/IndividualDocumentList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.individualdocument_list = resp.data.application_list;
                });
                if ($scope.applicant_type == "" || $scope.applicant_type == null) {
                    $scope.applicant_typenull = true;
                    $scope.applicant_typenotnull = false;
                }
                else {
                    $scope.applicant_typenotnull = true;
                    $scope.applicant_typenull = false;
                }

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

            var url = 'api/MstApplicationEdit/GetSocialTradeSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationlist = resp.data.applicationlist;
            });

            var url = 'api/MstApplicationEdit/GetCICEditIndividualSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cicindividuallist = resp.data.cicindividual_list;
                console.log(resp.data.cicindividual_list)
            });

            var url = 'api/MstApplicationEdit/GetCICEditInstitutionSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cicinstitutionlist = resp.data.cicinstitution_list;
            });

            var urls = 'api/MstApplicationAdd/CICUploadIndividualDocTempList';
            lockUI();
            SocketService.get(urls).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                unlockUI();
            });
            var url = 'api/MstApplicationEdit/GetProductChargesTempClear';
            SocketService.get(url).then(function (resp) {
            });
            var url = 'api/MstApplicationAdd/GetproductDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.loanproductlist = resp.data.loanproductlist;
                $scope.loantypelist = resp.data.loantypelist;
                $scope.principalfrequencylist = resp.data.principalfrequencylist;
                $scope.interestfrequencylist = resp.data.interestfrequencylist;
                $scope.buyerlist = resp.data.buyerlist;
                $scope.securitytype_list = resp.data.securitytype_list;
            });
        }

        $scope.doneclick = function () {
            var url = 'api/MstApplicationEdit/EditProceed';
            SocketService.get(url).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
            });
        }
   
        $scope.overallsubmit_application = function () {
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
      
        $scope.Back = function () {
            $state.go('app.MstApplicationCreationSummary');
        }

        $scope.overallsubmit_application = function () {
            $state.go('app.MstApplicationCreationSummary');
        }

        $scope.edit_basicdetails = function (application_gid) {
            $location.url('app/MstApplcreationBasicdtlEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
        }

        $scope.edit_individual = function (contact_gid) {
            $location.url('app/MstApplcreationIndividualdtlEdit?lscontact_gid=' + contact_gid + '&lstab=edit');
        }

        $scope.edit_institution = function (institution_gid) {
            $location.url('app/MstApplcreationInstitutiondtlEdit?lsinstitution_gid=' + institution_gid + '&lstab=edit');
        }

        $scope.edit_socialtrade = function (application_gid) {
            $location.url('app/MstApplcreationSocialTradeEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
        }

        $scope.edit_productcharges = function (application_gid) {
            $location.url('app/MstApplcreationProductchargesEdit?lsapplication_gid=' + application_gid + '&lstab=edit');
        }

        $scope.edit_cicupload = function () {
            $state.go('app.MstApplcreationCICUploadEdit');
        }
        
        $scope.cicupload_individualEdit = function (contact_gid) {
            $location.url('app/MstApplcreationCICUploadEdit?lscontact_gid=' + contact_gid + '');
        }

        $scope.cicupload_institutionEdit = function (institution_gid) {
            $location.url('app/MstApplcreationCICUploadInstEdit?lsinstitution_gid=' + institution_gid + '');
        }
        $scope.producttype = function () {
            var params = {
                loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid, application_gid: '',
                application2loan_gid: ''
            }
            var url = 'api/MstApplicationAdd/GetLoanSubProduct';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.loansubproductlist = resp.data.application_list;
            });
        }

        $scope.add_loaddetails = function () {
            if ($scope.txteditfacilityreqon_date == null || $scope.txteditfacilityreqon_date == '' || $scope.cboProductTypelist == null || $scope.cboProductTypelist == '' || $scope.cboProductSubTypelist == null || $scope.cboProductSubTypelist == '' || $scope.cboLoanTypelist == null || $scope.cboLoanTypelist == '' || $scope.txtloanfaility_amount == null || $scope.txtloanfaility_amount == '' || $scope.txteditrate_interest == null || $scope.txteditrate_interest == '' || $scope.txteditpanel_interest == null || $scope.txteditpanel_interest == '' || $scope.txtedittenure_years == null || $scope.txtedittenure_years == '' || $scope.txtedittenure_months == null || $scope.txtedittenure_months == '' || $scope.cboFacilityTypelist == null || $scope.cboFacilityTypelist == '' || $scope.txtedittenure_days == null || $scope.txtedittenure_days == '' || $scope.cboFacilitymodelist == null || $scope.cboFacilitymodelist == '') {
                Notify.alert('Kindly Fill all mandatory values', 'warning');
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
                            productsub_type: lsloansubproduct_name,
                            productsubtype_gid: lsloansubproduct_gid,
                            loantype_gid: lsloantype_gid,
                            loan_type: lsloan_type,
                            facilityloan_amount: $scope.txtloanfaility_amount,
                            facilityrequested_date: $scope.txteditfacilityreqon_date,
                            rate_interest: $scope.txteditrate_interest,
                            penal_interest: $scope.txteditpanel_interest,
                            facilityvalidity_year: $scope.txteditvalidity_years,
                            facilityvalidity_month: $scope.txteditvalidity_months,
                            facilityvalidity_days: $scope.txteditvalidity_days,
                            facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                            tenureproduct_year: $scope.txtedittenure_years,
                            tenureproduct_month: $scope.txtedittenure_months,
                            tenureproduct_days: $scope.txtedittenure_days,
                            tenureoverall_limit: $scope.txteditoveralllimit_validity,
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
                        var url = 'api/MstApplicationEdit/PostEditLoanDtl';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();

                            if (resp.data.status == true) {
                                var param = {
                                    application_gid: $scope.application_gid
                                }
                                var url = 'api/MstApplicationEdit/LoanDetailList';
                                SocketService.getparams(url, param).then(function (resp) {
                                    $scope.Loandtl_list = resp.data.mstloan_list;
                                    $scope.collateral_status = resp.data.collateral_status;
                                    $scope.buyer_status = resp.data.buyer_status;
                                });
                                Notify.alert(resp.data.message, {

                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $scope.cboProductTypelist = '';
                                $scope.txteditfacilityreqon_date = '';
                                $scope.cboProductSubTypelist = '';
                                $scope.cboLoanTypelist = '';
                                $scope.txtloanfaility_amount = '';
                                $scope.txteditrate_interest = '';
                                $scope.txteditpanel_interest = '';
                                $scope.txteditvalidity_years = '';
                                $scope.txteditvalidity_months = '';
                                $scope.txteditvalidity_days = '';
                                $scope.txtoverallfacilityvalidity_limit = '';
                                $scope.txtedittenure_years = '';
                                $scope.txtedittenure_months = '';
                                $scope.txtedittenure_days = '';
                                $scope.txteditoveralllimit_validity = '';
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
                        productsub_type: lsloansubproduct_name,
                        productsubtype_gid: lsloansubproduct_gid,
                        loantype_gid: lsloantype_gid,
                        loan_type: lsloan_type,
                        facilityloan_amount: $scope.txtloanfaility_amount,
                        facilityrequested_date: $scope.txteditfacilityreqon_date,
                        rate_interest: $scope.txteditrate_interest,
                        penal_interest: $scope.txteditpanel_interest,
                        facilityvalidity_year: $scope.txteditvalidity_years,
                        facilityvalidity_month: $scope.txteditvalidity_months,
                        facilityvalidity_days: $scope.txteditvalidity_days,
                        facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                        tenureproduct_year: $scope.txtedittenure_years,
                        tenureproduct_month: $scope.txtedittenure_months,
                        tenureproduct_days: $scope.txtedittenure_days,
                        tenureoverall_limit: $scope.txteditoveralllimit_validity,
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

                    var url = 'api/MstApplicationEdit/PostEditLoanDtl';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();

                        if (resp.data.status == true) {
                            var param = {
                                application_gid: $scope.application_gid
                            }
                            var url = 'api/MstApplicationEdit/LoanDetailList';
                            SocketService.getparams(url, param).then(function (resp) {
                                $scope.Loandtl_list = resp.data.mstloan_list;
                                $scope.collateral_status = resp.data.collateral_status;
                                $scope.buyer_status = resp.data.buyer_status;
                            });
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            loandetailslist();
                            $scope.cboProductTypelist = '';
                            $scope.txteditfacilityreqon_date = '';
                            $scope.cboProductSubTypelist = '';
                            $scope.cboLoanTypelist = '';
                            $scope.txtloanfaility_amount = '';
                            $scope.txteditrate_interest = '';
                            $scope.txteditpanel_interest = '';
                            $scope.txteditvalidity_years = '';
                            $scope.txteditvalidity_months = '';
                            $scope.txteditvalidity_days = '';
                            $scope.txtoverallfacilityvalidity_limit = '';
                            $scope.txtedittenure_years = '';
                            $scope.txtedittenure_months = '';
                            $scope.txtedittenure_days = '';
                            $scope.txteditoveralllimit_validity = '';
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

        function loandetailslist() {

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/MstApplicationEdit/LoanTempDetailList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Loandtl_list = resp.data.mstloan_list;
            });
        }
        $scope.collectdeduct = function () {
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }
        $scope.productchargesloan_edit = function (application2loan_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/loanedit.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstApplicationAdd/GetproductDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.loanproductlist = resp.data.loanproductlist;
                    $scope.loantypelist = resp.data.loantypelist;
                    $scope.principalfrequencylist = resp.data.principalfrequencylist;
                    $scope.interestfrequencylist = resp.data.interestfrequencylist;
                });

                var params = {
                    application2loan_gid: application2loan_gid
                }
                var url = 'api/MstApplicationEdit/LoanDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditfacilityreqon_date = new Date(resp.data.facilityrequested_date);
                    $scope.cboProductTypelist = resp.data.producttype_gid;

                    var params = {
                        loanproduct_gid: resp.data.producttype_gid, application_gid: '',
                        application2loan_gid: ''
                    }
                    var url = 'api/MstApplicationAdd/GetLoanSubProduct';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loansubproductlist = resp.data.application_list;
                    });

                    $scope.cboProductSubTypelist = resp.data.productsubtype_gid;
                    $scope.cboLoanTypelist = resp.data.loantype_gid;
                    $scope.txtloanfaility_amount = resp.data.facilityloan_amount;
                    $scope.txteditrate_interest = resp.data.rate_interest;
                    $scope.txteditpanel_interest = resp.data.penal_interest;
                    $scope.txteditvalidity_years = resp.data.facilityvalidity_year;
                    $scope.txteditvalidity_months = resp.data.facilityvalidity_month;
                    $scope.txteditvalidity_days = resp.data.facilityvalidity_days;
                    $scope.txtoverallfacilityvalidity_limit = resp.data.facilityoverall_limit;
                    $scope.txtedittenure_years = resp.data.tenureproduct_year;
                    $scope.txtedittenure_months = resp.data.tenureproduct_month;
                    $scope.txtedittenure_days = resp.data.tenureproduct_days;
                    $scope.txteditoveralllimit_validity = resp.data.tenureoverall_limit;
                    $scope.cboFacilityTypelist = resp.data.facility_type;
                    $scope.cboFacilitymodelist = resp.data.facility_mode;
                    $scope.cboprincipalfrequency = resp.data.principalfrequency_gid;
                    $scope.cboInterestFrequency = resp.data.interestfrequency_gid;
                    $scope.rdbinterest_status = resp.data.interest_status;
                    $scope.rdbmoratorium_status = resp.data.moratorium_status;
                    $scope.cbomoratorium_type = resp.data.moratorium_type;
                    $scope.txtmoratorium_startdate = new Date(resp.data.moratorium_startdate);
                    $scope.txtmoratorium_enddate = new Date(resp.data.moratorium_enddate);
                });

                $scope.producttype = function () {
                    var params = {
                        loanproduct_gid: $scope.cboProductTypelist.loanproduct_gid, application_gid: '',
                        application2loan_gid: ''
                    }
                    var url = 'api/MstApplicationAdd/GetLoanSubProduct';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.loansubproductlist = resp.data.application_list;
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_loandtl = function () {

                    var loanproductname = $('#loanproduct_name :selected').text();
                    var loansubproductname = $('#loansubproduct_name :selected').text();
                    var loantype = $('#loan_type :selected').text();
                    var principalfrequencyname = $('#principalfrequency_name :selected').text();
                    var interestfrequencyname = $('#interestfrequency_name :selected').text();

                    var params = {
                        product_type: loanproductname,
                        producttype_gid: $scope.cboProductTypelist,
                        facilityrequested_date: $scope.txteditfacilityreqon_date,
                        productsub_type: loansubproductname,
                        productsubtype_gid: $scope.cboProductSubTypelist,
                        loantype_gid: $scope.cboLoanTypelist,
                        loan_type: loantype,
                        facilityloan_amount: $scope.txtloanfaility_amount,
                        rate_interest: $scope.txteditrate_interest,
                        penal_interest: $scope.txteditpanel_interest,
                        facilityvalidity_year: $scope.txteditvalidity_years,
                        facilityvalidity_month: $scope.txteditvalidity_months,
                        facilityvalidity_days: $scope.txteditvalidity_days,
                        facilityoverall_limit: $scope.txtoverallfacilityvalidity_limit,
                        tenureproduct_year: $scope.txtedittenure_years,
                        tenureproduct_month: $scope.txtedittenure_months,
                        tenureproduct_days: $scope.txtedittenure_days,
                        tenureoverall_limit: $scope.txteditoveralllimit_validity,
                        facility_type: $scope.cboFacilityTypelist,
                        facility_mode: $scope.cboFacilitymodelist,
                        principalfrequency_name: principalfrequencyname,
                        principalfrequency_gid: $scope.cboprincipalfrequency,
                        interestfrequency_name: interestfrequencyname,
                        interestfrequency_gid: $scope.cboInterestFrequency,
                        interest_status: $scope.rdbinterest_status,
                        moratorium_status: $scope.rdbmoratorium_status,
                        moratorium_type: $scope.cbomoratorium_type,
                        moratorium_startdate: $scope.txtmoratorium_startdate,
                        moratorium_enddate: $scope.txtmoratorium_enddate,
                        application2loan_gid: application2loan_gid,
                        application_gid: localStorage.getItem("application_gid"),
                    }
                    var url = 'api/MstApplicationEdit/LoanDetailsUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            loandetailslist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.productchargesloan_delete = function (application2loan_gid) {
            var params =
               {
                   application2loan_gid: application2loan_gid
               }
            var url = 'api/MstApplicationEdit/DeleteLoanDetail';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    loandetailslist();
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

        $scope.buyer = function () {
            var params = {
                buyer_gid: $scope.cboBuyer.buyer_gid,
            }
            var url = 'api/MstApplicationAdd/GetBuyerInfo';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtbuyer_limit = resp.data.buyer_limit;
            });
        }

        $scope.add_buyerdtl = function () {
            if (($scope.cboBuyer == undefined) || ($scope.cboBuyer == '') || ($scope.txtbill_tenuredays == undefined) || ($scope.txtmargin == undefined) || ($scope.txtbill_tenuredays == '') || ($scope.txtmargin == '')) {
                Notify.alert('Enter all Mandatory Fields');
            }
            else {
                var params = {
                    buyer_name: $scope.cboBuyer.buyer_name,
                    buyer_gid: $scope.cboBuyer.buyer_gid,
                    buyer_limit: $scope.txtbuyer_limit,
                    availed_limit: $scope.txtavailed_limit,
                    balance_limit: $scope.txtbalance_limit,
                    bill_tenure: $scope.txtbill_tenuredays,
                    margin: $scope.txtmargin,
                }
                var url = 'api/MstApplicationAdd/PostBuyer';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        buyerdetailslist();
                        $scope.cboBuyer = '';
                        $scope.txtbuyer_limit = '';
                        $scope.txtavailed_limit = '';
                        $scope.txtbalance_limit = '';
                        $scope.txtbill_tenuredays = '';
                        $scope.txtmargin = '';
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

        function buyerdetailslist() {

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/MstApplicationEdit/BuyerTempDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.buyerdtl_list = resp.data.mstbuyer_list;
            });
        }

        $scope.buyerdtl_edit = function (application2buyer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/buyerdtledit.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstApplicationAdd/GetproductDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.buyerlist = resp.data.buyerlist;
                });

                var params = {
                    application2buyer_gid: application2buyer_gid
                }
                var url = 'api/MstApplicationEdit/BuyerDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboBuyer = resp.data.buyer_gid;
                    $scope.txteditbuyer_limit = resp.data.buyer_limit;
                    $scope.txteditavailed_limit = resp.data.availed_limit;
                    $scope.txteditbalance_limit = resp.data.balance_limit;
                    $scope.txteditbill_tenuredays = resp.data.bill_tenure;
                    $scope.txteditmargin = resp.data.margin;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.buyer = function () {
                    var params = {
                        buyer_gid: $scope.cboBuyer.buyer_gid,
                    }
                    var url = 'api/MstApplicationAdd/GetBuyerInfo';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtbuyer_limit = resp.data.buyer_limit;
                    });
                }

                $scope.update_buyerdtl = function () {

                    var buyername = $('#buyer_name :selected').text();

                    var params = {
                        buyer_name: buyername,
                        buyer_gid: $scope.cboBuyer,
                        buyer_limit: $scope.txteditbuyer_limit,
                        availed_limit: $scope.txteditavailed_limit,
                        balance_limit: $scope.txteditbalance_limit,
                        bill_tenure: $scope.txteditbill_tenuredays,
                        margin: $scope.txteditmargin,
                        application2buyer_gid: application2buyer_gid,
                        application_gid: localStorage.getItem("application_gid"),
                    }
                    var url = 'api/MstApplicationEdit/BuyerDetailsUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            buyerdetailslist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.buyerdtl_delete = function (application2buyer_gid) {
            var params =
               {
                   application2buyer_gid: application2buyer_gid
               }
            var url = 'api/MstApplicationEdit/DeleteBuyerDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    buyerdetailslist();
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

        $scope.add_Collateral = function () {
            if ($scope.cboSourceType == null || $scope.cboSourceType == '') {
                Notify.alert('Kindly select source type', 'warning')
            }
            else {
                var params = {
                    source_type: $scope.cboSourceType,
                    guideline_value: $scope.txtguidelinevalue,
                    guideline_date: $scope.txtguideline_date,
                    market_value: $scope.txtmarketValue,
                    marketvalue_date: $scope.txtmarketvalue_date,
                    forcedsource_value: $scope.txtforcedsource_value,
                    collateralSSV_value: $scope.txtcollateralSSV_value,
                    forcedvalueassessed_on: $scope.txtforcedvalueassessed_on,
                    collateralobservation_summary: $scope.txtcolateralobservation_summary,
                }
                var url = 'api/MstApplicationAdd/PostCollateral';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        collatertaldetailslist();
                        $scope.cboSourceType = '';
                        $scope.txtguidelinevalue = '';
                        $scope.txtguideline_date = '';
                        $scope.txtmarketValue = '';
                        $scope.txtmarketvalue_date = '';
                        $scope.txtforcedsource_value = '';
                        $scope.txtcollateralSSV_value = '';
                        $scope.txtforcedvalueassessed_on = '';
                        $scope.txtcolateralobservation_summary = '';
                        $scope.DocumentList = '';
                        document.getElementById('words_totalamount2').innerHTML = '';
                        document.getElementById('words_totalamount3').innerHTML = '';
                        document.getElementById('words_totalamount4').innerHTML = '';
                        document.getElementById('words_totalamount5').innerHTML = '';
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

        function collatertaldetailslist() {

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/MstApplicationEdit/CollateralTempDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Collateral_list = resp.data.collatertal_list;
                $scope.CollateralDocumentList = resp.data.DocumentList;
            });

        }

        $scope.edit_Collateraldtls = function (application2collateral_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Collateraldtledit.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];

            function ModalInstanceCtrl($scope, $modalInstance) {

                // function inWords1(num) {
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

                $scope.txtguidelinevaluechange = function () {
                    var input = document.getElementById('GuidelineValueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit2 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditguidelinevalue = "";
                    }
                    else {
                        //  $scope.txteditguidelinevalue = output;
                        document.getElementById('words_totalamountedit2').innerHTML = lswords_totalamountedit2;
                    }
                }

                $scope.txteditMarketValuechange = function () {
                    var input = document.getElementById('MarketValueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit3 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditMarketValue = "";
                    }
                    else {
                        //   $scope.txteditMarketValue = output;
                        document.getElementById('words_totalamountedit3').innerHTML = lswords_totalamountedit3;
                    }
                }

                $scope.txtForcedSourceValuechange = function () {
                    var input = document.getElementById('ForcedSourceValueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit4 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditForcedSourceValue = "";
                    }
                    else {
                        //    $scope.txteditForcedSourceValue = output;
                        document.getElementById('words_totalamountedit4').innerHTML = lswords_totalamountedit4;
                    }
                }

                $scope.txtCollateralSSVvaluechange = function () {
                    var input = document.getElementById('CollateralSSVvalueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit5 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditCollateralSSVvalue = "";
                    }
                    else {
                        //   $scope.txteditCollateralSSVvalue = output;
                        document.getElementById('words_totalamountedit5').innerHTML = lswords_totalamountedit5;
                    }
                }

                var params = {
                    application2collateral_gid: application2collateral_gid
                }

                var url = 'api/MstApplicationEdit/CollateralDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboeditSourceType = resp.data.source_type;
                    $scope.txteditguidelinevalue = resp.data.guideline_value;
                    $scope.txteditguideline_date = new Date(resp.data.guideline_date);
                    $scope.txteditMarketValue = resp.data.market_value;
                    $scope.txteditmarketvalue_date = new Date(resp.data.marketvalue_date);
                    $scope.txteditForcedSourceValue = resp.data.forcedsource_value;
                    $scope.txteditCollateralSSVvalue = resp.data.collateralSSV_value;
                    $scope.txteditForcedValueAssessedOn_date = new Date(resp.data.forcedvalueassessed_on);
                    $scope.txteditObservation_Summary = resp.data.collateralobservation_summary;

                });

                var url = 'api/MstApplicationEdit/CollateralDocumentTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.DocumentList = resp.data.DocumentList;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.upload_doc = function (val, val1, name) {
                    if (($scope.cboDocumentTitle == null) || ($scope.cboDocumentTitle == '') || ($scope.cboDocumentTitle == undefined)) {
                        $("#file").val('');
                        Notify.alert('Kindly Select the Document Title', 'warning');
                    }
                    else {
                        var item = {
                            name: val[0].name,
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
                        frm.append('file_name', item.name);
                        frm.append('document_name', $scope.documentname);
                        frm.append('document_title', $scope.cboDocumentTitle);
                        frm.append('application2collateral_gid', application2collateral_gid);
                        $scope.uploadfrm = frm;
                        lockUI();
                        var url = 'api/MstApplicationEdit/Editcollateraldocument';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            $("#file").val('');

                            if (resp.data.status == true) {
                                $scope.DocumentList = resp.data.DocumentList;
                                $scope.cboDocumentTitle = '';
                                $scope.uploadfrm = undefined;
                                $("#file").val('');
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

                $scope.uploaddocumentcancel = function (val, val1) {
                    var params = { document_gid: val };

                    var url = 'api/MstApplicationAdd/deletecollateraldoc';
                    SocketService.getparams(url, params).then(function (resp) {
                        var params = {
                            application2collateral_gid: application2collateral_gid
                        }
                        var url = 'api/MstApplicationEdit/CollateralDocumentTempList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.DocumentList = resp.data.DocumentList;
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
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }

                $scope.collateraldownloads = function (val1, val2) {
                    var phyPath = val1;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    link.download = val2;
                    var uri = str;
                    link.href = uri;
                    link.click();
                }

                $scope.update_Collateraldtl = function () {
                    var params = {
                        source_type: $scope.cboeditSourceType,
                        guideline_value: $scope.txteditguidelinevalue,
                        guideline_date: $scope.txteditguideline_date,
                        market_value: $scope.txteditMarketValue,
                        marketvalue_date: $scope.txteditmarketvalue_date,
                        forcedsource_value: $scope.txteditForcedSourceValue,
                        collateralSSV_value: $scope.txteditCollateralSSVvalue,
                        forcedvalueassessed_on: $scope.txteditForcedValueAssessedOn_date,
                        collateralobservation_summary: $scope.txteditObservation_Summary,
                        application2collateral_gid: application2collateral_gid,
                        applicationgid: localStorage.getItem("application_gid"),

                    }
                    var url = 'api/MstApplicationEdit/CollateralDetailsUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            collatertaldetailslist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.delete_Collateraldtls = function (application2collateral_gid) {
            var params =
               {
                   application2collateral_gid: application2collateral_gid
               }
            var url = 'api/MstApplicationEdit/DeleteCollateralDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    collatertaldetailslist();
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

        $scope.add_Hypothecationdtls = function () {
            if ($scope.cboSecurityType == null || $scope.cboSecurityType == '') {
                Notify.alert('Kindly select security type', 'warning')
            }
            else {

                var lsSecurityType_name = '';
                var lsSecurityType_gid = '';

                if ($scope.cboSecurityType != undefined || $scope.cboSecurityType != null) {
                    lsSecurityType_name = $scope.cboSecurityType.security_type;
                    lsSecurityType_gid = $scope.cboSecurityType.securitytype_gid;
                }

                var params = {
                    securitytype_gid: lsSecurityType_gid,
                    security_type: lsSecurityType_name,
                    security_description: $scope.txtsecurity_desc,
                    security_value: $scope.txtSecurity_Value,
                    securityassessed_date: $scope.txtSecurityAssessed_date,
                    asset_id: $scope.txtasset_id,
                    roc_fillingid: $scope.txtroc_fillingid,
                    CERSAI_fillingid: $scope.txtCERSAI_fillingid,
                    hypoobservation_summary: $scope.txthypoobservation_summary,
                    primary_security: $scope.txtprimary_security
                }
                var url = 'api/MstApplicationAdd/PostHypothecation';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        hypothecationdetailslist();
                        $scope.cboSecurityType = '';
                        $scope.txtsecurity_desc = '';
                        $scope.txtSecurity_Value = '';
                        $scope.txtSecurityAssessed_date = '';
                        $scope.txtasset_id = '';
                        $scope.txtroc_fillingid = '';
                        $scope.txtCERSAI_fillingid = '';
                        $scope.txthypoobservation_summary = '';
                        $scope.txtprimary_security = '';
                        $scope.DocumentList = '';
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

        function hypothecationdetailslist() {

            var param = {
                application_gid: $scope.application_gid
            };

            var url = 'api/MstApplicationEdit/HypothecationTempDetailsList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.Hypothecation_list = resp.data.hypothecation_list;
                $scope.HypothecationDocumentList = resp.data.DocumentList;
            });
        }

        $scope.edit_Hypothecationdtls = function (application2hypothecation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Hypothecationdtlsedit.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/MstApplicationAdd/GetproductDropDown';
                SocketService.get(url).then(function (resp) {
                    $scope.securitytype_list = resp.data.securitytype_list;
                });

                $scope.txtSecurityValuechange = function () {
                    var input = document.getElementById('SecurityValueedit').value;
                    var str1 = input.replace(/,/g, '');
                    var str = Math.round(str1);
                    var output = Number(str).toLocaleString('en-IN');
                    var lswords_totalamountedit6 = cmnfunctionService.fnConvertNumbertoWord(str);
                    if (output == "NaN") {
                        Notify.alert('Accept Number Format Only..!', {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txteditSecurity_Value = "";
                    }
                    else {
                        //    $scope.txteditSecurity_Value = output;
                        document.getElementById('words_totalamountedit6').innerHTML = lswords_totalamountedit6;
                    }
                }

                // function inWords1(num) {
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

                var params = {
                    application2hypothecation_gid: application2hypothecation_gid
                }
                var url = 'api/MstApplicationEdit/HypothecationDetailsEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.cboeditSecurityType = resp.data.securitytype_gid;
                    $scope.txteditsecurity_desc = resp.data.security_description;
                    $scope.txteditSecurity_Value = resp.data.security_value;
                    $scope.txtSecurityAssessededit_date = new Date(resp.data.securityassessed_date);
                    $scope.txtassetedit_id = resp.data.asset_id;
                    $scope.txtrocedit_fillingid = resp.data.roc_fillingid;
                    $scope.txtCERSAIedit_fillingid = resp.data.CERSAI_fillingid;
                    $scope.txthypoobservationedit_summary = resp.data.hypoobservation_summary;
                    $scope.txtprimaryedit_security = resp.data.primary_security;
                });

                var url = 'api/MstApplicationEdit/HypothecationDocumentTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.DocumentList = resp.data.DocumentList;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.uploadhypothecationdoc = function (val, val1, name) {
                    if (($scope.cbohypodoc_title == null) || ($scope.cbohypodoc_title == '') || ($scope.cbohypodoc_title == undefined)) {
                        $("#file").val('');
                        Notify.alert('Kindly Select the Document Title', 'warning');
                    }
                    else {
                        var item = {
                            name: val[0].name,
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
                        frm.append('file_name', item.name);
                        frm.append('document_name', $scope.documentname);
                        frm.append('document_title', $scope.cbohypodoc_title);
                        frm.append('application2hypothecation_gid', application2hypothecation_gid);
                        $scope.uploadfrm = frm;
                        lockUI();
                        var url = 'api/MstApplicationEdit/EditHypoDoc';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                            unlockUI();
                            $("#file").val('');
                            if (resp.data.status == true) {
                                $scope.DocumentList = resp.data.DocumentList;
                                $scope.cbohypodoc_title = '';
                                $scope.uploadfrm = undefined;
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

                $scope.hypodoccancel = function (val, val1) {
                    var params = { document_gid: val };

                    var url = 'api/MstApplicationAdd/deleteHypoDoc';
                    SocketService.getparams(url, params).then(function (resp) {
                        var params = {
                            application2hypothecation_gid: application2hypothecation_gid
                        }
                        var url = 'api/MstApplicationEdit/HypothecationDocumentTempList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.DocumentList = resp.data.DocumentList;
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
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                }

                $scope.hypodocdownloads = function (val1, val2) {
                    var phyPath = val1;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    link.download = val2;
                    var uri = str;
                    link.href = uri;
                    link.click();
                }

                $scope.update_Hypothecationdtl = function () {

                    var securitytype = '';
                    var securitytype_gid = '';

                    if ($scope.cboeditSecurityType != undefined || $scope.cboeditSecurityType != null) {
                        securitytype = $('#security_type :selected').text();

                        securitytype_gid = $scope.cboeditSecurityType;
                    }

                    var params = {
                        securitytype_gid: securitytype_gid,
                        security_type: securitytype,
                        security_description: $scope.txteditsecurity_desc,
                        security_value: $scope.txteditSecurity_Value,
                        securityassessed_date: $scope.txtSecurityAssessededit_date,
                        asset_id: $scope.txtassetedit_id,
                        roc_fillingid: $scope.txtrocedit_fillingid,
                        CERSAI_fillingid: $scope.txtCERSAIedit_fillingid,
                        hypoobservation_summary: $scope.txthypoobservationedit_summary,
                        primary_security: $scope.txtprimaryedit_security,
                        application2hypothecation_gid: application2hypothecation_gid,
                        application_gid: localStorage.getItem("application_gid")
                    }
                    var url = 'api/MstApplicationEdit/HypothecationDetailsUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            hypothecationdetailslist();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }

        $scope.delete_Hypothecationdtls = function (application2hypothecation_gid) {
            var params =
               {
                   application2hypothecation_gid: application2hypothecation_gid
               }
            var url = 'api/MstApplicationEdit/DeleteHypothecationDetails';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    hypothecationdetailslist();
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

        $scope.upload_doc = function (val, val1, name) {
            if (($scope.cboDocumentTitle == null) || ($scope.cboDocumentTitle == '') || ($scope.cboDocumentTitle == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
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
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cboDocumentTitle);
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/MstApplicationAdd/postcollateraldocument';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    $("#file").val('');
                    if (resp.data.status == true) {
                        $scope.CollateralDocumentList = resp.data.DocumentList;
                        $scope.cboDocumentTitle = '';

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

        $scope.uploaddocumentcancel = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/MstApplicationAdd/deletecollateraldoc';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.CollateralDocumentList = resp.data.DocumentList;
                    angular.forEach($scope.CollateralDocumentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.CollateralDocumentList.splice(key, 1);
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

        $scope.uploadhypothecationdoc = function (val, val1, name) {
            if (($scope.cbohypodoc_title == null) || ($scope.cbohypodoc_title == '') || ($scope.cbohypodoc_title == undefined)) {
                $("#hypofile").val('');
                Notify.alert('Kindly Select the Document Title', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
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
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbohypodoc_title);
                $scope.uploadfrm = frm;
                lockUI();
                var url = 'api/MstApplicationAdd/PostHypoDoc';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    unlockUI();
                    $("#hypofile").val('');
                    if (resp.data.status == true) {
                        $scope.HypothecationDocumentList = resp.data.DocumentList;
                        $scope.cbohypodoc_title = '';

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
        }

        $scope.hypodoccancel = function (val, data) {
            var params = { document_gid: val };

            var url = 'api/MstApplicationAdd/deleteHypoDoc';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.HypothecationDocumentList = resp.data.DocumentList;
                    angular.forEach($scope.HypothecationDocumentList, function (value, key) {
                        if (value.document_gid == val) {
                            $scope.HypothecationDocumentList.splice(key, 1);
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

        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.productcharge_Back = function () {
            if (lstab == 'add') {
                $location.url('app/MstApplicationGeneralAdd?lstab=' + lstab);
            }
            else {
                $state.go('app.MstApplicationCreationEdit');
            }
        }

        $scope.update_productcharge = function () {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_months,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_days,
                calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
                enduse_purpose: $scope.txtenduse_purpose,
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
                application_gid: $scope.application_gid,
            }
            var url = 'api/MstApplicationEdit/UpdateProductCharges';
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
                        $state.go('app.MstApplicationCreationEdit');
                    }
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                if (lstab == 'add') {
                    $location.url('app/MstApplicationGeneralAdd?lstab=' + lstab);
                }
                else {
                    $state.go('app.MstApplicationCreationEdit');
                }
            });
        }

        $scope.Submitproduct = function () {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_months,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_days,
                calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
                enduse_purpose: $scope.txtenduse_purpose,
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
                application_gid: $scope.application_gid,
            }
            var url = 'api/MstApplicationEdit/SubmitEditProductCharges';
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
        }

        $scope.saveProduct_charges = function () {
            var params = {
                overalllimit_amount: $scope.txtOveralllimit_amount,
                validityoveralllimit_year: $scope.txtvalidityoveralllimit_year,
                validityoveralllimit_month: $scope.txtvalidityoveralllimit_months,
                validityoveralllimit_days: $scope.txtvalidityoveralllimit_days,
                calculationoveralllimit_validity: $scope.txtcalculationoveralllimit_validity,
                enduse_purpose: $scope.txtenduse_purpose,
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
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationEdit/SaveEditProductCharges';
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
        }


        $scope.txtfacility = function () {
            var input = document.getElementById('loanamount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount1 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtloanfaility_amount = "";
            }
            else {
                //    $scope.txtloanfaility_amount = output;
                document.getElementById('words_totalamount1').innerHTML = lswords_totalamount1;
                if (($scope.txtloanfaility_amount.replace(/[\s,]+/g, '').trim()) > ($scope.txtOveralllimit_amount.replace(/[\s,]+/g, '').trim())) {
                    $scope.amount_validation = false;
                }
                else {
                    $scope.amount_validation = true;
                }
            }
        }

        // function inWords1(num) {
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

        $scope.Overalllimit_amountchange = function () {
            var input = document.getElementById('Overalllimitamount').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount7 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtOveralllimit_amount = "";
            }
            else {

                // $scope.txtOveralllimit_amount = output;
                document.getElementById('words_totalamount7').innerHTML = lswords_totalamount7;
            }
        }

        $scope.calculatelimit = function () {
            if ($scope.txtvalidityoveralllimit_year != undefined || $scope.txtvalidityoveralllimit_year != null) {
                var lsyear = $scope.txtvalidityoveralllimit_year + " - Year, ";
            }
            else {
                var lsyear = '';
            }
            if ($scope.txtvalidityoveralllimit_months != undefined || $scope.txtvalidityoveralllimit_months != null) {
                var lsmonth = $scope.txtvalidityoveralllimit_months + " - Month, ";
            }
            else {
                var lsmonth = '';
            }
            if ($scope.txtvalidityoveralllimit_days != undefined || $scope.txtvalidityoveralllimit_days != null) {
                var lsday = $scope.txtvalidityoveralllimit_days + " - Day";
            }
            else {
                var lsday = '';
            }
            $scope.txtcalculationoveralllimit_validity = lsyear + lsmonth + lsday;
        }
        $scope.calculatefacility = function () {
            if ($scope.txteditvalidity_years != undefined || $scope.txteditvalidity_years != null || $scope.txteditvalidity_years != '') {
                var lsyear = $scope.txteditvalidity_years + " - Year, ";
            }
            else {
                var lsyear = '';
            }
            if ($scope.txteditvalidity_months != undefined || $scope.txteditvalidity_months != null || $scope.txteditvalidity_months != '') {
                var lsmonth = $scope.txteditvalidity_months + " - Month, ";
            }
            else {
                var lsmonth = '';
            }
            if ($scope.txteditvalidity_days != undefined || $scope.txteditvalidity_days != null || $scope.txteditvalidity_days != '') {
                var lsday = $scope.txteditvalidity_days + " - Day";
            }
            else {
                var lsday = '';
            }
            $scope.txtoverallfacilityvalidity_limit = lsyear + lsmonth + lsday;
        }
        $scope.calculatetenure = function () {
            if ($scope.txtedittenure_years != undefined || $scope.txtedittenure_years != null) {
                var lsyear = $scope.txtedittenure_years + " - Year, ";
            }
            else {
                var lsyear = '';
            }
            if ($scope.txtedittenure_months != undefined || $scope.txtedittenure_months != null) {
                var lsmonth = $scope.txtedittenure_months + " - Month, ";
            }
            else {
                var lsmonth = '';
            }
            if ($scope.txtedittenure_days != undefined || $scope.txtedittenure_days != null) {
                var lsday = $scope.txtedittenure_days + " - Day";
            }
            else {
                var lsday = '';
            }
            $scope.txteditoveralllimit_validity = lsyear + lsmonth + lsday;
        }
        $scope.txtguidelinevaluechange = function () {
            var input = document.getElementById('GuidelineValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount2 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtguidelinevalue = "";
            }
            else {
                // $scope.txtguidelinevalue = output;
                document.getElementById('words_totalamount2').innerHTML = lswords_totalamount2;
            }
        }


        $scope.txtMarketValuechange = function () {
            var input = document.getElementById('MarketValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount3 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtMarketValue = "";
            }
            else {
                //  $scope.txtMarketValue = output;
                document.getElementById('words_totalamount3').innerHTML = lswords_totalamount3;
            }
        }

        $scope.txtForcedSourceValuechange = function () {
            var input = document.getElementById('ForcedSourceValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount4 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtForcedSourceValue = "";
            }
            else {
                //  $scope.txtForcedSourceValue = output;
                document.getElementById('words_totalamount4').innerHTML = lswords_totalamount4;
            }
        }

        $scope.txtCollateralSSVvaluechange = function () {
            var input = document.getElementById('CollateralSSVvalue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount5 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtCollateralSSVvalue = "";
            }
            else {
                // $scope.txtCollateralSSVvalue = output;
                document.getElementById('words_totalamount5').innerHTML = lswords_totalamount5;
            }
        }
        $scope.txtSecurityValuechange = function () {
            var input = document.getElementById('SecurityValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount6 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtSecurity_Value = "";
            }
            else {
                // $scope.txtSecurity_Value = output;
                document.getElementById('words_totalamount6').innerHTML = lswords_totalamount6;
            }
        }
        $scope.txtamountchange = function () {
            var input = document.getElementById('MarketValue').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount3 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtMarketValue = "";
            }
            else {
                // $scope.txtMarketValue = output;
                document.getElementById('words_totalamount3').innerHTML = lswords_totalamount3;
            }
        }
        $scope.txtamountchange1 = function () {
            var input = document.getElementById('processingfee').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount51 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtprocessing_fee = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount51').innerHTML = lswords_totalamount51;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange2 = function () {
            var input = document.getElementById('doc_charges').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount52 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtdoc_charges = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount52').innerHTML = lswords_totalamount52;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange3 = function () {
            var input = document.getElementById('fieldvisit_charges').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount53 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtfieldvisit_charges = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount53').innerHTML = lswords_totalamount53;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }
        $scope.txtamountchange4 = function () {
            var input = document.getElementById('adhoc_fee').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount54 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtadhoc_fee = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount54').innerHTML = lswords_totalamount54;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }

        $scope.txtamountchange5 = function () {
            var input = document.getElementById('life_insurance').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount55 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtlife_insurance = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount55').innerHTML = lswords_totalamount55;
            }
            var processing_fee = 0;
            var doc_charges = 0;
            var fieldvisit_charges = 0;
            var adhoc_fee = 0;
            var life_insurance = 0;
            if ($scope.rdbprocessing_collectiontype == 'Collect') {
                processing_fee = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Collect') {
                doc_charges = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Collect') {
                fieldvisit_charges = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Collect') {
                adhoc_fee = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Collect') {
                life_insurance = parseInt(document.getElementById("life_insurance").value)
            }
            var result = processing_fee + doc_charges + fieldvisit_charges + adhoc_fee + life_insurance

            document.getElementById("total_collect").value = result;

            var processing_fee_deduct = 0;
            var doc_charges_deduct = 0;
            var fieldvisit_charges_deduct = 0;
            var adhoc_fee_deduct = 0;
            var life_insurance_deduct = 0;
            if ($scope.rdbprocessing_collectiontype == 'Deduct') {
                processing_fee_deduct = parseInt(document.getElementById("processingfee").value)
            }
            if ($scope.rdbdoccharge_collectiontype == 'Deduct') {
                doc_charges_deduct = parseInt(document.getElementById("doc_charges").value)
                console.log(doc_charges)
            }
            if ($scope.rdbfieldvisit_collectiontype == 'Deduct') {
                fieldvisit_charges_deduct = parseInt(document.getElementById("fieldvisit_charges").value)
            }
            if ($scope.rdbadhoc_collectiontype == 'Deduct') {
                adhoc_fee_deduct = parseInt(document.getElementById("adhoc_fee").value)
            }

            if ($scope.rdblifeinsurance_collectiontype == 'Deduct') {
                life_insurance_deduct = parseInt(document.getElementById("life_insurance").value)
            }
            var result_deduct = processing_fee_deduct + doc_charges_deduct + fieldvisit_charges_deduct + adhoc_fee_deduct + life_insurance_deduct

            document.getElementById("total_deduct").value = result_deduct;

        }
        $scope.txtamountchange6 = function () {
            var input = document.getElementById('acct_insurance').value;
            var str1 = input.replace(/,/g, '');
            var str = Math.round(str1);
            var output = Number(str).toLocaleString('en-IN');
            var lswords_totalamount56 = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept Number Format Only..!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtacct_insurance = "";
            }
            else {
                //   $scope.txtprocessing_fee = output;
                document.getElementById('words_totalamount56').innerHTML = lswords_totalamount56;
            }
        }

        //Individual

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
        }
    }
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

    
    $scope.mobileno_add = function () {

        if (($scope.txtmobile_no == undefined) || ($scope.txtmobile_no == '') || ($scope.rdbprimary_no == undefined) || ($scope.rdbwhatsapp_no == undefined) || ($scope.rdbwhatsapp_no == '') || ($scope.rdbprimary_no == '')) {
            Notify.alert('Enter Mobile No/Select Primary Status and Whatsapp Number','warning');
        }
        else if ($scope.txtmobile_no.length < 10) {
            Notify.alert('Enter 10 Digit Mobile Number','warning');
        }
        else {
            var params = {
                mobile_no: $scope.txtmobile_no,
                primary_status: $scope.rdbprimary_no,
                whatsapp_no: $scope.rdbwhatsapp_no
            }
            var url = 'api/MstApplicationAdd/MobileNumberAdd';
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
                var url = 'api/MstApplicationAdd/GetMobileNoList';
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
        var url = 'api/MstApplicationAdd/MobileNoDelete';
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
            var url = 'api/MstApplicationAdd/GetMobileNoList';
            SocketService.get(url).then(function (resp) {
                $scope.contactmobileno_list = resp.data.contactmobileno_list;

            });

        });

    }


    $scope.email_add = function () {

        if (($scope.txtemail_address == undefined) || ($scope.txtemail_address == '') || ($scope.rdbprimary_email == undefined) || ($scope.rdbprimary_email == '')) {
            Notify.alert('Enter Email Address/Select Primary Status','warning');
        }
        else {


            var params = {
                email_address: $scope.txtemail_address,
                primary_status: $scope.rdbprimary_email,
            }
            var url = 'api/MstApplicationAdd/EmailAddressAdd';
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
                var url = 'api/MstApplicationAdd/GetEmailAddressList';
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
        var url = 'api/MstApplicationAdd/EmailAddressDelete';
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
            var url = 'api/MstApplicationAdd/GetEmailAddressList';
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
            var item = {
                name: val[0].name,
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
            frm.append('file_name', item.name);
            frm.append('idproof_type', $scope.cboIndividualProof.individualproof_name);
            frm.append('idproof_no', $scope.txtidproof_no);
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstApplicationAdd/IndividualProofDocumentUpload';
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
                        $scope.txtindividualproof_document = '';
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }

                    var url = 'api/MstApplicationAdd/GetIndividualProofList';
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

    $scope.idproof_delete = function (contact2idproof_gid) {

        var params = {
            contact2idproof_gid: contact2idproof_gid
        }
        lockUI();
        var url = 'api/MstApplicationAdd/IndividualProofDelete';
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
            var url = 'api/MstApplicationAdd/GetIndividualProofList';
            SocketService.get(url).then(function (resp) {
                $scope.contactidproof_list = resp.data.contactidproof_list;
            });
            unlockUI();
        });
    }

    function individualaddress_list() {
        var url = 'api/MstApplicationAdd/GetAddressList';
        SocketService.get(url).then(function (resp) {
            $scope.contactindividualaddress_list = resp.data.contactaddress_list;
        });
    }

    $scope.address_add = function () {
        var modalInstance = $modal.open({
            templateUrl: '/addresstype.html',
            controller: ModalInstanceCtrl,
            size: 'lg'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
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
                    country: $scope.txtcountry
                }
                var url = 'api/MstApplicationAdd/AddressAdd';
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
        var url = 'api/MstApplicationAdd/AddressDelete';
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

    $scope.individualdocument_upload = function (val, val1, name) {
        lockUI();
        if (($scope.cboIndividualDocument == null) || ($scope.cboIndividualDocument == '') || ($scope.cboIndividualDocument == undefined)) {
            $("#fileIndividuaDocument").val('');
            Notify.alert('Kindly Enter the Document Title', 'warning');
            unlockUI();
        }
        else {
            var item = {
                name: val[0].name,
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
            frm.append('file_name', item.name);
            frm.append('document_title', $scope.cboIndividualDocument.individualdocument_name);
            frm.append('individualdocument_gid', $scope.cboIndividualDocument.individualdocument_gid);
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstApplicationAdd/IndividualDocumentUpload';
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

                    var url = 'api/MstApplicationAdd/GetIndividualDocList';
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

    $scope.individualdocument_delete = function (contact2document_gid) {

        var params = {
            contact2document_gid: contact2document_gid
        }
        lockUI();
        var url = 'api/MstApplicationAdd/IndividualDocDelete';
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
            var url = 'api/MstApplicationAdd/GetIndividualDocList';
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

        if (lsstakeholdertype_gid == '' || lsstakeholdertype_gid == undefined || lsstakeholdertype_gid == null) {
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
                application_gid: $scope.application_gid
            }
            var url = 'api/MstApplicationEdit/SaveIndividualDtlAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.txtpan_no = '';
                    $scope.txtaadhar_no = '';
                    $scope.txtfirst_name = '';
                    $scope.txtmiddle_name = '';
                    $scope.txtlast_name = '';
                    $scope.txtindividual_dob = '';
                    $scope.txtage = '';
                    $scope.cboGender = '';
                    $scope.cboDesignation = '';
                    $scope.rdbpep_status = '';
                    $scope.txtpepverified_date = '';
                    $scope.txtfather_firstname = '';
                    $scope.cboStakeholderType = '';
                    $scope.cboMaritalStatus = '';
                    $scope.txtfather_middlename = '';
                    $scope.txtfather_lastname = '';
                    $scope.txtindividual_dob = '';
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
                    $scope.txtspouse_age = '';
                    $scope.txtspouse_dob = '';
                    $scope.cboEducationalQualification = '';
                    $scope.txtmain_occupation = '';
                    $scope.txtannual_income = '';
                    $scope.txtmonthly_income = '';

                    $scope.cboIncomeType = '';
                    $scope.cboOwnershipType = '';
                    $scope.cboResidenceType = '';
                    $scope.cboPropertyin = '';
                    $scope.txtcurrentresidence_years = '';
                    $scope.txtbranch_distance = '';
                    $scope.contactmobileno_list = '';
                    $scope.contactemail_list = '';
                    $scope.uploadidproofdoc_list = '';
                    $scope.contactidproof_list = '';
                    $scope.contactindividualaddress_list = '';
                    $scope.uploadindividualdoc_list = '';
                    window.scrollTo(0, 0);
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
            application_gid: $scope.application_gid
        }
        var url = 'api/MstApplicationEdit/SubmitIndividualDtlAdd'; 
        lockUI();
        SocketService.post(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {
                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtpan_no = '';
                $scope.txtaadhar_no = '';
                $scope.txtfirst_name = '';
                $scope.txtmiddle_name = '';
                $scope.txtlast_name = '';
                $scope.txtindividual_dob = '';
                $scope.txtage = '';
                $scope.cboGender = '';
                $scope.cboDesignation = '';
                $scope.rdbpep_status = '';
                $scope.txtpepverified_date = '';
                $scope.txtfather_firstname = '';
                $scope.cboStakeholderType = '';
                $scope.cboMaritalStatus = '';
                $scope.txtfather_middlename = '';
                $scope.txtfather_lastname = '';
                $scope.txtindividual_dob = '';
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
                $scope.txtspouse_age = '';
                $scope.txtspouse_dob = '';
                $scope.cboEducationalQualification = '';
                $scope.txtmain_occupation = '';
                $scope.txtannual_income = '';
                $scope.txtmonthly_income = '';

                $scope.cboIncomeType = '';
                $scope.cboOwnershipType = '';
                $scope.cboResidenceType = '';
                $scope.cboPropertyin = '';
                $scope.txtcurrentresidence_years = '';
                $scope.txtbranch_distance = '';
                $scope.contactmobileno_list = '';
                $scope.contactemail_list = '';
                $scope.uploadidproofdoc_list = '';
                $scope.contactidproof_list = '';
                $scope.contactindividualaddress_list = '';
                $scope.uploadindividualdoc_list = '';
                window.scrollTo(0, 0);
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
    $scope.downloads = function (val1, val2) {
        var phyPath = val1;
        var relPath = phyPath.split("StoryboardAPI");
        var relpath1 = relPath[1].replace("\\", "/");
        var hosts = window.location.host;
        var prefix = location.protocol + "//";
        var str = prefix.concat(hosts, relpath1);
        var link = document.createElement("a");
        link.download = val2;
        var uri = str;
        link.href = uri;
        link.click();
    }
    //Institution
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
        var phyPath = val1;
        var relPath = phyPath.split("StoryboardAPI");
        var relpath1 = relPath[1].replace("\\", "/");
        var hosts = window.location.host;
        var prefix = location.protocol + "//";
        var str = prefix.concat(hosts, relpath1);
        var link = document.createElement("a");
        link.download = val2;
        var uri = str;
        link.href = uri;
        link.click();
    }

    $scope.save_institution = function () {
        if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
            Notify.alert('Kindly select Stakeholder Type', 'warning')
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
            if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                lsdesignation_gid = $scope.cboDesignation.designation_gid;
                lsdesignation_type = $scope.cboDesignation.designation_type;
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
                application_gid: $scope.application_gid
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
                    $scope.txtcompany_name = '';
                    $scope.txtincorporation_date = '';
                    $scope.txtbusinessstart_date = '';
                    $scope.txtyearin_business = '';
                    $scope.txtmonthsin_business = '';
                    $scope.txtpan_number = '';
                    $scope.txtcin_no = '';
                    $scope.txtofficialtelephone_number = '';
                    $scope.txtofficial_mailid = '';
                    $scope.cboCompanytype = '';
                    $scope.cboStakeholdertype = '';
                    $scope.cboCreditAssessmentagency = '';
                    $scope.cboAssessmentRating = '';
                    $scope.txtratingason_date = '';
                    $scope.cboAMLCategory = '';
                    $scope.cboBusinesscategory = '';
                    $scope.txtfirst_name = '';
                    $scope.txtmiddle_name = '';
                    $scope.txtlast_name = '';
                    $scope.txtstart_date = '';
                    $scope.txtend_date = '';
                    $scope.rdbescrow = '';
                    $scope.txtlastyear_turnover = '';
                    $scope.cboDesignation = '';
                    $scope.institutionmobileno_list = '';
                    $scope.institutionmaildetails_list = '';
                    $scope.institutionaddresslist = '';
                    $scope.institutionupload_list = '';
                    $scope.institutionlicense_list = '';
                    window.scrollTo(0, 0);
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
        if ($scope.cboStakeholdertype == null || $scope.cboStakeholdertype == '' || $scope.cboStakeholdertype == undefined) {
            Notify.alert('Kindly select Stakeholder Type', 'warning')
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
            if ($scope.cboDesignation != undefined || $scope.cboDesignation != null) {
                lsdesignation_gid = $scope.cboDesignation.designation_gid;
                lsdesignation_type = $scope.cboDesignation.designation_type;
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
                application_gid:$scope.application_gid
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
                    $scope.txtcompany_name = '';
                    $scope.txtincorporation_date = '';
                    $scope.txtbusinessstart_date = '';
                    $scope.txtyearin_business = '';
                    $scope.txtmonthsin_business = '';
                    $scope.txtpan_number = '';
                    $scope.txtcin_no = '';
                    $scope.txtofficialtelephone_number = '';
                    $scope.txtofficial_mailid = '';
                    $scope.cboCompanytype = '';
                    $scope.cboStakeholdertype = '';
                    $scope.cboCreditAssessmentagency = '';
                    $scope.cboAssessmentRating = '';
                    $scope.txtratingason_date = '';
                    $scope.cboAMLCategory = '';
                    $scope.cboBusinesscategory = '';
                    $scope.txtfirst_name = '';
                    $scope.txtmiddle_name = '';
                    $scope.txtlast_name = '';
                    $scope.txtstart_date = '';
                    $scope.txtend_date = '';
                    $scope.rdbescrow = '';
                    $scope.txtlastyear_turnover = '';
                    $scope.cboDesignation = '';
                    $scope.institutionmobileno_list = '';
                    $scope.institutionmaildetails_list = '';
                    $scope.institutionaddresslist = '';
                    $scope.institutionupload_list = '';
                    $scope.institutionlicense_list = '';
                    window.scrollTo(0, 0);
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
        if (($scope.rdbgstregistered == undefined) || ($scope.rdbgstregistered == '') || ($scope.cboGstState.state_name == undefined) || ($scope.cboGstState.state_name == '') || ($scope.txtgst_number == undefined) || ($scope.txtgst_number == '')) {
            Notify.alert('Enter GST State / Select GST Registered Status / GST Number', 'warning');
        }
        else {
            var params = {
                gststate_gid: $scope.cboGstState.state_gid,
                gst_state: $scope.cboGstState.state_name,
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
                $scope.cboGstState = '';
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
            size: 'lg'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
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
                    country: $scope.txtcountry
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

    // Institution License Add

    $scope.institutionlicenseadd = function () {

        if (($scope.cboLicenseType == '') || ($scope.cboLicenseType == undefined) || ($scope.txtnumber == '') || ($scope.txtnumber == undefined) || ($scope.txtissue_date == '') || ($scope.txtissue_date == undefined) || ($scope.txtexpiry_date == '') || ($scope.txtexpiry_date == undefined)) {
            Notify.alert('Kindly Fill All Mandatory Fields', 'warning');
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


    $scope.InstitutionDocumentUpload = function (val, val1, name) {
        if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbocompanydocumentname == null) || ($scope.cbocompanydocumentname == '') || ($scope.cbocompanydocumentname == undefined)) {
            $("#institutionfile").val('');
            Notify.alert('Kindly Enter the Document Title', 'warning');
        }
        else {
            var item = {
                name: val[0].name,
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
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_title', $scope.cbocompanydocumentname.companydocument_name);
            frm.append('companydocument_gid', $scope.cbocompanydocumentname.companydocument_gid);
            frm.append('document_id', $scope.txtdocument_id);
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/MstApplicationAdd/InstitutionDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $scope.institutionupload_list = resp.data.institutionupload_list;
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

    $scope.save_socialtradecapital = function () {
        var params = {
            social_capital: $scope.SocialCapital,
            trade_capital: $scope.TradeCapital,
        }
        var url = 'api/MstApplicationEdit/SocialAndTradeCapitalSubmit';
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
        });
    }
}

})();

