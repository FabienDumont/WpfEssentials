using System;

namespace WpfEssentials.Services;

public interface INavigationPageResolver
{
  Uri? GetPageUri(string pageKey);
}
