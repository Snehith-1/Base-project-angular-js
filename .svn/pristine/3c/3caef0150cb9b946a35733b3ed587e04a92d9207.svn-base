(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnRetrievalReqSummary', idasTrnRetrievalReqSummary);

    idasTrnRetrievalReqSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams'];

    function idasTrnRetrievalReqSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams) {
        var vm = this;
        vm.title = 'idasTrnRetrievalReqSummary';

        activate();

        function activate() {
            $scope.user = {};
            $scope.IsCreate=false;
            $scope.totalDisplayedReq = 100;
            $scope.totalDisplayedTemp = 100;
            $scope.totalDisplayedPermanent = 100;
            $scope.totalDisplayedDespath=100;

            vm.calenderDespatch = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.openDespatch = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            var url='api/IdasTrnRecordRetrieval/GetRetrievalReqSummary';

            SocketService.get(url).then(function (resp) {

            $scope.reqsummary_list=resp.data.MdlIdasRecordReqSummary;
            $scope.ReqCount=$scope.reqsummary_list.length;

            });


            var url='api/IdasTrnRecordRetrieval/GetRetrievalTempSummary';

            SocketService.get(url).then(function (resp) {

            $scope.temp_list=resp.data.MdlIdasRecordReceivedSummary;
            
            if($scope.temp_list==null){
                $scope.TempCount=0;
            }
            else{
                $scope.TempCount=$scope.temp_list.length;
            }
    
            });

            var url='api/IdasTrnRecordRetrieval/GetRetrievalPermanentSummary';

            SocketService.get(url).then(function (resp) {

            $scope.permanent_list=resp.data.MdlIdasRecordReceivedSummary;
            if( $scope.TempCount==null){
                $scope.PermanentCount=0;
            }
            else{
                $scope.PermanentCount=$scope.permanent_list.length;
            }
            

            });

            var url='api/IdasTrnRecordRetrieval/GetReDespatchedSummary';

            SocketService.get(url).then(function (resp) {

            $scope.redespatch_list=resp.data.MdlReDespatchSummary;
            console.log($scope.redespatch_list);
            if( $scope.redespatch_list==null){
                $scope.DespatchCount=0;
            }
            else{
                $scope.DespatchCount=$scope.redespatch_list.length;
            }
            

            });
        }
        $scope.checkallRedespatch = function (selected) {
            angular.forEach($scope.temp_list, function (val) {  
              
                if(val.ensure_flag=='Y'){
                    val.checked = selected;
                }
               
            });
        }
        $scope.createDespatch = function () {
            var gid_list = [];
            var gid;
            angular.forEach($scope.temp_list, function (val) {

                if (val.checked == true) {
                    gid = val.retrievalrequestdtls_gid;

                    gid_list.push(gid);

                }
            });

            if(gid!=undefined)
            {
                var url = 'api/employee/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_data = resp.data.employee_list;
                    
                });
               
                $scope.IsCreate = true;
            }
            else {
                Notify.alert('Select Atleast One Record!')
            }
          
          
        }
         $scope.close=function()
        {
            $scope.IsCreate = false;
        }
        $scope.DespatchSubmit = function () {
            var gid_list = [];
            var gid;
            angular.forEach($scope.temp_list, function (val) {

                if (val.checked == true) {
                    gid = val.retrievalrequestdtls_gid;

                    gid_list.push(gid);

                }
            });
            if(gid==undefined)
            {
                Notify.alert('Select Atleast One Record!')
                return;
            }
          
            var params = {
                retrievalrequestdtls_gid: gid_list,
                redespatched_date: $scope.user.txtDespatchDate,
                contact_person: $scope.user.contactPerson,
                redespatchedby_name: $scope.user.DespatchedBy,
                remarks: $scope.user.despatchRemarks

            }
          

            var url = 'api/IdasTrnRecordRetrieval/ReDespatch';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                  
                   
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();

                    Notify.alert(resp.data.message)
                }
                activate();
            });

        }
      $scope.ensure=function(val){
          var params={
            retrievalrequestdtls_gid:val
          }
        SweetAlert.swal({
            title: 'Are you Sure?',
            text: 'The Retrieval Purpose is Completed',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Yes, Ensured it!',
            closeOnConfirm: false
        }, function (isConfirm) {
            if (isConfirm) {
                lockUI();
                var url = "api/IdasTrnRecordRetrieval/Ensure";
                SocketService.getparams(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        activate();
                        SweetAlert.swal('Ensured Successfully!');
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

        $scope.loadMoreReq = function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            
            $scope.totalDisplayedReq += Number;
            unlockUI();
        };
        $scope.loadMoredespatch = function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            
            $scope.totalDisplayedDespath += Number;
            unlockUI();
        };
        $scope.loadMoreTemp = function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            
            $scope.totalDisplayedTemp += Number;
            unlockUI();
        };
        $scope.loadMorePermanent = function (pagecount) {
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            
            $scope.totalDisplayedPermanent += Number;
            unlockUI();
        };
        $scope.createRetrievalReq = function () {
            $state.go('app.idasTrnRetrievalReqCreate');
        }

        $scope.back=function () {
            $state.go('app.idasTrnRetrievalReqSummary');
        }

        $scope.Docdtlsview=function(val){
            localStorage.setItem('retrievalrequest_gid',val);
            $state.go('app.idasTrnRetrievalReqView');
        }
        $scope.ReDespatchdtlsview=function(val){
            
            localStorage.setItem('redespatch_gid',val);
            $state.go('app.idasTrnReDespatch360View');
        }

       
    }

})();