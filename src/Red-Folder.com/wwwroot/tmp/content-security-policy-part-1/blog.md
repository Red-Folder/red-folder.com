## What is it?
Content Security Policy (CSP) provides an additional layer of security for your website.

By adding a CSP to your website you are able to protect against un-authorised javascript execution and force the use of HTTPS.

The CSP provides instructions to your users browser over what sources are trusted to allow javascript execution - this allows you to protect against Cross Site Scripting (XSS) attacks.

A fuller summary of it can be found in this [article](https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP)

## And the easy way to create it
One of the downsides of CSP is that it can be quite difficult to get right for a complex site.

With this in mind, CSP allows you to run in "report" mode which instructs the client browser to report out all the potential violations.

And the great news is that [report-uri.com](https://report-uri.com) is a service which will listen to those reports, then generate an approriate CSP for you.

## Report-Uri.com
[report-uri.com](https://report-uri.com) is provided by Troy Hunt and Scott Helme.

I've written previously about Troy's work on all things security.  I'm a regular lister to his podcasts, reader of his twitter and learning from his Pluralsight courses.

Scott (another security guru) stood up the [report-uri.com](https://report-uri.com) site a few years back.  With Troy coming onboard to promote and grow the service last year.

## Using the service
To start using the service you just sign up (free plan for less than 10,000 reports per month).

You then follow the initial setup (follow the links on the home page):

* Setup a subdomain (Left hand menu -> Setup -> Custom subdomain)
* Configure filters (Left hand menu -> Filters) - I took the defaults and set the Sites to collect for as red-folder.com
* Setup 2 factor authentication (Left hand menu -> Settings -> Two-Factor Authentication).  You will need a 2fa app on your phone - I'm using Google Authenticator on Android

From here, you can then then setup your reporting address (Setup -> Your reporting address) - select CSP as the Report Type and Wizard as the Report Disposition.  This gave me:

```
https://redfolder.report-uri.com/r/d/csp/wizard
```

## Adding to your site
I then add a custom security policy header to the website.

As I'm using Asp.Net Core, I use the below in Configuration of the Startup class:

```
app.Use(async (context, next) =>
{
    context.Response.Headers.Add(
        "Content-Security-Policy-Report-Only",
        "default-src 'none'; " +
        "form-action 'none'; " +
        "frame-ancestors 'none'; " +
        "report-uri https://redfolder.report-uri.com/r/d/csp/wizard");

    await next();
});
```

## What you should see
If you look at your response headers, you should seem similar:

![Response Headers](/media/blog/content-security-policy-part-1/browser-headers.PNG)

And you will see lots of errors showing in the console log:

![Console Errors](/media/blog/content-security-policy-part-1/browser-errors.PNG)

This is normal.  It is the CSP doing its job and reporting voilations - which in our case is everything because it is an empty CSP.

This same data is sent to [report-uri.com](https://report-uri.com) - for later reporting

## And wait
I now leave it running in report mode.

I'll return to the website next week to see what CSP the wizard recommends based on the data.