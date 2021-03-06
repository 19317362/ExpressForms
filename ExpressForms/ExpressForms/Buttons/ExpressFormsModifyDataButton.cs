﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExpressForms.Buttons
{
    /// <summary>
    /// A button that the user clicks to post data on a form to the server and modify data server-side.    
    /// </summary>    
    public class ExpressFormsModifyDataButton<T, TId> : ExpressFormsButton<T, TId>
    {
        public ExpressFormsModifyDataButton()
            : base()
        {
            // The default post type will be Ajax            
            PostType = PostTypeEnum.Ajax;
        }

        public string FormName { get; set; }

        /// <summary>
        /// The Url to post to when the user clicks the button.
        /// </summary>
        public string PostUrl { get; set; }

        public PostTypeEnum PostType { get; set; }

        public ActionTypeEnum ActionType { get; set; }

        private string ConfirmationMessage { get; set; }
        /// <summary>
        /// An optional function to generate a confirmation message based on the record being modified/deleted.
        /// For example, it could be used to warn a user before deleting a record.
        /// If null, no confirmation message will be displayed.
        /// </summary>
        public Func<T, TId, string> GetConfirmationMessageFromRecord { get; set; }

        public enum PostTypeEnum
        {
            Ajax, Form
        }

        public enum ActionTypeEnum
        {
            Insert, Update, Delete
        }

        #region data for deleting a record without the editor window open        
        /// <summary>
        /// The ID of the row to delete from the data source
        /// </summary>
        public TId IdForDeletion { get; set; }
        /// <summary>
        /// The ID of the table that contains the row to remove from the DOM.
        /// </summary>
        public string TableIdForDeletion { get; set; }
        #endregion

        public override void InitializeWithRecord(T record, TId id)
        {
            IdForDeletion = id;
            if (GetConfirmationMessageFromRecord!=null)
                ConfirmationMessage = GetConfirmationMessageFromRecord(record, id);
        }

        public override MvcHtmlString WriteButton(HtmlHelper helper, object htmlAttributes)
        {
            // Provide default value for PostUrl in case it hasn't been specified.
            if (string.IsNullOrWhiteSpace(PostUrl))
            {
                UrlHelper Url = new UrlHelper(helper.ViewContext.RequestContext);
                PostUrl = Url.Action("Postback");
            }

            TagBuilder tb = new TagBuilder("input");
            tb.Attributes.Add("type", "button");
            tb.Attributes.Add("class", "ExpressFormsModifyDataButton");
            tb.Attributes.Add("value", Text);
            tb.Attributes.Add("data-posturl", PostUrl);
            tb.Attributes.Add("data-actiontype", ActionType.ToString());
            tb.Attributes.Add("data-posttype", PostType.ToString());
            tb.Attributes.Add("data-formname", FormName);
            tb.Attributes.Add("data-message", ConfirmationMessage);
            tb.Attributes.Add("data-id", Convert.ToString(IdForDeletion));
            tb.Attributes.Add("data-tableid", TableIdForDeletion);
            if (!IsVisible)
                tb.Attributes.Add("style", "display: none;");

            IDictionary<string, object> efHtmlAttributes = new RouteValueDictionary(htmlAttributes);
            foreach (var kvp in efHtmlAttributes)
                tb.MergeAttribute(kvp.Key, Convert.ToString(kvp.Value));

            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
    }
}
