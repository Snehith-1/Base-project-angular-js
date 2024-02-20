(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnAddNocAndNdcController', idasTrnAddNocAndNdcController);

    idasTrnAddNocAndNdcController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function idasTrnAddNocAndNdcController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnAddNocAndNdcController';

        activate();

        function activate() {

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open1 = true;
            };
            vm.calender = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open2 = true;
            };
            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.open3 = true;
            };
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var url = 'api/IdasNocAndNdc/TempClear';
            SocketService.get(url).then(function (resp) {

            });
            var url = 'api/IdasNocAndNdc/GetDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.vertical_list = resp.data.vertical_list;
               
            });
        }

        $scope.Back = function () {
            $state.go('app.idasTrnNocAndNdc');

        }

        $scope.submit = function () {
            var lsvertical_gid = '';
            var lsvertical_name = '';

            if ($scope.cbovertical != undefined || $scope.cbovertical != null) {
                lsvertical_gid = $scope.cbovertical.vertical_gid;
                lsvertical_name = $scope.cbovertical.vertical_name;
            }

            var params = {
                maker_gid: $scope.cbomaker.employee_gid,
                maker_name: $scope.cbomaker.employee_name,
                checker_gid: $scope.cbochecker.employee_gid,
                checker_name: $scope.cbochecker.employee_name,
                vertical_gid: lsvertical_gid,
                vertical_name: lsvertical_name,
                customer_name: $scope.txtcustomer_name,
                sanction_ref_no: $scope.txtsanction_ref_no,
                sanction_date: $scope.txtsanctiondate,
                loan_account_no: $scope.txtloan_account_no,
                noc_issuance_date: $scope.txtnocissuancedate,
                nocandndc_date: $scope.txtnocrequestdate

            }
            //console.log(params);
            var url = 'api/IdasNocAndNdc/CreateIdasNocAndNdc';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()

                    $state.go('app.idasTrnNocAndNdc');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
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


        $scope.NocNDcDocumentUpload = function () {
            
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    frm.append('project_flag', "Default");
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fname, "");

                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        return false;
                    }
                }
                frm.append('file_name', $scope.txtdoc_name);
                var url = 'api/IdasNocAndNdc/NocDocumentUpload';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');

                    unlockUI();
                    if (resp.data.status == true) {
                        
                        var url = 'api/IdasNocAndNdc/GetNocDocumentList';
                        SocketService.get(url).then(function (resp) {
                        $scope.UploadDocumentList = resp.data.UploadNocDocumentList;
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
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
                unlockUI();
            }

            $scope.txtdoc_name = '';
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

        $scope.uploaddocumentcancel = function (nocandndcdocument_gid) {

            var params = {
                nocandndcdocument_gid: nocandndcdocument_gid
            }
            var url = 'api/IdasNocAndNdc/GetNocDocumentAddDelete';
            SocketService.getparams(url, params).then(function (resp) {
             
                if (resp.data.status == true) {

                    var url = 'api/IdasNocAndNdc/GetNocDocumentList';
                    SocketService.get(url).then(function (resp) {

                        $scope.UploadDocumentList = resp.data.UploadNocDocumentList;
                    });

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

                unlockUI();
            });
        }

       

    }
})();
