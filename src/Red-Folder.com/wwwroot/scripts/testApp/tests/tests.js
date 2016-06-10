describe("KarmaController", function() {
    //var controller;

    beforeEach(function () {
        bard.appModule('karma');
        bard.inject('$controller', '$q', '$rootScope');
    });

    beforeEach(function () {
        //controller = $controller('karmaController');
        $rootScope.$apply();
    });


	//beforeEach(angular.module('karma'));
	
	//var $controller;
	
	//beforeEach(inject(function(_$controller_) {
		//$controller = _$controller_;
	//}));
	
	describe("$scope.sum", function() {
		it("should add two numbers", function() {
			var $scope = {};
			var controller = $controller('karmaController', { $scope: $scope });
			
			$scope.num1 = 5;
			$scope.num2 = 10;
			$scope.AddNumbers();
			
			expect($scope.sum).to.equal(15);
		});

		it("should failed", function() {
			var $scope = {};
			var controller = $controller('karmaController', { $scope: $scope });
			
			$scope.num1 = 3;
			$scope.num2 = 10;
			$scope.AddNumbers();
			
			expect($scope.sum).to.equal(15);
		});

        it("should pass", function() {
			var $scope = {};
			var controller = $controller('karmaController', { $scope: $scope });
			
			$scope.num1 = 5;
			$scope.num2 = 10;
			$scope.AddNumbers();
			
			expect($scope.sum).to.equal(15);
        });

        it("should pass again", function() {
			var $scope = {};
			var controller = $controller('karmaController', { $scope: $scope });
			
			$scope.num1 = 5;
			$scope.num2 = 10;
			$scope.AddNumbers();
			
			expect($scope.sum).to.equal(15);
        });

        describe("nested $scope.sum", function() {
            it("should add two numbers", function() {
                var $scope = {};
                var controller = $controller('karmaController', { $scope: $scope });
                
                $scope.num1 = 5;
                $scope.num2 = 10;
                $scope.AddNumbers();
                
                expect($scope.sum).to.equal(15);
            });
        });
	});
});