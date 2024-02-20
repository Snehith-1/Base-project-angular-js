(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstCheckerSanctionViewController', idasMstCheckerSanctionViewController);

    idasMstCheckerSanctionViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function idasMstCheckerSanctionViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasMstCheckerSanctionViewController';

        var sanction_gid = $location.search().sanction_gid;
        var lspage = $location.search().lspage;

        activate();

        function activate() {
            var url = 'api/IdasMstSanction/checkertmpdoc_delete';

            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/IdasMstSanction/SanctionDtlsEdit';
            var params = {
                sanction_gid: sanction_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.sanctionrefnoEdit = resp.data.sanction_refno;
                $scope.SanctionDateEdit = resp.data.sanction_date;
                $scope.SanctionAmountEdit = resp.data.sanction_amount;
                $scope.customerNameEdit = resp.data.customername;
                $scope.CustomerurnEdit = resp.data.customer_urn;
                $scope.verticalCodeEdit = resp.data.vertical;
                $scope.txtSanctionLimit = resp.data.sanction_limit;
                $scope.checkerletter_flag = resp.data.checkerletter_flag;

            });
            var url = 'api/IdasMstSanction/Getcheckerdocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    $scope.uploadedfile = true;
                    $scope.file_name = resp.data.document_name;
                    $scope.file_gid = resp.data.document_gid;
                }

            });

            var url = 'api/IdasMstSanction/SanctionmarkerEdit';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.makerfile_name = resp.data.makerfile_name;
                $scope.makerfile_path = resp.data.makerfile_path;
            });

            var url = 'api/IdasMstSanction/SanctioncheckerEdit';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.checkerfile_name = resp.data.checkerfile_name;
                $scope.checkerfile_path = resp.data.checkerfile_path;
                $scope.uploaded_by = resp.data.uploaded_by;
                $scope.uploaded_date = resp.data.updated_date;
            });
        }
        $scope.checkerdoc_download = function (val1, val2) {
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
        $scope.makerdoc_download = function (val1, val2) {
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

        $scope.checkerUpdate = function () {
            var url = 'api/IdasMstSanction/UpdateCheckerdoc';
            var params = {
                sanction_gid: sanction_gid
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.idasMstCheckerPendingSummary')
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
        $scope.Checkerback = function () {
            var url = 'api/IdasMstSanction/checkertmpdoc_delete';

            SocketService.get(url).then(function (resp) {

            });
            if (lspage == 'Pending') {
                $state.go('app.idasMstCheckerPendingSummary')
            }
            else {
                $state.go('app.idasMstCheckerCompletedSummary')
            }
        }

        $scope.canceluploadedfile = function () {
            var params = {
                sanction_gid: sanction_gid
            };
            var url = 'api/IdasMstSanction/checkerdoc_delete';

            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.uploadedfile = false;
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

        $scope.uploades_checkerfile = function (val, val1, name) {

            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

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
            frm.append('sanction_gid', sanction_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            lockUI();
            var url = 'api/IdasMstSanction/Checkerupload_file';

            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#uploadwordfile").val('');

                unlockUI();
                if (resp.data.status == true) {
                    var url = 'api/IdasMstSanction/Getcheckertmpdocument';

                    SocketService.get(url).then(function (resp) {
                        if (resp.data.status == true) {

                            $scope.uploadedfile = true;
                            $scope.file_name = resp.data.document_name;
                            $scope.file_gid = resp.data.document_gid;
                            $scope.checkerfile_name = resp.data.checkerfile_name;
                            $scope.checkerfile_path = resp.data.checkerfile_path;
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
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }

        $scope.downloadsBAL = function (val1, val2) {
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

        $scope.esdownloaddocument = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }


        $scope.downloadmail = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }


        $scope.downloadsCAM = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }


        $scope.downloadsMOM = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.downloadsanctionletter = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.downloadsgeneral = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = val2;
            var uri = str;
            link.href = uri;
            link.click();
        }

    }
})();