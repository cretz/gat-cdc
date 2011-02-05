using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using Gat.Common.Project.Model;
using Microsoft.SqlServer.Server;
using Gat.Common.Error;

namespace Gat.SqlServer
{
    class CodeBuilder
    {

        private readonly List<string> _regexes = new List<string>();

        protected virtual CodeConditionStatement BuildConditionStatement(project proj, conditional cond)
        {
            return new CodeConditionStatement(
                BuildConditionalExpression(cond.when, ItemsChoiceType.all),
                BuildActionStatements(cond.then));
        }

        protected virtual CodeExpression BuildConditionalExpression(
            conditionalContainer container, ItemsChoiceType choiceType)
        {
            //grab the children
            List<CodeExpression> children = new List<CodeExpression>();
            for (int i = 0; i < container.Items.Length; i++)
            {
                if (container.Items[i] is conditionalContainer)
                {
                    children.Add(BuildConditionalExpression((conditionalContainer)container.Items[i],
                        container.ItemsElementName[i]));
                }
                else if (container.Items[i] is actionEquals)
                {
                    children.Add(BuildConditionalExpression((actionEquals)container.Items[i]));
                }
                else if (container.Items[i] is valueEquals)
                {
                    children.Add(BuildConditionalExpression((valueEquals)container.Items[i],
                        container.ItemsElementName[i]));
                }
                else
                {
                    throw new GatException("Unrecognized conditional item type: " + 
                        container.Items[i].GetType());
                }
            }
            //get the full expression
            CodeExpression expr = null;
            if (choiceType == ItemsChoiceType.none)
            {
                expr = new CodeBinaryOperatorExpression(children[0], 
                    CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(false));
                for (int i = 1; i < children.Count; i++)
                {
                    expr = new CodeBinaryOperatorExpression(expr,
                        CodeBinaryOperatorType.BooleanAnd,
                        new CodeBinaryOperatorExpression(children[i],
                            CodeBinaryOperatorType.IdentityEquality, new CodePrimitiveExpression(false)));
                }
            }
            else if (choiceType == ItemsChoiceType.all ||
                choiceType == ItemsChoiceType.any)
            {
                CodeBinaryOperatorType opType = choiceType == ItemsChoiceType.all ?
                    CodeBinaryOperatorType.BooleanAnd : CodeBinaryOperatorType.BooleanOr;
                expr = children[0];
                for (int i = 1; i < children.Count; i++)
                {
                    expr = new CodeBinaryOperatorExpression(expr,
                        CodeBinaryOperatorType.BooleanOr, children[i]);
                }
            }
            else
            {
                throw new GatException("Unrecognized choice type: " + choiceType);
            }
            return expr;
        }

        protected virtual CodeExpression BuildConditionalExpression(
            valueEquals valEquals, ItemsChoiceType choiceType)
        {
            int regexIndex = -1;
            if (valEquals.regex)
            {
                regexIndex = _regexes.Count;
                _regexes.Add(valEquals.value);
            }
            if (choiceType == ItemsChoiceType.columnEquals) {
                if (!valEquals.regex)
                {
                    //context.Columns.Contains(VALUE_LITERAL)
                    return new CodeMethodInvokeExpression(new CodePropertyReferenceExpression(
                        new CodeVariableReferenceExpression("context"), "Columns"), "Contains",
                        new CodePrimitiveExpression(valEquals.value));
                }
                else
                {
                    //context.IsRegexColumnPresent(_regexes[REGEX_INDEX])
                    return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(
                        new CodeVariableReferenceExpression("context"), "IsRegexColumnPresent"),
                        new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(
                            null, "_regexes"), new CodePrimitiveExpression(regexIndex)));
                }
            }
            else if (choiceType == ItemsChoiceType.schemaEquals ||
                choiceType == ItemsChoiceType.tableEquals)
            {
                CodePropertyReferenceExpression propRef;
                if (choiceType == ItemsChoiceType.schemaEquals)
                {
                    propRef = new CodePropertyReferenceExpression(
                        new CodeVariableReferenceExpression("context"), "Schema");
                }
                else
                {
                    propRef = new CodePropertyReferenceExpression(
                        new CodeVariableReferenceExpression("context"), "Table");
                }
                if (!valEquals.regex)
                {
                    //VALUE_LITERAL.Equals(context.Schema/Table)
                    return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(
                        new CodePrimitiveExpression(valEquals.value), "Equals"),
                        propRef);
                }
                else
                {
                    //_regexes[REGEX_INDEX].IsMatch(context.Schema/Table)
                    return new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(
                        new CodeArrayIndexerExpression(new CodeFieldReferenceExpression(null,
                            "_regexes"), new CodePrimitiveExpression(regexIndex)), "IsMatch"),
                            propRef);
                }
            }
            else
            {
                throw new GatException("Unrecognized choice type: " + choiceType);
            }
        }

        protected virtual CodeExpression BuildConditionalExpression(actionEquals actEquals)
        {
            //triggerContext.TriggerAction == TriggerAction.ACTION_NAME
            TriggerAction triggerAction;
            switch (actEquals.action)
            {
                case action.ALTER:
                    triggerAction = TriggerAction.AlterTable;
                    break;
                case action.CREATE:
                    triggerAction = TriggerAction.CreateTable;
                    break;
                case action.DELETE:
                    triggerAction = TriggerAction.Delete;
                    break;
                case action.DROP:
                    triggerAction = TriggerAction.DropTable;
                    break;
                case action.INSERT:
                    triggerAction = TriggerAction.Insert;
                    break;
                case action.UPDATE:
                    triggerAction = TriggerAction.Update;
                    break;
                default:
                    throw new GatException("Unrecognized action: " + actEquals.action);
            }
            return new CodeBinaryOperatorExpression(new CodePropertyReferenceExpression(
                new CodeVariableReferenceExpression("triggerContext"), "TriggerAction"),
                CodeBinaryOperatorType.IdentityEquality, new CodeFieldReferenceExpression(
                    new CodeTypeReferenceExpression(typeof(TriggerAction)), triggerAction.ToString()));
        }

        protected virtual CodeStatement[] BuildActionStatements(then thn)
        {
            throw new NotImplementedException();
        }
    }
}
