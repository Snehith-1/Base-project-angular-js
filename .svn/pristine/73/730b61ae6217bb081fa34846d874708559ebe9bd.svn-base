(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnRetrievalReqCreate', idasTrnRetrievalReqCreate);

    idasTrnRetrievalReqCreate.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout', 'DownloaddocumentService','cmnfunctionService'];

    function idasTrnRetrievalReqCreate($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout, DownloaddocumentService,cmnfunctionService) {
        var vm = this;

        activate();

        function activate() {

            $scope.batch_mode=false;
            $scope.box_mode=false;
            $scope.customer_mode=false;
            $scope.reconciliationmode=false;
            $scope.DivFile = false;
            
            vm.calenderreq = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openreq = true;
            };
            vm.calenderapp = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openapp = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

             var url = 'api/IdasTrnRecordRetrieval/DespatchCustomer';
             SocketService.get(url).then(function (resp) {
                 $scope.customer_list = resp.data.MdlCustomer;
             });

             var url = 'api/IdasTrnRecordRetrieval/DespatchBox';
             SocketService.get(url).then(function (resp) {
                 $scope.box_list = resp.data.MdlGetBox;
             });

            // var url = 'api/IdasTrnRecordRetrieval/DeleteRequestDtls';
          
            // SocketService.get(url).then(function (resp) {
               
            // });

            var params={
                uploaddocument_gid:undefined
            };

            var url = 'api/IdasTrnRecordRetrieval/TmpDocumentDelete';

            SocketService.getparams(url,params).then(function (resp) {

               // $scope.uploaddocument = resp.data.MdlIdasUploadDocument;

            });

            // var url = 'api/IdasTrnRecordRetrieval/BatchList';
            // SocketService.get(url).then(function (resp) {
            //     $scope.batch_list = resp.data.MdlGetBatch;
            //  //   console.log($scope.batch_list);
            // });

            // var url = 'api/IdasTrnRecordRetrieval/BoxList';
            // SocketService.get(url).then(function (resp) {
            //     $scope.box_list = resp.data.MdlBoxDtls;

            // });

            // var url = 'api/IdasTrnRecordRetrieval/DespatchList';
            //  SocketService.get(url).then(function (resp) {
            //  $scope.despatch_list = resp.data.MdlGetBox;

            //  var url = 'api/IdasTrnRecordRetrieval/DespatchBox';
            //  SocketService.get(url).then(function (resp) {
            //  $scope.despatch_list = resp.data.MdlDespatchDtls;

            // });

          
        }

        // $scope.deleteFiles=function(tmp_gid){
        //    var params={
        //         tmpretrievalrequestdtls_gid:tmp_gid
        //     }
        //     var url = 'api/IdasTrnRecordRetrieval/DeleteRequestDtls';
        //        SocketService.getparams(url,params).then(function (resp) {
        //       if(resp.data.status==true){
        //         url='api/IdasTrnRecordRetrieval/tmpRequestRetrievalDtls'
        //         SocketService.get(url).then(function (resp) {              
        //             $scope.tmpdetails= resp.data.MdltmpRequired;
        //     });
        //         Notify.alert(resp.data.message, 'success')

        //       }
        //       else
        //       {
        //         Notify.alert(resp.data.message, 'warning')

        //       }

        //     });

        // }
        $scope.checkallBatch = function (selected) {
            angular.forEach($scope.batch_list, function (val) {  
                
                    val.checked = selected;
            });
        }
        $scope.onchangemodecustomer=function(){
            $scope.customer_mode=true;
            $scope.box_mode=false;
            $scope.file_mode=false;
            $scope.reconciliationmode=false;
            $scope.customer="";
        }
        $scope.onchangemodebox=function(){
            $scope.customer_mode=false;
            $scope.box_mode=true;
            $scope.file_mode=false;
            $scope.reconciliationmode=false;
            $scope.box="";
        }
        $scope.onchangemodefile=function(){
            $scope.customer_mode=false;
            $scope.box_mode=false;
            $scope.file_mode=true;
            $scope.reconciliationmode=true;

            var params={
                reference_type:$scope.retrieval_mode
            }

            var url = 'api/IdasTrnRecordRetrieval/BatchList';
            SocketService.post(url, params).then(function (resp) {
             if(resp.data.status==true){
                $scope.file_mode=true;
                $scope.batch_list = resp.data.MdlGetBatch;
                $scope.total=$scope.batch_list.length;
             }
              else{
                  $scope.file_mode=false;
                  Notify.alert('No Data to Display','warning');
              }
               
              
            });

            var params={
                reference_type:$scope.retrieval_mode
            }

            var url = 'api/IdasTrnRecordRetrieval/ReconciliationCount';
            SocketService.post(url, params).then(function (resp) {
                $scope.file_count = resp.data.file_count;
                $scope.despatched_count=resp.data.despatched_count;
                $scope.permanet_count=resp.data.permanet_count;
                $scope.temporary_count=resp.data.temporary_count;
            
            });
        }
        $scope.FileShow = function () {
            if ($scope.DivFile == true) {
                $scope.DivFile = false;
            }
            else {
                $scope.DivFile = true;
            }
        }
        $scope.onSelectChangeCustomer=function(val1){
            console.log(val1);
            $scope.reconciliationmode=true;
              var params={
                reference_type:$scope.retrieval_mode,
                MdlCustomer:val1
            }
           if(val1!=undefined){
            var url = 'api/IdasTrnRecordRetrieval/BatchList';
            SocketService.post(url, params).then(function (resp) {
             if(resp.data.status==true){
                $scope.file_mode=true;
               $scope.batch_list = resp.data.MdlGetBatch;
               $scope.total=$scope.batch_list.length;
             }
          else{
                  $scope.file_mode=false;
                  Notify.alert('No Data to Display','warning');
              }
              
              });

           var params={
               reference_type:$scope.retrieval_mode,
               MdlCustomer:val1
           }

            var url = 'api/IdasTrnRecordRetrieval/ReconciliationCount';
            SocketService.post(url, params).then(function (resp) {
              $scope.file_count = resp.data.file_count;
              $scope.despatched_count=resp.data.despatched_count;
              $scope.permanet_count=resp.data.permanet_count;
              $scope.temporary_count=resp.data.temporary_count;
              $scope.insamunnati_count=$scope.permanet_count + $scope.temporary_count;
             
           
            });
      
           }
           else{
            $scope.customer_mode=true;
            $scope.box_mode=false;
            $scope.file_mode=false;
            $scope.reconciliationmode=false;
            $scope.customer="";
           }

           }

        $scope.onSelectChangeBox=function(val1){
            $scope.reconciliationmode=true;
           
            var params={
                reference_type:$scope.retrieval_mode,
                MdlGetBox:val1
            }
            if(val1!=undefined){
                var url = 'api/IdasTrnRecordRetrieval/BatchList';
                SocketService.post(url, params).then(function (resp) {
                 if(resp.data.status==true){
                    $scope.file_mode=true;
                    $scope.batch_list = resp.data.MdlGetBatch;
                    $scope.total=$scope.batch_list.length;
                 }
                  else{
                      $scope.file_mode=false;
                      Notify.alert('No Data to Display','warning');
                  }
                   
                  
                });
    
                var params={
                    reference_type:$scope.retrieval_mode,
                    MdlGetBox:val1
                }
    
                var url = 'api/IdasTrnRecordRetrieval/ReconciliationCount';
                SocketService.post(url, params).then(function (resp) {
                    $scope.file_count = resp.data.file_count;
                    $scope.despatched_count=resp.data.despatched_count;
                    $scope.permanet_count=resp.data.permanet_count;
                    $scope.temporary_count=resp.data.temporary_count;
                
                });
            }
            else{
                $scope.customer_mode=false;
                $scope.box_mode=true;
                $scope.file_mode=false;
                $scope.reconciliationmode=false;
                $scope.box="";
            }

           
        }

      
    
        $scope.deletedocument=function(val){
            var params = {
                uploaddocument_gid: val
            }
            var url = 'api/IdasTrnRecordRetrieval/TmpDocumentDelete';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document deleted Successfully!', 'success')

                    var url = 'api/IdasTrnRecordRetrieval/GetTmpUploadDocument';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.MdlIdasUploadDocument;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred')

                }

            });
        }
        $scope.downloadsdocument = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.commonupload = function (val, val1, name) {
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
            frm.append('document_title', $scope.txtdocument_title);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            var url = 'api/IdasTrnRecordRetrieval/UploadDocument';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                $("#commonupload").val('');
                $scope.txtdocument_title = '';
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')

                    var url = 'api/IdasTrnRecordRetrieval/GetTmpUploadDocument';

                    SocketService.get(url).then(function (resp) {

                        $scope.uploaddocument = resp.data.MdlIdasUploadDocument;

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }

            });

        }
       

        $scope.requestSubmit=function(){
          //  console.log('submiited');
          var batch_value = [];
         
          angular.forEach($scope.batch_list, function (val) {

              if (val.checked == true) {
                 

                  batch_value.push([val.customer_gid,val.customername,val.despatch_gid,val.despatchref_no,val.cartonbox_gid,val.boxstampref_no,val.batch_gid,val.filestampref_no,val.fileref_no]);

              }
          });
         if(batch_value==undefined){
            Notify.alert("Kindly Select Atleast One Record",'warning');
            return;
         }
             var params={
                 requested_date:$scope.requestDate,
                 requestedby_name:$scope.requested_by,
                 approvalby_name:$scope.approved_by,
                 approved_date:$scope.approvalDate,
                 retrieval_type:$scope.retrieval_type,
                 req_remarks:$scope.remarks,
                 requested_for:$scope.reason,
                 reteival_record:batch_value,
                 documentretrieved_mode:$scope.retrieval_mode

             };

             var url='api/IdasTrnRecordRetrieval/CreateRetrievalReq';
             lockUI();
             SocketService.post(url,params).then(function (resp) {
                 unlockUI();
               if(resp.data.status==true){
                 $state.go('app.idasTrnRetrievalReqSummary');
                 Notify.alert(resp.data.message, 'success');
               }
               else{
                 Notify.alert(resp.data.message, 'warning');
               }
             });
           
        }

        $scope.back=function(){
            $state.go('app.idasTrnRetrievalReqSummary');
        }
    }

})();