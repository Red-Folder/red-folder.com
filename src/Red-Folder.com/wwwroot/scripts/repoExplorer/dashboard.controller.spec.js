describe("Dashboard controller", function () {
    var $scope = null;
    var ctrl = null;

    beforeEach(function () {
        bard.appModule('app');
        bard.inject('$controller', '$q', '$rootScope');
    });

    beforeEach(function () {
        $scope = $rootScope.$new();
        ctrl = $controller('DashboardController', { $scope: $scope, repoService: null });
    });

    it("should have a title of dashboard", function () {
        expect(ctrl.title).to.equal('dashboard');
    });

    describe("after Repo has changed", function () {

        beforeEach(function () {
            $scope.$apply(function () {
                ctrl.repos = [
                    { tags: ['tag1']},
                    { tags: ['tag1','tag2']},
                    { tags: ['tag3']}
                ];
            });
        });

        it("options should have three values", function () {
            expect(ctrl.options.length).to.equal(3);
        });

        it("options should contain 'tag1'", function () {
            expect(ctrl.options).to.include('tag1');
        });

        it("options should not contain 'tag4'", function () {
            expect(ctrl.options).to.not.include('tag4');
        });

        it("selected should have three values", function () {
            expect(ctrl.selected.length).to.equal(3);
        });

        it("selected should contain 'tag1'", function () {
            expect(ctrl.selected).to.include('tag1');
        });

        it("selected should not contain 'tag4'", function () {
            expect(ctrl.selected).to.not.include('tag4');
        });

    });
});