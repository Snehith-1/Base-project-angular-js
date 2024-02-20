(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprApplicationEditCICUploadAddController', AgrMstSuprApplicationEditCICUploadAddController);

    AgrMstSuprApplicationEditCICUploadAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$filter', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstSuprApplicationEditCICUploadAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $filter, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprApplicationEditCICUploadAddController';
        var application_gid = $location.search().lsapplication_gid;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;

        activate();

        function activate() {
            $scope.application_gid = $location.search().lsapplication_gid;
            $scope.application_status = $location.search().lsstatus;

            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                $scope.onboarding_status = resp.data.onboarding_status;
                unlockUI();
            });

            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstSuprApplicationAdd/GetApprovalHierarchyFlag';
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

            var url = 'api/AgrMstSuprApplicationAdd/GetIndividualTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/AgrMstSuprApplicationEdit/GetCICEditIndividualSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cicindividuallist = resp.data.cicindividual_list;
            });

            var url = 'api/AgrMstSuprApplicationEdit/GetCICEditInstitutionSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cicinstitutionlist = resp.data.cicinstitution_list;
            });

            var urls = 'api/AgrMstApplicationAdd/CICUploadIndividualDocTempList';
            lockUI();
            SocketService.get(urls).then(function (resp) {
                unlockUI();
                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
            });

            var url = 'api/AgrMstSuprApplicationEdit/GetEditProductcharges';
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
        }

        $scope.cicupload_individual = function (contact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/CICUpload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                // Calender Popup... //

                vm.calender20 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    vm.open20 = true;
                };

                var url = 'api/AgrMstApplication360/BureauNameList';
                SocketService.get(url).then(function (resp) {
                    $scope.bureau_list = resp.data.bureauname_list;
                });

                var param = {
                    contact_gid: contact_gid
                };

                var url = 'api/AgrMstSuprApplicationEdit/CICIndividualEdit';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtscore_on = new Date(resp.data.bureauscoredateedit);
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.txtbureau_document = resp.data.txtbureau_document;
                    $scope.contact_gid = resp.data.contact_gid;

                    unlockUI();
                });
                var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });

                $scope.bureauname_change = function () {
                    var bureauname_name = $('#BureauName :selected').text();
                    if (bureauname_name == 'High Mark') {
                        $scope.highmarkselected = true;
                    } else {
                        $scope.highmarkselected = false;
                    }
                };

                $scope.generateHighmarkReport = function () {
                    $scope.html_content = '';
                    var url = 'api/AgrBureauAPI/GetHighmarkCreditInfo';
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {

                        if (resp.data.status == true) {
                            $scope.bureauscore_disabled = true;
                            $scope.bureauresponse_disabled = true;
                            $scope.txtbureau_score = resp.data.bureau_score;
                            $scope.txtbureau_response = resp.data.bureau_response;
                            var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
                            SocketService.getparams(url, param).then(function (resp) {
                                $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                            });
                            $scope.reportgeneration_success = true;
                            $scope.reportgeneration_message = 'Report Generated';
                            $scope.highmarkselected = false;
                            unlockUI();
                        }
                        else {
                            $scope.bureauscore_disabled = false;
                            $scope.bureauresponse_disabled = false;

                            $scope.reportgeneration_failure = true;
                            $scope.reportgeneration_message = 'Report Generation Failed';
                            $scope.highmarkselected = false;
                            unlockUI();
                        }
                    });                    
                };
                
                $scope.report_View = function (tmpcicdocument_gid) {
                    var tmpcicdocument_gid = tmpcicdocument_gid;
                    localStorage.setItem('tmpcicdocument_gid', tmpcicdocument_gid);
                    var URL = location.protocol + "//" + location.hostname + "/v1/#/app/HighmarkReport";
                    window.open(URL, '_blank');
                };

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

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
                        var url = 'api/AgrMstSuprApplicationAdd/CICIndividualDocumentUpload';
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
                            var param = {
                                contact_gid: contact_gid
                            };
                            var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
                            SocketService.getparams(url, param).then(function (resp) {
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
                    var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocDelete';
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
                        var param = {
                            contact_gid: contact_gid
                        };
                        var url = 'api/AgrMstSuprApplicationEdit/CICUploadIndividualDocList';
                        SocketService.getparams(url, param).then(function (resp) {
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

                $scope.update_CICUploads = function () {
                    var bureauname_name = $('#BureauName :selected').text();
                    var params = {
                        bureauname_name: bureauname_name,
                        bureauname_gid: $scope.bureau_gid,
                        bureau_score: $scope.txtbureau_score,
                        bureauscore_date: $scope.txtscore_on,
                        bureauscoredate: $scope.txtscore_on,
                        observations: $scope.txtobservations,
                        bureau_response: $scope.txtbureau_response,
                        contact_gid: $scope.contact_gid

                    }
                    var url = 'api/AgrMstSuprApplicationAdd/PostCICUploadIndividual';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            $modalInstance.close('closed');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        $modalInstance.close('closed');
                    });
                }
            }
        }

        $scope.cicupload_institution = function (institution_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/CICUpload.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/AgrMstApplication360/BureauNameList';
                SocketService.get(url).then(function (resp) {
                    $scope.bureau_list = resp.data.bureauname_list;
                });

                var param = {
                    institution_gid: institution_gid
                };

                var url = 'api/AgrMstSuprApplicationEdit/CICInstitutionEdit';

                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bureauname_name = resp.data.bureauname_name;
                    $scope.bureau_gid = resp.data.bureauname_gid;
                    $scope.txtbureau_score = resp.data.bureau_score;
                    $scope.txtscore_on = new Date(resp.data.bureauscoredateedit);
                    $scope.txtobservations = resp.data.observations;
                    $scope.txtbureau_response = resp.data.bureau_response;
                    $scope.txtbureau_document = resp.data.txtbureau_document;
                    $scope.institution_gid = resp.data.institution_gid;

                    unlockUI();
                });

                var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

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
                            unlockUI();
                            $("#file").val('');
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
                            var param = {
                                institution_gid: institution_gid
                            };
                            var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
                            SocketService.getparams(url, param).then(function (resp) {
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
                        var param = {
                            institution_gid: institution_gid
                        };
                        var url = 'api/AgrMstSuprApplicationEdit/CICUploadInstitutionDocList';
                        SocketService.getparams(url, param).then(function (resp) {
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



                $scope.update_CICUploads = function () {
                    var bureauname_name = $('#BureauName :selected').text();
                    var params = {
                        bureauname_name: bureauname_name,
                        bureauname_gid: $scope.bureau_gid,
                        bureau_score: $scope.txtbureau_score,
                        bureauscore_date: $scope.txtscore_on,
                        bureauscoredate_edit: $scope.txtscore_on,
                        observations: $scope.txtobservations,
                        bureau_response: $scope.txtbureau_response,
                        institution_gid: $scope.institution_gid

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
                            activate();
                            $modalInstance.close('closed');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                        }
                    });
                }
            }
        }

        $scope.Back = function () {
            $location.url('app/AgrMstSuprApplicationCreationSummary');
        }

        var params = {
            application_gid: $scope.application_gid
        }

        $scope.doneclick = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/EditProceed';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                $scope.approveinitiated_flag = resp.data.approveinitiated_flag;
                $scope.onboarding_status = resp.data.onboarding_status;
                unlockUI();
            });
            var proceed_flag = $scope.proceed_flag;
            var approveinitiated_flag = $scope.approveinitiated_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/AgrMstSuprApplicationAdd/GetApprovalHierarchyFlag';
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
        
        $scope.submit = function () {
            lockUI();
            var params = {
                application_gid: $scope.application_gid
            }
            var url = 'api/AgrMstSuprApplicationEdit/EditAppReProceed';            
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
                $state.go('app.AgrMstSuprApplicationCreationSummary');
            });

        }

        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstSuprApplicationGeneralEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls = true;
            }
        }

        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstSuprApplicationInstitutionEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Institution_dtls = true;
            }
        }
        $scope.Group_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstSuprApplicationGroupEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Group_dtls = true;
            }
        }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstSuprApplicationIndividualEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.Individual_dtls = true;
            }
        }

        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstSuprApplicationSocialTradeCapitalEdit?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.EconomicCapital_dtls = true;
            }
        }

        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstSuprAppEditProductAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }
        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Hypothecation_dtls = true;
            }
            else {
                $location.url('app/AgrMstSuprAppEditHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status == 'Completed') {
                $location.url('app/AgrMstSuprApplicationEditCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsstatus=' + application_status);
            }
            else {
                $scope.BureauUpdates_dtls = true;
            }
        }

        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ProductCharges_dtls = true;
            }
            else {
                $location.url('app/AgrMstSuprAppEditOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.ServiceCharges_dtls = true;
            }
            else {
                $location.url('app/AgrMstSuprAppEditChargeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.Tradeclick = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;

            if ($scope.applicant_type == null || $scope.applicant_type == '') {
                $scope.Trade_dtls = true;
            }
            else {
                $location.url('app/AgrMstSuprAppEditTradeAdd?lsapplication_gid=' + application_gid + '&lstab=edit&lsapplicant_type=' + applicant_type);
            }
        }

        $scope.overallsubmit_application = function () {
            var url = 'api/AgrMstSuprApplicationEdit/EditAppProceed';
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
                $state.go('app.AgrMstSuprApplicationCreationSummary');
            });

        }

        $scope.bureauupdate_individual = function (contact_gid) {
            $location.url('app/AgrMstSuprBureauUpdateIndividual?lscontact_gid=' + contact_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
        }

        $scope.bureauupdate_institution = function (institution_gid) {
            $location.url('app/AgrMstSuprBureauUpdateInstitution?lsinstitution_gid=' + institution_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
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

                var url = 'api/AgrMstSuprApplicationAdd/GetApprovalHierarchyChangeList';
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
                    var url = 'api/AgrMstSuprApplicationAdd/UpdateApprovalHierarchyChange';
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

    }
})();
