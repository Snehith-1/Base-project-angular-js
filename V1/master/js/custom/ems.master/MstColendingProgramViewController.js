(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstColendingProgramViewController', MstColendingProgramViewController);

    MstColendingProgramViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstColendingProgramViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingProgramViewController';
        $scope.colendingprogram_gid = $location.search().lscolendingprogram_gid;
        var colendingprogram_gid = $scope.colendingprogram_gid;
        activate();
        lockUI();
        function activate() {
           
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

            var params = {
                colendingprogram_gid: $scope.colendingprogram_gid
            };

            var url = 'api/MstApplication360/EditPortfolioSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.portfolio_list = resp.data.PortfolioColending_List;
            });
            var url = 'api/MstApplication360/EditColendingProgram';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtcolendar_name = resp.data.colendar_name;
                $scope.lblcbocolendingcategory = resp.data.colendingcategory_gid;
                $scope.colendingprogram_gid = resp.data.colendingprogram_gid;
            });

            var url = 'api/MstApplication360/GetDropDown';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                if (resp.data.colending_list != null && resp.data.colending_list.length > 0) {
                    $scope.colending_list = resp.data.colending_list;
                    unlockUI();
                }
                else if (resp.data.status == false)
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

                $scope.editcolending_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
               

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

      

        $scope.ColendingDocumentUpload = function (val, val1, name) {

            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
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

        $scope.colending_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.downloadall = function () {
            for (var i = 0; i < $scope.upload_list.length; i++) {
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
            }
        }
        $scope.portfolio_edit = function (portfolio_gid) {
           
            var modalInstance = $modal.open({
                templateUrl: '/PortfolioEdit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];

            
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.portfolio_gid = portfolio_gid;
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
                var params =
                {
                    portfolio_gid: portfolio_gid
                }
                var url = 'api/MstApplication360/GetPortfolioRemarksView';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txteditportfolioremarks = resp.data.remarks;
                    $scope.txteditwef_date = resp.data.wef_date;
                    $scope.txteditpercentage_name = resp.data.percentage_name;
                    $scope.portfolio_gid = resp.data.portfolio_gid;

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

                $scope.downloadallportfolio_doc = function () {
                    for (var i = 0; i < $scope.PortfolioDocumentView_List.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.PortfolioDocumentView_List[i].document_path, $scope.PortfolioDocumentView_List[i].document_name);
                    }
                }

                $scope.download_portpoliodoc = function (val1, val2) {
                    DownloaddocumentService.Downloadocument(val1, val2);
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.editcolending_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }

                //  Edit overall update

              
                $scope.update_portfolio = function () {

                    var url = 'api/MstApplication360/UpdatePortfolio';
                    var params = {
                        wef_date: $scope.txteditwef_date,
                        percentage_name: $scope.txteditpercentage_name,
                        remarks: $scope.txteditportfolioremarks,
                        portfolio_gid: portfolio_gid
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();



                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    $modalInstance.close('closed');
                }
                
               
            }
           
        }

        //  Edit overall update 
        $scope.update = function () {
          
            var category_name = $('#colendingcategory_name :selected').text();

                lockUI();

            var params = {
                category_gid: $scope.cbocolendingcategory,
                category_name: category_name,
                colendar_name: $scope.txtcolendar_name,                  
                colendingprogram_gid: colendingprogram_gid,
                }
            var url = 'api/MstApplication360/updatecolendingprogram';
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
           /* }*/
        }
         $scope.Back = function () {
            $state.go('app.MstColendingPrograms');
        };
        $scope.programdtl_add = function (colendingprogram_gid) {
            //portfoliolist();
            //if (($scope.portfolio_list == undefined) || ($scope.portfolio_list == '') || ($scope.portfolio_list == null)) {
            //    Notify.alert('Enter Portfolio Details', 'warning');
            //}
            //else {

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

                var url = 'api/MstApplication360/GetColendingTempList';
                SocketService.get(url).then(function (resp) {
                    $scope.portfolio_list = resp.data.colendingprogram_list;

                });



            });


        }
    

    }
})();
