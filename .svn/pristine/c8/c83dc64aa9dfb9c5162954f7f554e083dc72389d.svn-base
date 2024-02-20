(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstColendingProgramEditController', MstColendingProgramEditController);

    MstColendingProgramEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstColendingProgramEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingProgramEditController';
        $scope.colendingprogram_gid = $location.search().lscolendingprogram_gid;
        var colendingprogram_gid = $scope.colendingprogram_gid;
        var portfolio_gid = $scope.portfolio_gid;
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
            var url = 'api/MstApplication360/ColendingTempClear';
            SocketService.get(url).then(function (resp) {
            });
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
                $scope.cbocolendingcategory = resp.data.colendingcategory_gid;
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

        ////function posrfoliosummary_list() {
        // function portfolio_list() {
        //         lockUI();
        //                    var params = {
        //                        colendingprogram_gid: colendingprogram_gid,
        //                    }
        //                   var url = 'api/MstApplication360/GetColendingTempList';
        //                    SocketService.getparams(url, params).then(function (resp) {
        //                        $scope.portfolio_list = resp.data.colendingprogram_list;
        //                    unlockUI();
        //                });
        //}

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

       
       $scope.Back = function () {
            $state.go('app.MstColendingPrograms');
        };
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
                frm.append('project_flag', "Default");
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
                       
                        $scope.upload_list = resp.data.programdoc_list;
                    });
                    unlockUI();
                });
            }
            else {
                alert('Please select a file.')
            }
            $scope.upload_list = '';
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

     // Edit  Portfolio Delete Start

        $scope.portfolio_editdelete = function (portfolio_gid) {
            var params =
            {
                portfolio_gid: portfolio_gid,
                colendingprogram_gid: colendingprogram_gid,
            }
            lockUI();
            var url = 'api/MstApplication360/DeletePortfolioedit';
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
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params = {

                    colendingprogram_gid: colendingprogram_gid
                }
                lockUI();
                var url = 'api/MstApplication360/GetPortfolioEditTempList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.portfolio_list = resp.data.colendingprogram_list;
                    unlockUI();
                });
            });

        }
         // Edit  Portfolio Delete End

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
                              
               
            }
           
        }

        //  Edit overall update 
        $scope.update = function () {
            /*portfolio_list();*/
            if (($scope.portfolio_list == undefined) || ($scope.portfolio_list == '') || ($scope.portfolio_list == null)) {
                Notify.alert('Enter Portfolio Details', 'warning');
            }
            else {
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
            }
        }

        $scope.programdtl_add = function () {
            if (($scope.txteditwef_date == undefined) || ($scope.txteditwef_date == '') || ($scope.txteditwef_date == null) || ($scope.txteditpercentage_name == undefined) || ($scope.txteditpercentage_name == '') || ($scope.txteditpercentage_name == null) || ($scope.txteditportfolioremarks == undefined) || ($scope.txteditportfolioremarks == '') || ($scope.txteditportfolioremarks == null)) {
                Notify.alert('Enter Portfolio Details', 'warning');
            }
            else {

                var params = {

                    wef_date: $scope.txteditwef_date,
                    percentage_name: $scope.txteditpercentage_name,
                    remarks: $scope.txteditportfolioremarks, 
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
                        portfolio_list();
                      /*  posrfoliosummary_list();*/
                        $scope.txteditwef_date = '';
                        $scope.txteditpercentage_name = '';
                        $scope.txteditportfolioremarks = '';
                        $scope.upload_list = '';

                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    } 
                     //posrfoliosummary_list();
                     //   $scope.txteditwef_date = '';
                     //   $scope.txteditpercentage_name = '';
                     //   $scope.txteditportfolioremarks = '';
                     //   $scope.upload_list = '';
             

            });
        }

        }
        $scope.editportfolio = function (portfolio_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editportfolio.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.portfolio_gid = portfolio_gid;

                $scope.open1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $scope.opened1 = true;
                };
                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };

                var params = {
                    portfolio_gid: portfolio_gid
                }
                var url = 'api/MstApplication360/GetColendingPortfolioList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtremarks = resp.data.remarks;
                    $scope.txteditwef_date = resp.data.wef_date;
                    $scope.txteditpercentage_name = resp.data.percentage_name;
                    $scope.portfolio_gid = resp.data.portfolio_gid;
                    /* $scope.portfolio_list = resp.data.colendingprogram_list;*/

                });
                var url = 'api/MstApplication360/ColendingUploadDocList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.upload_list = resp.data.programdoc_list;
                });
                $scope.colendingedit_downloads = function (val1, val2) {
                    DownloaddocumentService.Downloaddocument(val1, val2);
                }
                $scope.update_portfolio = function () {
                    lockUI();
                    var url = 'api/MstApplication360/UpdatePortfolio';
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
                        wef_date: $scope.txteditwef_date,
                        percentage_name: $scope.txteditpercentage_name,
                        remarks: $scope.txtremarks,
                        portfolio_gid: portfolio_gid
                    }

                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                          
                            $scope.txteditwef_date == '';
                            $scope.txteditpercentage_name == '';
                            $scope.txtremarks == '';
                            

                        }
                        else {
                            
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        } 
                        portfolio_list();
                       
                    });
                    $modalInstance.close('closed');
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

                $scope.downloadall = function () {
                    for (var i = 0; i < $scope.upload_list.length; i++) {
                        DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

               

                $scope.ColendingDocumentUpload = function (val) {

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
                    frm.append('document_name', $scope.txteditcolending_document);
                    $scope.uploadfrm = frm;
                    if ($scope.uploadfrm != undefined) {
                        lockUI();
                        var url = 'api/MstApplication360/ProgramAddDocumentUpload';
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            $scope.upload_list = resp.data.colendingupload_list;
                            unlockUI();
                            $("#chequefilefile").val('');
                            $scope.uploadfrm = undefined;

                            if (resp.data.status == true) {
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $("#file").val('');
                                $scope.txteditcolending_document = "";
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                            var param = {
                                portfolio_gid: $scope.portfolio_gid
                            };
                            var url = 'api/MstApplication360/ColendingUploadDocList';
                            SocketService.getparams(url, param).then(function (resp) {
                                $scope.upload_list = resp.data.programdoc_list;
                            });
                            unlockUI();
                        });
                    }
                    else {
                        alert('Please select a file.')
                    }
                    $scope.upload_list = '';
                }

                
            }
        }

        function portfolio_list() {
            lockUI();
            var params = {
                colendingprogram_gid: colendingprogram_gid,
            }
            var url = 'api/MstApplication360/GetColendingTempList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.portfolio_list = resp.data.colendingprogram_list;
                console.log(resp)
                unlockUI();
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
