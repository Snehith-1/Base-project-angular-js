(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnSanctionMIS', idasTrnSanctionMIS);

    idasTrnSanctionMIS.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnSanctionMIS($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnSanctionMIS';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/IdasSanctionMIS/GetSanctionMISSummary";
            SocketService.get(url).then(function (resp) {
                $scope.sanctionlist = resp.data.sanctionMISdtl;
                if ($scope.sanctionlist == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.sanctionlist.length;
                    if ($scope.sanctionlist.length < 100) {
                        $scope.totalDisplayed = $scope.sanctionlist.length;
                    }
                }
                unlockUI();
            });
        }

        document.getElementById('pagecount').onkeyup = function () {

            if ($scope.pagecount == null) {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#DCDCDC';
            }
            else {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#ffa';
            }
        };

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.export = function () {

            lockUI();
            var url = 'api/IdasSanctionMIS/GetSanctionMISExport';

            SocketService.get(url).then(function (resp) {

                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.excel_cloudpath, resp.data.excel_name);
                    //DownloaddocumentService.Downloaddocument(val1, val2);
                    var phyPath = resp.data.excel_path;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = resp.data.excel_name.split('.');
                    link.download = name[0];
                    var uri = str;
                    link.href = uri;
                    link.click();

                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
        }


        $scope.importExcel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/excelImport.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                    $("#excelImport").val('');
                };


                $scope.upload = function (val, val1, name) {
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
                    frm.append('uploadtype', $scope.cboexcel_type);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;

                }

                $scope.uploadexcelclick = function () {
                    lockUI();
                    var url = "api/IdasMstSanction/postexcelupload";
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                        $("#excelImport").val('');

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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        unlockUI();
                    });
                }

            }
        }

        $scope.sanctionMIS360 = function (sanction_gid) {
            $location.url('app/idasSanctionMIS360?sanction_gid=' + sanction_gid);
        }

        $scope.EditSanction = function (sanction_gid) {
           
            localStorage.setItem('sanction_gid', sanction_gid);
            $state.go('app.idasTrnSanctionDashboard');
           
        }
    }
})();
