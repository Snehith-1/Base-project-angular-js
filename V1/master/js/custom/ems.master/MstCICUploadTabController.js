(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCICUploadTabController', MstCICUploadTabController);

    MstCICUploadTabController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstCICUploadTabController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCICUploadTabController';
        var application_gid = $location.search().lsapplication_gid;
        var application_gid = $scope.application_gid;
        var lstab = $location.search().lstab;
        var lsstatus = $location.search().lsstatus;

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstApplicationAdd/GetGeneralInfo';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.lblvertical_name = resp.data.vertical_name;
                $scope.lblcustomer_urn = resp.data.customer_urn;
                $scope.lblcreated_by = resp.data.created_by;
                $scope.lblcreated_date = resp.data.created_date;
                $scope.application_gid = resp.data.application_gid;
                $scope.application_status = resp.data.application_status;
                $scope.applicant_type = resp.data.applicant_type;
                });
            
            var url = 'api/MstApplicationAdd/GetIndividualTempClear';
            SocketService.get(url).then(function (resp) {
            });

            $scope.application_status == 'null';

            var url = 'api/MstApplicationAdd/GetCICIndividualSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cicindividual_list = resp.data.cicindividual_list;
            });

            var url = 'api/MstApplicationAdd/GetCICInstitutionSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cicinstitution_list = resp.data.cicinstitution_list;
            });
            var url = 'api/MstApplicationAdd/GetAppProductcharges';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.economical_flag = resp.data.economical_flag;
              
                if ($scope.economical_flag == 'Y') {
                    $scope.social_tradetab = false;
                    $scope.social_trade = true;
                }
                else {
                    $scope.social_tradetab = true;
                    $scope.social_trade = false;
                }
            });
            var url = 'api/MstApplicationAdd/GetProceed';
            SocketService.get(url).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                unlockUI();
            });
            var proceed_flag = $scope.proceed_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                }
                else if ($scope.hierarchyupdated_flag == 'Y' && proceed_flag == 'Y') {
                    $scope.done_enable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N') {
                    $scope.done_disable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_enable = false;
                }
                else {

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

                var url = 'api/MstApplication360/BureauNameList';
                SocketService.get(url).then(function (resp) {
                    $scope.bureau_list = resp.data.bureauname_list;
                });

                var param = {
                    contact_gid: contact_gid
                };

                var url = 'api/MstApplicationEdit/CICIndividualEdit';

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
                var url = 'api/MstApplicationEdit/CICUploadIndividualDocList';
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
                    var url = 'api/BureauAPI/GetHighmarkCreditInfo';
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {

                        if (resp.data.status == true) {
                            $scope.bureauscore_disabled = true;
                            $scope.bureauresponse_disabled = true;
                            $scope.txtbureau_score = resp.data.bureau_score;
                            $scope.txtbureau_response = resp.data.bureau_response;
                            var url = 'api/MstApplicationEdit/CICUploadIndividualDocList';
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
                    frm.append('file', item.file);

                    frm.append('document_name', $scope.documentname);
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        lockUI();
                        var url = 'api/MstApplicationAdd/CICIndividualDocumentUpload';
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
                            var url = 'api/MstApplicationEdit/CICUploadIndividualDocList';
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
                    var url = 'api/MstApplicationEdit/CICUploadIndividualDocDelete';
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
                        var url = 'api/MstApplicationEdit/CICUploadIndividualDocList';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                        });

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
                    var url = 'api/MstApplicationAdd/PostCICUploadIndividual';
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

                var url = 'api/MstApplication360/BureauNameList';
                SocketService.get(url).then(function (resp) {
                    $scope.bureau_list = resp.data.bureauname_list;
                });

                var param = {
                    institution_gid: institution_gid
                };

                var url = 'api/MstApplicationEdit/CICInstitutionEdit';

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

                var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.CICDocumentUpload = function (val, val1, name) {
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
                    frm.append('file', item.file);

                    frm.append('document_name', $scope.documentname);
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        lockUI();
                        var url = 'api/MstApplicationAdd/CICInstitutionDocumentUpload';
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
                            var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
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
                    var url = 'api/MstApplicationEdit/CICUploadInstitutionDocDelete';
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
                        var url = 'api/MstApplicationEdit/CICUploadInstitutionDocList';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.cicuploaddoc_list = resp.data.cicuploaddoc_list;
                        });

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
                    var url = 'api/MstApplicationAdd/PostCICUploadInstitution';
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
            $location.url('app/MstApplicationCreationSummary');
        }

        $scope.general_Tab = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
            {
                $location.url('app/MstApplicationGeneralAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls=true;
                }
            }

        $scope.company_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
            {
                $location.url('app/MstApplicationInstitutionAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
            }
            else {
                $scope.Company_dtls=true;
                }
            }

        $scope.Individual_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/MstApplicationIndividualAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
                else {
                    $scope.Individual_dtls=true;
                    }
                }

        $scope.Group_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
                if ($scope.application_status=='Completed')
                {
                    $location.url('app/MstApplicationGroupAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
                else {
                    $scope.Group_dtls=true;
                }
            }

        $scope.EconomicCapital_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/MstApplicationSocialTradeCapitalAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
                else {
                    $scope.EconomicCapital_dtls=true;
                }
            }

        $scope.OverallLimit_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.OverallLimit_dtls=true;
                }
                else {                    
                    $location.url('app/MstApplicationOverallLimitAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.ProductCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.ProductCharges_dtls=true;
                }
                else {                    
                    $location.url('app/MstApplicationProductChargesAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.ServiceCharges_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.ServiceCharges_dtls=true;
                }
                else {                    
                    $location.url('app/MstApplicationServiceChargeAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.Hypothecation_add = function () {
            var application_gid = $scope.application_gid;
            var applicant_type = $scope.applicant_type;
            if ($scope.applicant_type == null || $scope.applicant_type == '' )
                {
                    $scope.Hypothecation_dtls=true;
                }
                else {                    
                    $location.url('app/MstApplicationHypothecationAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsapplicant_type=' + applicant_type);
                }
            }

        $scope.BureauUpdates_add = function () {
            var application_gid = $scope.application_gid;
            var application_status = $scope.application_status;
            if ($scope.application_status=='Completed')
                {
                    $location.url('app/MstApplicationCICUploadAdd?lsapplication_gid=' + application_gid + '&lstab=add&lsstatus=' + application_status);
                }
            else {
                 $scope.BureauUpdates_dtls=true;
                }
            }

        $scope.doneclick = function () {
            lockUI();
            var url = 'api/MstApplicationAdd/GetGeneralInfo';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.application_status = resp.data.application_status;
            });
            lockUI();
            var url = 'api/MstApplicationAdd/GetProceed';
            SocketService.get(url).then(function (resp) {
                $scope.proceed_flag = resp.data.proceed_flag;
                $scope.level_zero = resp.data.level_zero;
                $scope.level_one = resp.data.level_one;
                $scope.clusterhead = resp.data.cluster_head;
                $scope.zonalhead = resp.data.zonal_head;
                $scope.regionhead = resp.data.regional_head;
                $scope.businesshead = resp.data.business_head;
                unlockUI();
            });
            var proceed_flag = $scope.proceed_flag;
            var application_gid = $scope.application_gid;
            var params = {
                application_gid: application_gid
            }

            var url = 'api/MstApplicationAdd/GetApprovalHierarchyFlag';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lshierarchychange_flag = resp.data.lshierarchychange_flag;
                $scope.hierarchyupdated_flag = resp.data.hierarchyupdated_flag;
                if ($scope.hierarchyupdated_flag == 'N' && proceed_flag == 'Y') {
                    $scope.hierarchyshow = true;
                    $scope.done_disable = true;
                    $scope.done_enable = false;
                }
                else if ($scope.hierarchyupdated_flag == 'Y' && proceed_flag == 'Y') {
                    $scope.done_enable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_disable = false;
                }
                else if (proceed_flag == 'N') {
                    $scope.done_disable = true;
                    $scope.hierarchyshow = false;
                    $scope.done_enable = false;
                }
                else {

                }
            });
        }
        
        $scope.overallsubmit_application = function () {

            var params = {
    
            }
            var url = 'api/MstApplicationAdd/PostAppProceed';
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
                $state.go('app.MstApplicationCreationSummary');
            });
    
        }

        $scope.bureauupdate_individual = function (contact_gid) {
            $location.url('app/BureauUpdateIndividual?lscontact_gid=' + contact_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
        }

        $scope.bureauupdate_institution = function (institution_gid) {
            $location.url('app/BureauUpdateInstitution?lsinstitution_gid=' + institution_gid + '&lsapplication_gid=' + application_gid + '&lstab=' + lstab + '&lsstatus=' + lsstatus);
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

    }
})();

