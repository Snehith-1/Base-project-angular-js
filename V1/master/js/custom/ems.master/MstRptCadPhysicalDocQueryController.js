(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRptCadPhysicalDocQueryController', MstRptCadPhysicalDocQueryController);

    MstRptCadPhysicalDocQueryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstRptCadPhysicalDocQueryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRptCadPhysicalDocQueryController';
        var application_gid = $location.search().application_gid;
        var lspage = $location.search().lspage;
        var credit_gid = $location.search().credit_gid;
        var lstype = $location.search().lstype;
        var lsdocumentcheckdtl_gid = $location.search().lsdocumentcheckdtl_gid;
        var processtypeassign_gid = $location.search().processtypeassign_gid;
        var lspath = $location.search().lspath;

        activate();
        lockUI();
        function activate() {
            lockUI();
            var url = 'api/MstPhysicalDocument/tmpphysicalclearQueryuploaded';
            SocketService.get(url).then(function (resp) {
            });
            var param = {
                application_gid: application_gid
            };

            var url = 'api/MstApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblapplref_no = resp.data.application_no;
                $scope.lblapplicant_name = resp.data.customer_name;
                $scope.lblcurrentappl_stage = resp.data.approval_status;
                $scope.lblappl_state = resp.data.region;
                $scope.lbloveralllimit_request = resp.data.overalllimit_amount;
            });

            var param = {
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            };
            var url = 'api/MstPhysicalDocument/GetPhysicalAppcadQuerySummary';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.lbldocumentcode = resp.data.documenttype_code;
                $scope.lbldocumenttype_name = resp.data.documenttype_name;
                $scope.query_list = resp.data.mdlcadquery;
            });

            var params = {
                groupdocumentcheckdtl_gid: lsdocumentcheckdtl_gid
            }
            var url = 'api/MstPhysicalDocument/GetMakerCheckerConversation';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.makercheckerconversation = resp.data.mdlmakercheckerconversation;
            });

            if (lspage === 'CadPhysicalCompleted') {
                $scope.hideeditevent = false;
            }
            else {
                $scope.hideeditevent = true;
            }
        }

        $scope.query_add = function () {
            var params = {
                query_title: $scope.txtquery_title,
                query_description: $scope.txtquery_desc,
                application_gid: application_gid,
                documentcheckdtl_gid: lsdocumentcheckdtl_gid
            }
            var url = 'api/MstPhysicalDocument/PostAppcadPhysicalqueryadd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.txtquery_title = '';
                    $scope.txtquery_desc = '';
                    $scope.institutionupload_list = '';
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'error',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });

            $modalInstance.close('closed');
        }

        $scope.QueryDocumentUpload = function (val, val1, name) {
            lockUI();
            var frm = new FormData();

            for (var i = 0; i < val.length; i++) {
                var item = {
                    name: val[i].name,
                    file: val[i]
                };
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
            }

            frm.append('tagquery_gid', "");
            frm.append('documentcheckdtl_gid', lsdocumentcheckdtl_gid);

            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstPhysicalDocument/PhysicalQueryDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#queryfile").val('');

                    var params = {
                        tagquery_gid: ''
                    }
                    var url = 'api/MstPhysicalDocument/GetTmpPhysicalQueryDocument';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.institutionupload_list = resp.data.queryuploaddocument;
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
            else {
                alert('Please select a file.')

            }
        }
        $scope.download_doc2 = function (val1, val2) {
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

        $scope.defdoc_delete = function (tagquerydocument_gid, tagquery_gid) {
            lockUI();
            var params = {
                tagquerydocument_gid: tagquerydocument_gid
            }
            var url = 'api/MstScannedDocument/cancelQueryuploaddocument';
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
                var params = {
                    tagquery_gid: tagquery_gid
                }
                var url = 'api/MstPhysicalDocument/GetTmpPhysicalQueryDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.institutionupload_list = resp.data.queryuploaddocument;
                });
                unlockUI();
            });
        }


        $scope.download_doc = function (val1, val2) {
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

        $scope.view_querydesc = function (query_description, query_responseremarks) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblquery_responseremarks = query_responseremarks;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.defferaldoc_view = function (tagquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/document_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    tagquery_gid: tagquery_gid
                }
                var url = 'api/MstScannedDocument/GetQueryDocument';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.queryuploaddocument = resp.data.queryuploaddocument;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.download_doc1 = function (val1, val2) {
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
            }

        }

        $scope.responseclick = function () {
            var maker_flag = "N";
            if (lspage == "CadPhysicalDocMaker") {
                maker_flag = "Y";
            }
            var params = {
                send_message: $scope.txtresponse,
                groupdocumentdtl_gid: lsdocumentcheckdtl_gid,
                application_gid: application_gid,
                credit_gid: credit_gid,
                maker_flag: maker_flag
            }
            var url = 'api/MstPhysicalDocument/postPhysicalMakerCheckerConversation';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var params = {
                        groupdocumentcheckdtl_gid: lsdocumentcheckdtl_gid
                    }
                    var url = 'api/MstPhysicalDocument/GetMakerCheckerConversation';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        $scope.txtresponse = "";
                        $scope.makercheckerconversation = resp.data.mdlmakercheckerconversation;
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'error',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.clearmessage = function () {
            $scope.txtresponse = "";
        }


        $scope.Back = function () {
            $location.url('app/MstRptCadPhysicalDocchecklist?application_gid=' + application_gid + '&credit_gid=' + credit_gid + '&lspage=' + lspage + '&lstype=' + lstype + '&processtypeassign_gid=' + processtypeassign_gid + '&lspath=' + lspath);

        }

    }
})();
