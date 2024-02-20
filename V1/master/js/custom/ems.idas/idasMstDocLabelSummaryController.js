(function () {
    'use strict';
    angular
           .module('angle')
           .controller('idasMstDocLabelSummaryController', idasMstDocLabelSummaryController);

           idasMstDocLabelSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function idasMstDocLabelSummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasMstDocLabelSummaryController';

        activate();
        lockUI();
        function activate() {
            $scope.totalDisplayed = 50;
            var url = 'api/IdasTrnDocumentUpload/DocumentLabelSummary';
          
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.label_list = resp.data.DocumentLabelList;
                // new code start   
                if ($scope.label_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.label_list.length;
                    if ($scope.label_list.length < 100) {
                        $scope.totalDisplayed = $scope.label_list.length;
                    }
                }
                // new code end
                // $scope.total=$scope.customer_data.length;
            });
        }
        

        $scope.createlabel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/createDocumentLabelModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.documenttname = function (string) {
                    if (string.length > 128) {
                        $scope.message = "Allowed only  128 Characters";
                    }
                    else {
                        $scope.message = ""
                    }
                }
                var url = 'api/IdasTrnDocumentUpload/GetDepartmentList';
                SocketService.get(url).then(function (resp) {
                    $scope.department_list = resp.data.department_list;
                });
                
                $scope.documentlabelSubmit = function () {
                    var lsdepartment_name = '';
                    var lsdepartment_gid = '';
                    if ($scope.cbodepartment != undefined || $scope.cbodepartment != null) {
                        lsdepartment_name = $scope.cbodepartment.department_name;
                        lsdepartment_gid = $scope.cbodepartment.department_gid;
                    }

                    var params = {
                        documentlabel_name: $scope.document_label,
                        documentlabel_desc: $scope.label_description,
                        department_name: lsdepartment_name,
                        department_gid: lsdepartment_gid,
                    }
                    lockUI();
                    var url = "api/IdasTrnDocumentUpload/CreateDocumentLabel";
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
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.editlabel = function (val) {
            var modalInstance = $modal.open({
                templateUrl: '/editDocumentLabelModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.documentretname = function (string1) {
                    if (string1.length > 128) {
                        $scope.message1 = "Allowed only  128 Characters";
                    }
                    else {
                        $scope.message1 = ""
                    }
                }
                var url = 'api/IdasTrnDocumentUpload/GetDepartmentList';
                SocketService.get(url).then(function (resp) {
                    $scope.department_list = resp.data.department_list;
                });
                var params = {
                    documentlabel_gid: val
                }
                var url = "api/IdasTrnDocumentUpload/GetDocumentLabel";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.editdocument_label = resp.data.documentlabel_name;
                    $scope.editlabel_description = resp.data.documentlabel_desc;
                
                    $scope.cbodepartment = resp.data.department_gid;
                });
                $scope.close = function () {
                    $modalInstance.close('closed');
                };
               
                $scope.update_documentlabel = function () {
                    lockUI();
                    var params = {
                        documentlabel_name: $scope.editdocument_label,
                        documentlabel_desc:$scope.editlabel_description,
                        documentlabel_gid: val,
                        department_name: $('#department_name :selected').text(),
                        department_gid: $scope.cbodepartment,
                    }
                    var url = "api/IdasTrnDocumentUpload/UpdateDocumentLabel";
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
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }

                    });
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        
         $scope.deletelabel = function (val) {
            var params = {
                documentlabel_gid: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Document List ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/IdasTrnDocumentUpload/DocumentLabelDelete";
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

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();

            var Number = parseInt(pagecount);
            // new code start
            if ($scope.label_list != null) {

                if (pagecount < $scope.label_list.length) {
                    $scope.totalDisplayed += Number;
                    if ($scope.label_list.length < $scope.totalDisplayed) {
                        $scope.totalDisplayed = $scope.label_list.length;
                        Notify.alert(" Total Summary " + $scope.label_list.length + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.label_list.length + " Records Only", "warning");
                    return;
                }
                
            }
          
            unlockUI();
        };
       
      
       
    }

   
})();
