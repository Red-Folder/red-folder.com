﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedFolder.ViewModels
{
    public class BlogCollection: IPager
    {
        private int _pageNo;
        private int _blogsPerPage;
        private string _filterBy;
        private OrderBy _orderedBy;
        private string _series;
        private IList<RedFolder.Website.Data.Blog> _blogs;

        public int PageNo
        {
            get
            {
                return _pageNo;
            }
        }

        public int BlogsPerPage
        {
            get
            {
                return _blogsPerPage;
            }
        }

        public string FilterBy
        {
            get
            {
                return _filterBy;
            }
        }

        public OrderBy OrderedBy
        {
            get
            {
                return _orderedBy;
            }
        }

        public string Series
        {
            get
            {
                return _series;
            }
        }

        public int Pages
        {
            get
            {
                if (BlogsPerPage < 1 || Blogs.Count < BlogsPerPage)
                {
                    return 1;
                }
                else
                {
                    if (Blogs.Count % BlogsPerPage == 0)
                    {
                        return Blogs.Count / BlogsPerPage;
                    }
                    else
                    {
                        return (Blogs.Count / BlogsPerPage) + 1;
                    }
                }
            }
        }

        public IList<RedFolder.Website.Data.Blog> Blogs
        {
            get
            {
                return _blogs.Where(_filterBy, _series).OrderBy(_orderedBy).ToList();
            }
        }

        public IList<RedFolder.Website.Data.Blog> BlogsForCurrentPage
        {
            get
            {
                if (_blogsPerPage == 0)
                {
                    return Blogs;
                }
                else
                { 
                    return Blogs
                        .Skip(_blogsPerPage * (_pageNo - 1))
                        .Take(_blogsPerPage)
                        .ToList();
                }
            }
        }

        public IList<KeyValuePair<string, int>> KeyWords
        {
            get
            {
                return Blogs
                    .SelectMany(x => x.KeyWords)
                    .GroupBy(x => x)
                    .Select(x => new KeyValuePair<string, int>(x.Key, x.Count()))
                    .ToList();
            }
        }

        public BlogCollection(IList<RedFolder.Website.Data.Blog> blogs, string series)
        {
            _blogs = blogs;
            _pageNo = 1;
            _blogsPerPage = 0;
            _filterBy = null;
            _orderedBy = OrderBy.PublishedAscending;
            _series = series;
        }


        public BlogCollection(IList<RedFolder.Website.Data.Blog> blogs, int pageNo = 1, int blogsPerPage = 0, string filterBy = null, OrderBy orderedBy = OrderBy.PublishedDescending)
        {
            _blogs = blogs;
            _pageNo = pageNo;
            _blogsPerPage = blogsPerPage;
            _filterBy = filterBy;
            _orderedBy = orderedBy;

            // Reset the page no if its greater than the number of pages
            // This can happen if a filter is applied which reduces the number of pages beyond
            // the current page no
            if (_pageNo > Pages)
            {
                _pageNo = 1;
            }
        }

        public enum OrderBy
        {
            PublishedAscending,
            PublishedDescending
        }

    }

    static class CollectionExtension
    {
        public static IList<Website.Data.Blog> OrderBy(this IList<Website.Data.Blog> list, BlogCollection.OrderBy orderBy)
        {
            if (orderBy == BlogCollection.OrderBy.PublishedAscending) return list.OrderBy(x => x.Published).ToList();
            if (orderBy == BlogCollection.OrderBy.PublishedDescending) return list.OrderByDescending(x => x.Published).ToList();

            return list;
        }

        public static IList<Website.Data.Blog> Where(this IList<Website.Data.Blog> list, string filter, string series)
        {
            var tmpList = list;
            if (series != null)
            {
                tmpList = list.Where(x => x.Series == series).ToList();
            }

            if (filter == null)
            {
                return tmpList;
            }
            else
            {
                var filterList = filter.Split(',').ToList();
                return tmpList.Where(x => filterList.All(f => x.KeyWords.Contains(f))).ToList();
            }
        }
    }

}
