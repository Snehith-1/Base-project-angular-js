(function () {
    'use strict';

    angular
        .module('angle')
        .controller('documentationcontroller', documentationcontroller);

    documentationcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function documentationcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'documentationcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/documentation/getdocumentationdtlList";
            SocketService.get(url).then(function (resp) {
                $scope.documentation_list = resp.data.documentationdtl;
                if ($scope.documentation_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.documentation_list.length;
                    if ($scope.documentation_list.length < 100) {
                        $scope.totalDisplayed = $scope.documentation_list.length;
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
            if ($scope.documentation_list != null)
            {
                if ($scope.totalDisplayed < $scope.documentation_list.length) {
                    $scope.totalDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.documentation_list.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.addDocument = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/addDocumentModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentformSubmit = function () {

                    var params = {
                        //documentation_refno: $scope.txtdocumentrefno,
                        documentation_name: $scope.txtdocumentationname,
                        documentation_type: $scope.txtdocumentationtype
                    }
                    lockUI();
                    var url = "api/documentation/postdocumentationdtls";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();

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
            }
        }

        $scope.editdocument = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/EditDocumentModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    customer2document_gid: val
                }
                var url = 'api/documentation/getdocumentationdtl';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditdocumentrefno = resp.data.documentation_refno;
                    $scope.txteditdocumentationname = resp.data.documentation_name;
                    $scope.txteditdocumentationtype = resp.data.documentation_type;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.documentformUpdate = function () {
                    lockUI();
                    var params = {
                        documentation_name: $scope.txteditdocumentationname,
                        documentation_type: $scope.txteditdocumentationtype,
                        customer2document_gid: val
                    }
                    var url = "api/documentation/postdocumentationupdate";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            unlockUI();
                            activate();
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
            }
        }

        $scope.deleteDocumentation = function (val) {
            var params = {
                customer2document_gid: val
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
                    lockUI();
                    var url = "api/documentation/documentationdelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
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

            });
        }
    }
})();
