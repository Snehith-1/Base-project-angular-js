(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sanctionManagementcontroller', sanctionManagementcontroller);

    sanctionManagementcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function sanctionManagementcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sanctionManagementcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/sanction/getsanctiondtlList";
            SocketService.get(url).then(function (resp) {
                $scope.sanctionlist = resp.data.sanctiondetails;
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
            if ($scope.sanctionlist != null) {
                if ($scope.totalDisplayed < $scope.sanctionlist.length) {
                    $scope.totalDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.sanctionlist.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.selectedFile = null;
        $scope.msg = "";

        $scope.importExcel = function () {
            $scope.test = true;
            $scope.btnimport = true;
        }

        $scope.uploadcancel = function () {
            lockUI();
            $scope.test = false;
            $scope.btnimport = false;
            unlockUI();
        }

        $scope.documetnchecklistsanction = function (customer2sanction_gid)
        {
            localStorage.setItem('customer2sanction_gid', customer2sanction_gid)
            $state.go('app.documentCheckList')
        }
        $scope.handleFile = function (val, val1, name) {

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

            // var item = {
            //     name: val[0].name,
            //     file: val[0]
            // };

            // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "documentformatonly");

            // if (IsValidExtension == false) {
            //     alert("File format is not supported..!", {
            //         status: 'danger',
            //         pos: 'top-center',
            //         timeout: 3000
            //     });
            //     return false;
            // }

            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('project_flag', "documentformatonly");
            $scope.uploadfrm = frm;

            var url = "api/sanction/postexcelupload";
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $scope.filename_list = resp.data.filename_list;
                console.log(resp);
                $("#addupload").val('');

                if (resp.data.status == true) {

                    Notify.alert('Document Uploaded Successfully..!!', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
                else {
                    Notify.alert('File Format Not Supported!', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });
        }

        $scope.loadFile = function (files) {

            $scope.$apply(function () {

                $scope.selectedFile = files[0];

            })

        }
        $scope.save = function (params) {
           
            lockUI();

            var url = "api/sanction/postexcelupload";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $("#excelImport").val('');
                    activate();
                    unlockUI();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }

            });
        }

        $scope.addSanction = function () {
            $state.go('app.sanctionAdd');
        }

        $scope.editsanction = function (val) {
            localStorage.setItem('customer2sanction_gid', val);
            $state.go('app.sanctionEdit');
        }

        $scope.viewsanction = function (val) {
            localStorage.setItem('customer2sanction_gid', val);
            $state.go('app.sanctionView');
        }

        $scope.deletesanction = function (val) {
            var params = {
                customer2sanction_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/sanction/getsanctiondelete";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });

                }

            });
        }
    }
})();
