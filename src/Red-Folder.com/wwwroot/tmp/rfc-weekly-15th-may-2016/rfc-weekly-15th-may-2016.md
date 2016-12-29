## Angular Testing
Recently started playing with Angular tests (1.x).  As with all things JavaScript there are a dizzying array of tools out there.  So I thought I’d write a quick summary on what seem to be the key ones and how they fit together.

I'm basing most of this on the [Angular 1 Style guide by John Papa](https://github.com/johnpapa/angular-styleguide/tree/master/a1#testing) as this provides an opinionated starting point.

[Karma](http://karma-runner.github.io/0.13/index.html) is a test runner.  It integrates well with build tools like Grunt &amp; Gulp (my preference being Gulp) – making automation &amp; Continuous Integration possible.

[Jasmine](http://jasmine.github.io/) is a testing library (similar in concept to xUnit, nUnit, jUnit, etc)

[Mocha](http://mochajs.org/) is comparable to Jasmine (you’d use one or the other).  John’s preference is Mocha.

[Chai](http://chaijs.com/) is a “BDD/ TDD assertion library … that can be delightfully paired with any javascript testing framework”.  From what I can see it allows you use a variety of different styles in your testing code – be it the BDD style of foo.should.be.a(‘string’) or expect(foo).to.be.a(‘string’) – or the TDD style assert.typeOf(foo, ‘string’).  John recommends choosing an assert library when working with Mocha (he specifically calls out Chai).

[Sinon](http://sinonjs.org/) provides test spies, stubs and mocks.  It is unit testing framework agnostics (so can work with Jasmine &amp; Mocha &amp; others).  It allows you fake objects (fair crucial for unit testing) as well as provide validation that certainly calls were made, values were passed etc.

[ngMock](https://docs.angularjs.org/api/ngMock) provides a means of injecting and mocking Angular services into unit tests.  I'm currently working my way through the [AngularJS Unit Testing in-depth, Using ngMock](https://www.pluralsight.com/courses/angularjs-ngmock-unit-testing) course from Pluralsight

[PhantomJS](http://phantomjs.org/) is a “headless” web browser.  It provides the functionality of a web browser – but without the visual element.  This makes it useless for humans – but great for automated testing.  You can achieve the same with Chrome, IE, Firefox, etc – but the absence of the visual element makes it much faster.

Good article for how most it plumbs together can be found [here](http://jasonwatmore.com/post/2015/04/09/Unit-Testing-in-AngularJS-So-many-libraries-what-does-what.aspx)

## Koa
If you are familiar with the NodeJs world, you will probably be aware of Express – a framework for providing lightweight web application and APIs.

Express is even part of the “standard” MEAN stack (MongoDB, Express, Angular, NodeJs).  Similar to the LAMP stack (Linux Apache MySQL PHP) in that it describes a full stack for produce web applications.

The team behind Express are now bringing forward Koa which “aims to be a smaller, more expressive, and more robust foundation”.  I'm not sure of the history behind this – if this is a completing project or ultimately a replacement for Express.

Given how popular Express is, then I would expect similar interested in Koa.

The project website can be found [here](http://koajs.com/)

## Why and how testing can make you happier
A great little [article](http://mikbe.com/code/testing/dx/2016/03/11/why-and-how-testing-can-make-you-happier.html) on why writing tests are a good thing.

## Webpack module bundler
I've written previously about using Gulp &amp; Grunt for assisting in your web deployment pipeline.  [Webpack](https://webpack.github.io/) provides similar functionality.  It seems to be a bit more firmly aimed at the just deployment.  Possibly this may make it an easier entry than Gulp &amp; Grunt which allow you to do everything.

At the moment I'm not seeing a reason to adopt if you are already using Gulp or Grunt.

## Sitespeed.io
Another [tool](https://www.sitespeed.io/) for identifying problems with you website.

## Codenamer
Great fun little [tool](http://mikbe.com/awesome/projects/2016/02/14/generate-awesome-codenames-with-codenamer.html) for helping you create names for projects or releases .  Often the most difficult part of development.

## Shameless self-promotion
Within my ROI series on LinkedIn, I've asked the question of [“When will you admit you are technology company?”](/blog/when-are-you-going-to-admit-you-are-a-technology-company).  With the disruption caused through technology, organisations need to assess if they are thinking about it in the correct way.  Is IT seen as a cost or an enabler?
