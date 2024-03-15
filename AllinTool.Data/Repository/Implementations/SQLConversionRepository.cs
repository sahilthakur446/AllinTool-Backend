using AllinTool.Data.Repository.Interfaces;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AllinTool.Data.Repository.Implementations
    {
    public class SQLConversionRepository : ISQLConversionRepository
        {

        public string ConvertMYSQLQueryTOMSSQLQuery(string inputMySqlQuery) 
            {
            string convertedQuery = ConvertLimitToTop(inputMySqlQuery);
            convertedQuery = IFNULLtoISNULL(convertedQuery);
            convertedQuery = IFNULLtoISNULL(convertedQuery);
            convertedQuery = ConvertBooleanValues(convertedQuery);
            convertedQuery = ConvertDateFunctions( convertedQuery);
            convertedQuery = ConvertAutoIncrement( convertedQuery);
            convertedQuery = ConvertBooleanToBit( convertedQuery);
            convertedQuery = ConvertQuotes( convertedQuery);
            return convertedQuery;
            }

        public string ConvertMSSQLQueryToMYSQLQuery(string inputMSSqlQuery)
            {
            string convertedQuery = ConvertTopToLimit(inputMSSqlQuery);
            convertedQuery = ISNULLToIFNULL(convertedQuery);
            convertedQuery = ConvertBitToBoolean(convertedQuery);
            convertedQuery = ConvertDateFunctionsConvertToMYSQL(convertedQuery);
            convertedQuery = ConvertIdentityToAutoIncrement(convertedQuery);
            convertedQuery = ConvertBooleanValuesConvertToMYSQL(convertedQuery);
            convertedQuery = ConvertSquareBracketsToBackticks(convertedQuery);

            return convertedQuery;
            }

        private string ConvertLimitToTop(string inputQueryString)
            {
            if (string.IsNullOrEmpty(inputQueryString))
                {
                throw new ArgumentNullException(nameof(inputQueryString), "Query is empty or null.");
                }

            StringBuilder adjustedQuery = new StringBuilder();

            // Split the input into separate queries
            string[] individualQueries = inputQueryString.Split(';');

            for (int i = 0; i < individualQueries.Length; i++)
                {
                adjustedQuery.Clear();
                int limitKeywordIndex = individualQueries[i].IndexOf("LIMIT", StringComparison.OrdinalIgnoreCase);
                if (limitKeywordIndex != -1)
                    {
                    string queryPartBeforeLimit = individualQueries[i].Substring(0, limitKeywordIndex);
                    string queryPartAfterLimit = individualQueries[i].Substring(limitKeywordIndex);
                    queryPartAfterLimit = queryPartAfterLimit.Replace("LIMIT", "TOP", StringComparison.OrdinalIgnoreCase);

                    int asteriskIndex = queryPartBeforeLimit.IndexOf("*", StringComparison.OrdinalIgnoreCase);
                    if (asteriskIndex != -1)
                        {
                        string queryPartBeforeAsterisk = queryPartBeforeLimit.Substring(0, asteriskIndex);
                        string queryPartAfterAsterisk = queryPartBeforeLimit.Substring(asteriskIndex - 1);

                        adjustedQuery.Append(queryPartBeforeAsterisk);
                        adjustedQuery.Append(queryPartAfterLimit);
                        adjustedQuery.Append(queryPartAfterAsterisk);

                        individualQueries[i] = adjustedQuery.ToString();
                        }
                    if (asteriskIndex == -1)
                        {
                        int selectKeywordIndex = queryPartBeforeLimit.IndexOf("SELECT", StringComparison.OrdinalIgnoreCase);
                        if (selectKeywordIndex != -1)
                            {
                            string firstPart1 = queryPartBeforeLimit.Substring(0, selectKeywordIndex + 7);
                            string secondPart1 = queryPartBeforeLimit.Substring(selectKeywordIndex + 6);

                            adjustedQuery.Append(firstPart1);
                            adjustedQuery.Append(queryPartAfterLimit);
                            adjustedQuery.Append(secondPart1);

                            individualQueries[i] = adjustedQuery.ToString();
                            }
                        }
                    }
                }

            // Join the queries back together
            return string.Join(";", individualQueries);
            }

        private string IFNULLtoISNULL(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            string[] queries = query.Split(';');

            for (int i = 0; i < queries.Length; i++)
                {
                if (queries[i].Contains("IFNULL"))
                    {
                    queries[i] = queries[i].Replace("IFNULL", "ISNULL", StringComparison.OrdinalIgnoreCase);
                    }
                }

            return string.Join(";", queries);
            }

        private string ConvertBooleanValues(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            string[] queries = query.Split(';');

            for (int i = 0; i < queries.Length; i++)
                {
                // Convert MySQL boolean values to MSSQL format
                queries[i] = queries[i].Replace("TRUE", "1", StringComparison.OrdinalIgnoreCase)
                                     .Replace("FALSE", "0", StringComparison.OrdinalIgnoreCase);
                }

            return string.Join(";", queries);
            }

        private string ConvertDateFunctions(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            // Convert MySQL date functions to MSSQL format
            query = query.Replace("NOW()", "GETDATE()")
                         .Replace("CURDATE()", "GETDATE()")
                         .Replace("CURTIME()", "GETDATE()");

            return query;
            }

        private string ConvertAutoIncrement(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            // Convert MySQL auto-increment syntax to MSSQL format
            query = query.Replace("AUTO_INCREMENT", "IDENTITY(1,1)");

            return query;
            }

        private string ConvertBooleanToBit(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            query = query.Replace("BOOLEAN", "BIT", StringComparison.OrdinalIgnoreCase);
            return query;
            }

        private string ConvertQuotes(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            query = query.Replace("`", "[")
                         .Replace("`", "]");

            return query;
            }
        //------------------------------------------//
        private string ConvertTopToLimit(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            string[] queries = query.Split(';');

            for (int i = 0; i < queries.Length; i++)
                {
                int topKeywordIndex = queries[i].IndexOf("TOP", StringComparison.OrdinalIgnoreCase);
                if (topKeywordIndex != -1)
                    {
                    string queryPartBeforeTop = queries[i].Substring(0, topKeywordIndex);
                    string queryPartAfterTop = queries[i].Substring(topKeywordIndex + 3).Trim(); // +3 to remove "TOP"
                    int firstSpaceIndex = queryPartAfterTop.IndexOf(" ");
                    string topValue = queryPartAfterTop.Substring(0, firstSpaceIndex);
                    string remainingQuery = queryPartAfterTop.Substring(firstSpaceIndex).Trim();

                    queries[i] = $"{queryPartBeforeTop} {remainingQuery} LIMIT {topValue}";
                    }
                }

            return string.Join(";", queries);
            }

        private string ConvertDateFunctionsConvertToMYSQL(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            // Convert MSSQL date functions to MySQL format
            query = query.Replace("GETDATE()", "NOW()")
                         .Replace("GETDATE()", "CURDATE()")
                         .Replace("GETDATE()", "CURTIME()");

            return query;
            }

        private string ISNULLToIFNULL(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            string[] queries = query.Split(';');

            for (int i = 0; i < queries.Length; i++)
                {
                if (queries[i].Contains("ISNULL"))
                    {
                    queries[i] = queries[i].Replace("ISNULL", "IFNULL", StringComparison.OrdinalIgnoreCase);
                    }
                }

            return string.Join(";", queries);
            }

        private string ConvertBitToBoolean(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            query = query.Replace("BIT", "BOOLEAN", StringComparison.OrdinalIgnoreCase);
            return query;
            }

        private string ConvertBooleanValuesConvertToMYSQL(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            // Define a regular expression that matches only standalone 1 and 0
            var regex = new Regex(@"\b1\b|\b0\b");

            string[] queries = query.Split(';');

            for (int i = 0; i < queries.Length; i++)
                {
                // Convert MSSQL boolean values to MySQL format
                queries[i] = regex.Replace(queries[i], match =>
                {
                    return match.Value == "1" ? "TRUE" : "FALSE";
                });
                }

            return string.Join(";", queries);
            }

        private string ConvertIdentityToAutoIncrement(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            query = query.Replace("IDENTITY(1,1)", "AUTO_INCREMENT", StringComparison.OrdinalIgnoreCase);
            return query;
            }

        private string ConvertSquareBracketsToBackticks(string query)
            {
            if (string.IsNullOrEmpty(query))
                {
                throw new ArgumentNullException(nameof(query), "Query is empty or null");
                }

            query = query.Replace("[", "`").Replace("]", "`");
            return query;
            }
        }
    }
