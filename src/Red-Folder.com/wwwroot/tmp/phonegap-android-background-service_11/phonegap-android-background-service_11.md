## Summary
The blog will provide some background behind my Phonegap Background Service Plugin.

A future blog will cover how to use the Plugin to create your own service, however I would recommend reading the below so that the concepts make sense.

## What is a Background Service?
Android Applications work differently than the applications we are used to in our PC world.  On our PC's we have become used to running multiple applications and have become used to them working away in tandem.

For example, if we open a YouTube video in IE, we know that it will continue to run regardless of if we are watching it or we switch to Excel.

With Android however an Application will "run" only when it is onscreen.  Once an Application is not on screen (be it because we've taken a phone call, the screen saver comes on, or you open another application) then our original Application will be suspended.  When suspended, none of the Application code will run.

This is a problem if you have code that needs to continue running.  An example of this would be an email client were we want to check for new mail on a regular basis (regardless of if the Application is running or not).

Android provides for this by allowing developers to run Background Services.  A Background Service, as the name implies, runs in the Background.  The benefit of this for us is that we can use a Background Service to run code all the time - regardless of if our Application is running or not.

## Why does Phonegap need a Background Service Plugin?
Background Services for Android need to be developed in Java.  To be able to access that java code from Phonegap then a Plugin is required.

## The Phonegap Background Service Plugin explained
The Plugin, as available [here](https://github.com/Red-Folder/phonegap-plugins/tree/master/Android/BackgroundService), is made up of two parts:


* The core Background Service files
* Sample files on how to produce your own Background Service

The files logically break up as per this diagram:
<!--[if mso & !supportInlineShapes & supportFields]><span style='font-size:11.0pt;line-height:115%;font-family:"Calibri","sans-serif"; mso-ascii-theme-font:minor-latin;mso-fareast-font-family:Calibri;mso-fareast-theme-font: minor-latin;mso-hansi-theme-font:minor-latin;mso-bidi-font-family:"Times New Roman"; mso-bidi-theme-font:minor-bidi;mso-ansi-language:EN-GB;mso-fareast-language: EN-US;mso-bidi-language:AR-SA'><span style='mso-element:field-begin;mso-field-lock: yes'></span><span style='mso-spacerun:yes'> </span>SHAPE <span style='mso-spacerun:yes'> </span>\* MERGEFORMAT <span style='mso-element:field-separator'></span></span><![endif]--><span style="font-family: &quot;Calibri&quot;,&quot;sans-serif&quot;; font-size: 11.0pt; line-height: 115%; mso-ansi-language: EN-GB; mso-ascii-theme-font: minor-latin; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-bidi-language: AR-SA; mso-bidi-theme-font: minor-bidi; mso-fareast-font-family: Calibri; mso-fareast-language: EN-US; mso-fareast-theme-font: minor-latin; mso-hansi-theme-font: minor-latin;"><!--[if gte vml 1]><v:group id="Group_x0020_18"  o:spid="_x0000_s1026" style='width:451.5pt;height:281.25pt;  mso-position-horizontal-relative:char;mso-position-vertical-relative:line'  coordorigin=",-5619" coordsize="57340,35718" o:gfxdata="UEsDBBQABgAIAAAAIQC75UiUBQEAAB4CAAATAAAAW0NvbnRlbnRfVHlwZXNdLnhtbKSRvU7DMBSF dyTewfKKEqcMCKEmHfgZgaE8wMW+SSwc27JvS/v23KTJgkoXFsu+P+c7Ol5vDoMTe0zZBl/LVVlJ gV4HY31Xy4/tS3EvRSbwBlzwWMsjZrlprq/W22PELHjb51r2RPFBqax7HCCXIaLnThvSAMTP1KkI +gs6VLdVdad08ISeCho1ZLN+whZ2jsTzgcsnJwldluLxNDiyagkxOquB2Knae/OLUsyEkjenmdzb mG/YhlRnCWPnb8C898bRJGtQvEOiVxjYhtLOxs8AySiT4JuDystlVV4WPeM6tK3VaILeDZxIOSsu ti/jidNGNZ3/J08yC1dNv9v8AAAA//8DAFBLAwQUAAYACAAAACEArTA/8cEAAAAyAQAACwAAAF9y ZWxzLy5yZWxzhI/NCsIwEITvgu8Q9m7TehCRpr2I4FX0AdZk2wbbJGTj39ubi6AgeJtl2G9m6vYx jeJGka13CqqiBEFOe2Ndr+B03C3WIDihMzh6RwqexNA281l9oBFTfuLBBhaZ4ljBkFLYSMl6oAm5 8IFcdjofJ0z5jL0MqC/Yk1yW5UrGTwY0X0yxNwri3lQgjs+Qk/+zfddZTVuvrxO59CNCmoj3vCwj MfaUFOjRhrPHaN4Wv0VV5OYgm1p+LW1eAAAA//8DAFBLAwQUAAYACAAAACEAPi1/9IQFAACfMAAA HwAAAGNsaXBib2FyZC9kcmF3aW5ncy9kcmF3aW5nMS54bWzsW1tv2zYUfh+w/yDoqXuIbTm+xEad IpclGJClRpyhjwMtUZZaitRI2rH7a/Zb9st2eJHkW9Lajeog0YtDioeHh+fGj5e8/zBPiDPDXMSM Dlyv1nAdTH0WxHQycP+6vzo6cR0hEQ0QYRQP3AUW7ofTX395j/oTjtIo9h3gQEUfDdxIyrRfrws/ wgkSNZZiCm0h4wmSUOWTesDRA3BOSL3ZaHTqCYqpe1qwukQSOVMe78GKMP8LDi4QnSEBLInfX/5i ZST+j3NGfTq75ukoHXIluX87G3InDgYuaI6iBFTk1m2DJYNqfa3XpGAwD3mi6FkYOnPNZaF+NQ88 l44PH9vd41ajDQP40Hbc7non3bYdJfq4pZ8f/f6NniCQGRgKS8LoopLmkTl64A1mktecTVMH6oeb 7FG74/UKRew5ZZGa+W4atJfN9Q77EAETgp1ePl1Fnhk26yqsT6yZtNPpgJQOmG5F4sy4rUbrxGsC gTJuq9FrmynlFkL9lAt5jVniqMLA5SCOjho0uxHSCJGRaFfL5JDzkZ6cnJ+zYKGmOYa/4K6cARtw J5H6VzHwvEFCDhGHEIaPkAzkR/gJCXsYuMyWXCdi/Ou274oewgpaXecBUsLAFf9MEceuQ/6gYuD2 vFYL2EpdabW7Tajw5ZbxcgudJheMQCbS0umiopckK4acJZ8YD87UqNCEqA9jD1xf8qxyIaEOTZB5 fHx2pss+S1Ikb+gohRzgafUpnd3PPyGeWsVKMMktG0Uoxdv0a2i1t7CzqWRhbJVvtKoaiJAjuSBY B4XWPbiEg8gEcqsSUNMoGVQBtD/0pTNDarpeu9HQUQ92J0sUZ6HMaKWwtAUlsNAE0Mn6HlfUHAYl 4LEDF9Oj63NQ5VcY4gQGUK04DMGDjOtADTlykeIQ+ZC6LhCJxzy2Xi6WWu7jBAvnFj84dyxBVFHA oDCQ4ihP/1yMMJ/FPq59RjOkmkBsRaAHpIHyrrutUnnPLxXOx9MygB2UPHkQTAUepSqmzRyyKBHK cEpcQu9wCCkdEm9TK0wvaPiCcKN/5PuYymPTFKEAm8/KgJkF8x56aM1QcQ5jQnLexglzylXeRjRL r3WojZZ3NpZ8srMxM/TQIzMq885JTJnxxTUGRHrWsKGh1+JbxYAOTa58PGN6EHRmeShSJnzT0QAL 5Q45ExhBNmxDaoTMqPtn6XJlLfR6kDpzrWd5N0uG35UvYa1jV2AXPQihSlmq/yUSkTFJACWrFdWs 1WDdvsqvrzG/5kG8Q3IFJywvuY6R/2UCeIsGLzrJQuDo6Hn+FW8fi8C6Up5F3gGcqHEc/B0yEmBe SyPYHE1QWkvJdBLTWmExYZbF39aWxLFeGLev02UJDpnLDFusj1tQwk9Zj8tzFW12s3ruAo5Kjd// /l2zfmWADXRaqgHevTj9lwBILZ4sBZDmsNAA2N0Aadb5EIAUtpEbgFTLo09udgCkxSa+0+h1DOSE zZM9Zqn28NUefqc9/D6IptQUWSAWizGHBsm8yO18efBhH7tUSHP9RKhCmo/D+1KjuEKa3z4HLdUA FdLMjxT3O/rMwOJeR59Z50MgzeYWpNnc5+izQJqeB1cn6+efK1ATgGijui6qrovyC6Xnuy4qNU1u oM3aZ1Ht0dW7CXXN9MYyZ2tL5mz9YObs9hrdJzfpVeZ8tRfth71mz/OmYCQO1M2iOi0TfDLOL5C9 q1ave2nvFFfIyriaP89vj15cht3Yx1emUzbKX1XYo5jKbiqEvmNLvRJLBww5c3b24sxWXTr8nFcw 8JZv49Ihe6+50yuYYivYBLxy/ORWsHo5+GoBzaFfDuaQpgx4EtMAz2uRTMhbyJdv9dUgLD32bXl9 7Wm+3uzafyVQ7/+X66f/AwAA//8DAFBLAwQUAAYACAAAACEAnE5eIeIGAAA6HAAAGgAAAGNsaXBi b2FyZC90aGVtZS90aGVtZTEueG1s7FlPbxtFFL8j8R1Ge2/j/42jOlXs2A20aaPYLepxvB7vTjO7 s5oZJ/UNtUckJERBHKjEjQMCKrUSl/JpAkVQpH4F3szsrnfiNUnbCCpoDvHu29+8/+/Nm93LV+5F DB0SISmPO171YsVDJPb5hMZBx7s1GlxY95BUOJ5gxmPS8eZEelc233/vMt7wGU3GHIvJKCQRQcAo lhu444VKJRtra9IHMpYXeUJieDblIsIKbkWwNhH4CAREbK1WqbTWIkxjbxM4Ks2oz+BfrKQm+EwM NRuCYhyB9JvTKfWJwU4Oqhoh57LHBDrErOMBzwk/GpF7ykMMSwUPOl7F/Hlrm5fX8Ea6iKkVawvr BuYvXZcumBzUjEwRjHOh1UGjfWk7528ATC3j+v1+r1/N+RkA9n2w1OpS5NkYrFe7Gc8CyF4u8+5V mpWGiy/wry/p3O52u812qotlakD2srGEX6+0Gls1B29AFt9cwje6W71ey8EbkMW3lvCDS+1Ww8Ub UMhofLCE1gEdDFLuOWTK2U4pfB3g65UUvkBBNuTZpUVMeaxW5VqE73IxAIAGMqxojNQ8IVPsQ072 cDQWFGsBeIPgwhNL8uUSSctC0hc0UR3vwwTHXgHy8tn3L589Qcf3nx7f/+n4wYPj+z9aRs6qHRwH xVUvvv3sz0cfoz+efPPi4RfleFnE//rDJ7/8/Hk5EMpnYd7zLx//9vTx868+/f27hyXwLYHHRfiI RkSiG+QI7fMIDDNecTUnY/FqK0YhpsUVW3EgcYy1lBL+fRU66BtzzNLoOHp0ievB2wLaRxnw6uyu o/AwFDNFSyRfCyMHuMs563JR6oVrWlbBzaNZHJQLF7Mibh/jwzLZPRw78e3PEuibWVo6hvdC4qi5 x3CscEBiopB+xg8IKbHuDqWOX3epL7jkU4XuUNTFtNQlIzp2smmxaIdGEJd5mc0Qb8c3u7dRl7My q7fJoYuEqsCsRPkRYY4br+KZwlEZyxGOWNHh17EKy5QczoVfxPWlgkgHhHHUnxApy9bcFGBvIejX MHSs0rDvsnnkIoWiB2U8r2POi8htftALcZSUYYc0DovYD+QBpChGe1yVwXe5WyH6HuKA45Xhvk2J E+7Tu8EtGjgqLRJEP5mJklheJdzJ3+GcTTExrQaautOrIxr/XeNmFDq3lXB+jRta5fOvH5Xo/ba2 7C3YvcpqZudEo16FO9mee1xM6NvfnbfxLN4jUBDLW9S75vyuOXv/+ea8qp7PvyUvujA0aD2L2EHb jN3Ryql7Shkbqjkj16UZvCXsPZMBEPU6c7ok+SksCeFSVzIIcHCBwGYNElx9RFU4DHECQ3vV00wC mbIOJEq4hMOiIZfy1ngY/JU9ajb1IcR2DonVLp9Ycl2Ts7NGzsZoFZgDbSaorhmcVVj9UsoUbHsd YVWt1JmlVY1qpik60nKTtYvNoRxcnpsGxNybMNQgGIXAyy0432vRcNjBjEy0322MsrCYKJxniGSI JySNkbZ7OUZVE6QsV5YM0XbYZNAHx1O8VpDW1mzfQNpZglQU11ghLovem0Qpy+BFlIDbyXJkcbE4 WYyOOl67WWt6yMdJx5vCORkuowSiLvUciVkAb5h8JWzan1rMpsoX0WxnhrlFUIVXH9bvSwY7fSAR Um1jGdrUMI/SFGCxlmT1rzXBredlQEk3OpsW9XVIhn9NC/CjG1oynRJfFYNdoGjf2du0lfKZImIY To7QmM3EPobw61QFeyZUwusO0xH0Dbyb0942j9zmnBZd8Y2YwVk6ZkmI03arSzSrZAs3DSnXwdwV 1APbSnU3xr26Kabkz8mUYhr/z0zR+wm8fahPdAR8eNErMNKV0vG4UCGHLpSE1B8IGBxM74Bsgfe7 8BiSCt5Km19BDvWvrTnLw5Q1HCLVPg2QoLAfqVAQsgdtyWTfKcyq6d5lWbKUkcmogroysWqPySFh I90DW3pv91AIqW66SdoGDO5k/rn3aQWNAz3kFOvN6WT53mtr4J+efGwxg1FuHzYDTeb/XMV8PFjs qna9WZ7tvUVD9IPFmNXIqgKEFbaCdlr2r6nCK261tmMtWVxrZspBFJctBmI+ECXwDgnpf7D/UeEz +wVDb6gjvg+9FcHHC80M0gay+oIdPJBukJY4hsHJEm0yaVbWtenopL2WbdbnPOnmck84W2t2lni/ orPz4cwV59TieTo79bDja0tb6WqI7MkSBdI0O8iYwJR9ydrFCRoH1Y4HX5Mg0PfgCr5HeUCraVpN 0+AKPjLBsGS/DHW89CKjwHNLyTH1jFLPMI2M0sgozYwCw1n6DSajtKBT6c8m8NlO/3go+0ICE1z6 RSVrqs7nvs2/AAAA//8DAFBLAwQUAAYACAAAACEAnGZGQbsAAAAkAQAAKgAAAGNsaXBib2FyZC9k cmF3aW5ncy9fcmVscy9kcmF3aW5nMS54bWwucmVsc4SPzQrCMBCE74LvEPZu0noQkSa9iNCr1AcI yTYtNj8kUezbG+hFQfCyMLPsN7NN+7IzeWJMk3ccaloBQae8npzhcOsvuyOQlKXTcvYOOSyYoBXb TXPFWeZylMYpJFIoLnEYcw4nxpIa0cpEfUBXNoOPVuYio2FBqrs0yPZVdWDxkwHii0k6zSF2ugbS L6Ek/2f7YZgUnr16WHT5RwTLpRcWoIwGMwdKV2edNS1dgYmGff0m3gAAAP//AwBQSwECLQAUAAYA CAAAACEAu+VIlAUBAAAeAgAAEwAAAAAAAAAAAAAAAAAAAAAAW0NvbnRlbnRfVHlwZXNdLnhtbFBL AQItABQABgAIAAAAIQCtMD/xwQAAADIBAAALAAAAAAAAAAAAAAAAADYBAABfcmVscy8ucmVsc1BL AQItABQABgAIAAAAIQA+LX/0hAUAAJ8wAAAfAAAAAAAAAAAAAAAAACACAABjbGlwYm9hcmQvZHJh d2luZ3MvZHJhd2luZzEueG1sUEsBAi0AFAAGAAgAAAAhAJxOXiHiBgAAOhwAABoAAAAAAAAAAAAA AAAA4QcAAGNsaXBib2FyZC90aGVtZS90aGVtZTEueG1sUEsBAi0AFAAGAAgAAAAhAJxmRkG7AAAA JAEAACoAAAAAAAAAAAAAAAAA+w4AAGNsaXBib2FyZC9kcmF3aW5ncy9fcmVscy9kcmF3aW5nMS54 bWwucmVsc1BLBQYAAAAABQAFAGcBAAD+DwAAAAA= "> <v:rect id="Rectangle_x0020_9" o:spid="_x0000_s1027" style='position:absolute;   left:666;top:-5619;width:40482;height:4095;visibility:visible;   mso-wrap-style:square;v-text-anchor:middle' o:gfxdata="UEsDBBQABgAIAAAAIQDw94q7/QAAAOIBAAATAAAAW0NvbnRlbnRfVHlwZXNdLnhtbJSRzUrEMBDH 74LvEOYqbaoHEWm6B6tHFV0fYEimbdg2CZlYd9/edD8u4goeZ+b/8SOpV9tpFDNFtt4puC4rEOS0 N9b1Cj7WT8UdCE7oDI7ekYIdMayay4t6vQvEIrsdKxhSCvdSsh5oQi59IJcvnY8TpjzGXgbUG+xJ 3lTVrdTeJXKpSEsGNHVLHX6OSTxu8/pAEmlkEA8H4dKlAEMYrcaUSeXszI+W4thQZudew4MNfJUx QP7asFzOFxx9L/lpojUkXjGmZ5wyhjSRJQ8YKGvKv1MWzIkL33VWU9lGfl98J6hz4cZ/uUjzf7Pb bHuj+ZQu9z/UfAMAAP//AwBQSwMEFAAGAAgAAAAhADHdX2HSAAAAjwEAAAsAAABfcmVscy8ucmVs c6SQwWrDMAyG74O9g9G9cdpDGaNOb4VeSwe7CltJTGPLWCZt376mMFhGbzvqF/o+8e/2tzCpmbJ4 jgbWTQuKomXn42Dg63xYfYCSgtHhxJEM3Elg372/7U40YalHMvokqlKiGBhLSZ9aix0poDScKNZN zzlgqWMedEJ7wYH0pm23Ov9mQLdgqqMzkI9uA+p8T9X8hx28zSzcl8Zy0Nz33r6iasfXeKK5UjAP VAy4LM8w09zU50C/9q7/6ZURE31X/kL8TKv1x6wXNXYPAAAA//8DAFBLAwQUAAYACAAAACEAMy8F nkEAAAA5AAAAEAAAAGRycy9zaGFwZXhtbC54bWyysa/IzVEoSy0qzszPs1Uy1DNQUkjNS85PycxL t1UKDXHTtVBSKC5JzEtJzMnPS7VVqkwtVrK34+UCAAAA//8DAFBLAwQUAAYACAAAACEA2BnXHMIA AADaAAAADwAAAGRycy9kb3ducmV2LnhtbESPQWvCQBSE7wX/w/IEb82mFaRG1xAEoaUno9Lra/a5 Cc2+Dbtbjf31bqHQ4zAz3zDrcrS9uJAPnWMFT1kOgrhxumOj4HjYPb6ACBFZY++YFNwoQLmZPKyx 0O7Ke7rU0YgE4VCggjbGoZAyNC1ZDJkbiJN3dt5iTNIbqT1eE9z28jnPF9Jix2mhxYG2LTVf9bdN lAXXJ+flrTq8+583+/kRjJkrNZuO1QpEpDH+h//ar1rBEn6vpBsgN3cAAAD//wMAUEsBAi0AFAAG AAgAAAAhAPD3irv9AAAA4gEAABMAAAAAAAAAAAAAAAAAAAAAAFtDb250ZW50X1R5cGVzXS54bWxQ SwECLQAUAAYACAAAACEAMd1fYdIAAACPAQAACwAAAAAAAAAAAAAAAAAuAQAAX3JlbHMvLnJlbHNQ SwECLQAUAAYACAAAACEAMy8FnkEAAAA5AAAAEAAAAAAAAAAAAAAAAAApAgAAZHJzL3NoYXBleG1s LnhtbFBLAQItABQABgAIAAAAIQDYGdccwgAAANoAAAAPAAAAAAAAAAAAAAAAAJgCAABkcnMvZG93 bnJldi54bWxQSwUGAAAAAAQABAD1AAAAhwMAAAAA " fillcolor="#9bbb59 [3206]" strokecolor="#4e6128 [1606]" strokeweight="2pt">  <v:textbox>   <![if !mso]>   <table cellpadding=0 cellspacing=0 width="100%"><tr>     <td><![endif]>          <p class=MsoNormal align=center style='text-align:center'><span      style='font-size:20.0pt;line-height:115%'>index.html<o:p></o:p></span></p><![if !mso]></td>    </tr></table><![endif]></v:textbox> </v:rect><v:rect id="Rectangle_x0020_10" o:spid="_x0000_s1028" style='position:absolute;   top:5048;width:57340;height:19812;visibility:visible;mso-wrap-style:square;   v-text-anchor:middle' o:gfxdata="UEsDBBQABgAIAAAAIQDw94q7/QAAAOIBAAATAAAAW0NvbnRlbnRfVHlwZXNdLnhtbJSRzUrEMBDH 74LvEOYqbaoHEWm6B6tHFV0fYEimbdg2CZlYd9/edD8u4goeZ+b/8SOpV9tpFDNFtt4puC4rEOS0 N9b1Cj7WT8UdCE7oDI7ekYIdMayay4t6vQvEIrsdKxhSCvdSsh5oQi59IJcvnY8TpjzGXgbUG+xJ 3lTVrdTeJXKpSEsGNHVLHX6OSTxu8/pAEmlkEA8H4dKlAEMYrcaUSeXszI+W4thQZudew4MNfJUx QP7asFzOFxx9L/lpojUkXjGmZ5wyhjSRJQ8YKGvKv1MWzIkL33VWU9lGfl98J6hz4cZ/uUjzf7Pb bHuj+ZQu9z/UfAMAAP//AwBQSwMEFAAGAAgAAAAhADHdX2HSAAAAjwEAAAsAAABfcmVscy8ucmVs c6SQwWrDMAyG74O9g9G9cdpDGaNOb4VeSwe7CltJTGPLWCZt376mMFhGbzvqF/o+8e/2tzCpmbJ4 jgbWTQuKomXn42Dg63xYfYCSgtHhxJEM3Elg372/7U40YalHMvokqlKiGBhLSZ9aix0poDScKNZN zzlgqWMedEJ7wYH0pm23Ov9mQLdgqqMzkI9uA+p8T9X8hx28zSzcl8Zy0Nz33r6iasfXeKK5UjAP VAy4LM8w09zU50C/9q7/6ZURE31X/kL8TKv1x6wXNXYPAAAA//8DAFBLAwQUAAYACAAAACEAMy8F nkEAAAA5AAAAEAAAAGRycy9zaGFwZXhtbC54bWyysa/IzVEoSy0qzszPs1Uy1DNQUkjNS85PycxL t1UKDXHTtVBSKC5JzEtJzMnPS7VVqkwtVrK34+UCAAAA//8DAFBLAwQUAAYACAAAACEAIA1EYMEA AADbAAAADwAAAGRycy9kb3ducmV2LnhtbERPzWrDMAy+F/YORoNdSutsY2WkdUsYhHSHHdbuAUSs xqGxHGIvyd5+OhR6k74/fdodZt+pkYbYBjbwvM5AEdfBttwY+DmXq3dQMSFb7AKTgT+KcNg/LHaY 2zDxN42n1CgJ4ZijAZdSn2sda0ce4zr0xMJdwuAxyTo02g44Sbjv9EuWbbTHluWCw54+HNXX0683 MBbH11J/4XlchiiY+5yr6s2Yp8e52IJKNKe7+OY+Wqkv7eUXGUDv/wEAAP//AwBQSwECLQAUAAYA CAAAACEA8PeKu/0AAADiAQAAEwAAAAAAAAAAAAAAAAAAAAAAW0NvbnRlbnRfVHlwZXNdLnhtbFBL AQItABQABgAIAAAAIQAx3V9h0gAAAI8BAAALAAAAAAAAAAAAAAAAAC4BAABfcmVscy8ucmVsc1BL AQItABQABgAIAAAAIQAzLwWeQQAAADkAAAAQAAAAAAAAAAAAAAAAACkCAABkcnMvc2hhcGV4bWwu eG1sUEsBAi0AFAAGAAgAAAAhACANRGDBAAAA2wAAAA8AAAAAAAAAAAAAAAAAmAIAAGRycy9kb3du cmV2LnhtbFBLBQYAAAAABAAEAPUAAACGAwAAAAA= " filled="f" strokecolor="#243f60 [1604]" strokeweight="2pt">  <v:stroke dashstyle="dash"/>  <v:textbox>   <![if !mso]>   <table cellpadding=0 cellspacing=0 width="100%"><tr>     <td><![endif]>          <p class=MsoNormal align=right style='text-align:right'><span      style='font-size:20.0pt;line-height:115%;color:#1F497D;mso-themecolor:      text2'>Background<o:p></o:p></span></p><p class=MsoNormal align=right style='text-align:right'><span      style='font-size:20.0pt;line-height:115%;color:#1F497D;mso-themecolor:      text2'>Service<o:p></o:p></span></p><p class=MsoNormal align=right style='text-align:right'><span      style='font-size:20.0pt;line-height:115%;color:#1F497D;mso-themecolor:      text2'>Plugin<o:p></o:p></span></p><![if !mso]></td>    </tr></table><![endif]></v:textbox> </v:rect><v:rect id="Rectangle_x0020_11" o:spid="_x0000_s1029" style='position:absolute;   left:666;top:6096;width:40482;height:4095;visibility:visible;   mso-wrap-style:square;v-text-anchor:middle' o:gfxdata="UEsDBBQABgAIAAAAIQDw94q7/QAAAOIBAAATAAAAW0NvbnRlbnRfVHlwZXNdLnhtbJSRzUrEMBDH 74LvEOYqbaoHEWm6B6tHFV0fYEimbdg2CZlYd9/edD8u4goeZ+b/8SOpV9tpFDNFtt4puC4rEOS0 N9b1Cj7WT8UdCE7oDI7ekYIdMayay4t6vQvEIrsdKxhSCvdSsh5oQi59IJcvnY8TpjzGXgbUG+xJ 3lTVrdTeJXKpSEsGNHVLHX6OSTxu8/pAEmlkEA8H4dKlAEMYrcaUSeXszI+W4thQZudew4MNfJUx QP7asFzOFxx9L/lpojUkXjGmZ5wyhjSRJQ8YKGvKv1MWzIkL33VWU9lGfl98J6hz4cZ/uUjzf7Pb bHuj+ZQu9z/UfAMAAP//AwBQSwMEFAAGAAgAAAAhADHdX2HSAAAAjwEAAAsAAABfcmVscy8ucmVs c6SQwWrDMAyG74O9g9G9cdpDGaNOb4VeSwe7CltJTGPLWCZt376mMFhGbzvqF/o+8e/2tzCpmbJ4 jgbWTQuKomXn42Dg63xYfYCSgtHhxJEM3Elg372/7U40YalHMvokqlKiGBhLSZ9aix0poDScKNZN zzlgqWMedEJ7wYH0pm23Ov9mQLdgqqMzkI9uA+p8T9X8hx28zSzcl8Zy0Nz33r6iasfXeKK5UjAP VAy4LM8w09zU50C/9q7/6ZURE31X/kL8TKv1x6wXNXYPAAAA//8DAFBLAwQUAAYACAAAACEAMy8F nkEAAAA5AAAAEAAAAGRycy9zaGFwZXhtbC54bWyysa/IzVEoSy0qzszPs1Uy1DNQUkjNS85PycxL t1UKDXHTtVBSKC5JzEtJzMnPS7VVqkwtVrK34+UCAAAA//8DAFBLAwQUAAYACAAAACEAq2EkKL8A AADbAAAADwAAAGRycy9kb3ducmV2LnhtbERPzYrCMBC+C75DGMGbpl1ESzWKCLKyl2XVBxiasa02 k5JEW/fpN8KCt/n4fme16U0jHuR8bVlBOk1AEBdW11wqOJ/2kwyED8gaG8uk4EkeNuvhYIW5th3/ 0OMYShFD2OeooAqhzaX0RUUG/dS2xJG7WGcwROhKqR12Mdw08iNJ5tJgzbGhwpZ2FRW3490osOl3 +Dp1sztT5z6z+lo0v4tMqfGo3y5BBOrDW/zvPug4P4XXL/EAuf4DAAD//wMAUEsBAi0AFAAGAAgA AAAhAPD3irv9AAAA4gEAABMAAAAAAAAAAAAAAAAAAAAAAFtDb250ZW50X1R5cGVzXS54bWxQSwEC LQAUAAYACAAAACEAMd1fYdIAAACPAQAACwAAAAAAAAAAAAAAAAAuAQAAX3JlbHMvLnJlbHNQSwEC LQAUAAYACAAAACEAMy8FnkEAAAA5AAAAEAAAAAAAAAAAAAAAAAApAgAAZHJzL3NoYXBleG1sLnht bFBLAQItABQABgAIAAAAIQCrYSQovwAAANsAAAAPAAAAAAAAAAAAAAAAAJgCAABkcnMvZG93bnJl di54bWxQSwUGAAAAAAQABAD1AAAAhAMAAAAA " fillcolor="#4f81bd [3204]" strokecolor="#243f60 [1604]" strokeweight="2pt">  <v:textbox>   <![if !mso]>   <table cellpadding=0 cellspacing=0 width="100%"><tr>     <td><![endif]>          <p class=MsoNormal align=center style='text-align:center'><span      style='font-size:20.0pt;line-height:115%'>backgroundService.js<o:p></o:p></span></p><![if !mso]></td>    </tr></table><![endif]></v:textbox> </v:rect><v:rect id="Rectangle_x0020_12" o:spid="_x0000_s1030" style='position:absolute;   left:666;top:11144;width:40482;height:6001;visibility:visible;   mso-wrap-style:square;v-text-anchor:middle' o:gfxdata="UEsDBBQABgAIAAAAIQDw94q7/QAAAOIBAAATAAAAW0NvbnRlbnRfVHlwZXNdLnhtbJSRzUrEMBDH 74LvEOYqbaoHEWm6B6tHFV0fYEimbdg2CZlYd9/edD8u4goeZ+b/8SOpV9tpFDNFtt4puC4rEOS0 N9b1Cj7WT8UdCE7oDI7ekYIdMayay4t6vQvEIrsdKxhSCvdSsh5oQi59IJcvnY8TpjzGXgbUG+xJ 3lTVrdTeJXKpSEsGNHVLHX6OSTxu8/pAEmlkEA8H4dKlAEMYrcaUSeXszI+W4thQZudew4MNfJUx QP7asFzOFxx9L/lpojUkXjGmZ5wyhjSRJQ8YKGvKv1MWzIkL33VWU9lGfl98J6hz4cZ/uUjzf7Pb bHuj+ZQu9z/UfAMAAP//AwBQSwMEFAAGAAgAAAAhADHdX2HSAAAAjwEAAAsAAABfcmVscy8ucmVs c6SQwWrDMAyG74O9g9G9cdpDGaNOb4VeSwe7CltJTGPLWCZt376mMFhGbzvqF/o+8e/2tzCpmbJ4 jgbWTQuKomXn42Dg63xYfYCSgtHhxJEM3Elg372/7U40YalHMvokqlKiGBhLSZ9aix0poDScKNZN zzlgqWMedEJ7wYH0pm23Ov9mQLdgqqMzkI9uA+p8T9X8hx28zSzcl8Zy0Nz33r6iasfXeKK5UjAP VAy4LM8w09zU50C/9q7/6ZURE31X/kL8TKv1x6wXNXYPAAAA//8DAFBLAwQUAAYACAAAACEAMy8F nkEAAAA5AAAAEAAAAGRycy9zaGFwZXhtbC54bWyysa/IzVEoSy0qzszPs1Uy1DNQUkjNS85PycxL t1UKDXHTtVBSKC5JzEtJzMnPS7VVqkwtVrK34+UCAAAA//8DAFBLAwQUAAYACAAAACEAW7O6X8EA AADbAAAADwAAAGRycy9kb3ducmV2LnhtbERP22rCQBB9L/gPywi+1U2CtCG6igil0pdS9QOG7JhE s7Nhd3OxX98tFPo2h3OdzW4yrRjI+caygnSZgCAurW64UnA5vz3nIHxA1thaJgUP8rDbzp42WGg7 8hcNp1CJGMK+QAV1CF0hpS9rMuiXtiOO3NU6gyFCV0ntcIzhppVZkrxIgw3Hhho7OtRU3k+9UWDT z/BxHlc90+je8+ZWtt+vuVKL+bRfgwg0hX/xn/uo4/wMfn+JB8jtDwAAAP//AwBQSwECLQAUAAYA CAAAACEA8PeKu/0AAADiAQAAEwAAAAAAAAAAAAAAAAAAAAAAW0NvbnRlbnRfVHlwZXNdLnhtbFBL AQItABQABgAIAAAAIQAx3V9h0gAAAI8BAAALAAAAAAAAAAAAAAAAAC4BAABfcmVscy8ucmVsc1BL AQItABQABgAIAAAAIQAzLwWeQQAAADkAAAAQAAAAAAAAAAAAAAAAACkCAABkcnMvc2hhcGV4bWwu eG1sUEsBAi0AFAAGAAgAAAAhAFuzul/BAAAA2wAAAA8AAAAAAAAAAAAAAAAAmAIAAGRycy9kb3du cmV2LnhtbFBLBQYAAAAABAAEAPUAAACGAwAAAAA= " fillcolor="#4f81bd [3204]" strokecolor="#243f60 [1604]" strokeweight="2pt">  <v:textbox>   <![if !mso]>   <table cellpadding=0 cellspacing=0 width="100%"><tr>     <td><![endif]>          <p class=MsoNormal align=center style='margin-bottom:0cm;margin-bottom:      .0001pt;text-align:center'><span style='font-size:20.0pt;line-height:115%'>backgroundServicePlugin.java<o:p></o:p></span></p><p class=MsoNormal align=center style='margin-bottom:0cm;margin-bottom:      .0001pt;text-align:center'><span style='font-size:10.0pt;line-height:115%'>(com.red_folder.phonegap.plugin.backgroundservice)<br      style='mso-special-character:line-break'>     <![if !supportLineBreakNewLine]><br style='mso-special-character:line-break'>     <![endif]><o:p></o:p></span></p><p class=MsoNormal align=center style='text-align:center'><span      style='font-size:20.0pt;line-height:115%'><o:p> </o:p></span></p><p class=MsoNormal align=center style='text-align:center'><span      style='font-size:20.0pt;line-height:115%'>(<o:p></o:p></span></p><![if !mso]></td>    </tr></table><![endif]></v:textbox> </v:rect><v:rect id="Rectangle_x0020_14" o:spid="_x0000_s1031" style='position:absolute;   left:666;top:17907;width:40482;height:6000;visibility:visible;   mso-wrap-style:square;v-text-anchor:middle' o:gfxdata="UEsDBBQABgAIAAAAIQDw94q7/QAAAOIBAAATAAAAW0NvbnRlbnRfVHlwZXNdLnhtbJSRzUrEMBDH 74LvEOYqbaoHEWm6B6tHFV0fYEimbdg2CZlYd9/edD8u4goeZ+b/8SOpV9tpFDNFtt4puC4rEOS0 N9b1Cj7WT8UdCE7oDI7ekYIdMayay4t6vQvEIrsdKxhSCvdSsh5oQi59IJcvnY8TpjzGXgbUG+xJ 3lTVrdTeJXKpSEsGNHVLHX6OSTxu8/pAEmlkEA8H4dKlAEMYrcaUSeXszI+W4thQZudew4MNfJUx QP7asFzOFxx9L/lpojUkXjGmZ5wyhjSRJQ8YKGvKv1MWzIkL33VWU9lGfl98J6hz4cZ/uUjzf7Pb bHuj+ZQu9z/UfAMAAP//AwBQSwMEFAAGAAgAAAAhADHdX2HSAAAAjwEAAAsAAABfcmVscy8ucmVs c6SQwWrDMAyG74O9g9G9cdpDGaNOb4VeSwe7CltJTGPLWCZt376mMFhGbzvqF/o+8e/2tzCpmbJ4 jgbWTQuKomXn42Dg63xYfYCSgtHhxJEM3Elg372/7U40YalHMvokqlKiGBhLSZ9aix0poDScKNZN zzlgqWMedEJ7wYH0pm23Ov9mQLdgqqMzkI9uA+p8T9X8hx28zSzcl8Zy0Nz33r6iasfXeKK5UjAP VAy4LM8w09zU50C/9q7/6ZURE31X/kL8TKv1x6wXNXYPAAAA//8DAFBLAwQUAAYACAAAACEAMy8F nkEAAAA5AAAAEAAAAGRycy9zaGFwZXhtbC54bWyysa/IzVEoSy0qzszPs1Uy1DNQUkjNS85PycxL t1UKDXHTtVBSKC5JzEtJzMnPS7VVqkwtVrK34+UCAAAA//8DAFBLAwQUAAYACAAAACEAuxaHsL8A AADbAAAADwAAAGRycy9kb3ducmV2LnhtbERP24rCMBB9X/Afwgi+ramLuKUaRYRF8UW8fMDQjG21 mZQk2urXG0HYtzmc68wWnanFnZyvLCsYDRMQxLnVFRcKTse/7xSED8gaa8uk4EEeFvPe1wwzbVve 0/0QChFD2GeooAyhyaT0eUkG/dA2xJE7W2cwROgKqR22MdzU8idJJtJgxbGhxIZWJeXXw80osKNd 2B7b8Y2pdeu0uuT18zdVatDvllMQgbrwL/64NzrOH8P7l3iAnL8AAAD//wMAUEsBAi0AFAAGAAgA AAAhAPD3irv9AAAA4gEAABMAAAAAAAAAAAAAAAAAAAAAAFtDb250ZW50X1R5cGVzXS54bWxQSwEC LQAUAAYACAAAACEAMd1fYdIAAACPAQAACwAAAAAAAAAAAAAAAAAuAQAAX3JlbHMvLnJlbHNQSwEC LQAUAAYACAAAACEAMy8FnkEAAAA5AAAAEAAAAAAAAAAAAAAAAAApAgAAZHJzL3NoYXBleG1sLnht bFBLAQItABQABgAIAAAAIQC7FoewvwAAANsAAAAPAAAAAAAAAAAAAAAAAJgCAABkcnMvZG93bnJl di54bWxQSwUGAAAAAAQABAD1AAAAhAMAAAAA " fillcolor="#4f81bd [3204]" strokecolor="#243f60 [1604]" strokeweight="2pt">  <v:textbox>   <![if !mso]>   <table cellpadding=0 cellspacing=0 width="100%"><tr>     <td><![endif]>          <p class=MsoNormal align=center style='margin-bottom:0cm;margin-bottom:      .0001pt;text-align:center'><span style='font-size:20.0pt;line-height:115%'>backgroundService.java<o:p></o:p></span></p><p class=MsoNormal align=center style='margin-bottom:0cm;margin-bottom:      .0001pt;text-align:center'><span style='font-size:10.0pt;line-height:115%'>(com.red_folder.phonegap.plugin.backgroundservice)<br      style='mso-special-character:line-break'>     <![if !supportLineBreakNewLine]><br style='mso-special-character:line-break'>     <![endif]><o:p></o:p></span></p><p class=MsoNormal align=center style='text-align:center'><span      style='font-size:20.0pt;line-height:115%'><o:p> </o:p></span></p><p class=MsoNormal align=center style='text-align:center'><span      style='font-size:20.0pt;line-height:115%'>(<o:p></o:p></span></p><![if !mso]></td>    </tr></table><![endif]></v:textbox> </v:rect><v:rect id="Rectangle_x0020_15" o:spid="_x0000_s1032" style='position:absolute;   left:666;top:26003;width:40482;height:4096;visibility:visible;   mso-wrap-style:square;v-text-anchor:middle' o:gfxdata="UEsDBBQABgAIAAAAIQDw94q7/QAAAOIBAAATAAAAW0NvbnRlbnRfVHlwZXNdLnhtbJSRzUrEMBDH 74LvEOYqbaoHEWm6B6tHFV0fYEimbdg2CZlYd9/edD8u4goeZ+b/8SOpV9tpFDNFtt4puC4rEOS0 N9b1Cj7WT8UdCE7oDI7ekYIdMayay4t6vQvEIrsdKxhSCvdSsh5oQi59IJcvnY8TpjzGXgbUG+xJ 3lTVrdTeJXKpSEsGNHVLHX6OSTxu8/pAEmlkEA8H4dKlAEMYrcaUSeXszI+W4thQZudew4MNfJUx QP7asFzOFxx9L/lpojUkXjGmZ5wyhjSRJQ8YKGvKv1MWzIkL33VWU9lGfl98J6hz4cZ/uUjzf7Pb bHuj+ZQu9z/UfAMAAP//AwBQSwMEFAAGAAgAAAAhADHdX2HSAAAAjwEAAAsAAABfcmVscy8ucmVs c6SQwWrDMAyG74O9g9G9cdpDGaNOb4VeSwe7CltJTGPLWCZt376mMFhGbzvqF/o+8e/2tzCpmbJ4 jgbWTQuKomXn42Dg63xYfYCSgtHhxJEM3Elg372/7U40YalHMvokqlKiGBhLSZ9aix0poDScKNZN zzlgqWMedEJ7wYH0pm23Ov9mQLdgqqMzkI9uA+p8T9X8hx28zSzcl8Zy0Nz33r6iasfXeKK5UjAP VAy4LM8w09zU50C/9q7/6ZURE31X/kL8TKv1x6wXNXYPAAAA//8DAFBLAwQUAAYACAAAACEAMy8F nkEAAAA5AAAAEAAAAGRycy9zaGFwZXhtbC54bWyysa/IzVEoSy0qzszPs1Uy1DNQUkjNS85PycxL t1UKDXHTtVBSKC5JzEtJzMnPS7VVqkwtVrK34+UCAAAA//8DAFBLAwQUAAYACAAAACEAMbGPyMIA AADbAAAADwAAAGRycy9kb3ducmV2LnhtbESPQWsCMRCF70L/Q5hCb5rVoshqFCkUlJ66Kl7HzZhd 3EyWJOraX98IgrcZ3nvfvJkvO9uIK/lQO1YwHGQgiEunazYKdtvv/hREiMgaG8ek4E4Blou33hxz 7W78S9ciGpEgHHJUUMXY5lKGsiKLYeBa4qSdnLcY0+qN1B5vCW4bOcqyibRYc7pQYUtfFZXn4mIT ZcLF3nl5X21//N/GHg/BmE+lPt671QxEpC6+zM/0Wqf6Y3j8kgaQi38AAAD//wMAUEsBAi0AFAAG AAgAAAAhAPD3irv9AAAA4gEAABMAAAAAAAAAAAAAAAAAAAAAAFtDb250ZW50X1R5cGVzXS54bWxQ SwECLQAUAAYACAAAACEAMd1fYdIAAACPAQAACwAAAAAAAAAAAAAAAAAuAQAAX3JlbHMvLnJlbHNQ SwECLQAUAAYACAAAACEAMy8FnkEAAAA5AAAAEAAAAAAAAAAAAAAAAAApAgAAZHJzL3NoYXBleG1s LnhtbFBLAQItABQABgAIAAAAIQAxsY/IwgAAANsAAAAPAAAAAAAAAAAAAAAAAJgCAABkcnMvZG93 bnJldi54bWxQSwUGAAAAAAQABAD1AAAAhwMAAAAA " fillcolor="#9bbb59 [3206]" strokecolor="#4e6128 [1606]" strokeweight="2pt">  <v:textbox>   <![if !mso]>   <table cellpadding=0 cellspacing=0 width="100%"><tr>     <td><![endif]>          <p class=MsoNormal align=center style='text-align:center'><span      style='font-size:18.0pt;line-height:115%'>MyService.java<o:p></o:p></span></p><![if !mso]></td>    </tr></table><![endif]></v:textbox> </v:rect><w:wrap type="none"/> <w:anchorlock/></v:group><![endif]--><!--[if !vml]--><!--[endif]--><!--[if gte vml 1]><v:shapetype  id="_x0000_t75" coordsize="21600,21600" o:spt="75" o:preferrelative="t"  path="m@4@5l@4@11@9@11@9@5xe" filled="f" stroked="f"> <v:stroke joinstyle="miter"/> <v:formulas>  <v:f eqn="if lineDrawn pixelLineWidth 0"/>  <v:f eqn="sum @0 1 0"/>  <v:f eqn="sum 0 0 @1"/>  <v:f eqn="prod @2 1 2"/>  <v:f eqn="prod @3 21600 pixelWidth"/>  <v:f eqn="prod @3 21600 pixelHeight"/>  <v:f eqn="sum @0 0 1"/>  <v:f eqn="prod @6 1 2"/>  <v:f eqn="prod @7 21600 pixelWidth"/>  <v:f eqn="sum @8 21600 0"/>  <v:f eqn="prod @7 21600 pixelHeight"/>  <v:f eqn="sum @10 21600 0"/> </v:formulas> <v:path o:extrusionok="f" gradientshapeok="t" o:connecttype="rect"/> <o:lock v:ext="edit" aspectratio="t"/></v:shapetype><![endif]--></span><!--[if mso & !supportInlineShapes & supportFields]><span style='font-size:11.0pt;line-height:115%;font-family:"Calibri","sans-serif"; mso-ascii-theme-font:minor-latin;mso-fareast-font-family:Calibri;mso-fareast-theme-font: minor-latin;mso-hansi-theme-font:minor-latin;mso-bidi-font-family:"Times New Roman"; mso-bidi-theme-font:minor-bidi;mso-ansi-language:EN-GB;mso-fareast-language: EN-US;mso-bidi-language:AR-SA'><v:shape id="_x0000_i1025" type="#_x0000_t75"  style='width:451.5pt;height:281.25pt'> <v:imagedata croptop="-65520f" cropbottom="65520f"/></v:shape><span style='mso-element:field-end'></span></span><![endif]-->
![Image](/media/blog/phonegap-android-background-service_11/Image-1.png)

Every Phonegap Application that uses the Plugin needs to include the core Background Service files:


* backgroundService.js
* backgroundServicePlugin.java
* backgroundService.java

Each Phonegap Application will then need to have, for every Background Service, two files:
* A java class which extends the backgroundService.java.  In the above example this is the MyService.java.  This class contains the code that is run by the Background Service.
* A js file.  In the above example this is the myService.js.  This provides a simple interface over the backgroundService.js javascript for use in your html code.


## What next?
Next I will start a short series on using the Plugin to create your own Background Service. 