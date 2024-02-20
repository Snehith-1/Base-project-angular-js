(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnPreFilManagementController', idasTrnPreFilManagementController);

    idasTrnPreFilManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','DownloaddocumentService'];

    function idasTrnPreFilManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,DownloaddocumentService) {
        $scope.title = 'idasTrnPreFilManagementController';

        activate();

        function activate() {
            $scope.PrefilGenerateDocument = false;
            $scope.GeneratedPrefilDoc = true;

            var url = 'api/IdasMstDocList/GetGeneratedDocList';
            SocketService.get(url).then(function (resp) {
                $scope.generateddoc_list = resp.data.IDASDocument;
            });
        }

        $scope.generate = function (sanction_gid) {

            $location.url('app/idasTrnPreFilGeneration?sanction_gid=' + sanction_gid + '&lspage=generatedprefil');
        }

        $scope.generatedocument = function () {
            $scope.PrefilGenerateDocument = true;
            $scope.GeneratedPrefilDoc = false;

            $scope.back = function () {
                $scope.PrefilGenerateDocument = false;
                $scope.GeneratedPrefilDoc = true;

                $scope.customer = '';
                $scope.cbocustomer2sanction_gid = '';
                $scope.document2sanction_list = '';
            }

            $scope.complete = function (string) {

                if (string.length >= 3) {
                    $scope.message = "";
                    var url = 'api/customer/ExploreCustomer';
                    var params = {
                        customername: string
                    }
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $scope.message = "";
                            $scope.customer_list = resp.data.Customers;
                        }
                        else {
                            $scope.message = "No Records";
                        }
                    });
                }
                else {
                    $scope.customer_list = null;
                    $scope.message = "Enter atleast three character";
                }
            }
            $scope.fillTextbox = function (customer_gid, customer_name) {
                $scope.customer = customer_name;
                $scope.customer_gid = customer_gid;
                $scope.customer_list = null;

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/IdasTrnLsaManagement/customer2sanction';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customer2sanction_list = resp.data.customer2sanction_list;

                });
            }

            $scope.onselectedchangesanction = function (sanction) {
                lockUI();
                var params = {
                    sanction_gid: $scope.cbocustomer2sanction_gid.customer2sanction_gid
                }
                var url = 'api/IdasMstDocList/GetDocument2SanctionList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.document2sanction_list = resp.data.IDASDocument;
                });
            }

            $scope.generate = function (val, val1, val2, val3) {
                $location.url('app/idasTrnDocumentGeneration?documentlist_gid=' + val + '&sanction_gid=' + val1 + '&document_code=' + val2 + '&doctemplate_flag=' + val3 + '&lspage=createprefil');
            }

            $scope.WordGenerate = function (documentlist_gid, sanction_gid) {
                var params = {
                    documentlist_gid: documentlist_gid,
                    sanction_gid: sanction_gid
                };
                var url = 'api/IdasMstDocList/GetDocWordGenerate';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        var phyPath = resp.data.lspath;
                var filename1 = resp.data.lsname;
                var phyPath = phyPath.replace("\\", "/");
                var phyPath = phyPath.replace("//", "/");
                var relPath = phyPath.split("EMS/");
                var relpath1 = relPath[1].replace("\\", "/");
                var url1 = filename1;
                var filename = url1.substring(url1.lastIndexOf('/')+1);                                                                      
               var url = 'api/azurestorage/FileUploadDocument';
                var params = {
                    file_path : relpath1
                }
                SocketService.post(url,params).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(relpath1, filename1);
                   
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }
                });
                        // var phyPath = resp.data.lspath;
                        // var relPath = phyPath.split("EMS");
                        // var relpath1 = relPath[1].replace("\\", "/");
                        // var hosts = window.location.host;
                        // var prefix = location.protocol + "//";
                        // var str = prefix.concat(hosts, relpath1);
                        // var link = document.createElement("a");
                        // var name = resp.data.lsname.split('.');
                        // link.download = name[0];
                        // var uri = str;
                        // link.href = uri;
                        // link.click();
                        unlockUI();
                    }
                    else {
                        unlockUI();
                        Notify.alert('Error Occurred While Downloading !', 'warning')
                        activate();

                    }
                });
            }
        }
    }
})();