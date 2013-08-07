ExpressForms
============

Express Forms Web Development Framework

============

ExpressForms-0.5, released August 7, 2013, is the first publicly released version of Express Forms.
It is realeased under the MIT license.  You are encouraged to contribute to it if you like.

The basic idea of Express Forms is to automate common aspects of the development of data-driven web applications.
Most of these applications could be described as "CRUD" applications; having four basic operations:
Create, Read, Update, and Delete.  Express Forms makes it easy to stand up web pages that provide the user with 
access to these operations.  Using Express Forms instad of designing these pages by hand is meant to make
.net web development more DRY, and thus faster and less error-prone.

This may sound somewhat like ASP.net MVC scaffolding, but I want to provide a much richer set of features
so that it can be used in production systems.

The starting point for working with Express Forms is the creation of two classes.  One inherits "ExpressFormsController"
and the other implements IExpressFormsExchange.

The ExpressFormsController class you derive has two type parameters: a class type and an Id type.  The class type
is the class whose properties will be represented on the form.  The Id type is meant to be a primitive type
describing an identifier for the data, but in this early version, only 32-bit integers are supported; furthermore,
the name of this property *must* be Id; this is hard-coded in a number of places; to be fixed in a later version.

The ExpressForms.Entities project provides an example of how ExpressFormsController can be used with Entity Framework.
Note how this class is inherited in the ExpressForsmExample project.  This is a good example of how I envision this
class to be used.  Note that although you can use Entity Framework with Express Forms, the framework is not tightly
coupled with Entity Framework.

ExpressFormsController exposes three public methods that the web browser can use: Index, Editor, and Postback.
Index renders a form for the user to browse records.  In this early version, the records are all rendered into a single
table, but in a later version, this is expected to be fully customizable so that AJAX frameworks can be used
to provide a rich interface.

Editor is renders a form for the user to create and update records.  At this time, it is much more advanced than Index.
Editor may be customized to render the form in any way imaginable.  The primary feature that has not been implemented here
is the ability to work with more than one table at a time in Entity Framework.  I expect that to be the trickiest feature
of all to implement.

Postback provides a single point of entry for the browser to update the data.  It is meant to work with both AJAX and form
posts, but the AJAX functionality is much more mature.

Next is the IExpressFormsExchange.  It is an interface where the methods to update the data are defined.  The ExpressForms.Entities
project defines a generic Entity Framework implementation.  It works very well with classes that represent the contents of a
single table.  However, you'll find lots of commented out code from where I tried to make it work with multiple tables
at once.  The generic Entity Framework exchange class seems to work very well, but it uses reflection liberally, and once again,
it assumes that the identifier for the row is a 32-bit integer called Id.

I think that a future version should perhaps be able to generate Exchange implementations for entity classes with no reflection.

Express Forms provides a single set of Javascript files that read and write data from forms.  It is designed to be
extensible so that you can easily add your own functionality in a logical way.


Here is a partial list of what I see as being critical features to implement in the future:

1) User validation input.  Scaffolding provides this; we should, too!

2) Extensibility so that developers can specify their own event handlers for various things, such as confirmation dialogs

3) The Index view needs to be flexible and work with AJAX frameworks; not just write a plain table.

4) Express Forms should be able to quickly stand up pages that work with data from two tables.


One final thing: Express Forms is not about writing code; it's about *not* writing code.

Therefore, the less code in this project, the better!
