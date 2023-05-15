/*
 *  (c) Copyright 2022, 2023 Kondensor Contributors
 *  Written by Warwick Molloy.
 *  Distributed under the Kondensor License.
 */


using Optional;
using Parser;

namespace Actions
{

// <tr>
//     <td rowspan="3">
//         <a id="awsaccountmanagement-DeleteAlternateContact"></a>
//         <a href="https://docsResourceTypeDefinitionId.aws.amazon.com/accounts/latest/reference/API_DeleteAlternateContact.html">DeleteAlternateContact</a>
//     </td>
//     <td rowspan="3">Grants permission to delete the alternate contacts for an account</td>
//     <td rowspan="3">Write</td>
//     <td>
//         <p>
//             <a href="#awsaccountmanagement-account">account</a>
//         </p>
//     </td>
//     <td></td>
//     <td></td>
// </tr>

  public static class ModelParser
  {
    public static void Demo(IPipe pipe)
    {
      ReplayWrapPipe relay = new ReplayWrapPipe(pipe);
      var foo = Parsing.Group(relay)
        .Expect(TableRowsAndCells.TR)
        .Expect(TableRowsAndCells.TdRowSpan)
        .Then( (matches, writer) => {
          matches.Where(m => {
            string name = m.MatcherName.ValueOr("");
            return name == nameof(TableRowsAndCells.TdRowSpan);
          });
        })
        .Else();
    }
  }


}