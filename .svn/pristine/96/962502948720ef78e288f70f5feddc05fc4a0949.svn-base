(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCreditInstitutionDtlAddController', AgrTrnSuprCreditInstitutionDtlAddController);

    AgrTrnSuprCreditInstitutionDtlAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrTrnSuprCreditInstitutionDtlAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCreditInstitutionDtlAddController';
        $scope.institution_gid = $location.search().institution_gid;
        var institution_gid = $scope.institution_gid;
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        /*  lockUI(); */
        activate();
        function activate() {

            var url = 'api/AgrMstSuprApplicationAdd/GetInstitutionBureauTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var param = {
                institution_gid: institution_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/GetInstitutionBureauList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.institutionbureau_list = resp.data.institutionbureau_list;
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

            var url = 'api/AgrMstSuprApplication360/BureauNameList';
            SocketService.get(url).then(function (resp) {
                $scope.bureau_list = resp.data.bureauname_list;
            });


            $scope.reportgeneration_success = false;
            $scope.reportgeneration_failure = false;

        }

        $scope.bureauname_change = function () {

            var bureauname_name = $('#BureauName :selected').text();

            if (bureauname_name == 'High Mark') {

                $scope.reportbureauselected = true;

            } else {

                $scope.reportbureauselected = false;

            }
        };

        $scope.generateDetails = function () {
            $scope.html_content = '';

            var bureauname_name = $('#BureauName :selected').text();
            if (bureauname_name == 'TransUnion') {

                var params = {
                    institution_gid: institution_gid
                }
                var url = 'api/AgrSuprBureauAPI/GetTransUnionCommercialCreditInfo';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        $scope.bureauscore_disabled = true;
                        $scope.txtbureau_score = resp.data.bureau_score;

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.reportbureauselected = false;

                        var param = {
                            institution2bureau_gid: $scope.institution2bureau_gid
                        }
                        var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                        });

                        unlockUI();
                    }
                    else {
                        $scope.bureauscore_disabled = false;

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.reportbureauselected = false;
                        unlockUI();
                    }
                });



            }
            else if (bureauname_name == 'High Mark') {
                $scope.html_content = '';
                var params = {
                    institution_gid: $scope.institution_gid
                }
                var url = 'api/AgrSuprBureauAPI/GetHighmarkInstitutionCreditInfo';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        $scope.bureauscore_disabled = true;
                        $scope.bureauresponse_disabled = true;
                        $scope.txtbureau_score = resp.data.bureau_score;
                        $scope.txtbureau_response = resp.data.bureau_response;

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        $scope.reportbureauselected = false;
                        var param = {
                            institution2bureau_gid: $scope.institution2bureau_gid
                        };
                        var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                        });
                        unlockUI();
                    }
                    else {
                        $scope.bureauscore_disabled = false;
                        $scope.bureauresponse_disabled = false;

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        $scope.reportbureauselected = false;
                        unlockUI();
                    }
                });
            }
            var paramstemp = {
                institution2bureau_gid: $scope.institution2bureau_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
            SocketService.getparams(url, paramstemp).then(function (resp) {
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });


        };

        $scope.report_View = function (tmpcicdocument_gid) {
            var tmpcicdocument_gid = tmpcicdocument_gid;
            localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
            var URL = location.protocol + "//" + location.hostname + "/v1/#/app/AgrSuprHighmarkInstitutionReport";
            window.open(URL, '_blank');
        };

        $scope.add_bureauDtl = function () {
            if (($scope.cboBureauName == undefined) || ($scope.txtbureauscore_date == undefined) || ($scope.txtbureau_score == undefined)
            || ($scope.txtobservations == undefined) || ($scope.txtbureau_response == undefined)) {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                var params = {
                    institution_gid: $scope.institution_gid,
                    bureauname_gid: $scope.cboBureauName.bureauname_gid,
                    bureauname_name: $scope.cboBureauName.bureauname_name,
                    bureau_score: $scope.txtbureau_score,
                    bureauscore_date: $scope.txtbureauscore_date,
                    observations: $scope.txtobservations,
                    bureau_response: $scope.txtbureau_response,
                }
                var url = 'api/AgrMstSuprApplicationAdd/PostCICUploadInstitution';
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
                    var param = {
                        institution_gid: $scope.institution_gid
                    };
                    var url = 'api/AgrMstSuprApplicationAdd/GetInstitutionBureauList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.institutionbureau_list = resp.data.institutionbureau_list;

                    });

                    $scope.cboBureauName = '';
                    $scope.txtbureauscore_date = '';
                    $scope.txtobservations = '';
                    $scope.txtbureau_response = '';
                    $scope.txtbureau_score = '';
                    $scope.cicuploaddoc_list = '';


                });
            }
        }


        $scope.bureau_delete = function (institution2bureau_gid) {
            var params = {
                institution2bureau_gid: institution2bureau_gid
            }
            var url = 'api/AgrMstSuprApplicationAdd/DeleteInstitutionBureau';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
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



        $scope.CICDocumentUpload = function (val, val1, name) {
            
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }

            var item = {
                file: val[0]
            };
            var frm = new FormData();
            frm.append('file', item.file);

            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                lockUI();
                var url = 'api/AgrMstSuprApplicationAdd/CICInstitutionDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcic_document = "";
                        $scope.uploadfrm = undefined;
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var params = {
                        institution2bureau_gid: $scope.institution2bureau_gid
                    };
                    var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }

        $scope.uploaddocumentcancel = function (tmpcicdocument_gid) {
            lockUI();
            var params = {
                tmpcicdocument_gid: tmpcicdocument_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocDelete';
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
                var params = {
                    institution2bureau_gid: $scope.institution2bureau_gid
                };
                var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });

                unlockUI();
            });
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
            $location.url('app/AgrTrnSuprStartCreditUnderwriting?application_gid=' + application_gid);
        }

        $scope.company_deferraldocchecklist = function () {
            $location.url('app/AgrTrnSuprDocumentCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_covenantdocchecklist = function () {
            $location.url('app/AgrTrnSuprCreditAddCovenantCheckList?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_geneticadd = function () {
            $location.url('app/AgrTrnSuprCreditCompanyDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_PSLdata = function () {
            $location.url('app/AgrTrnSuprCreditPSLDataFlaggingAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_supplier = function () {
            $location.url('app/AgrTrnSuprCreditSuppliersDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_buyer = function () {
            $location.url('app/AgrTrnSuprCreditBuyerDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_bankaccount = function () {
            $location.url('app/AgrTrnSuprCreditBankAccountDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_existingbankaccount = function () {
            $location.url('app/AgrTrnSuprCreditExistingBankDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_repayment = function () {
            $location.url('app/AgrTrnSuprCreditRepaymentDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.company_observation = function () {
            $location.url('app/AgrTrnSuprCreditObservationAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.tan_verification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=TAN' + '&lspage=' + lspage);
        }
        $scope.companyllpno_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LLPNO' + '&lspage=' + lspage);
        }
        $scope.mcasign_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=MCASIGNATURE' + '&lspage=' + lspage);
        }
        $scope.iecdetailed_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=IECDETAILED' + '&lspage=' + lspage);
        }
        $scope.fssai_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FSSAI' + '&lspage=' + lspage);
        }
        $scope.fda_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=FDA' + '&lspage=' + lspage);
        }
        $scope.gst_verification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=GST' + '&lspage=' + lspage);
        }
        $scope.lpgid_verification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=LPGID' + '&lspage=' + lspage);
        }
        $scope.shop_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=SHOP' + '&lspage=' + lspage);
        }
        $scope.rcauthadv_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCAUTHADV' + '&lspage=' + lspage);
        }
        $scope.rcsearch_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=RCSEARCH' + '&lspage=' + lspage);
        }
        $scope.propertytax_vertification = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROPERTYTAX' + '&lspage=' + lspage);
        }
        $scope.probe42_api = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=PROBEAPI' + '&lspage=' + lspage);
        }
        $scope.company_bankstatement = function () {
            $location.url('app/AgrTrnSuprCreditBankStatementAnalysisAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.company_fsa = function () {
            $location.url('app/AgrTrnSuprCreditFsaDetailAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }

        $scope.bureau_edit = function (institution2bureau_gid) {
            $location.url('app/AgrTrnSuprCreditInstitutionBureauEdit?lsinstitution2bureau_gid=' + institution2bureau_gid + '&lsinstitution_gid=' + $scope.institution_gid + '&lsapplication_gid=' + application_gid + '&lspage=' + lspage);
        }

        $scope.bureau_view = function (institution2bureau_gid) {
            $location.url('app/AgrTrnSuprCreditInstitutionBureauView?lsinstitution2bureau_gid=' + institution2bureau_gid + '&lsinstitution_gid=' + $scope.institution_gid + '&lsapplication_gid=' + application_gid + '&lspage=' + lspage);
        }

        $scope.institution_bureauadd = function () {
            $location.url('app/AgrTrnSuprCreditInstitutionDtlAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid);
        }
        $scope.crimecheck_record = function () {
            $location.url('app/AgrTrnSuprCompanyCrimeCheckRecordAPI?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lspage=' + lspage);
        }
        $scope.crimecheckreport_realtime = function () {
            $location.url('app/AgrTrnSuprCreditCompanyAPIAdd?application_gid=' + application_gid + '&institution_gid=' + institution_gid + '&lsapi_name=CRIMECHECKREPORTREALTIME' + '&lspage=' + lspage);
        }

        $scope.downloadall = function () {
            for (var i = 0; i < $scope.cicuploaddoc_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.cicuploaddoc_list[i].document_path, $scope.cicuploaddoc_list[i].document_name);
            }
        }

    }
})();
