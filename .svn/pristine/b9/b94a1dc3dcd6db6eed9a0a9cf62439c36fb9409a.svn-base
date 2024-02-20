(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstColendingProgramAddController', MstColendingProgramAddController);

    MstColendingProgramAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstColendingProgramAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingProgramAddController';

        activate();
        function activate() {
            
            var url = 'api/MstApplication360/ColendingTempClear';
            SocketService.get(url).then(function (resp) {
            });
            vm.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            
            var url = 'api/MstApplication360/GetDropDown';
            lockUI();
            SocketService.get(url).then(function (resp) {              
                $scope.colending_list = resp.data.colending_list;
                unlockUI();
            });
            
        }
        $scope.program_submit = function () {
            /*portfoliolist();*/
            if (($scope.portfolio_list == undefined) || ($scope.portfolio_list == '') || ($scope.portfolio_list == null)) {
                Notify.alert('Enter Portfolio Details', 'warning');
            }
            else {
                var lscategory_gid = '';
                var lscategory_name = '';
                if ($scope.cbocolendingcategory != undefined || $scope.cbocolendingcategory != null) {
                    lscategory_gid = $scope.cbocolendingcategory.colendingcategory_gid;
                    lscategory_name = $scope.cbocolendingcategory.colendingcategory_name;
                }

                var params = {
                    colendar_name: $scope.txtcolendar_name,
                    category_gid: lscategory_gid,
                    category_name: lscategory_name
                }
                lockUI();
                var url = 'api/MstApplication360/CreateColendingProgram';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.MstColendingPrograms');
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
        }

        $scope.Back = function () {
            $state.go('app.MstColendingPrograms');
        };

      
        
        

        //multiple add

        $scope.programdtl_add = function () {
           // portfoliolist();
            if (($scope.txtapproved_date == undefined) || ($scope.txtapproved_date == '') || ($scope.txtapproved_date == null) || ($scope.txtpercentage_name == undefined) || ($scope.txtpercentage_name == '') || ($scope.txtpercentage_name == null) || ($scope.txtremarks == undefined) || ($scope.txtremarks == '') || ($scope.txtremarks == null)) {
               Notify.alert('Enter Portfolio Details', 'warning');
            }
           else {

                var params = {

                    wef_date: $scope.txtapproved_date,
                    percentage_name: $scope.txtpercentage_name,
                    remarks: $scope.txtremarks,
                    colendingprogram_gid: $scope.colendingprogram_gid,

                }
                var url = 'api/MstApplication360/PostColendingAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $scope.txtapproved_date = '';
                        $scope.txtpercentage_name = '';
                        $scope.txtremarks = '';
                        $scope.upload_list = '';

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                 
                    var url = 'api/MstApplication360/GetColendingAddTempList';
                    SocketService.get(url).then(function (resp) {
                        $scope.portfolio_list = resp.data.colendingprogram_list;

                    });



                });
       }
                     
        }

        // add upload Document js
        $scope.ColendingDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
               frm.append('project_flag', "Default");
                for (var i = 0; i <= fi.files.length - 1; i++) {

                    frm.append(fi.files[i].name, fi.files[i])
                   
                    $scope.uploadfrm = frm;
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
               var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "documentformatonly");

                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }
                var url = 'api/MstApplication360/ProgramAddDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    $scope.upload_list = resp.data.colendingupload_list;
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
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
            $scope.upload_list = '';
        }
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.upload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);

            }
        }
       
       // download documentStart
        $scope.colending_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
       
        // download document End
        $scope.ColendingDocumentUpload = function (val, val1, name) {
           
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
               frm.append('project_flag', "Default");
                for (var i = 0; i < fi.files.length; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(fi.files[i].name, "documentformatonly");

                    if (IsValidExtension == false) {
                        Notify.alert("File format is not supported..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                }
                frm.append('document_name', $scope.txtcolending_document);
                $scope.uploadfrm = frm;
                var url = 'api/MstApplication360/ProgramAddDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                    $scope.upload_list = resp.data.colendingupload_list;
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $("#file").val('');
                        $scope.txtcolending_document = "";
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
                        portfolio_gid: $scope.portfolio_gid
                    };
                    var url = 'api/MstApplication360/ColendingUploadDocList';
                    SocketService.getparams(url, param).then(function (resp) {
                        //$scope.lrfilename = resp.data.filename;
                        //$scope.lrfilepath = resp.data.filepath;
                        $scope.upload_list = resp.data.programdoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
        }

        //Document Delete
        $scope.uploaddocumentcancel = function (colendingprogramdocumentupload_gid) {
            lockUI();
            var params = {
                colendingprogramdocumentupload_gid: colendingprogramdocumentupload_gid
            }
            var url = 'api/MstApplication360/DeleteColendingDocuments';
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
                    portfolio_gid: $scope.portfolio_gid
                };
                var url = 'api/MstApplication360/ColendingSummaryDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    //$scope.lrfilename = resp.data.filename;
                    //$scope.lrfilepath = resp.data.filepath;
                    $scope.upload_list = resp.data.programdoc_list;
                });
                unlockUI();
            });
        }

        $scope.remarksdoc_view = function (portfolio_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/RemarksDocumentView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    portfolio_gid: portfolio_gid
                }
                var url = 'api/MstApplication360/GetPortfolioRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtportfolioremarks = resp.data.remarks;

                });

                var params =
               {
                   portfolio_gid: portfolio_gid
               }
                var url = 'api/MstApplication360/GetPortfolioDocDtl';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.PortfolioDocumentView_List = resp.data.PortfolioDocumentView_List;

                });

                $scope.documentviewer = function (val1, val2) {
                    lockUI();
                    var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
                    if (IsValidExtension == false) {
                        Notify.alert("View is not supported for this format..!", {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        unlockUI();
                        return false;
                    }
                    DownloaddocumentService.DocumentViewer(val1, val2);
                }

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.PortfolioDocumentView_List.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.PortfolioDocumentView_List[i].document_path, $scope.PortfolioDocumentView_List[i].document_name);

                    }
                }
                $scope.download_portpoliodoc = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

            }

        }
        //Document Delete End

        //Colending Program Summary Delete Start
        $scope.program_delete = function (portfolio_gid) {
            lockUI();
            var params = {
                portfolio_gid: portfolio_gid
            }
            var url = 'api/MstApplication360/DeleteColendingDocuments';
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
                    portfolio_gid: $scope.portfolio_gid
                };
                var url = 'api/MstApplication360/ColendingSummaryDocList';
                SocketService.getparams(url, param).then(function (resp) {
                    //$scope.lrfilename = resp.data.filename;
                    //$scope.lrfilepath = resp.data.filepath;
                    $scope.upload_list = resp.data.programdoc_list;
                });
                unlockUI();
            });
        }
        function portfolio_list() {
            var url = 'api/MstApplication360/GetColendingAddTempList';
            SocketService.get(url).then(function (resp) {              
                $scope.portfolio_list = resp.data.colendingprogram_list;
            });
        }
        //Portfolio Summary delete Start
        $scope.Portfolio_delete = function (portfolio_gid) {
            var params =
            {
                portfolio_gid: portfolio_gid
            }
            var url = 'api/MstApplication360/DeletePortfolioSummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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

                portfolio_list();
            });

        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                unlockUI();
                return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }
    }
})();